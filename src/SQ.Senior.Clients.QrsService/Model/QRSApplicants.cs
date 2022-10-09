using Newtonsoft.Json;
using System.Collections.Generic;

namespace SQ.Senior.Clients.QrsService.Models {
    public class QRSApplicants : RecordStamp {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "account_id")]
        public string AccountId { get; set; }

        [JsonProperty(PropertyName = "sqs_applicant_id")]
        public string SQSApplicantId { get; set; }

        [JsonProperty(PropertyName = "first_name")]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "middle_initial")]
        public string MiddleInitial { get; set; }

        [JsonProperty(PropertyName = "last_name")]
        public string LastName { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "date_of_birth")]
        public string DOB { get; set; }

        [JsonProperty(PropertyName = "gender")]
        public string Gender { get; set; }

        [JsonProperty(PropertyName = "is_primary")]
        public bool? IsPrimary { get; set; }

        [JsonProperty(PropertyName = "prescriptions")]
        public List<QRSPrescription> Prescriptions { get; set; }

        [JsonProperty(PropertyName = "pharmacies")]
        public List<QRSPharmacy> Pharmacies { get; set; }

        [JsonProperty(PropertyName = "providers")]
        public List<QRSProvider> Providers { get; set; }
        [JsonProperty(PropertyName = "contacts")]
        public List<QRSContact> Contacts { get; set; }

        [JsonProperty(PropertyName = "plans")]
        public List<QRSPlan> Plans { get; set; }

        [JsonProperty(PropertyName = "extra_help_level")]
        public int? ExtraHelpLevel { get; set; }

        public int QrsApplicantId { get; set; }

    }
}
