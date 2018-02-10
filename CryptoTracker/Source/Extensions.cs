using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        public static float PercentToFloat(this string s)
        {
            return (float)Convert.ToDouble(s.TrimEnd('%'));
        }

        public static string InsertCommasIntoDigitMonetary(this string s)
        {
            return "$" + Convert.ToDouble(s).ToString("#,##0");
        }

        public static string InsertCommasIntoDigit(this string s)
        {
            return string.Format("{0:n0}", s);
        }

        public static int SelectedIndex(this ListView listView)
        {
            if (listView.SelectedIndices.Count > 0)
                return listView.SelectedIndices[0];
            else
                return 0;
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

        public static DateTime UnixTimeStampToDateTime(this double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }
    }
}
