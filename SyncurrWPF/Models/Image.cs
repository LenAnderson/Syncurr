using Imgur.API.Endpoints.Impl;
using Imgur.API.Models;
using SyncurrWPF.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SyncurrWPF.Models
{
	public class Image : ObservableObject
	{
		private IImage _imgurImage;
		public IImage ImgurImage
		{
			get { return _imgurImage; }
			set
			{
				if (_imgurImage != value)
				{
					_imgurImage = value;
					OnPropertyChanged(nameof(ImgurImage));
					Id = value.Id;
					Name = value.Name ?? value.Id;
					Link = value.Link;
				}
			}
		}

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
				}
			}
		}

		private string _link;
		public string Link
		{
			get { return _link; }
			set
			{
				if (_link != value)
				{
					_link = value;
					OnPropertyChanged(nameof(Link));
				}
			}
		}

		public string Extension
		{
			get { return new Regex(@"^.+\.([^\.]+)$").Replace(Link, "$1"); }
		}

		public string FileName
		{
			get
			{
				return string.Format("{0}.{1}", new Regex(@"[^a-z0-9\-_ ]+", RegexOptions.IgnoreCase).Replace(Name, "_"), Extension);
			}
		}




		public Image() { }
		public Image(IImage imgurImage)
		{
			ImgurImage = imgurImage;
		}




		public async Task Download(Album album)
		{
			WebRequest request = WebRequest.Create(ImgurImage.Link);
			using (WebResponse response = await request.GetResponseAsync())
			using (Stream src = response.GetResponseStream())
			using (Stream dst = File.Create(Path.Combine(album.Root, FileName)))
			{
				byte[] buffer = new byte[4096];
				int bytesRead;
				while ((bytesRead = src.Read(buffer, 0, buffer.Length)) > 0)
				{
					dst.Write(buffer, 0, bytesRead);
				}
			}
		}

		public async Task Upload(Album album)
		{
			ImageEndpoint endpoint = new ImageEndpoint(await ImgurHelper.GetClient());
			using (Stream src = File.OpenRead(Path.Combine(album.Root, FileName)))
			{
				await endpoint.UploadImageStreamAsync(src, album.Id, Name);
			}
		}

		public async Task Delete()
		{
			ImageEndpoint endpoint = new ImageEndpoint(await ImgurHelper.GetClient());
			await endpoint.DeleteImageAsync(Id);
		}
	}
}
