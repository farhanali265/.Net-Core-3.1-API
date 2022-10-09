using SQ.Senior.SpecialEnrollmentPeriods.Marx.Models;
using System.Threading.Tasks;

namespace SQ.Senior.SpecialEnrollmentPeriods.Marx.Services {
    public interface IMarxService {
        Task<MarxSearchCasesResponse> SearchCases(MarxSearchCasesRequest marxSearchCasesRequest);
    }
}
