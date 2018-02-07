using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoTracker
{
    //Reuseable extension methods
    public static class Extensions
    {
        /// <summary>
        /// Returns true if a string is a valid number
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsNumeric(this string s)
        {
            foreach (char c in s)
            {
                if (!char.IsDigit(c) && c != '.')
                {
                    return false;
                }
            }

            return true;
        }

        public static string StripDollarSign(this string s)
        {
            if (s.Contains('$'))
                return s.Replace("$", "");
            return s;
        }

        /// <summary>
        /// Converts a float value to a human readable monetary value
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string FloatToMonetary(this float s)
        {
            return "$" + s.ToString("#,##0.##");
        }

        /// <summary>
        /// Convert a float to a human readable percentage
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string FloatToPercent(this float s)
        {
            return s.ToString("#,##0.#") + "%";
        }

        /// <summary>
        /// Parses a float value from an input string
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static float ParseFloatFromString(this string s)
        {
            return float.Parse(s, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Convert a float to a single decimal point limited value, comma seperated
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string FloatToOneDecimal(this float s)
        {
            return s.ToString("#,##0.#");
        }

        public static string InsertCommasIntoDigitMonetary(this string s)
        {
            return "$" + Convert.ToDouble(s).ToString("#,##0");
        }

        public static string InsertCommasIntoDigit(this string s)
        {
            return string.Format("{0:n0}", s);
        }

        /// <summary>
        /// Converts a datetime value to it's UNIX equivalent
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static UInt64 DateTimeToUNIX(this DateTime s)
        {
            return (UInt64)s.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
        }
    }
}
