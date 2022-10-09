using System;

namespace SQ.Senior.Quoting.DatabaseAdapter.Models {

    public class ZelisProvider_Location {
        public long ID { get; set; }
        public long ZelisProviderID { get; set; }
        public long ProviderLocationID { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string County { get; set; }
        public string Zip { get; set; }
        public string Phone { get; set; }
        public DateTime DateCreated { get; set; }
    }
}