namespace SQ.Senior.Clients.DrxServices.Models {
    public class PharmacyDrxRecord {
        public string PharmacyPhone { get; set; }
        public string PharmacyID { get; set; } // Id has kept in Caps because of drx API requirment
        public int PharmacyRecordID { get; set; }
    }
}
