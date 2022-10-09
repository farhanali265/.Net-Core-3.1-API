using System;

namespace SQ.Senior.Quoting.DatabaseAdapter.Models {
    public class AppLog {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public string Logger { get; set; }
        public string Message { get; set; }
        public string ViewName { get; set; }
        public string SearchedZipCode { get; set; }
        public string SessionId { get; set; }
        public string SessionData { get; set; }
        public string SessionUserName { get; set; }
        public string UserId { get; set; }
        public string DrxApiResponse { get; set; }
        public string DrxApiRequest { get; set; }
        public string DrxSessionId { get; set; }
    }
}