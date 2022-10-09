using System;

namespace SQ.Senior.Quoting.DatabaseAdapter.Models {
    public class ApplicantTracking {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Email { get; set; }
        public long? InteractionId { get; set; }
        public string ZIPCode { get; set; }
        public string FipsCode { get; set; }
        public string EntrySource { get; set; }
        public string EntryToken { get; set; }
        public string EntryKeyword { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? AccountId { get; set; }
        public long? IndividualId { get; set; }
        public long? QrsApplicantId { get; set; }
        public long? ScreenSize { get; set; }
        public string DeviceType { get; set; }
    }
}