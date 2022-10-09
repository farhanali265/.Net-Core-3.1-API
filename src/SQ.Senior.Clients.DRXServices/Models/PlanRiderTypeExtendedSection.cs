namespace SQ.Senior.Clients.DrxServices.Models {

    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public class PlanRiderTypeExtendedSection {
        public byte HeaderType { get; set; }
        public PlanRiderTypeExtendedSection SectionDetails { get; set; }
    }
}
