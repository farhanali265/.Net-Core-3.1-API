using System.Collections.Generic;

namespace SQ.Senior.Clients.DrxServices.Models {

    public class FomularyObject {
        public string TierDescription { get; set; }
        public int TierNumber { get; set; }
        public int FormularyId { get; set; }
        public int CoveredDosages { get; set; }
        public bool IsExceptionTier { get; set; }
        public List<CopayPrices> CopayPrices { get; set; }
    }
}
