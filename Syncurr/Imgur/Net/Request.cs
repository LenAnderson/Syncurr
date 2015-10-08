using com.LandonKey.SocksWebProxy;
using com.LandonKey.SocksWebProxy.Proxy;
using Syncurr.Imgur.Auth;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Syncurr.Imgur.Net
{
    class Request
    {
        public String url { get; set; }
        public Boolean authorized { get; set; }

        public static bool useProxy { get { return Properties.Settings.Default.proxy; } }
        public static String proxyType { get { return Properties.Settings.Default.proxyType; } }
        public static String proxyUrl { get { return Properties.Settings.Default.proxyUrl; } }
        public static int proxyPort { get { return (int)Properties.Settings.Default.proxyPort; } }
        public static String proxyInternalUrl { get { return Properties.Settings.Default.proxyInternalUrl; } }
        public static int proxyInternalPort { get { return (int)Properties.Settings.Default.proxyInternalPort; } }
        public static String proxyUsername { get { return Properties.Settings.Default.proxyUsername; } }
        public static String proxyPassword { get { return Properties.Settings.Default.proxyPassword; } }

        public static IWebProxy proxy
        {
            get
            {
                if (proxyType == "HTTP")
                {
                    WebProxy proxy = new WebProxy(new Uri(proxyUrl + ":" + proxyPort));
                    if (proxyUsername != "" && proxyPassword != "")
                    {
                        proxy.Credentials = new NetworkCredential(proxyUsername, proxyPassword);
                    }
                    return proxy;
                }
                else if (proxyType.StartsWith("SOCKS"))
                {
                    SocksWebProxy proxy = new SocksWebProxy(new ProxyConfig(
                        IPAddress.Parse(proxyInternalUrl),
                        proxyInternalPort,
                        IPAddress.Parse(proxyUrl),
                        proxyPort,
                        proxyType == "SOCKS4" ? ProxyConfig.SocksVersion.Four : ProxyConfig.SocksVersion.Five
                        ));
                    if (proxyUsername != "" && proxyPassword != "")
                    {
                        proxy.Credentials = new NetworkCredential(proxyUsername, proxyPassword);
                    }
                    return proxy;
                }
                else
                {
                    return null;
                }
            }
        }


        public Request() { }
        public Request(String url)
        {
            this.url = url;
        }


        virtual public WebRequest Prepare()
        {
            WebRequest req = WebRequest.Create(this.url);
            if (this.authorized)
            {
                req.Headers.Add("Authorization", "Bearer " + OAuth.accessToken);
            }
            else
            {
                //req.Headers.Add("Authorization", "Client-ID " + OAuth.clientId);
            }
            if (useProxy)
            {
                req.Proxy = proxy;
            }

            return req;
        }


        virtual public WebResponse Send()
        {
            WebRequest req = this.Prepare();
            WebResponse response;
            try
            {
                response = req.GetResponse();
                return response;
            }
            catch (WebException we)
            {
                if (we.Response != null && ((HttpWebResponse)we.Response).StatusCode == HttpStatusCode.Forbidden)
                {
                    if (OAuth.Authorize())
                    {
                        return this.Send();
                    }
                    else
                    {
                        //TODO: ask for pin...?
                        throw we;
                    }
                }
                else
                {
                    throw we;
                }
            }
        }
    }
}
