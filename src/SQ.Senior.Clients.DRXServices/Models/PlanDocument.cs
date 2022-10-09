using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace SQ.Senior.Clients.DrxServices.Models {

    [SerializableAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true)]
    public class PlanDocument {
        public string LinkName { get; set; }
        public string Name { get; set; }
        [XmlArrayItemAttribute("Type", IsNullable = false)]
        public PlanDocumentType[] Types { get; set; }

        [XmlArrayItemAttribute("URL", IsNullable = false)]
        public string Url { get; set; }
    }
}
