using System.Collections.Generic;

namespace SQ.Senior.Clients.DrxServices.Models {
    public class PrescriptionCost {
        public string LabelName { get; set; }
        public double Quantity { get; set; }
        public decimal FullCost { get; set; }
        public decimal Deductible { get; set; }
        public decimal BeforeGap { get; set; }
        public decimal Gap { get; set; }
        public decimal AfterGap { get; set; }
        public List<PrescriptionFootnote> PrescriptionFootnotes { get; set; }
    }
}
