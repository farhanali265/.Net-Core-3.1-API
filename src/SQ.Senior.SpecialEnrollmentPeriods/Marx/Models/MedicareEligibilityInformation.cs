using Newtonsoft.Json;
using System;

namespace SQ.Senior.SpecialEnrollmentPeriods.Marx.Models {
    public class MedicareEligibilityInformation {

        [JsonProperty("part")]
        public string Part { get; set; }

        [JsonProperty("start")]
        public DateTime? StartDate { get; set; }

        [JsonProperty("end")]
        public DateTime? EndDate { get; set; }
    }
}
