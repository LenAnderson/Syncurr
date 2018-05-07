using SyncurrWPF.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MahApps.Metro.Controls.Dialogs;
using System.Collections.ObjectModel;
using SyncurrWPF.Models;
using System.Windows.Input;
using System.Diagnostics;
using System.IO;

namespace SyncurrWPF.ViewModels
{
	public class UserListViewModel : SyncViewModel
	{
		public ObservableCollection<User> Users { get; set; } = new ObservableCollection<User>();
		public ObservableCollection<User> Albums { get { return Users; } }

		private User _selectedUser;
		public User SelectedUser
		{
			get { return _selectedUser; }
			set
			{
				if (_selectedUser != value)
				{
					_selectedUser = value;
					OnPropertyChanged(nameof(SelectedUser));
					OnPropertyChanged(nameof(SelectedAlbum));
				}
			}
		}
		public User SelectedAlbum
		{
			get { return _selectedUser; }
			set
			{
				if (_selectedUser != value)
				{
					_selectedUser = value;
					OnPropertyChanged(nameof(SelectedAlbum));
					OnPropertyChanged(nameof(SelectedUser));
				}
			}
		}




		public UserListViewModel()
		{
			Title = "Users";

			if (Properties.Settings.Default.Users == null)
			{
				Properties.Settings.Default.Users = new System.Collections.Specialized.StringCollection();
			}
			foreach (string path in Properties.Settings.Default.Users)
			{
				Users.Add(User.Get(path));
			}
		}




		protected override async Task DoSync(ProgressDialogController pdc)
		{
			IsLoading = true;
			try
			{
				foreach (User user in Users)
				{
					pdc.SetMessage(string.Format("synchronizing users\n\n{0}\n{1}", user.Name, user.Root));
					await user.Sync(this);
				}
			}
			catch (Exception ex)
			{
				List<string> msgs = new List<string>();
				while (ex != null)
				{
					msgs.Add(ex.Message);
					ex = ex.InnerException;
				}
				await DialogCoordinator.Instance.ShowMessageAsync(this, "Failed to synchronize users", string.Join("\n\n", msgs));
			}
		}




		#region Commands
		private ICommand _removeCommand;
		public ICommand RemoveCommand
		{
			get
			{
				if (_removeCommand == null)
				{
					_removeCommand = new RelayCommand(p =>
					{
						Properties.Settings.Default.Users.Remove(SelectedUser.Root);
						Properties.Settings.Default.Save();
						Users.Remove(SelectedUser);
					},
					p =>
					{
						return SelectedUser != null;
					});
				}
				return _removeCommand;
			}
		}

		private ICommand _toggleSyncCommand;
		public ICommand ToggleSyncCommand
		{
			get
			{
				if (_toggleSyncCommand == null)
				{
					_toggleSyncCommand = new RelayCommand(
						async p => {
							SelectedUser.Synchronize = !SelectedUser.Synchronize;
							await SelectedUser.Save();
						},
						p =>
						{
							return SelectedUser != null;
						}
					);
				}
				return _toggleSyncCommand;
			}
		}

		private ICommand _openLocalCommand;
		public ICommand OpenLocalCommand
		{
			get
			{
				if (_openLocalCommand == null)
				{
					_openLocalCommand = new RelayCommand(p => {
						Process.Start(SelectedUser.Root);
					},
					p =>
					{
						return SelectedUser != null;
					});
				}
				return _openLocalCommand;
			}
		}

		private ICommand _openRemoteCommand;
		public ICommand OpenRemoteCommand
		{
			get
			{
				if (_openRemoteCommand == null)
				{
					_openRemoteCommand = new RelayCommand(p => {
						Process.Start(string.Format("https://imgur.com/user/{0}", SelectedUser.Name));
					},
					p =>
					{
						return SelectedUser != null;
					});
				}
				return _openRemoteCommand;
			}
		}

		private ICommand _relocateCommand;
		public ICommand RelocateCommand
		{
			get
			{
				if (_relocateCommand == null)
				{
					_relocateCommand = new RelayCommand(async p => {
						string newRoot = await DialogCoordinator.Instance.ShowInputAsync(this, "Relocate user", SelectedUser.Root);
						if (!string.IsNullOrWhiteSpace(newRoot))
						{
							try
							{
								Directory.Move(SelectedUser.Root, newRoot);
								SelectedUser.Root = newRoot;
							}
							catch (Exception ex)
							{
								List<string> msgs = new List<string>();
								while (ex != null)
								{
									msgs.Add(ex.Message);
									ex = ex.InnerException;
								}
								await DialogCoordinator.Instance.ShowMessageAsync(this, "Failed to relocate user", string.Join("\n\n", msgs));
							}
						}
					},
					p =>
					{
						return SelectedUser != null;
					});
				}
				return _relocateCommand;
			}
		}
		#endregion
	}
}
