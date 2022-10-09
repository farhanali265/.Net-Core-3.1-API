using Newtonsoft.Json;
using System.Collections.Generic;

namespace SQ.Senior.SpecialEnrollmentPeriods.Marx.Models {
    public class Capture {

        [JsonProperty("medicare-enrollment-information")]
        public List<MedicareEnrollmentInformation> MedicareEnrollmentInformation { get; set; }

        [JsonProperty("medicare-entitlement-information")]
        public List<MedicareEntitlementInformation> MedicareEntitlementInformation { get; set; }

        [JsonProperty("medicare-eligibility-information")]
        public List<MedicareEligibilityInformation> MedicareEligibilityInformation { get; set; }

        //TODO: these are being reset on API, business entities are to be defined accordingly then
        //[JsonProperty("medicare-ineligibility-incarceration")]
        //public List<object> MedicareIneligibilityIncarceration { get; set; }

        //[JsonProperty("medicare-ineligibility-not-lawfully-present")]
        //public List<object> MedicareIneligibilityNotLawfullyPresent { get; set; }

        [JsonProperty("medicare-uncovered-months")]
        public List<MedicareUncoveredMonth> MedicareUncoveredMonths { get; set; }

        [JsonProperty("medicare-election-type-code-usage")]
        public List<MedicareElectionTypeCodeUsage> MedicareElectionTypeCodeUsages { get; set; }

        [JsonProperty("medicare-low-income-status")]
        public List<MedicareLowIncomeStatus> MedicareLowIncomeStatus { get; set; }
    }
}
