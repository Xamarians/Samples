using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatDemo.Models
{
    class LoginResult
    {
        [JsonProperty("access_token")]
        public string Token { get; set; }

        /// <summary>
        /// Expiry time in seconds from now
        /// </summary>
        [JsonProperty("expires_in")]
        public int ExpiryTime { get; set; }
    }

   
}
