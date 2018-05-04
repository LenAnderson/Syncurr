using Imgur.API.Authentication.Impl;
using Imgur.API.Endpoints.Impl;
using Imgur.API.Models;
using Imgur.API.Models.Impl;
using MahApps.Metro.Controls.Dialogs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncurrWPF.Helpers
{
	public class ImgurHelper
	{
		public static object Context;




		public static async Task<IOAuth2Token> GetToken()
		{
			OAuth2Endpoint auth = new OAuth2Endpoint(new ImgurClient(Properties.Settings.Default.ClientId, Properties.Settings.Default.ClientSecret));
			IOAuth2Token token = null;
			if (!string.IsNullOrWhiteSpace(Properties.Settings.Default.Token))
			{
				token = JsonConvert.DeserializeObject<OAuth2Token>(Properties.Settings.Default.Token);
				if (token != null)
				{
					if (token.ExpiresIn > 0) return token;
					token = await auth.GetTokenByRefreshTokenAsync(token.RefreshToken);
					Properties.Settings.Default.Token = JsonConvert.SerializeObject(token);
					Properties.Settings.Default.Save();
				}
			}
			Process.Start(auth.GetAuthorizationUrl(Imgur.API.Enums.OAuth2ResponseType.Pin));
			string pin = await DialogCoordinator.Instance.ShowInputAsync(Context, "Enter Imgur Pin", "");
			token = await auth.GetTokenByPinAsync(pin);
			Properties.Settings.Default.Token = JsonConvert.SerializeObject(token);
			Properties.Settings.Default.Save();
			return token;
		}

		public static async Task<ImgurClient> GetClient()
		{
			IOAuth2Token token = await GetToken();
			ImgurClient client = new ImgurClient(Properties.Settings.Default.ClientId, Properties.Settings.Default.ClientSecret, token);
			return client;
		}
	}
}
