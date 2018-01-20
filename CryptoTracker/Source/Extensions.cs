using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoTracker
{
    public static class Extensions
    {
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

        public static UInt64 DateTimeToUNIX(this DateTime s)
        {
            return (UInt64)s.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
        }
    }
}
