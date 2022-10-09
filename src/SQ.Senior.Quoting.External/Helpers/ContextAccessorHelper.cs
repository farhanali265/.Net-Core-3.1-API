using Microsoft.AspNetCore.Http;

namespace SQ.Senior.Quoting.External.Helpers {
    public class ContextAccessorHelper {

        private static IHttpContextAccessor _httpContextAccessor;

        private static IHttpContextAccessor HttpContextAccessor {
            get {
                if (_httpContextAccessor is null)
                    _httpContextAccessor = new HttpContextAccessor();

                return _httpContextAccessor;
            }
        }
        public static void Configure(IHttpContextAccessor httpContextAccessor) {
            _httpContextAccessor = httpContextAccessor;
        }

        public static HttpContext HttpContext {
            get { return HttpContextAccessor.HttpContext; }
        }

        public static string GetRemoteIP {
            get { return HttpContext.Connection.RemoteIpAddress.ToString(); }
        }

        public static string GetScheme {
            get { return HttpContext.Request.Scheme; }
        }
    }
}
