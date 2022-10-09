using System;

namespace SQ.Senior.Quoting.DatabaseAdapter.Models {
    public class ZelisProvider_Location_Network_ProviderType_Specialty {
        public long ID { get; set; }
        public long ZelisProvider_Location_Network_ProviderTypeID { get; set; }
        public int ZelisSpecialtyID { get; set; }
        public bool? AcceptingNewPatients { get; set; }
        public string AcceptingNewPatientsText { get; set; }
        public string EnrollmentID { get; set; }
        public DateTime DateCreated { get; set; }
        public string PracticeStatus { get; set; }
    }
}