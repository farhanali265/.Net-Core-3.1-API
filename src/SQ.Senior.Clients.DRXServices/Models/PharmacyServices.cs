namespace SQ.Senior.Clients.DrxServices.Models {

    public class PharmacyServices {
        public bool Has24hrService { get; set; }
        public bool HasCompounding { get; set; }
        public bool HasDelivery { get; set; }
        public bool HasDriveup { get; set; }
        public bool HasDurableEquipment { get; set; }
        public bool HasEPrescriptions { get; set; }
        public bool HasHandicapAccess { get; set; }
        public bool IsHomeInfusion { get; set; }
        public bool IsLongTermCare { get; set; }
    }
}
