using System;
using SQ.Senior.Clients.FEMAService.Models;
using System.Threading.Tasks;
using SQ.Senior.Clients.FEMAService.Enums;
using SQ.Senior.Clients.FEMAService.FEMAService.Enums;

namespace SQ.Senior.Clients.FEMAService.Infrastructure.Services {
    public class FEMAService : IFEMAService {
        /// <summary>
        /// Get disaster declarations summaries
        /// </summary>
        /// <param name="state">2 letter state alpha code</param>
        /// <param name="fips">fips county code</param>
        /// /// <param name="userId">Agent/User Id: for error logging</param>
        /// <param name="declarationDateFrom">optional parameter: declaration start date, it will get data with declaration from the beginning if not passed</param>
        /// <param name="declarationDateTo">optional parameter: declaration end date, it will get data with declaration to the max date if not passed</param>
        /// /// <param name="disasterCloseoutDateFrom">optional parameter: disaster Closeout start date, it will get data with disaster Closeout from the beginning if not passed</param>
        /// <param name="disasterCloseoutDateTo">optional parameter: disaster Closeout end date, it will get data with disaster Closeout to the max date if not passed</param>
        /// <returns>Full disaster declarations summaries response from FEMA based on given arguments</returns>
        public async Task<DisasterDeclarationsSummariesResponse> GetDisasterDeclarationsSummaries(FEMARequest femaRequest) {
            var response = new DisasterDeclarationsSummariesResponse();
            try {

                var url =
                    $"{FEMAServiceConfig.FEMASettings.APIVersion}{FEMAServiceConfig.FEMASettings.GetDisasterDeclarationsSummaries}?" +
                    $"$filter={GetUrlFilters(femaRequest)}";

                response = await FEMAAPIWrapper<DisasterDeclarationsSummariesResponse>.Get(url);
                response.ResponseStatus = true;

            } catch (Exception ex) {
                response.ResponseStatus = false;
                response.Exception = ex;
            }
            return response;
        }

        private string GetUrlFilters(FEMARequest femaRequest) {

            Func<Filters, Operators, object, bool, string> GetFilter = (filter, op, value, isFirstFilter) =>
                $"{(isFirstFilter ? string.Empty : $" and ")}{filter} {op} '{value}'";

            Func<Filters, Operators, DateTime?, string> GetDateFilter = (filter, op, date) =>
                $"{(date != null ? GetFilter(filter, op, date.Value.ToString("yyyy-MM-dd"), false) : string.Empty)}";

            var declarationDatesFilter =
              $"{GetDateFilter(Filters.declarationDate, Operators.ge, femaRequest.DeclarationDateFrom)}" +
              $"{GetDateFilter(Filters.declarationDate, Operators.le, femaRequest.DeclarationDateTo)}";

            var disasterCloseoutDatesFilter =
                $"{GetDateFilter(Filters.disasterCloseoutDate, Operators.ge, femaRequest.DisasterCloseoutDateFrom)}" +
              $"{GetDateFilter(Filters.disasterCloseoutDate, Operators.le, femaRequest.DisasterCloseoutDateTo)}";

            var urlFilters =
                    $"{GetFilter(Filters.state, Operators.eq, femaRequest.State, true)}" +
                    $"{GetFilter(Filters.fipsCountyCode, Operators.eq, femaRequest.Fips, false)}" +
                    $"{declarationDatesFilter}{disasterCloseoutDatesFilter}";

            return urlFilters;
        }
    }
}
