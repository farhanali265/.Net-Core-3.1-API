using SQ.Senior.Clients.QrsService.Infrastructure.Constants;
using System;
using System.Configuration;

namespace SQ.Senior.Clients.DrxServices.Infrastructure.Services {
    public static class ApiLogService {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(ConfigurationManager.AppSettings["DrxLogger"]);

        public static string BuildExceptionMessage(Exception ex, string userId) {
            log4net.ThreadContext.Properties[Log4NetCustomFields.UserId] = userId;
            string error = $"{Environment.NewLine}Message:{Convert.ToString(ex.Message)}";
            error += $"{Environment.NewLine}Inner Exception:{Convert.ToString(ex.InnerException)}";
            error += $"{Environment.NewLine}Source:{ex.Source}";
            error += $"{Environment.NewLine}Stack Trace:{ex.StackTrace}";
            error += $"{Environment.NewLine}TargetSite:{ex.TargetSite}";
            return error;
        }

        public static void LogException(Exception ex, string userId) {
            Log.Error(BuildExceptionMessage(ex, userId), ex);
        }
    }
}
