namespace SQ.Senior.Clients.DrxServices.Models {

    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public class PlanPharmacyPrescriptionCost {
        public decimal AfterGap { get; set; }
        public decimal BeforeGap { get; set; }
        public decimal Deductible { get; set; }
        public decimal FullCost { get; set; }
        public decimal Gap { get; set; }
        public string LabelName { get; set; }
        public decimal Quantity { get; set; }
    }
}
