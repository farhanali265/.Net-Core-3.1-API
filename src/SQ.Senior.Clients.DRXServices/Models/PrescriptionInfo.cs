using System.Collections.Generic;
using System.Xml.Serialization;

namespace SQ.Senior.Clients.DrxServices.Models {

    [XmlRoot(ElementName = "Drug")]
    public class PrescriptionInfo : Prescription {
        public PrescriptionInfo() {
            Dosages = new List<DosageInfo>();
        }
        public string PrescriptionType { get; set; }
        public List<DosageInfo> Dosages { get; set; }
        public string ChemicalName { get; set; }
        public string PrescriptionInfoAsXml { get; set; }
    }
}
