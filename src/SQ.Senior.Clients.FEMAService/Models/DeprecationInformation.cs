using System;
using Newtonsoft.Json;

namespace SQ.Senior.Clients.FEMAService.Models
{
   public class DeprecationInformation
    {
        [JsonProperty(PropertyName = "depDate")]
        public DateTime? DeprecatedDate { get; set; }

        [JsonProperty(PropertyName = "deprecatedComment")]
        public string DeprecatedComment { get; set; }

        [JsonProperty(PropertyName = "depApiMessage")]
        public string DeprecatedApiMessage { get; set; }

        [JsonProperty(PropertyName = "depNewURL")]
        public string DeprecatedNewUrl { get; set; }

        [JsonProperty(PropertyName = "depWebMessage")]
        public string DeprecatedWebMessage { get; set; }
    }
}
