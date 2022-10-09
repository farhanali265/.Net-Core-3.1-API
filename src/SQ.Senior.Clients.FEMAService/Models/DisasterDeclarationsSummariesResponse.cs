using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace SQ.Senior.Clients.FEMAService.Models
{
    public class DisasterDeclarationsSummariesResponse
    {
        [JsonProperty(PropertyName = "metadata")]
        public MetaData MetaData { get; set; }

        [JsonProperty(PropertyName = "DisasterDeclarationsSummaries")]
        public List<DisasterDeclarationsSummary> DisasterDeclarationsSummaries { get; set; }

        public bool ResponseStatus { get; set; }

        public Exception Exception { get; set; }
    }
}
