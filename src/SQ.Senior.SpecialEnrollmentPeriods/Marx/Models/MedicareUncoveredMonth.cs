using Newtonsoft.Json;
using System;

namespace SQ.Senior.SpecialEnrollmentPeriods.Marx.Models {
    public class MedicareUncoveredMonth {

        [JsonProperty("start-date")]
        public DateTime? StartDate { get; set; }

        [JsonProperty("indicator")]
        public string Indicator { get; set; }

        [JsonProperty("number-of-uncovered-months")]
        public int? NumberOfUncoveredMonths { get; set; }

        [JsonProperty("total-number-of-uncovered-months")]
        public int? TotalNumberOfUncoveredMonths { get; set; }

        [JsonProperty("recordaddtimestamp")]
        public DateTime? RecordedDateTimeStamp { get; set; }
    }
}
