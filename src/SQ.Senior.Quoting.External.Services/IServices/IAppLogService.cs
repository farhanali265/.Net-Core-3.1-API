using System.Threading.Tasks;

namespace SQ.Senior.Quoting.External.Services.IServices {

    public interface IAppLogService {
        Task<string> InsertAppLogAsync(string loggerName, string message, string searchedZipCode = "", string viewName = "", string sessionId = "", string sessionData = "", string sessionUserName = "", string DrxApiXmlResponse = "", string UserId = "", string xmlSessionData = "", string DrxSessionId = "");
    }
}
