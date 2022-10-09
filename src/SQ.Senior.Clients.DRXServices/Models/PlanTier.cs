using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace SQ.Senior.Clients.DrxServices.Models {

    [SerializableAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true)]
    public class PlanTier {
        public byte DaysSupply { get; set; }
        [XmlArrayItemAttribute("Tier", IsNullable = false)]
        public PlanTier[] TierInformation { get; set; }
        public int PharmacyType { get; set; }
    }
}
