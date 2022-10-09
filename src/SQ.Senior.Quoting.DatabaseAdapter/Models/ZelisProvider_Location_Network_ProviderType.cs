using System;

namespace SQ.Senior.Quoting.DatabaseAdapter.Models {

    public class ZelisProvider_Location_Network_ProviderType {
        public long ID { get; set; }
        public long ZelisProvider_Location_NetworkID { get; set; }
        public int ZelisProviderTypeID { get; set; }
        public DateTime DateCreated { get; set; }
    }
}