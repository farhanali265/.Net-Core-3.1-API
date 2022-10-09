using SQ.Senior.Quoting.DatabaseAdapter.Abstractions;
using SQ.Senior.Quoting.DatabaseAdapter.Models;
using SQ.Senior.Quoting.External.Services.IServices;
using System;
using System.Threading.Tasks;

namespace SQ.Senior.Quoting.External.Services {
    public class AppLogService : IAppLogService {
        private readonly ISQSeniorExternalQuotingDbContext _dbContext;

        public AppLogService(ISQSeniorExternalQuotingDbContext dbContext) {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<string> InsertAppLogAsync(string loggerName, string message, string searchedZipCode = "", string viewName = "", string sessionId = "", string sessionData = "", string sessionUserName = "", string DrxApiXmlResponse = "", string UserId = "", string xmlSessionData = "", string DrxSessionId = "") {
            var nlog = new AppLog {
                Logger = loggerName,
                SearchedZipCode = searchedZipCode,
                ViewName = viewName,
                SessionId = sessionId,
                SessionData = sessionData,
                DrxApiResponse = DrxApiXmlResponse,
                SessionUserName = sessionUserName,
                UserId = UserId,
                Message = message,
                DrxApiRequest = xmlSessionData,
                DrxSessionId = DrxSessionId,
                Date = DateTime.UtcNow
            };
            var newAddedLog = await _dbContext.Set<AppLog>().AddAsync(nlog);
            await _dbContext.SaveChangesAsync();
            int nLogId = newAddedLog.Entity.ID;
            return nLogId.ToString();
        }
    }
}
