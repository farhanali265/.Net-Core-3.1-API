using System;

namespace SQ.Senior.Quoting.DatabaseAdapter.Models {
    public class Pharmacy {
        public Guid Id { get; set; }
        public string PharmacyDrxId { get; set; }
        public string PharmacyName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string StateCode { get; set; }
        public string PharmacyPhone { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Distance { get; set; }
        public string PharmacyNpi { get; set; }
        public bool? Has24hrService { get; set; }
        public bool? HasCompounding { get; set; }
        public bool? HasDelivery { get; set; }
        public bool? HasDriveup { get; set; }
        public bool? HasDurableEquipment { get; set; }
        public bool? HasEPrescriptions { get; set; }
        public bool? HasHandicapAccess { get; set; }
        public bool? IsHomeInfusion { get; set; }
        public bool? IsLongTermCare { get; set; }
        public string UserId { get; set; }
        public DateTime Dated { get; set; } = DateTime.UtcNow;
        public bool IsDeleted { get; set; } = false;
        public string JsonResponse { get; set; }
        public long? QrsPharmacyId { get; set; }
    }
}