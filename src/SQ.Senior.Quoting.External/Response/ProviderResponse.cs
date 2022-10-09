using SQ.Senior.Clients.QrsService.Models;
using SQ.Senior.Quoting.DatabaseAdapter.Models;
using System.Collections.Generic;

namespace SQ.Senior.Quoting.External.Response {
    public class ProviderResponse {
        public string Status { get; set; }
        public List<ApplicantProvider> ApplicantProviders { get; set; }
        public int ProviderId { get; set; }
        public QRSSearchProvider QRSSearchProvider { get; set; }
    }
}
