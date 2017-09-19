using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fornax.Net.Util.Text
{
    public static class StringExts
    {
        public static bool IsEmptyorNull(this string @string) {
            return (@string.Equals(String.Empty) || @string == null);
        }

        public static byte[] GetBytes(this string text, Encoding encoding) {
            return encoding.GetBytes(text);
        }

        public static byte[] GetByte(this string text) {
            return Encoding.Default.GetBytes(text);
        }

        public static int CompareToOrdinal(this string str, string value) {
            return string.CompareOrdinal(str, value);
        }

        public static int CodepointAt(this string str, int index) => 1;//implement after Character class.;
    }
}
