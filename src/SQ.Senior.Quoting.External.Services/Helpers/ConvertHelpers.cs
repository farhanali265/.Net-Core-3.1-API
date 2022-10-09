using System;
using System.Collections.Generic;

namespace SQ.Senior.Quoting.External.Services.Helpers {
    public class ConvertHelper {
        public static int GetIntValue(string sValue) {
            int returnVal = 0;
            try {
                returnVal = Int32.Parse(sValue);
            } catch {
                returnVal = 0;
            }
            return returnVal;
        }

        public static int GetIntValue(object obj) {
            int returnVal = 0;
            try {
                returnVal = Int32.Parse(Convert.ToString(obj));
            } catch {
                try {
                    returnVal = Convert.ToInt32(obj);
                } catch {
                    returnVal = 0;
                }
            }
            return returnVal;
        }

        public static int? GetNullAbleIntValue(string sValue) {
            int? returnVal = null;
            try {
                if (sValue != null) {
                    returnVal = Int32.Parse(sValue);
                }
            } catch {
                returnVal = null;
            }

            return returnVal;
        }

        public static int? GetNullAbleIntValue(object obj) {
            int? returnVal = null;
            try {
                if (obj != null) {
                    returnVal = Int32.Parse(obj.ToString());
                }
            } catch {
                try {
                    if (obj != null) {
                        returnVal = Convert.ToInt32(obj);
                    }
                } catch {
                    returnVal = null;
                }
            }
            return returnVal;
        }

        public static double GetDoubleValue(object obj) {
            double returnVal = 0;
            try {
                returnVal = double.Parse(obj.ToString());
            } catch {
                returnVal = 0;
            }
            return returnVal;
        }

        public static double? GetNullAbleDoubleValue(object obj) {
            double? returnVal = null;
            try {
                returnVal = double.Parse(obj.ToString());
            } catch {
                returnVal = null;
            }
            return returnVal;
        }

        public static decimal GetDecimalValue(object obj) {
            decimal returnVal = 0.00M;
            try {
                returnVal = decimal.Parse(obj.ToString());
            } catch {
                returnVal = 0.00M;
            }
            return returnVal;
        }

        public static decimal? GetNullAbleDecimalValue(object obj) {
            decimal? returnVal = null;
            try {
                returnVal = decimal.Parse(obj.ToString());
            } catch {
                returnVal = null;
            }
            return returnVal;
        }

        public static decimal? GetNullAbleDecimalValue(object obj, int DecimalPoints) {
            decimal? returnVal = null;
            try {
                returnVal = !string.IsNullOrEmpty(obj.ToString()) ? Math.Round(GetDecimalValue(obj), DecimalPoints) : decimal.Parse(obj.ToString());
            } catch {
                returnVal = null;
            }
            return returnVal;
        }

        public static decimal GetDecimalValue(object obj, int DecimalPoints) {
            decimal returnVal = GetDecimalValue(obj);
            try {
                returnVal = Math.Round(returnVal, DecimalPoints);
            } catch {
                returnVal = 0.0M;
            }
            return returnVal;
        }

        public static float GetFloatValue(object obj) {
            float returnVal = 0;
            try {
                returnVal = float.Parse(obj.ToString());
            } catch {
                returnVal = 0;
            }
            return returnVal;
        }


        public static Guid GetGuidValue(object obj) {
            Guid returnVal = Guid.Empty;
            try {
                returnVal = new Guid(obj.ToString());
            } catch {
                returnVal = Guid.Empty;
            }
            return returnVal;
        }

        public static string GetStringValue(object obj) {
            string returnVal = "";
            try {
                returnVal = Convert.ToString(obj);
            } catch {
                returnVal = "";
            }
            return returnVal;
        }

        public static bool GetBoolValue(string sValue) {
            bool returnVal = false;
            try {
                returnVal = bool.Parse(sValue);
            } catch {
                returnVal = false;
            }
            return returnVal;
        }

        public static bool GetBoolValue(object obj) {
            bool returnVal = false;
            try {
                returnVal = bool.Parse(Convert.ToString(obj));
            } catch {
                // Might be 1 / 0
                try {
                    if (GetIntValue(obj) == 1)
                        returnVal = true;
                    else
                        returnVal = false;
                } catch {
                    returnVal = false;
                }
            }
            return returnVal;
        }

        public static DateTime GetDateTimeValue(object obj) {
            DateTime returnVal = DateTime.MinValue;
            try {
                returnVal = DateTime.Parse(obj.ToString());
            } catch {
                try {
                    System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en-GB");
                    returnVal = DateTime.Parse(obj.ToString(), culture, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                } catch {
                    returnVal = DateTime.MinValue;
                }
            }
            return returnVal;
        }

        public static DateTime? GetNullableDateTimeValue(object val) {
            return val == null ? (DateTime?)null : GetDateTimeValue(val);
        }

        /// <summary>
        /// Convert enum type to its name and value pairs dictionory for binding with list o drop down list
        /// </summary>
        /// <param name="enumType"></param>
        /// <returns></returns>
        public static Dictionary<string, int> GetEnumNamesAndValues(Type enumType) {
            if (!enumType.IsEnum)
                throw new Exception("Invlaid type passed. Enum type is required.");

            Dictionary<string, int> namesvalues = new Dictionary<string, int>();
            string[] names = Enum.GetNames(enumType);
            foreach (string name in names) {
                string key = GetStringValue(Enum.Parse(enumType, name));
                int val = GetIntValue(Enum.Parse(enumType, name));
                namesvalues.Add(key, val);
            }
            return namesvalues;
        }
    }
}
