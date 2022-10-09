using Newtonsoft.Json;

namespace SQ.Senior.Clients.DrxServices.Models {

    public class PrescriptionDetails : Prescription {
        public string ChemicalName { get; set; }
        [JsonProperty("DrugType")]
        public string PrescriptionType { get; set; }
        [JsonProperty("DrugTypeEnum")]
        public int? PrescriptionTypeEnum { get; set; }        
        public string SearchMatchType { get; set; }
    }
}
