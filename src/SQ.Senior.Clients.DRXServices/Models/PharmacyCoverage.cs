namespace SQ.Senior.Clients.DrxServices.Models {

    public class PharmacyCoverage {
        public bool IsNetwork { get; set; }
        public bool IsMailOrder { get; set; }
        public bool IsPreferred { get; set; }
        public bool IsRetail { get; set; }
        public bool IsSpecialty { get; set; }
        public bool HasExtendedDaysOfSupply { get; set; }
    }
}
