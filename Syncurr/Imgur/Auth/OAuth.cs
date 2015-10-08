using Syncurr.Imgur.Net;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Syncurr.Imgur.Auth
{
    class OAuth
    {
        static String refreshToken
        {
            get
            {
                return Properties.Settings.Default.refreshToken;
            }
            set
            {
                Properties.Settings.Default.refreshToken = value;
                Properties.Settings.Default.Save();
            }
        }
        public static String accessToken
        {
            get
            {
                return Properties.Settings.Default.accessToken;
            }
            set
            {
                Properties.Settings.Default.accessToken = value;
                Properties.Settings.Default.Save();
            }
        }
        public static String clientId
        {
            get
            {
                return Properties.Settings.Default.clientId;
            }
        }
        static String clientSecret
        {
            get
            {
                return Properties.Settings.Default.clientSecret;
            }
        }
        public static String pinUrl
        {
            get
            {
                return "https://api.imgur.com/oauth2/authorize?client_id=" + clientId + "&response_type=pin&state=nostate";
            }
        }
        public static String accountUsername
        {
            get
            {
                return Properties.Settings.Default.accountUsername;
            }
            set
            {
                Properties.Settings.Default.accountUsername = value;
                Properties.Settings.Default.Save();
            }
        }


        public static bool Authorize()
        {
            if (refreshToken == "")
            {
                return false;
            }
            else
            {
                return RetrieveTokensWithRefresh();
            }
        }


        public static bool RetrieveTokens(NameValueCollection data)
        {
            POSTRequest request = new POSTRequest("https://api.imgur.com/oauth2/token", data);
            WebResponse response = request.Send();
            using (Stream stream = response.GetResponseStream())
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(TokenResponse));
                TokenResponse tokens = (TokenResponse)serializer.ReadObject(stream);
                accessToken = tokens.accessToken;
                refreshToken = tokens.refreshToken;
                accountUsername = tokens.accountUsername;
            }
            return true;
        }

        public static bool RetrieveTokensWithRefresh()
        {
            NameValueCollection data = new NameValueCollection
            {
                {"client_id", clientId },
                {"client_secret", clientSecret },
                {"grant_type", "refresh_token" },
                {"refresh_token", refreshToken }
            };
            return RetrieveTokens(data);
        }

        public static bool RetrieveTokensWithPin(String pin)
        {
            NameValueCollection data = new NameValueCollection
            {
                {"client_id", clientId },
                {"client_secret", clientSecret },
                {"grant_type", "pin" },
                {"pin", pin }
            };
            return RetrieveTokens(data);
        }
    }
}
