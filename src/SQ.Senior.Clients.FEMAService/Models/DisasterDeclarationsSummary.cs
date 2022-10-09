using System;
using Newtonsoft.Json;

namespace SQ.Senior.Clients.FEMAService.Models
{
   public class DisasterDeclarationsSummary
    {
        [JsonProperty(PropertyName = "disasterNumber")]
        public string DisasterNumber { get; set; }

        [JsonProperty(PropertyName = "ihProgramDeclared")]
        public bool IHProgramDeclared { get; set; }

        [JsonProperty(PropertyName = "iaProgramDeclared")]
        public bool IAProgramDeclared { get; set; }

        [JsonProperty(PropertyName = "paProgramDeclared")]
        public bool PAProgramDeclared { get; set; }

        [JsonProperty(PropertyName = "hmProgramDeclared")]
        public bool HMProgramDeclared { get; set; }

        [JsonProperty(PropertyName = "state")]
        public string State { get; set; }

        [JsonProperty(PropertyName = "declarationDate")]
        public DateTime? DeclarationDate { get; set; }

        [JsonProperty(PropertyName = "fyDeclared")]
        public int FyDeclared { get; set; }

        [JsonProperty(PropertyName = "disasterType")]
        public string DisasterType { get; set; }

        [JsonProperty(PropertyName = "incidentType")]
        public string IncidentType { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "incidentBeginDate")]
        public DateTime? IncidentBeginDate { get; set; }

        [JsonProperty(PropertyName = "incidentEndDate")]
        public DateTime? IncidentEndDate { get; set; }

        [JsonProperty(PropertyName = "disasterCloseOutDate")]
        public DateTime? DisasterCloseOutDate { get; set; }

        [JsonProperty(PropertyName = "declaredCountyArea")]
        public string DeclaredCountyArea { get; set; }

        [JsonProperty(PropertyName = "placeCode")]
        public string PlaceCode { get; set; }

        [JsonProperty(PropertyName = "hash")]
        public string Hash { get; set; }

        [JsonProperty(PropertyName = "lastRefresh")]
        public DateTime? LastRefresh { get; set; }

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
    }
}
