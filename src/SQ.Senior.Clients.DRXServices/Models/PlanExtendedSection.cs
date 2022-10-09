using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace SQ.Senior.Clients.DrxServices.Models {

    [SerializableAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true)]
    public class PlanExtendedSection {
        public byte HeaderType { get; set; }
        [XmlArrayItemAttribute("PlanExtendedDetail", IsNullable = false)]
        public PlanExtendedSectionDetail[] SectionDetails { get; set; }
    }
}
