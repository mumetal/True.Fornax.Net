using System;
using System.Collections;
using System.Linq;

namespace Fornax.Net.Util.Archive.File
{

    public static partial class FileFormat {

        /// <summary>
        /// Web File Extension Handler for Fornax.Net.
        /// Default Web Types include <see cref="Defaults"/>.
        /// </summary>
        /// <seealso cref="IEnumerable" />
        public class Web {

            private Web() { }

            #region Constants
            /// <summary>
            /// <c>Asp</c>: Microsoft(Old) Asp.Net Web file extension.
            /// </summary>
            public const string Asp = @".asp";
            /// <summary>
            /// <c>Aspx</c>: Microsoft(New) Asp.Net Web file extension.
            /// </summary>
            public const string Aspx = @".aspx";
            #endregion

            /// <summary>
            /// All Webs
            /// </summary>
            private static string[] AllWebs = new string[WebExtensions.Count];

            /// <summary>
            /// Gets the format category.
            /// </summary>
            /// <value>
            /// The format category.
            /// </value>
            public static FornaxFormat FormatCategory => FornaxFormat.Web;

            /// <summary>
            /// Gets an array collection of a default Web types supported by Fornax.Net.
            /// [.asp, .aspx].
            /// </summary>
            /// <value>
            /// The default Web Types.
            /// </value>
            public static string[] Defaults => GetDefaultWebTypes();

            /// <summary>
            /// Gets an array collection of all Web types existing in Fornax's database.
            /// </summary>
            /// <value>
            /// The default Web Types.
            /// </value>
            public static string[] All => GetAllWebTypes();

            /// <summary>
            /// Gets all Web types.
            /// </summary>
            /// <returns></returns>
            private static string[] GetAllWebTypes() {
                try {
                    WebExtensions.CopyTo(AllWebs, 0);
                    Array.Sort(AllWebs);
                    return AllWebs;
                } catch (Exception) {
                    return Defaults;
                }
            }

            /// <summary>
            /// Returns a <see cref="string" /> that represents this instance.
            /// </summary>
            /// <returns>
            /// A <see cref="string" /> that represents this instance.
            /// </returns>
            public override string ToString() {
                string str = string.Format(@"{0}:Web File Types: Asp, Aspx...", nameof(Web));
                return base.ToString();
            }

            /// <summary>
            ///  Adds a valid Web extension to fornax.net database.
            /// </summary>
            /// <param name="extension">The extension.</param>
            /// <returns>True if Database update succeeds, else: False.</returns>
            [Obsolete("Addition of custom/user Web file types is not Supported by yet Fornax.net",true)]
            public static bool AddWebType(string extension) {

                if (string.IsNullOrWhiteSpace(extension)) { return false; }
                ParseToExtensionFormat(ref extension);
                try {
                    if (WebExtensions.Contains(extension)) { return false; }
                    WebExtensions.Add(extension);
                    Fornax.Default.Save();
                    return true;

                } catch (Exception) { return false; }
            }

            /// <summary>
            /// Deletes a specific <see cref="Web"/> type from the Fornax.net database.
            /// Default <c>Web Types </c>cannot be deleted.
            /// For list of Default Web Types see: <see cref="Defaults"/>.
            /// </summary>
            /// <param name="extension">The extension.</param>
            /// <returns>true if Deletion is successful, else : false</returns>
            [Obsolete("Deletion of Web Types is not yet Supported by Fornax.net",true)]
            public static bool DeleteWebType(string extension) {
                try {
                    if (string.IsNullOrWhiteSpace(extension) || !WebExtensions.Contains(extension)) { return false; }
                    ParseToExtensionFormat(ref extension);
                    if (Defaults.Contains(extension)) { return false; }
                    WebExtensions.Remove(extension);
                    Fornax.Default.Save();
                    return true;
                } catch (Exception) { return false; }
            }

            /// <summary>
            /// Gets the default Web types.
            /// </summary>
            /// <returns></returns>
            private static string[] GetDefaultWebTypes() => new[] { Asp, Aspx};

            /// <summary>
            /// Returns an enumerator that iterates through <see cref="All"/>
            /// </summary>
            /// <returns>
            /// An <see cref="T: System.Collections.IEnumerator" /> object that can be used to iterate through the collection.
            /// </returns>
            public static IEnumerator GetEnumerator() {
                return All.ToList().GetEnumerator();
            }
        }
    }
}
