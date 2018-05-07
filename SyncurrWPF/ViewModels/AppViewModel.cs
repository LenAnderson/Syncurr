using Imgur.API.Authentication.Impl;
using Imgur.API.Endpoints.Impl;
using Imgur.API.Models;
using MahApps.Metro.Controls.Dialogs;
using Newtonsoft.Json;
using SyncurrWPF.Helpers;
using SyncurrWPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

namespace SyncurrWPF.ViewModels
{
	public class AppViewModel : ViewModel
	{
		public ObservableCollection<SyncViewModel> Tabs { get; set; } = new ObservableCollection<SyncViewModel>();
		private MeViewModel Me;
		private UserListViewModel Users;
		private AlbumListViewModel Albums;



		private object _selectedTab;
		public object SelectedTab
		{
			get { return _selectedTab; }
			set
			{
				if (_selectedTab != value)
				{
					_selectedTab = value;
					OnPropertyChanged(nameof(SelectedTab));
				}
			}
		}

		private bool _isFlyoutOpen = false;
		public bool IsFlyoutOpen
		{
			get { return _isFlyoutOpen; }
			set
			{
				if (_isFlyoutOpen != value)
				{
					_isFlyoutOpen = value;
					OnPropertyChanged(nameof(IsFlyoutOpen));
					if (!value)
					{
						InitTabs();
						Timer.Interval = new TimeSpan(0, Properties.Settings.Default.SyncInterval, 0);
					}
				}
			}
		}

		private ViewModel _flyoutContent;
		public ViewModel FlyoutContent
		{
			get { return _flyoutContent; }
			set
			{
				if (_flyoutContent != value)
				{
					_flyoutContent = value;
					OnPropertyChanged(nameof(FlyoutContent));
				}
			}
		}

		private ProgressDialogController Progress;

		private bool _hasWindow = true;
		public bool HasWindow
		{
			get { return _hasWindow; }
			set
			{
				if (_hasWindow != value)
				{
					_hasWindow = value;
					OnPropertyChanged(nameof(HasWindow));
				}
			}
		}

		private DispatcherTimer Timer;




		public AppViewModel()
		{
			Title = "Syncurr";

			InitTabs();
			SelectedTab = Tabs[0];

			Timer = new DispatcherTimer();
			Timer.Interval = new TimeSpan(0, Properties.Settings.Default.SyncInterval, 0);
			Timer.Tick += (s, e) =>
			{
				if (SyncCommand.CanExecute(null))
				{
					SyncCommand.Execute(null);
				}
			};
			Timer.Start();

			if (Properties.Settings.Default.StartMinimized)
			{
				HasWindow = false;
			}
		}


		public void InitTabs()
		{
			Tabs.Clear();
			if (Properties.Settings.Default.SyncMe)
			{
				Me = new MeViewModel();
				Tabs.Add(Me);
			}

			Users = new UserListViewModel();
			Tabs.Add(Users);

			Albums = new AlbumListViewModel();
			Tabs.Add(Albums);
		}




		public async Task Drop(string[] files)
		{
			if (files.Length > 0)
			{
				foreach (string file in files)
				{
					if (new Regex(@"^https?://imgur\.com/(gallery|a)/.+$").IsMatch(file))
					{
						string root = await DialogCoordinator.Instance.ShowInputAsync(this, "Add Imgur Album", "Enter local directory");
						Albums.Albums.Add(Album.Create(root, "", new Regex(@"^https?://imgur\.com/(gallery|a)/([^/#\?]+).*$").Replace(file, "$2")));
					}
					else if (new Regex(@"^https?://imgur\.com/user/.+$").IsMatch(file))
					{
						string root = await DialogCoordinator.Instance.ShowInputAsync(this, "Add Imgur User", "Enter local directory");
						Users.Users.Add(User.Create(root, new Regex(@"^https?://imgur\.com/user/([^/#\?]+).*$").Replace(file, "$1")));
					}
					else if (Directory.Exists(file))
					{
						if (Album.Exists(file, ""))
						{
							Album album = Album.Get(file, "");
							album.Save();
							Albums.Albums.Add(album);
						}
						else if (User.Exists(file))
						{
							User user = User.Get(file);
							await user.Save();
							Users.Users.Add(user);
						}
						else
						{
							MessageDialogResult result = await DialogCoordinator.Instance.ShowMessageAsync(this, "Add local folder", "Do you want to add an album or a user?", MessageDialogStyle.AffirmativeAndNegative, new MetroDialogSettings
							{
								AffirmativeButtonText = "Album",
								NegativeButtonText = "User"
							});
							if (result == MessageDialogResult.Affirmative)
							{
								string id = await DialogCoordinator.Instance.ShowInputAsync(this, "Add local folder", "Enter album ID");
								Albums.Albums.Add(Album.Create(file, "", id));
							}
							else
							{
								string id = await DialogCoordinator.Instance.ShowInputAsync(this, "Add local folder", "Enter user name");
								Users.Users.Add(User.Create(file, id));
							}
						}
					}
				}
			}
		}




		#region Commands
		private ICommand _showSettingsCommand;
		public ICommand ShowSettingsCommand
		{
			get
			{
				if (_showSettingsCommand == null)
				{
					_showSettingsCommand = new RelayCommand(p => {
						FlyoutContent = new SettingsViewModel();
						IsFlyoutOpen = true;
					});
				}
				return _showSettingsCommand;
			}
		}

		private ICommand _syncCommand;
		public ICommand SyncCommand
		{
			get
			{
				if (_syncCommand == null)
				{
					_syncCommand = new RelayCommand(
						async p =>
						{
							IsLoading = true;
							ProgressDialogController pdc = await DialogCoordinator.Instance.ShowProgressAsync(this, "Synchronizing", "");
							pdc.SetIndeterminate();
							foreach(SyncViewModel tab in Tabs)
							{
								await tab.Sync(pdc);
							}
							await pdc.CloseAsync();
							IsLoading = false;
						},
						p =>
						{
							return !IsLoading;
						}
					);
				}
				return _syncCommand;
			}
		}

		private ICommand _exitCommand;
		public ICommand ExitCommand
		{
			get
			{
				if (_exitCommand == null)
				{
					_exitCommand = new RelayCommand(p => {
						App.Current.Shutdown();
					});
				}
				return _exitCommand;
			}
		}

		private ICommand _toggleWindowCommand;
		public ICommand ToggleWindowCommand
		{
			get
			{
				if (_toggleWindowCommand == null)
				{
					_toggleWindowCommand = new RelayCommand(p => {
						HasWindow = !HasWindow;
					});
				}
				return _toggleWindowCommand;
			}
		}
		#endregion
	}
}
