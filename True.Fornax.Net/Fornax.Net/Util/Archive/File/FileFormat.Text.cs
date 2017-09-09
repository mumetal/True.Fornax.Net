using System;
using System.Collections;
using System.Linq;

namespace Fornax.Net.Util.Archive.File
{

    public static partial class FileFormat
    {
        /// <summary>
        /// Text-Document File Extension Handler for Fornax.Net
        /// Default Text_Types include <see cref="Defaults"/>
        /// </summary>
        /// <seealso cref="System.Collections.IEnumerable" />
        public class Text
        {
            private Text() { }

            #region Constants
            /// <summary>
            /// <c>Csv</c>: Comma separated values file extension.
            /// </summary>
            public const string Csv = @".csv";
            /// <summary>
            /// <c>Doc</c>: Microsoft Word Document<c>(97 - 2003)</c> file extension.
            /// </summary>
            public const string Doc = @".doc";
            /// <summary>
            /// <c>Docx</c>: Microsoft Word document<c>(2007 - 2017)</c> file extension.
            /// </summary>
            public const string Docx = @".docx";
            /// <summary>
            /// <c>Epub</c>:Portable E-book file extension.
            /// </summary>
            public const string Epub = @".epub";
            /// <summary>
            /// <c>Pdf</c>: Portable Document format<c>(by Adobe)</c> file extension.
            /// </summary>
            public const string Pdf = @".pdf";
            /// <summary>
            /// <c>Rtf</c>: Rich Text format<c>(Microsoft Wordpad)</c> document file extension.
            /// </summary>
            public const string Rtf = @".rtf";
            #endregion

            /// <summary>
            /// All texts
            /// </summary>
            private static string[] AllText = new string[TextExtensions.Count];

            /// <summary>
            /// Gets the format category.
            /// </summary>
            /// <value>
            /// The format category.
            /// </value>
            public static FornaxFormat FormatCategory => FornaxFormat.Text;

            /// <summary>
            /// Gets an array collection of a default Text-Document types supported by Fornax.Net.
            /// [.csv, .doc, .docx, .epub, .pdf, .rtf].
            /// </summary>
            /// <value>
            /// The default Text Types.
            /// </value>
            public static string[] Defaults => GetDefaultTextTypes();

            /// <summary>
            /// Gets an array collection of all Text-Document types existing in Fornax's database.
            /// </summary>
            /// <value>
            /// The default Text Types.
            /// </value>
            public static string[] All => GetAllTextTypes();

            /// <summary>
            /// Gets all Text types.
            /// </summary>
            /// <returns></returns>
            private static string[] GetAllTextTypes() {
                try {
                    TextExtensions.CopyTo(AllText, 0);
                    Array.Sort(AllText);
                    return AllText;
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
                string str = string.Format(@"{0}: Texted Documents: Csv , Doc , Docx, Epub, Pdf , Rtf...", nameof(Text));
                return base.ToString();
            }

            /// <summary>
            ///  Adds a valid Text extension to fornax.net database.
            ///  Note: Fornax.net only allows users to add only strongly-typed commercial Text_Document Types.  
            ///  i.e. <code>extension must not Contain a numeric value </code> e.g. .w3, .bz2
            /// </summary>
            /// <param name="extension">The extension.</param>
            /// <returns>True if Database update succeeds, else: False.</returns>
            public static bool AddTextType(string extension) {

                if (string.IsNullOrWhiteSpace(extension)) { return false; }
                ParseToExtensionFormat(ref extension);
                try {
                    if (extension.ContainsNumeric() || TextExtensions.Contains(extension)) { return false; }
                    TextExtensions.Add(extension);
                    Fornax.Default.Save();
                    return true;

                } catch (Exception) { return false; }
            }

            /// <summary>
            /// Deletes a specific <see cref="Text"/> type from the Fornax.net database.
            /// Default <c>Text_Types </c>cannot be deleted.
            /// For list of Default Text_Types see: <see cref="Defaults"/>.
            /// </summary>
            /// <param name="extension">The extension.</param>
            /// <returns>true if Deletion is successful, else : false</returns>
            public static bool DeleteTextType(string extension) {
                try {
                    if (string.IsNullOrWhiteSpace(extension) || !TextExtensions.Contains(extension)) { return false; }
                    ParseToExtensionFormat(ref extension);
                    if (Defaults.Contains(extension)) { return false; }
                    TextExtensions.Remove(extension);
                    Fornax.Default.Save();
                    return true;
                } catch (Exception) { return false; }
            }

            /// <summary>
            /// Gets the default Text types.
            /// </summary>
            /// <returns></returns>
            private static string[] GetDefaultTextTypes() => new[] { Csv, Doc, Docx, Epub, Pdf, Rtf };

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


