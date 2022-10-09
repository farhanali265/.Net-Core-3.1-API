using System.Xml.Serialization;

namespace SQ.Senior.Clients.DrxServices.Models {


    [XmlRoot(ElementName = "Plan")]
    public class MedicarePlanDetail : Medicare {
        public MedicarePlanDetail PrescriptionCostMonthly { get; set; }
        public string ViewPlanYear { get; set; }
    }
}
