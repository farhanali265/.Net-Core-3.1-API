using Newtonsoft.Json;
using System;

namespace SQ.Senior.SpecialEnrollmentPeriods.Marx.Models {
    public class MedicareEnrollmentInformation {

        [JsonProperty("contract")]
        public string Contract { get; set; }

        [JsonProperty("pbp")]
        public string Pbp { get; set; }

        [JsonProperty("plan-type-code-description")]
        public string PlanTypeCodeDescription { get; set; }

        [JsonProperty("start")]
        public DateTime? StartDate { get; set; }

        [JsonProperty("end")]
        public DateTime? EndDate { get; set; }

        [JsonProperty("drug-plan")]
        public string DrugPlan { get; set; }
    }
}
