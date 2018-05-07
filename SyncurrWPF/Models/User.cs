using Imgur.API.Endpoints.Impl;
using Imgur.API.Models;
using MahApps.Metro.Controls.Dialogs;
using Newtonsoft.Json;
using SyncurrWPF.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncurrWPF.Models
{
	public class User : ObservableObject, ISyncTarget
	{
		#region Static
		public static bool Exists(string path)
		{
			return File.Exists(Path.Combine(path, "syncurr.user.json"));
		}
		public static User Get(string path)
		{
			User user;
			if (Exists(path))
			{
				user = JsonConvert.DeserializeObject<User>(File.ReadAllText(Path.Combine(path, "syncurr.user.json")));
				user.Root = path;
				Album[] albums = user.AlbumRoots.Where(it => Album.Exists(it, "")).Select(it => Album.Get(it, "")).ToArray();
				user.Albums.Clear();
				foreach (Album album in albums)
				{
					user.Albums.Add(album);
				}
			}
			else
			{
				user = new User(path);
				user.Save();
			}
			return user;
		}
		public static User Create(string path, string name, string id = null)
		{
			User user = new User(path, name);
			user.Id = id;
			user.Save();
			return user;
		}
		#endregion




		private string _root;
		[JsonIgnore]
		public string Root
		{
			get { return _root; }
			set
			{
				if (_root != value)
				{
					_root = value;
					OnPropertyChanged(nameof(Root));
				}
			}
		}

		private bool _synchronize = true;
		public bool Synchronize
		{
			get { return _synchronize; }
			set
			{
				if (_synchronize != value)
				{
					_synchronize = value;
					OnPropertyChanged(nameof(Synchronize));
				}
			}
		}

		private IAccount _imgurUser;
		[JsonIgnore]
		public IAccount ImgurUser
		{
			get { return _imgurUser; }
			set
			{
				if (_imgurUser != value)
				{
					_imgurUser = value;
					OnPropertyChanged(nameof(ImgurUser));
					Id = value.Id.ToString();
					Name = value.Url;
				}
			}
		}

		[JsonIgnore]
		public ObservableCollection<Album> Albums { get; set; } = new ObservableCollection<Album>();

		public List<string> AlbumRoots { get; set; } = new List<string>();

		private string _id;
		public string Id
		{
			get { return _id; }
			set
			{
				if (_id != value)
				{
					_id = value;
					OnPropertyChanged(nameof(Id));
				}
			}
		}

		private string _name;
		public string Name
		{
			get { return _name; }
			set
			{
				if (_name != value)
				{
					_name = value;
					OnPropertyChanged(nameof(Name));
					OnPropertyChanged(nameof(Title));
				}
			}
		}
		public string Title { get { return Name; } }




		public User() { }
		public User(string path)
		{
			Root = path;
		}
		public User(string path, string name)
		{
			Root = path;
			Name = name;
		}




		public async Task Sync(object context)
		{
			await Task.Run(async () =>
			{
				// get account
				AccountEndpoint endpoint = new AccountEndpoint(await ImgurHelper.GetClient());
				ImgurUser = await endpoint.GetAccountAsync(Name);
				bool me = (await ImgurHelper.GetToken()).AccountId == ImgurUser.Id.ToString();

				// get local albums
				Album[] local = new DirectoryInfo(Root).GetDirectories().Where(it => it.Name[0] != '.').Select(it => Album.Get(it.FullName, "", null, true)).ToArray();

				// get remote albums
				IAlbum[] remote = (await endpoint.GetAlbumsAsync(Name)).ToArray();

				// (A) filter for albums with id only in local (not in remote)
				Album[] onlyLocal = local.Where(l => l.Id != null && !remote.Any(r => r.Id == l.Id)).ToArray();

				// (B) filter for albums only in remote (not in local)
				IAlbum[] onlyRemote = remote.Where(r => !local.Any(l => l.Id == r.Id)).ToArray();

				if (me)
				{
					// download albums (B) where not in json
					IAlbum[] download = onlyRemote.Where(r => !Albums.Any(a => a.Id == r.Id)).ToArray();
					foreach (IAlbum album in download)
					{
						await Album.Get(Root, album.Title ?? album.Id, album.Id, true).Sync(context);
					}
					// delete albums (A) from local where in json
					if (Properties.Settings.Default.DeleteLocalFolder)
					{
						Album[] deleteLocal = onlyLocal.Where(l => Albums.Any(a => a.Id == l.Id && a.Synchronize)).ToArray();
						foreach (Album album in deleteLocal)
						{
							bool ok = true;
							if (Properties.Settings.Default.AskDeleteLocalFolder)
							{
								MessageDialogResult result = await DialogCoordinator.Instance.ShowMessageAsync(context, "Delete local album?", album.Root, MessageDialogStyle.AffirmativeAndNegative);
								ok = result == MessageDialogResult.Affirmative;
							}
							if (ok)
							{
								Directory.Delete(album.Root, true);
							}
						}
					}
					// delete albums (B) from remote where in json
					if (Properties.Settings.Default.DeleteRemoteFolder)
					{
						IAlbum[] deleteRemote = onlyRemote.Where(r => Albums.Any(a => a.Id == r.Id && a.Synchronize)).ToArray();
						foreach (IAlbum album in deleteRemote)
						{
							bool ok = true;
							if (Properties.Settings.Default.AskDeleteRemoteFolder)
							{
								MessageDialogResult result = await DialogCoordinator.Instance.ShowMessageAsync(context, "Delete Imgur album?", album.Title, MessageDialogStyle.AffirmativeAndNegative);
								ok = result == MessageDialogResult.Affirmative;
							}
							if (ok)
							{
								await endpoint.DeleteAlbumAsync(album.Id, Name);
							}
						}
					}
					// create albums without id
					Album[] upload = local.Where(l => l.Id == null && l.Synchronize).ToArray();
					foreach (Album album in upload)
					{
						AlbumEndpoint ep = new AlbumEndpoint(await ImgurHelper.GetClient());
						IAlbum rAlbum = await ep.CreateAlbumAsync(album.Title);
						album.ImgurAlbum = rAlbum;
						await album.Sync(context);
					}
				}
				else
				{
					// remove albums (A)
					if (Properties.Settings.Default.DeleteLocalFolder)
					{
						foreach (Album album in onlyLocal.Where(it => it.Synchronize))
						{
							bool ok = true;
							if (Properties.Settings.Default.AskDeleteLocalFolder)
							{
								MessageDialogResult result = await DialogCoordinator.Instance.ShowMessageAsync(context, "Delete local album?", album.Root, MessageDialogStyle.AffirmativeAndNegative);
								ok = result == MessageDialogResult.Affirmative;
							}
							if (ok)
							{
								album.Remove();
							}
						}
					}
					// download albums (B)
					foreach (IAlbum album in onlyRemote)
					{
						await Album.Get(Root, album.Title ?? album.Id, album.Id, true).Sync(context);
					}
				}

				// udpate json
				AlbumRoots = new DirectoryInfo(Root).GetDirectories().Where(it => it.Name[0] != '.').Select(it => it.FullName).ToList();
				Album[] albums = AlbumRoots.Select(it => Album.Get(it, "")).ToArray();

				App.Current.Dispatcher.Invoke((Action)delegate
				{
					Albums.Clear();
					foreach (Album album in albums)
					{
						Albums.Add(album);
					}
				});
				await Save();

				// synchronize album images
				foreach (Album album in albums)
				{
					await album.Sync(context);
				}
			});
		}




		public async Task Save()
		{
			if (!Directory.Exists(Root))
			{
				Directory.CreateDirectory(Root);
			}
			File.WriteAllText(Path.Combine(Root, "syncurr.user.json"), JsonConvert.SerializeObject(this));
			bool me = (await ImgurHelper.GetToken()).AccountId == Id;
			if (!me && !Properties.Settings.Default.Users.Contains(Root))
			{
				Properties.Settings.Default.Users.Add(Root);
				Properties.Settings.Default.Save();
			}
		}
		public async Task Remove()
		{
			if (Properties.Settings.Default.Users.Contains(Root))
			{
				Properties.Settings.Default.Users.Remove(Root);
				Properties.Settings.Default.Save();
			}
		}
	}
}
