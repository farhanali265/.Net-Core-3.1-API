using System;

namespace SQ.Senior.Quoting.DatabaseAdapter.Models {
    public class ApplicantProvider {
        public int Id { get; set; }
        public long SQSAccountId { get; set; }
        public long SQSApplicantId { get; set; }
        public string ProviderType { get; set; }
        public long NationalProviderIdentifier { get; set; }
        public string Name { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Phone { get; set; }
        public bool IsActive { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public string UserId { get; set; }
        public string Specialty { get; set; }
        public long? QrsProviderId { get; set; }
    }
}