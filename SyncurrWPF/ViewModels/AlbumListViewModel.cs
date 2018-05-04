using MahApps.Metro.Controls.Dialogs;
using SyncurrWPF.Helpers;
using SyncurrWPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SyncurrWPF.ViewModels
{
	public class AlbumListViewModel : SyncViewModel
	{
		public ObservableCollection<Album> Albums { get; set; } = new ObservableCollection<Album>();

		private Album _selectedAlbum;
		public Album SelectedAlbum
		{
			get { return _selectedAlbum; }
			set
			{
				if (_selectedAlbum != value)
				{
					_selectedAlbum = value;
					OnPropertyChanged(nameof(SelectedAlbum));
				}
			}
		}




		public AlbumListViewModel()
		{
			Title = "Albums";

			if (Properties.Settings.Default.Albums == null)
			{
				Properties.Settings.Default.Albums = new System.Collections.Specialized.StringCollection();
			}
			foreach (string path in Properties.Settings.Default.Albums)
			{
				Albums.Add(Album.Get(path, ""));
			}
		}




		protected override async Task DoSync(ProgressDialogController pdc)
		{
			IsLoading = true;
			try
			{
				foreach (Album album in Albums)
				{
					pdc.SetMessage(string.Format("synchronizing albums\n\n{0}\n{1}\n{2}", album.Title, album.Id, album.Root));
					await album.Sync();
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
				await DialogCoordinator.Instance.ShowMessageAsync(this, "Failed to synchronize albums", string.Join("\n\n", msgs));
			}
			IsLoading = false;
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
						Properties.Settings.Default.Albums.Remove(SelectedAlbum.Root);
						Properties.Settings.Default.Save();
						Albums.Remove(SelectedAlbum);
					},
					p =>
					{
						return SelectedAlbum != null;
					});
				}
				return _removeCommand;
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
						Process.Start(SelectedAlbum.Root);
					},
					p =>
					{
						return SelectedAlbum != null;
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
						Process.Start(string.Format("https://imgur.com/a/{0}", SelectedAlbum.Id));
					},
					p =>
					{
						return SelectedAlbum != null;
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
						string newRoot = await DialogCoordinator.Instance.ShowInputAsync(this, "Relocate album", SelectedAlbum.Root);
						if (!string.IsNullOrWhiteSpace(newRoot))
						{
							try
							{
								Directory.Move(SelectedAlbum.Root, newRoot);
								SelectedAlbum.Root = newRoot;
							}
							catch (Exception ex)
							{
								List<string> msgs = new List<string>();
								while (ex != null)
								{
									msgs.Add(ex.Message);
									ex = ex.InnerException;
								}
								await DialogCoordinator.Instance.ShowMessageAsync(this, "Failed to relocate album", string.Join("\n\n", msgs));
							}
						}
					},
					p =>
					{
						return SelectedAlbum != null;
					});
				}
				return _relocateCommand;
			}
		}
		#endregion
	}
}
