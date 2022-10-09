using System;

namespace SQ.Senior.Clients.QrsService.QRSSettingsModel {
    public class OtherSettings {
        public string CMSPending { get; set; }
        public string LastUpdated { get; set; }
        public string EnableMPNAWNOverride { get; set; }
        public string EnableEnrollEmail { get; set; }
        public string EncryptionKey { get; set; }
        public string SEQLogger { get; set; }
        public string Environment { get; set; }
        public string MedicareHelplineHostName { get; set; }
        public string MobileRedirectURLSQ { get; set; }
        public string MobileRedirectURLMHL { get; set; }
        public string EnrollPhoneNumber { get; set; }
    }
}
