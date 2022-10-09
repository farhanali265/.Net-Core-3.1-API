using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace SQ.Senior.Clients.FEMAService.Infrastructure.Services
{
    public class FEMAAPIWrapper<T> where T : class {
        public static async Task<T> Get(string url) {
            T result = null;
            using (var client = new HttpClient() { BaseAddress = new Uri(FEMAServiceConfig.FEMASettings.APIBaseURL) }) {
                var response = client.GetAsync(url).Result;
                response.EnsureSuccessStatusCode();
                await response.Content.ReadAsStringAsync().ContinueWith((Task<string> x) => {
                    if (x.IsFaulted)
                        if (x.Exception != null)
                            throw x.Exception;

                    result = JsonConvert.DeserializeObject<T>(x.Result);
                }).ConfigureAwait(false);
            }
            return result;
        }
    }
}
