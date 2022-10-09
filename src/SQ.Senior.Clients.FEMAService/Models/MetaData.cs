using System;
using Newtonsoft.Json;

namespace SQ.Senior.Clients.FEMAService.Models
{
    public class MetaData
    {
        [JsonProperty(PropertyName = "skip")]
        public int Skip { get; set; }

        [JsonProperty(PropertyName = "top")]
        public int Top { get; set; }

        [JsonProperty(PropertyName = "count")]
        public int Count { get; set; }

        [JsonProperty(PropertyName = "filter")]
        public string Filter { get; set; }

        [JsonProperty(PropertyName = "format")]
        public string Format { get; set; }

        [JsonProperty(PropertyName = "metadata")]
        public bool IsMetaData { get; set; }

        [JsonProperty(PropertyName = "select")]
        public object Select { get; set; }

        [JsonProperty(PropertyName = "entityname")]
        public string EntityName { get; set; }

        [JsonProperty(PropertyName = "version")]
        public string Version { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        [JsonProperty(PropertyName = "rundate")]
        public DateTime? RunDate { get; set; }

        [JsonProperty(PropertyName = "DeprecationInformation")]
        public DeprecationInformation DeprecationInformation { get; set; }
    }
}
