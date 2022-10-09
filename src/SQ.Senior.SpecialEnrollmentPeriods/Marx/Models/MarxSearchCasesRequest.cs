using SQ.Senior.SpecialEnrollmentPeriods.Marx.Enums;

namespace SQ.Senior.SpecialEnrollmentPeriods.Marx.Models {
    public class MarxSearchCasesRequest {
        public string IdentifierId { get; set; }

        public IdentifierType IdentifierType { get; set; }

        internal string RequestUri {
            get {
                string target = IdentifierType == IdentifierType.SSN ? "&target=alt" : string.Empty;
                return $"marx-case/search?pid={IdentifierId}{target}";
            }
        }
    }
}
