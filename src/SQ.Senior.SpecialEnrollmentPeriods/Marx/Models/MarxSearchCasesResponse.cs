using Newtonsoft.Json;
using System;

namespace SQ.Senior.SpecialEnrollmentPeriods.Marx.Models {
    public class MarxSearchCasesResponse {

        [JsonProperty("requested")]
        public DateTime RequestDate { get; set; }

        [JsonProperty("completed")]
        public DateTime DateCompleted { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("status")]
        public string RequestStatus { get; set; }

        [JsonProperty("outcome")]
        public string RequestOutcome { get; set; }

        [JsonProperty("capture")]
        public Capture Capture { get; set; }

        public bool ResponseStatus { get; set; }

        public Exception Exception { get; set; }
    }
}
