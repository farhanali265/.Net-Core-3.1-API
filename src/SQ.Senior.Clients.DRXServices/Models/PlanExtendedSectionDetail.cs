using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace SQ.Senior.Clients.DrxServices.Models {

    [SerializableAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true)]
    public class PlanExtendedSectionDetail {
        public string Description { get; set; }
        public bool IsEnrollLink { get; set; }
        public bool IsOutOfNetwork { get; set; }
        public string LinkName { get; set; }
        [XmlArrayItemAttribute("Type", IsNullable = false)]
        public PlanTypeExtendedDetail[] Types { get; set; }
        public string Value { get; set; }
        public bool IsExternalLink { get; set; }
    }
}
