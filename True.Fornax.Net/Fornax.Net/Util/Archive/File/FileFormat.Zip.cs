using System;
using System.Collections;
using System.Linq;

namespace Fornax.Net.Util.Archive.File
{

    public static partial class FileFormat
    {

        /// <summary>
        /// Compressed/Zipped document File Extension Handler for Fornax.Net
        /// Defaults Zip types include <see cref="Defaults"/>
        /// </summary>
        /// <seealso cref="System.Collections.IEnumerable" />
        public class Zip
        {

            private Zip() { }

            #region Constants
            /// <summary>
            /// <c>Gzip</c>: GNU Zipped/Compressed document file extension.
            /// </summary>
            public const string Gzip = @".gzip";
            /// <summary>
            /// <c>Rar</c>: Winrar Type compressed document file extension.
            /// </summary>
            public const string Rar = @".rar";
            /// <summary>
            /// <c>Zip</c>: Windows Zipped document(Folders) file extension.
            /// </summary>
            public const string _Zip = @".zip";
            #endregion

            /// <summary>
            /// All Zips
            /// </summary>
            private static string[] AllZip = new string[ZipExtensions.Count];

            /// <summary>
            /// Gets the format category.
            /// </summary>
            /// <value>
            /// The format category.
            /// </value>
            public static FornaxFormat FormatCategory => FornaxFormat.Zip;

            /// <summary>
            /// Gets an array collection of a default Zip-Document types supported by Fornax.Net.
            /// [.gzip, .rar, .zip].
            /// </summary>
            /// <value>
            /// The default Zip Types.
            /// </value>
            public static string[] Defaults => GetDefaultZipTypes();

            /// <summary>
            /// Gets an array collection of all Zip-Document types existing in Fornax's database.
            /// </summary>
            /// <value>
            /// The default Zip Types.
            /// </value>
            public static string[] All => GetAllZipTypes();

            /// <summary>
            /// Gets all Zip types.
            /// </summary>
            /// <returns></returns>
            private static string[] GetAllZipTypes() {
                try {
                    ZipExtensions.CopyTo(AllZip, 0);
                    Array.Sort(AllZip);
                    return AllZip;
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
                string str = string.Format(@"{0}: Ziped Documents: Gzip, Rar, Zip...", nameof(Zip));
                return base.ToString();
            }

            /// <summary>
            ///  Adds a valid Zip extension to fornax.net database.
            ///  <c>Note: Forfornax.net to accept any new <see cref="Zip"/> type, <paramref name="extension"/> 
            ///  must containat least one ['z'] e.g. .zip, .gzip , .bzip, .7zip , .tar.gz.
            /// </c>
            /// </summary>
            /// <param name="extension">The extension.</param>
            /// <returns>True if Database update succeeds, else: False.</returns>
            public static bool AddZipType(string extension) {

                if (string.IsNullOrWhiteSpace(extension)) { return false; }
                ParseToExtensionFormat(ref extension);
                try {
                    if (!extension.Contains("z") || ZipExtensions.Contains(extension)) { return false; }
                    ZipExtensions.Add(extension);
                    Fornax.Default.Save();
                    return true;

                } catch (Exception) { return false; }
            }

            /// <summary>
            /// Deletes a specific <see cref="Zip"/> type from the Fornax.net database.
            /// Default <c>Zip_Types </c>cannot be deleted.
            /// For list of Default Zip_Types see: <see cref="Defaults"/>.
            /// </summary>
            /// <param name="extension">The extension.</param>
            /// <returns>true if Deletion is successful, else : false</returns>
            public static bool DeleteZipType(string extension) {
                try {
                    if (string.IsNullOrWhiteSpace(extension) || !ZipExtensions.Contains(extension)) { return false; }
                    ParseToExtensionFormat(ref extension);
                    if (Defaults.Contains(extension)) { return false; }
                    ZipExtensions.Remove(extension);
                    Fornax.Default.Save();
                    return true;
                } catch (Exception) { return false; }
            }

            /// <summary>
            /// Gets the default Zip types.
            /// </summary>
            /// <returns></returns>
            private static string[] GetDefaultZipTypes() => new[] { Gzip, Rar, _Zip };

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
