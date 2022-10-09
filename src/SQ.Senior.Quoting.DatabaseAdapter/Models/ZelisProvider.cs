using System;

namespace SQ.Senior.Quoting.DatabaseAdapter.Models {

    public class ZelisProvider {
        public long ZelisProviderID { get; set; }
        public long ProviderID { get; set; }
        public long NpiNumber { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Suffix { get; set; }
        public string FacilityName { get; set; }
        public int? MinimumDistanceInMiles { get; set; }
        public string Degrees { get; set; }
        public string MedicalSchools { get; set; }
        public string Gender { get; set; }
        public string BoardCertifications { get; set; }
        public int? MatchScore { get; set; }
        public int ZelisLogID { get; set; }
        public DateTime DateCreated { get; set; }
    }
}