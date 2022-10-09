using System;

namespace SQ.Senior.Quoting.DatabaseAdapter.Models {
    public class ZelisError {
        public int ID { get; set; }
        public string NpiNumbers { get; set; }
        public string Exception { get; set; }
        public DateTime DateCreated { get; set; }
    }
}