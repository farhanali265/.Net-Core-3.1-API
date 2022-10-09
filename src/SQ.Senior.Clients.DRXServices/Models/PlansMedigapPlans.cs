using System.Xml.Serialization;

namespace SQ.Senior.Clients.DrxServices.Models {
    public class PlansMedigapPlans {
        [XmlElement(ElementName = "Plan")]
        public Medigap[] Plan { get; set; }
    }
}
