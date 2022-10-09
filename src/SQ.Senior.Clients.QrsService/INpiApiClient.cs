using System.Threading.Tasks;

namespace SQ.Senior.Clients.QrsService {
    public interface INpiApiClient {
        Task<TResponse> PostRequestAsync<TRequest, TResponse>(string url, TRequest request, string userId) 
            where TRequest : class 
            where TResponse : class;
    }
}
