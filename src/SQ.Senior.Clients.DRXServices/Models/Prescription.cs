using Newtonsoft.Json;
using System.Xml.Serialization;

namespace SQ.Senior.Clients.DrxServices.Models {

    [XmlRoot(ElementName = "Drug")]
    public class Prescription {

        [JsonProperty("DrugId")]
        public int PrescriptionId { get; set; }

        [JsonProperty("DrugName")]
        public string PrescriptionName { get; set; }

        [JsonProperty("DrugTypeID")]
        public int? PrescriptionTypeId { get; set; }

        [JsonProperty("GenericDrugId")]
        public int? GenericPrescriptionId { get; set; }

        [JsonProperty("GenericDrugName")]
        public string GenericPrescriptionName { get; set; }
    }
}
