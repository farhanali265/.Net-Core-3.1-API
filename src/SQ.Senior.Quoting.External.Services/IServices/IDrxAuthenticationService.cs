using System.Threading.Tasks;

namespace SQ.Senior.Quoting.External.Services.IServices {
    public interface IDrxAuthenticationService {
        Task<string> GetDrxAuthentication(string userId);
    }
}
