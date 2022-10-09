using System;

namespace SQ.Senior.Quoting.DatabaseAdapter.Models {
    public class AspNetUser {
        public string Id { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTime? LockoutEndDateUtc { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string MiddleInitial { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string ZipCode { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string FipsCode { get; set; }
        public int? ExtraHelpLevel { get; set; }
        public long? QrsApplicantId { get; set; }
        public long? QrsContactId { get; set; }
        public string EntrySource { get; set; }
        public string EntryToken { get; set; }
        public string EntryKeyword { get; set; }
        public string EntryPhoneNumber { get; set; }
        public bool? PrescriptionDoesNotApply { get; set; }
        public bool? ProviderDoesNotApply { get; set; }
        public bool? PharmacyDoesNotApply { get; set; }
        public long? AccountId { get; set; }
        public long? IndividualId { get; set; }
    }
}