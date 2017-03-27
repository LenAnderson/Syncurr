using Syncurr.Imgur.Net;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Syncurr.Imgur.API
{
    [DataContract]
    class Image : Model<Image>
    {
        [DataMember]
        public String id { get; set; }
        protected String _name;
        [DataMember]
        public String name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = new Regex(@"^(.+)(\.[^\.]+)$").Replace(value, "$1");
            }
        }
        [DataMember]
        public String link { get; set; }
        public String filename { get { return this.name + new Regex(@"^.+(\.[^\.]+)$").Replace(this.link, "$1"); } }


        public static Image Get(String id)
        {
            return GetItem("image/" + id);
        }


        public Image()
        {
            this.name = "";
            this.link = "";
        }


        public void Delete()
        {
            DELETERequest request = new DELETERequest("https://api.imgur.com/3/image/" + this.id);
            request.authorized = true;
            request.Send();
        }

        public void Download(String folder)
        {
            Request request = new Request(this.link);
            //request.authorized = true;
            WebResponse response = request.Send();
            Stream responseStream = response.GetResponseStream();
            byte[] buffer = new byte[4096];
            int bytesRead;
            FileStream fileStream = File.Create(folder + this.filename);
            while ((bytesRead = responseStream.Read(buffer, 0, buffer.Length)) > 0)
            {
                fileStream.Write(buffer, 0, bytesRead);
            }
            fileStream.Close();
        }

        public void Save(String folder, String file, String albumId)
        {
            Request request = new POSTRequest("https://api.imgur.com/3/image", new NameValueCollection { { "album", albumId }, { "name", this.name }, { "type", "base64" }, { "image", Convert.ToBase64String(File.ReadAllBytes(folder + file)) } });
            request.authorized = true;
            request.Send();
        }
    }
}
