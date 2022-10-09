using Newtonsoft.Json;
using System;

namespace SQ.Senior.SpecialEnrollmentPeriods.Marx.Models {
    public class MedicareLowIncomeStatus {

        [JsonProperty("subsidy-start-date")]
        public DateTime? SubsidyStartDate { get; set; }

        [JsonProperty("subsidy-end-date")]
        public DateTime? SubsidyEndDate { get; set; }

        [JsonProperty("premium-subsidy-level")]
        public string PremiumSubsidyLevel { get; set; }

        [JsonProperty("copayment-level")]
        public string CopaymentLevel { get; set; }

        [JsonProperty("subsidy-source")]
        public string SubsidySource { get; set; }
    }
}
