using SQ.Senior.SpecialEnrollmentPeriods.Marx.Models;
using System.Threading.Tasks;

namespace SQ.Senior.SpecialEnrollmentPeriods.Marx.Services {
    public class MarxService : IMarxService {
        IMarxApiClient _marxApiClient;

        public MarxService(IMarxApiClient marxApiClient) {
            _marxApiClient = marxApiClient;
        }

        public async Task<MarxSearchCasesResponse> SearchCases(MarxSearchCasesRequest marxSearchCasesRequest) {
            return await _marxApiClient.SearchCases(marxSearchCasesRequest);
        }
    }
}
