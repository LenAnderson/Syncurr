using Imgur.API.Authentication.Impl;
using Imgur.API.Endpoints.Impl;
using Imgur.API.Models;
using SyncurrWPF.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SyncurrWPF.ViewModels
{
	public class SyncListViewModel : ViewModel
	{
		public Func<Task<List<IAlbum>>> DataProvider;

		private bool _isLoading = false;
		public bool IsLoading
		{
			get { return _isLoading; }
			set
			{
				if (_isLoading != value)
				{
					_isLoading = value;
					OnPropertyChanged(nameof(IsLoading));
				}
			}
		}

		public ObservableCollection<IAlbum> Albums { get; set; } = new ObservableCollection<IAlbum>();




		public SyncListViewModel()
		{
			
		}




		public async Task Refresh()
		{
			IsLoading = true;
			try
			{
				if (DataProvider != null)
				{
					List<IAlbum> albums = await DataProvider();
					Albums.Clear();
					foreach (IAlbum album in albums)
					{
						Albums.Add(album);
					}
				}
			}
			catch (Exception ex)
			{
				//TODO: ERROR - refresh failed
			}
			IsLoading = false;
		}




		#region Commands
		private ICommand _refreshCommand;
		public ICommand RefreshCommand
		{
			get
			{
				if (_refreshCommand == null)
				{
					_refreshCommand = new RelayCommand(async p => {
						/* 
						 * Refresh needs to happen in multiple steps:
						 *		1. check that all users and albums still exist, if not: ask how to proceed
						 *		2. check for all users if albums / folders have been added or removed and ask how to proceed
						 *		3. knowing all albums (both connected to users or independent) --> sync
						 *		
						 * RefreshCommand needs to be provided based on tab (done via ISyncTarget):
						 *		Me =     User.Refresh
						 *		Users =  UserCollection.Refresh
						 *		Albums = AlbumCollection.Refresh
						 *		
						 *		Album.Refresh ->           sync local folder with associated remote album
						 *		AlbumCollection.Refresh -> foreach album: Album.Refresh
						 *		User.Refresh  ->           1. compare local folders with remote albums
						 *		                                 new album: create folder
						 *		                                 album missing: warning
						 *		                                 new folder: create album
						 *		                                 folder missing (how? user.json?): warning
						 *		                           2. foreach album: Album.Refresh
						 *		UserCollection.Refresh ->  foreach user: User.Refresh
						 *
						 */

						await Task.Delay(1);
					});
				}
				return _refreshCommand;
			}
		}
		#endregion
	}
}
