using System;

namespace SQ.Senior.Clients.FEMAService.Models {
    public class FEMARequest {
        public string State { get; set; }
        public string Fips { get; set; }
        public DateTime? DeclarationDateFrom { get; set; }
        public DateTime? DeclarationDateTo { get; set; }
        public DateTime? DisasterCloseoutDateFrom { get; set; }
        public DateTime? DisasterCloseoutDateTo { get; set; }
    }
}
