using SQ.Senior.Clients.QrsService.Models;
namespace SQ.Senior.Quoting.External.Response {
    public class ApplicantResponse {
        public string Status { get; set; }
        public QRSApplicants Applicant { get; set; }
    }
}
