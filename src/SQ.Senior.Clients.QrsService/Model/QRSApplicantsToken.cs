using Newtonsoft.Json;
using System;
namespace SQ.Senior.Clients.QrsService.Models {
    public class QRSApplicantsTokens : RecordStamp {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "applicant_id")]
        public string ApplicantId { get; set; }

        [JsonProperty(PropertyName = "token")]
        public string Token { get; set; }

        [JsonProperty(PropertyName = "packet")]
        public string Packet { get; set; }

        [JsonProperty(PropertyName = "date_issued")]
        public DateTime? DateIssued { get; set; }

        [JsonProperty(PropertyName = "date_expires")]
        public DateTime? DateExpired { get; set; }

    }
}