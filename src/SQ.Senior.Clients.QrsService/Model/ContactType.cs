using Newtonsoft.Json;
namespace SQ.Senior.Clients.QrsService.Models {
    public class ContactType {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "contact_type")]
        public string ContactTypeText { get; set; }

        [JsonProperty(PropertyName = "order_by")]
        public int OrderBy { get; set; }

        [JsonProperty(PropertyName = "is_active")]
        public bool IsActive { get; set; }

        [JsonProperty(PropertyName = "is_deleted")]
        public bool IsDeleted { get; set; }

    }
}