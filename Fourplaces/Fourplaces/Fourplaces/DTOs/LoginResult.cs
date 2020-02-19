using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;


namespace Fourplaces.DTOs
{
    class LoginResult
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }

        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }
    }
}
