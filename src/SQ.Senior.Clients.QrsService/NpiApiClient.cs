using Newtonsoft.Json;
using SQ.Senior.Clients.QrsService.Infrastructure.Services;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SQ.Senior.Clients.QrsService {
    public class NpiApiClient : INpiApiClient {
        private readonly HttpClient _httpClient;

        public NpiApiClient(HttpClient httpClient) {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<TResponse> PostRequestAsync<TRequest, TResponse>(string url, TRequest request, string userId)
            where TRequest : class
            where TResponse : class {
            try {
                TResponse result = null;
                var jsonString = JsonConvert.SerializeObject(request);
                var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(url, httpContent);
                response.EnsureSuccessStatusCode();
                await response.Content.ReadAsStringAsync().ContinueWith((Task<string> x) => {
                    if (x.IsFaulted)
                        if (x.Exception != null)
                            throw x.Exception;
                    result = JsonConvert.DeserializeObject<TResponse>(x.Result);
                }).ConfigureAwait(false);

                return result;
            } catch (Exception ex) {
                ApiLogService.LogException(ex, userId);
                return null;
            }
        }
    }
}
