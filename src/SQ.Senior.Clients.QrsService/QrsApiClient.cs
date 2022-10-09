using Newtonsoft.Json;
using SQ.Senior.Clients.QrsService.Infrastructure.Services;
using System;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;

namespace SQ.Senior.Clients.QrsService {
    public class QrsApiClient : IQrsApiClient {
        private readonly HttpClient _httpClient;

        public QrsApiClient(HttpClient httpClient) {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<T> GetAsync<T>(string url, string userId) where T : class {
            try {
                T result = null;
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                await response.Content.ReadAsStringAsync().ContinueWith((Task<string> x) => {
                    if (x.IsFaulted)
                        if (x.Exception != null)
                            throw x.Exception;
                    result = JsonConvert.DeserializeObject<T>(x.Result);
                }).ConfigureAwait(false);

                return result;
            } catch (Exception ex) {
                ApiLogService.LogException(ex, userId);
                return null;
            }
        }

        public async Task<T> GetByIdAsync<T>(string apiUrl, int id, string userId) where T : class {
            try {
                T result = null;
                var comUrl = $"{apiUrl}/{id}";
                var response = await _httpClient.GetAsync(comUrl);
                response.EnsureSuccessStatusCode();
                await response.Content.ReadAsStringAsync().ContinueWith((Task<string> x) => {
                    if (x.IsFaulted)
                        if (x.Exception != null)
                            throw x.Exception;
                    result = JsonConvert.DeserializeObject<T>(x.Result);
                }).ConfigureAwait(false);

                return result;
            } catch (Exception ex) {
                ApiLogService.LogException(ex, userId);
                return null;
            }
        }

        public async Task<T> PostRequestAsync<T>(string apiUrl, T postObject, string userId) where T : class {
            try {
                T result = null;
                var response = await _httpClient.PostAsync(apiUrl, postObject, new JsonMediaTypeFormatter());
                response.EnsureSuccessStatusCode();
                await response.Content.ReadAsStringAsync().ContinueWith((Task<string> x) => {
                    if (x.IsFaulted)
                        if (x.Exception != null)
                            throw x.Exception;
                    result = JsonConvert.DeserializeObject<T>(x.Result);
                }).ConfigureAwait(false);

                return result;
            } catch (Exception ex) {
                ApiLogService.LogException(ex, userId);
                return null;
            }
        }

        public async Task<TResponse> PostRequestAsync<TRequest, TResponse>(string apiUrl, TRequest request, string userId) where TRequest : class where TResponse : class {
            try {
                TResponse result = null;
                var jsonString = JsonConvert.SerializeObject(request);
                var stringContent = new StringContent(JsonConvert.SerializeObject(request));
                var response = await _httpClient.PostAsync(apiUrl, stringContent, new JsonMediaTypeFormatter());
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

        public async Task<T> PostRequestAsync<T>(string apiUrl, string userId) where T : class {
            try {
                T result = null;
                var response = await _httpClient.PostAsync(apiUrl, null);
                response.EnsureSuccessStatusCode();
                await response.Content.ReadAsStringAsync().ContinueWith((Task<string> x) => {
                    if (x.IsFaulted)
                        if (x.Exception != null)
                            throw x.Exception;
                    result = JsonConvert.DeserializeObject<T>(x.Result);
                }).ConfigureAwait(false);

                return result;
            } catch (Exception ex) {
                ApiLogService.LogException(ex, userId);
                return null;
            }
        }

        public async Task<T> PatchRequestAsync<T>(string apiUrl, int id, T patchObject, string userId) where T : class {
            try {
                T result = null;
                var settings = new JsonSerializerSettings { ContractResolver = new LowercaseContractResolver() };
                HttpContent iContent = new StringContent(JsonConvert.SerializeObject(patchObject, Formatting.Indented, settings), Encoding.UTF8, "application/json");
                var method = new HttpMethod("PATCH");
                var comUrl = $"{apiUrl}/{id}";
                var request = new HttpRequestMessage(method, comUrl) {
                    Content = iContent
                };
                var response = await _httpClient.SendAsync(request);
                await response.Content.ReadAsStringAsync().ContinueWith((Task<string> x) => {
                    if (x.IsFaulted) {
                        if (x.Exception != null) {
                            throw x.Exception;
                        }
                    }
                    result = JsonConvert.DeserializeObject<T>(x.Result);
                }).ConfigureAwait(false);

                return result;
            } catch (Exception ex) {
                ApiLogService.LogException(ex, userId);
                return null;
            }
        }

        public async Task PutRequestAsync<T>(string apiUrl, T putObject, string userId) where T : class {
            try {
                var response = await _httpClient.PutAsync(apiUrl, putObject, new JsonMediaTypeFormatter());
                response.EnsureSuccessStatusCode();
            } catch (Exception ex) {
                ApiLogService.LogException(ex, userId);
            }
        }

        public async Task DeleteRequestAsync(string apiUrl, int id, string userId) {
            try {
                var comUrl = $"{apiUrl}/{id}";
                var response = await _httpClient.DeleteAsync(comUrl).ConfigureAwait(false);
                response.EnsureSuccessStatusCode();
            } catch (Exception ex) {
                ApiLogService.LogException(ex, userId);
            }
        }
    }
}
