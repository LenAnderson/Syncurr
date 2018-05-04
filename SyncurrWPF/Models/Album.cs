using Imgur.API.Endpoints.Impl;
using Imgur.API.Models;
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
	public class Album : ObservableObject, ISyncTarget
	{
		#region Static
		public static string SanitizePath(string path)
		{
			char[] invalid = Path.GetInvalidFileNameChars();
			return string.Join("_", path.Split(invalid, StringSplitOptions.RemoveEmptyEntries)).TrimEnd('.');
		}
		public static bool Exists(string path, string name)
		{
			name = SanitizePath(name);
			return File.Exists(Path.Combine(path, name, "syncurr.album.json"));
		}
		public static Album Get(string path, string name, string id=null, bool managed=false)
		{
			name = SanitizePath(name);
			Album album;
			if (Exists(path, name))
			{
				album = JsonConvert.DeserializeObject<Album>(File.ReadAllText(Path.Combine(path, name, "syncurr.album.json")));
				album.Root = Path.Combine(path, name);
			}
			else
			{
				album = Create(path, name, id, managed);
			}
			return album;
		}
		public static Album Create(string path, string name, string id, bool managed=false)
		{
			name = SanitizePath(name);
			Album album = new Album(path, name, id);
			album.Root = Path.Combine(path, name);
			album.Managed = managed;
			album.Save();
			return album;
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

		public bool Managed = false;
		
		private IAlbum _imgurAlbum;
		[JsonIgnore]
		public IAlbum ImgurAlbum
		{
			get { return _imgurAlbum; }
			set
			{
				if (_imgurAlbum != value)
				{
					_imgurAlbum = value;
					OnPropertyChanged(nameof(ImgurAlbum));
					Id = value.Id;
					Title = value.Title;
				}
			}
		}
		
		public ObservableCollection<string> Images { get; set; } = new ObservableCollection<string>();

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

		private string _title;
		public string Title
		{
			get
			{
				return _title ?? (ImgurAlbum != null ? ImgurAlbum.Title : new DirectoryInfo(Root).Name);
			}
			set
			{
				if (_title != value)
				{
					_title = value;
					OnPropertyChanged(nameof(Title));
				}
			}
		}




		public Album() { }
		public Album(string path, string name)
		{
			name = SanitizePath(name);
			Root = Path.Combine(path, name);
		}
		public Album(string path, string name, string id)
		{
			name = SanitizePath(name);
			Root = Path.Combine(path, name);
			Id = id;
		}




		public async Task Sync()
		{
			await Task.Run(async () =>
			{
				// get local files
				FileInfo[] local = new DirectoryInfo(Root).GetFiles().Where(it => new string[] { ".jpg", ".gif", ".png", ".bmp" }.Contains(it.Extension.ToLowerInvariant())).ToArray();

				// get remote files
				AlbumEndpoint endpoint = new AlbumEndpoint(await ImgurHelper.GetClient());
				ImgurAlbum = await endpoint.GetAlbumAsync(Id);
				bool owned = (await ImgurHelper.GetToken()).AccountId == ImgurAlbum.AccountId.ToString();

				// (A) filter for files only in local (not in remote)
				FileInfo[] onlyLocal = local.Where(l => !ImgurAlbum.Images.Select(r => new Image(r)).Any(r => r.FileName == l.Name)).ToArray();
				// (B) filter for files only in remote (not in local)
				Image[] onlyRemote = ImgurAlbum.Images.Select(r => new Image(r)).Where(r => !local.Any(l => r.FileName == l.Name)).ToArray();

				if (owned) //own album
				{
					// upload files (A) where not in json
					FileInfo[] upload = onlyLocal.Where(l => !Images.Any(i => i == l.Name)).ToArray();
					foreach (FileInfo img in upload)
					{
						Image image = new Image();
						image.Name = Path.GetFileNameWithoutExtension(img.Name);
						image.Link = img.Name;
						await image.Upload(this);
					}
					// download files (B) where not in json
					Image[] download = onlyRemote.Where(r => !Images.Any(i => i == r.FileName)).ToArray();
					foreach (Image img in download)
					{
						await img.Download(this);
					}
					// delete files (A) from local where in json
					FileInfo[] deleteLocal = onlyLocal.Where(l => Images.Any(i => i == l.Name)).ToArray();
					foreach (FileInfo img in deleteLocal)
					{
						File.Delete(img.FullName);
					}
					// delete files (B) from remote where in json
					Image[] deleteRemote = onlyRemote.Where(r => Images.Any(i => i == r.FileName)).ToArray();
					foreach (Image img in deleteRemote)
					{
						await img.Delete();
					}
				}
				else //not own album
				{
					// remove files (A)
					foreach (FileInfo img in onlyLocal)
					{
						File.Delete(img.FullName);
					}
					// download files (B)
					foreach (Image img in onlyRemote)
					{
						await img.Download(this);
					}
				}

				// update json
				string[] images = new DirectoryInfo(Root).GetFiles().Where(it => new string[] { ".jpg", ".gif", ".png", ".bmp" }.Contains(it.Extension.ToLowerInvariant())).Select(it => it.Name).ToArray();

				App.Current.Dispatcher.Invoke((Action)delegate
				{
					Images.Clear();
					foreach (string img in images)
					{
						Images.Add(img);
					}
				});
				Save();
			});
		}


		public void Save()
		{
			if (!Directory.Exists(Root))
			{
				Directory.CreateDirectory(Root);
			}
			File.WriteAllText(Path.Combine(Root, "syncurr.album.json"), JsonConvert.SerializeObject(this));
			if (!Managed && !Properties.Settings.Default.Albums.Contains(Root))
			{
				Properties.Settings.Default.Albums.Add(Root);
				Properties.Settings.Default.Save();
			}
		}

		public void Remove()
		{
			if (Properties.Settings.Default.Albums.Contains(Root))
			{
				Properties.Settings.Default.Albums.Remove(Root);
				Properties.Settings.Default.Save();
			}
		}
	}
}
