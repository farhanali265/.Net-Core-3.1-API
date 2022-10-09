using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace SQ.Senior.Clients.DrxServices.Models {

    [SerializableAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true)]
    public class PlanPharmacyMonthlyCostDetail {
        public decimal FullCost { get; set; }
        public string LabelName { get; set; }
        public decimal MemberCost { get; set; }
        public string Phase { get; set; }
    }
}
