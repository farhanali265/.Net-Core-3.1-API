using System;

namespace SQ.Senior.Clients.DrxServices.Models {
    //TODO: add appliction user model in api project
    public class ApplicationUser {
        public string FirstName { get; set; }
        public string MiddleInitial { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string ZIPCode { get; set; }
        public string FipsCode { get; set; }
        public int? ExtraHelpLevel { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string EntrySource { get; set; }
        public string EntryToken { get; set; }
        public string EntryKeyword { get; set; }
        public string EntryPhoneNumber { get; set; }
        public long? AccountId { get; set; }
        public long? IndividualId { get; set; }
        public long? QRSApplicantId { get; set; }
        public string Email { get; set; }
    }
}
