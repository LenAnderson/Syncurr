using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Syncurr.Imgur.Auth;
using System.IO;
using System.Web;
using System.Collections.Specialized;

namespace Syncurr.Imgur.Net
{
    class POSTRequest : Request
    {
        public NameValueCollection data { get; set; }
        public String folder { get; set; }
        public String file { get; set; }


        public POSTRequest() : base() { }
        public POSTRequest(String url) : base(url) { }
        public POSTRequest(String url, NameValueCollection data)
        {
            this.url = url;
            this.data = data;
        }


        public override WebRequest Prepare()
        {
            WebRequest req = base.Prepare();

            req.Method = WebRequestMethods.Http.Post;

            String data = "";
            foreach (String key in this.data.Keys)
            {
                data += "&" + key + "=" + EscapeDataString(this.data[key]);
            }
            byte[] byteData = Encoding.UTF8.GetBytes(data.Substring(1));

            req.ContentLength = byteData.Length;
            req.ContentType = "application/x-www-form-urlencoded";
            using (Stream stream = req.GetRequestStream())
            {
                stream.Write(byteData, 0, byteData.Length);
                stream.Close();
            }

            return req;
        }

        protected String EscapeDataString(String data)
        {
            StringBuilder sb = new StringBuilder();
            int limit = 65519;
            int loops = data.Length / limit;
            for (int i=0; i<=loops; i++)
            {
                if (i < loops)
                {
                    sb.Append(Uri.EscapeDataString(data.Substring(limit * i, limit)));
                }
                else
                {
                    sb.Append(Uri.EscapeDataString(data.Substring(limit * i)));
                }
            }
            return sb.ToString();
        }

        public WebRequest PrepareFile()
        {
            HttpWebRequest req = (HttpWebRequest)base.Prepare();

            String boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
            byte[] boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");

            req.ContentType = "multipart/form-data; boundary=" + boundary;
            req.Method = WebRequestMethods.Http.Post;
            req.KeepAlive = true;

            Stream rs = req.GetRequestStream();

            String formdataTemplate = "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";
            foreach (String key in this.data.Keys)
            {
                rs.Write(boundarybytes, 0, boundarybytes.Length);
                String formItem = String.Format(formdataTemplate, key, data[key]);
                byte[] formItemBytes = System.Text.Encoding.UTF8.GetBytes(formItem);
                rs.Write(formItemBytes, 0, formItemBytes.Length);
            }
            rs.Write(boundarybytes, 0, boundarybytes.Length);

            String contentType = MimeMapping.GetMimeMapping(folder + file);
            String headerTemplate = "Content-Disposition form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n";
            String header = String.Format(headerTemplate, "image", file, contentType);
            byte[] headerBytes = System.Text.Encoding.UTF8.GetBytes(header);
            rs.Write(headerBytes, 0, headerBytes.Length);

            FileStream fileStream = new FileStream(folder + file, FileMode.Open, FileAccess.Read);
            byte[] buffer = new byte[4096];
            int bytesRead = 0;
            while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) > 0)
            {
                rs.Write(buffer, 0, bytesRead);
            }
            fileStream.Close();

            byte[] trailer = System.Text.Encoding.UTF8.GetBytes("\r\n--" + boundary + "--\r\n");
            rs.Write(trailer, 0, trailer.Length);
            rs.Close();

            return req;
        }
    }
}
