using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace SQ.Senior.Clients.DrxServices.Models {

    [SerializableAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true)]
    public class PlanPharmacyCost {
        [XmlArrayItemAttribute("DrugCost", IsNullable = false)]
        public PlanPharmacyPrescriptionCost[] PrescriptionsCost { get; set; }
        public bool HasCeilingPrice { get; set; }
        public bool IsNetwork { get; set; }
        public bool IsPreferred { get; set; }

        [System.Xml.Serialization.XmlArrayItemAttribute("PharmacyID")]
        public string PharmacyId { get; set; }
        [XmlArrayItemAttribute("MonthlyCost", IsNullable = false)]
        public PlanPharmacyCostMonthly[] MonthlyCosts { get; set; }
        public byte PharmacyType { get; set; }
    }
}
