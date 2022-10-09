using System.Text.RegularExpressions;

namespace SQ.Senior.Clients.DrxServices.Infrastructure.Helpers {
    public static class DrxHelper {
        public static string RemoveSpecialCharacters(string validateString) {
            if (string.IsNullOrWhiteSpace(validateString))
                return string.Empty;
            return Regex.Replace(validateString, "[^a-zA-Z0-9_.]+", string.Empty, RegexOptions.Compiled);
        }
    }
}
