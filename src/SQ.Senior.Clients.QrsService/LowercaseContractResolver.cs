using Newtonsoft.Json.Serialization;

namespace SQ.Senior.Clients.QrsService {
    public class LowercaseContractResolver : DefaultContractResolver {
        protected override string ResolvePropertyName(string propertyName) {
            return propertyName.ToLower();
        }
    }
}