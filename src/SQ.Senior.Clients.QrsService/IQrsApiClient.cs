using System.Collections.Generic;
using System.Threading.Tasks;

namespace SQ.Senior.Clients.QrsService {
    public interface IQrsApiClient {
        Task<T> GetAsync<T>(string url, string userId) where T : class;
        Task<T> GetByIdAsync<T>(string url, int id, string userId) where T : class;
        Task<T> PostRequestAsync<T>(string url, string userId) where T : class;
        Task<T> PostRequestAsync<T>(string url, T request, string userId) where T : class;
        Task<TResponse> PostRequestAsync<TRequest, TResponse>(string url, TRequest request, string userId) where TRequest : class where TResponse : class;
        Task<T> PatchRequestAsync<T>(string url, int id, T request, string userId) where T : class;
        Task PutRequestAsync<T>(string url, T request, string userId) where T : class;
        Task DeleteRequestAsync(string url, int id, string userId);
    }
}
