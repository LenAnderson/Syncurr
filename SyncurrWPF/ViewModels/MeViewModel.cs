using Imgur.API.Models;
using MahApps.Metro.Controls.Dialogs;
using Newtonsoft.Json;
using SyncurrWPF.Helpers;
using SyncurrWPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SyncurrWPF.ViewModels
{
	public class MeViewModel : AlbumListViewModel
	{
		private User _me;
		public User Me
		{
			get { return _me; }
			set
			{
				if (_me != value)
				{
					_me = value;
					OnPropertyChanged(nameof(Me));
					OnPropertyChanged(nameof(Albums));
				}
			}
		}

		public new ObservableCollection<Album> Albums
		{
			get { return Me?.Albums; }
		}




		public MeViewModel()
		{
			Title = "Me";

			if (User.Exists(Properties.Settings.Default.MeRoot))
			{
				Me = User.Get(Properties.Settings.Default.MeRoot);
			}
		}




		protected override async Task DoSync(ProgressDialogController pdc)
		{
			if (Me?.Name == null)
			{
				if (User.Exists(Properties.Settings.Default.MeRoot))
				{
					Me = User.Get(Properties.Settings.Default.MeRoot);
				}
				if (Me?.Name == null)
				{
					IOAuth2Token token = await ImgurHelper.GetToken();
					Me = User.Create(Properties.Settings.Default.MeRoot, token.AccountUsername, token.AccountId);
				}
			}
			if (Me?.Name != null)
			{
				IsLoading = true;
				try
				{
					pdc.SetMessage(string.Format("synchronizing account\n\n{0}\n{1}", Me.Name, Me.Root));
					await Me.Sync(this);
				}
				catch (Exception ex)
				{
					List<string> msgs = new List<string>();
					while (ex != null)
					{
						msgs.Add(ex.Message);
						ex = ex.InnerException;
					}
					await DialogCoordinator.Instance.ShowMessageAsync(this, "Failed to synchronize albums", string.Join("\n\n", msgs));
				}
				IsLoading = false;
			}
		}




		#region Commands
		private ICommand _removeCommand;
		public new ICommand RemoveCommand
		{
			get
			{
				if (_removeCommand == null)
				{
					_removeCommand = new RelayCommand(p =>
					{
						SelectedAlbum.Synchronize = false;
						SelectedAlbum.Save();
					},
					p =>
					{
						return SelectedAlbum != null && SelectedAlbum.Synchronize;
					});
				}
				return _removeCommand;
			}
		}
		#endregion
	}
}
