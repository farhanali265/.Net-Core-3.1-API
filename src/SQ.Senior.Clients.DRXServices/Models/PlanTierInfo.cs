using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace SQ.Senior.Clients.DrxServices.Models {

    [SerializableAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true)]
    public class PlanTierInfo {
        public string Amount { get; set; }
        public string Description { get; set; }
        public byte TierNumber { get; set; }
    }
}
