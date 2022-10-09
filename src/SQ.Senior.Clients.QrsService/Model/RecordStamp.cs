using Newtonsoft.Json;
using System;
namespace SQ.Senior.Clients.QrsService.Models {
    public abstract class RecordStamp {
        [JsonProperty(PropertyName = "date_created")]
        public DateTime? DateCreated { get; set; }

        [JsonProperty(PropertyName = "date_modified")]
        public DateTime? DateModified { get; set; }

        [JsonProperty(PropertyName = "date_deleted")]
        public DateTime? DateDeleted { get; set; }
    }
}