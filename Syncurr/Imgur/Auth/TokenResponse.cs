using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Syncurr.Imgur.Auth
{
    [DataContract]
    class TokenResponse
    {
        [DataMember(Name = "access_token")]
        public String accessToken { get; set; }
        [DataMember(Name = "token_type")]
        public String tokenType { get; set; }
        [DataMember(Name = "refresh_token")]
        public String refreshToken { get; set; }
        [DataMember(Name = "account_id")]
        public String accountId { get; set; }
        [DataMember(Name = "account_username")]
        public String accountUsername { get; set; }

        public TokenResponse() { }
    }
}
