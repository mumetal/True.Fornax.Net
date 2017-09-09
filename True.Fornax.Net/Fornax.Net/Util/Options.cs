using System.Linq;

namespace Fornax.Net.Util
{
    /// <summary>
    /// Extension Method provider for Fornax.Net.
    /// <!--Example-->Extension methods Provided for [<see cref="string"/>, <see cref="class"/>, <see cref="Array"/>, <see cref="Dictionary{TKey, TValue}"/>...]
    /// </summary>
    public static class Options
    {



        #region String Extensions

        /// <summary>
        /// Determines whether <see cref="string"/> <paramref name="@string"/> contains a numeric value.
        /// </summary>
        /// <param name="string">The string.</param>
        /// <returns>
        ///   <c>true</c> if the specified string contains numeric; otherwise, <c>false</c>.
        /// </returns>
        public static bool ContainsNumeric(this string @string) {
            for (int i = 0; i < @string.Length; i++) {
                if (char.IsNumber(@string[i]) || char.IsDigit(@string[i])) {
                    return true;
                }
            }
            return false;
        }

        #endregion

        static Options() {

        }
    }
}
