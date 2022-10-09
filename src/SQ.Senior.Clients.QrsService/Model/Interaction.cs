using Newtonsoft.Json;
namespace SQ.Senior.Clients.QrsService.Models {
    public class Interaction : RecordStamp {
        [JsonProperty(PropertyName = "id")]
        public long Id { get; set; }

        [JsonProperty(PropertyName = "applicant_id")]
        public long? ApplicantId { get; set; }

        [JsonProperty(PropertyName = "source")]
        public string Source { get; set; }

    }
}