using System;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using SQ.Senior.SpecialEnrollmentPeriods.Marx.Models;

namespace SQ.Senior.SpecialEnrollmentPeriods.Marx.Services {
    public class MarxApiClient : IMarxApiClient {
        public HttpClient _httpClient;

        public MarxApiClient(HttpClient httpClient) {
            _httpClient = httpClient;
        }

        public async Task<MarxSearchCasesResponse> SearchCases(MarxSearchCasesRequest marxSearchCasesRequest) {
            var marxResponse = new MarxSearchCasesResponse();
            try {
                var response = _httpClient.GetAsync(marxSearchCasesRequest.RequestUri).Result;
                response.EnsureSuccessStatusCode();

                await response.Content.ReadAsStringAsync().ContinueWith((Task<string> x) => {
                    if (x.IsFaulted)
                        if (x.Exception != null)
                            throw x.Exception;

                    marxResponse = JsonConvert.DeserializeObject<MarxSearchCasesResponse>(x.Result);
                }).ConfigureAwait(false);

                marxResponse.ResponseStatus = true;
            } catch (Exception ex) {
                marxResponse.Exception = ex;
                marxResponse.ResponseStatus = false;
            }
            return marxResponse;
        }
    }
}
