using SQ.Senior.Clients.QrsService.Infrastructure.Constants;
using SQ.Senior.Clients.QrsService.Models;
using System;
using System.Text;

namespace SQ.Senior.Clients.QrsService.Infrastructure.Helpers {
    public static class QRSHelper {
        private static readonly Random _random;

        static QRSHelper() {
            _random = new Random();
        }

        public static string GetCommunicationType(string propertyName) {
            if (string.IsNullOrEmpty(propertyName)) {
                return string.Empty;
            }

            if (propertyName.Contains("AddressLine1") ||
                propertyName.Contains("AddressLine2") ||
                propertyName.Contains("City") ||
                propertyName.Contains("County") ||
                propertyName.Contains("State") ||
                propertyName.Contains("StateId") ||
                propertyName.Contains("ZipCode") ||
                propertyName.Contains("FipsCode")
                ) {
                return CommunicationTypes.AddressType;
            }

            if (propertyName.Contains("PhoneNumber")) {
                return CommunicationTypes.PhoneType;
            }

            if (propertyName.Contains("EmailAddress")) {
                return CommunicationTypes.EmailType;
            }
            return string.Empty;
        }

        public static bool ValidateValue(object value) {
            if (value == null)
                return false;

            var convertedValue = Convert.ToString(value);
            if (string.IsNullOrEmpty(convertedValue) || convertedValue.Equals("0"))
                return false;
            return true;
        }

        public static string GetSourceType(BrandType brandType) {
            switch (brandType) {
                case BrandType.SelectQuote:
                    return SourceTypes.SEQ;
                case BrandType.MedicareHelpline:
                    return SourceTypes.MHL_CQE;
                default:
                    throw new InvalidOperationException(nameof(brandType));
            }
        }

        public static string RandomString(int size, bool lowerCase = false) {
            var builder = new StringBuilder(size);
            Random _random = new Random();

            // Unicode/ASCII Letters are divided into two blocks
            // (Letters 65–90 / 97–122):
            // The first group containing the uppercase letters and
            // the second group containing the lowercase.

            // char is a single Unicode character
            char offset = lowerCase ? 'a' : 'A';
            const int lettersOffset = 26; // A...Z or a..z: length = 26

            for (var i = 0; i < size; i++) {
                var @char = (char)_random.Next(offset, offset + lettersOffset);
                builder.Append(@char);
            }
            return lowerCase ? builder.ToString().ToLower() : builder.ToString();
        }

        public static int RandomNumber(int min, int max) {
            return _random.Next(min, max);
        }

        public static string GetRandomPassword() {
            var passwordBuilder = new StringBuilder();
            // 4-Letters lower case
            passwordBuilder.Append(RandomString(4, true));
            // 4-Digits between 1000 and 9999
            passwordBuilder.Append(RandomNumber(1000, 9999));
            // 2-Letters upper case
            passwordBuilder.Append(RandomString(2));
            return passwordBuilder.ToString();
        }
    }
}
