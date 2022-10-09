using System;

namespace SQ.Senior.Quoting.DatabaseAdapter.Models {
    public class ZelisLog {
        public int ID { get; set; }
        public long SqsAccountID { get; set; }
        public long SqsApplicantID { get; set; }
        public string Zip { get; set; }
        public string Fips { get; set; }
        public string NpiNumbers { get; set; }
        public string Networks { get; set; }
        public string ResponseJson { get; set; }
        public DateTime DateCreated { get; set; }
        public string UserId { get; set; }
    }
}