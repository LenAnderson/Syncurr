using Syncurr.Imgur.API;
using Syncurr.Imgur.Net;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Syncurr.Local
{
    [DataContract]
    class Folder : INotifyPropertyChanged
    {
        protected String _path;
        protected String _albumId;
        protected String _albumTitle;

        public String path
        {
            get { return _path; }
            set
            {
                if (value.Substring(value.Length - 1) != @"\")
                {
                    value += @"\";
                }
                if (_path != null && _path != value)
                {
                    Properties.Settings.Default.folders.Remove(_path);
                    Properties.Settings.Default.folders.Add(value);
                    Properties.Settings.Default.Save();
                }
                SetProperty(ref _path, value);
            }
        }
        [DataMember]
        public List<String> images { get; set; }
        [DataMember]
        public String albumId { get { return _albumId; } set { SetProperty(ref _albumId, value); } }
        [DataMember]
        public String albumTitle { get { return _albumTitle; } set { SetProperty(ref _albumTitle, value); } }


        protected static List<Folder> _folders = new List<Folder>();
        public static List<Folder> folders
        {
            get
            {
                foreach (String path in Properties.Settings.Default.folders)
                {
                    Folder folder = Get(path);
                }
                return _folders;
            }
        }


        public static Folder Get(String path)
        {
            if (path.Substring(path.Length - 1) != @"\")
            {
                path += @"\";
            }
            Folder folder;
            List<Folder> search = _folders.Where(f => f.path == path).ToList<Folder>();
            if (search.Count > 0)
            {
                folder = search[0];
            }
            else if (File.Exists(path + "syncur.json"))
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Folder));
                FileStream file = File.OpenRead(path + "syncur.json");
                folder = (Folder)serializer.ReadObject(file);
                file.Close();
                folder.path = path;
                _folders.Add(folder);
            }
            else
            {
                folder = new Folder(path);
                _folders.Add(folder);
            }
            return folder;
        }

        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(storage, value))
            {
                return false;
            }

            storage = value;
            this.OnPropertyChanged(propertyName);
            return true;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler eventHandler = this.PropertyChanged;
            if (eventHandler != null)
            {
                eventHandler(this, new PropertyChangedEventArgs(propertyName));
            }
        }


        public Folder() { }
        public Folder(String path)
        {
            this.path = path;
            this.images = new List<String>();
        }


        public void Sync()
        {
            if (this.albumId != null)
            {
                // get local files
                String[] local = new DirectoryInfo(this.path).GetFiles().Select(o => o.Name).Where(s => s.EndsWith(".jpg") || s.EndsWith(".gif") || s.EndsWith(".png") || s.EndsWith(".bmp")).ToArray<String>();
                // get remote files
                Album album = Album.Get(this.albumId);
                this.albumTitle = album.title;
                // (A) filter for files only in local (not in remote)
                String[] onlyLocal = local.Where(s => album.images.Where(i => i.filename == s).Count() == 0).ToArray();
                // (B) filter for files only in remote (not in local)
                Image[] onlyRemote = album.images.Where(i => local.Where(s => i.filename == s).Count() == 0).ToArray();
                // upload files (A) where not in json
                String[] upload = onlyLocal.Where(s => this.images.Where(j => j == s).Count() == 0).ToArray();
                foreach (String img in upload)
                {
                    Image image = new Image();
                    image.name = img;
                    image.Save(this.path, img, this.albumId);
                }
                // download files (B) where not in json
                Image[] download = onlyRemote.Where(i => this.images.Where(j => j == i.filename).Count() == 0).ToArray();
                foreach (Image img in download)
                {
                    img.Download(this.path);
                }
                // delete files (A) from local where in json
                String[] deleteLocal = onlyLocal.Where(s => this.images.Where(j => j == s).Count() > 0).ToArray();
                foreach (String img in deleteLocal)
                {
                    File.Delete(this.path + img);
                }
                // delete files (B) from remote where in json
                Image[] deleteRemote = onlyRemote.Where(i => this.images.Where(j => j == i.filename).Count() > 0).ToArray();
                foreach (Image img in deleteRemote)
                {
                    img.Delete();
                }
                // update json
                images = new DirectoryInfo(this.path).GetFiles().Select(o => o.Name).Where(s => s.EndsWith(".jpg") || s.EndsWith(".gif") || s.EndsWith(".png") || s.EndsWith(".bmp")).ToList<String>();
                SaveFolderStatus();
            }
        }

        protected void SaveFolderStatus()
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Folder));
            FileStream fileStream = File.Create(this.path + "syncur.json");
            serializer.WriteObject(fileStream, this);
            fileStream.Close();
        }

        public void Save()
        {
            SaveFolderStatus();
            if (!Properties.Settings.Default.folders.Contains(this.path))
            {
                Properties.Settings.Default.folders.Add(this.path);
                Properties.Settings.Default.Save();
            }
        }

        public void Remove()
        {
            Properties.Settings.Default.folders.Remove(this.path);
            Properties.Settings.Default.Save();
            _folders.Remove(this);
        }
    }
}
