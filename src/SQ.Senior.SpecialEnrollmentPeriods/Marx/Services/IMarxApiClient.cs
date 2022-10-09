using System.Threading.Tasks;
using SQ.Senior.SpecialEnrollmentPeriods.Marx.Models;

namespace SQ.Senior.SpecialEnrollmentPeriods.Marx.Services {
    public interface IMarxApiClient {
        Task<MarxSearchCasesResponse> SearchCases(MarxSearchCasesRequest marxSearchCasesRequest);
    }
}
