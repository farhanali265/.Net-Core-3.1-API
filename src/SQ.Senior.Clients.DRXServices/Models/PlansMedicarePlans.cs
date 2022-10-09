using System.Xml.Serialization;

namespace SQ.Senior.Clients.DrxServices.Models {
    public class PlansMedicarePlans {
        [XmlElement(ElementName = "Plan")]
        public Medicare[] Plan { get; set; }
    }
}
