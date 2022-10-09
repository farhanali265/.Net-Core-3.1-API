using Newtonsoft.Json;
using System;

namespace SQ.Senior.Clients.DrxServices.Models {
    public class DrxToken {

        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        public DateTime AccessTokenExpiry { get; set; }
        public string SessionId { get; set; }
        public DateTime SessionIdExpiry { get; set; }
    }
}
