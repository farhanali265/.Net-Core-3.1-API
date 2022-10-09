namespace SQ.Senior.Clients.QrsService.QRSSettingsModel {
    public class EmailSettings {
        public string Email { get; set; }
        public string Password { get; set; }
        public string SmtpServer { get; set; }
        public string GSuiteServiceAccount { get; set; }
        public string EmailBodySigninLink { get; set; }
        public string EmailBodyResetPasswordLink { get; set; }
        public string FromEmail { get; set; }
        public string EmailTemplates { get; set; }
    }
}
