using Newtonsoft.Json;
using System;

namespace SQ.Senior.SpecialEnrollmentPeriods.Marx.Models {
    public class MedicareElectionTypeCodeUsage {

        [JsonProperty("election-type-code")]
        public string ElectionTypeCode { get; set; }

        [JsonProperty("election-type-description")]
        public string ElectionTypeDescription { get; set; }

        [JsonProperty("enrolldisenroll")]
        public string EnrollDisEnroll { get; set; }

        [JsonProperty("last-used-date")]
        public DateTime? LastUsedDate { get; set; }
    }
}
