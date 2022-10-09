using System;

namespace SQ.Senior.Quoting.DatabaseAdapter.Models {

    public class ZelisProvider_Location_Network {
        public long ID { get; set; }
        public long ZelisProvider_LocationID { get; set; }
        public string ZelisNetworkID { get; set; }
        public string Flags { get; set; }
        public string TierLevel { get; set; }
        public string TierLabel { get; set; }
        public string Messages { get; set; }
        public DateTime DateCreated { get; set; }
    }
}