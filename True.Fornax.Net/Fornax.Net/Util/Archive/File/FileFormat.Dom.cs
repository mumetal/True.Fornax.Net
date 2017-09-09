using System;
using System.Collections;
using System.Linq;

namespace Fornax.Net.Util.Archive.File
{

    public static partial class FileFormat
    {
        /// <summary>
        /// DOM File Extension Handler for Fornax.Net.
        /// Default DOM Types include <see cref="Defaults"/>.
        /// </summary>
        /// <seealso cref="System.Collections.IEnumerable" />
        public class Dom
        {
            private Dom() { }

            #region Constants
            /// <summary>
            /// <c>Html</c>: Hypertext markup language support.
            /// </summary>
            public const string Html = @".html";
            /// <summary>
            /// <c>DHtml</c>: Script-Secured (Extensible) Hypertext Markup-Language file extension.
            /// </summary>
            public const string XHtml = @".xhtml";
            /// <summary>
            /// <c>DHtml</c>: Dynamic Hypertext Markup-Language file extension.
            /// </summary>
            public const string DHtml = @".dhtml";
            /// <summary>
            /// <c>SHtml</c>: Static Hypertext Markup-Language file extension.
            /// </summary>
            public const string SHtml = @".shtml";
            /// <summary>
            /// <c>Xml</c>: Extensible Markup-Language file extension.
            /// </summary>
            public const string Xml = @".xml";
            /// <summary>
            /// <c>Htm</c>: Web Browser(e.g. Chrome) <see cref="Html"/> type file extension.
            /// </summary>
            public const string Htm = @".htm";
            /// <summary>
            /// <c>Fxml</c>: Java-Fox Markup-Language 
            /// </summary>
            public const string Fxml = @".fxml";
            /// <summary>
            /// <c>Xaml</c>: Web Browser(e.g. Chrome) specific html type file extension.
            /// </summary>
            public const string Xaml = @".xaml";
            #endregion

            /// <summary>
            /// All doms
            /// </summary>
            private static string[] AllDoms = new string[DOMExtensions.Count];

            /// <summary>
            /// Gets the format category.
            /// </summary>
            /// <value>
            /// The format category.
            /// </value>
            public static FornaxFormat FormatCategory => FornaxFormat.DOM;

            /// <summary>
            /// Gets an array collection of a default DOM types supported by Fornax.Net.
            /// [.dhtml, .fxml, .htm, .html, .shtml, .xaml, .xhtml, .xml].
            /// </summary>
            /// <value>
            /// The default DOM Types.
            /// </value>
            public static string[] Defaults => GetDefaultDOMTypes();

            /// <summary>
            /// Gets an array collection of all DOM types existing in Fornax's database.
            /// </summary>
            /// <value>
            /// The default DOM Types.
            /// </value>
            public static string[] All => GetAllDOMTypes();

            /// <summary>
            /// Gets all DOM types.
            /// </summary>
            /// <returns></returns>
            private static string[] GetAllDOMTypes() {
                try {
                    DOMExtensions.CopyTo(AllDoms, 0);
                    Array.Sort(AllDoms);
                    return AllDoms;
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
                string str = string.Format(@"{0}: Document Object Models: Html{xhtml,dhtml,shtml,xhtml}, Xml{Fxml,Xaml}, Htm...", nameof(Dom));
                return base.ToString();
            }

            /// <summary>
            ///  Adds a valid DOM extension to fornax.net database.
            ///  Note: Fornax.net only allows users to add only explicitly defined Markup-Language DOM types 
            ///  i.e. <code>extension must Endwith("ml");</code> e.g. xml,html... 
            /// </summary>
            /// <param name="extension">The extension.</param>
            /// <returns>True if Database update succeeds, else: False.</returns>
            public static bool AddDomType(string extension) {

                if (string.IsNullOrWhiteSpace(extension) || !extension.EndsWith("ml")) { return false; }
                ParseToExtensionFormat(ref extension);
                try {
                    if (DOMExtensions.Contains(extension)) { return false; }
                    DOMExtensions.Add(extension);
                    Fornax.Default.Save();
                    return true;

                } catch (Exception) { return false; }
            }

            /// <summary>
            /// Deletes a specific <see cref="Dom"/> type from the Fornax.net database.
            /// Default <c>DOM Types </c>cannot be deleted.
            /// For list of Default DOM Types see: <see cref="Defaults"/>.
            /// </summary>
            /// <param name="extension">The extension.</param>
            /// <returns>true if Deletion is successful, else : false</returns>
            public static bool DeleteDomType(string extension) {
                try {
                    if (string.IsNullOrWhiteSpace(extension) || !DOMExtensions.Contains(extension)) { return false; }
                    ParseToExtensionFormat(ref extension);
                    if (Defaults.Contains(extension)) { return false; }
                    DOMExtensions.Remove(extension);
                    Fornax.Default.Save();
                    return true;
                } catch (Exception) { return false; }
            }

            /// <summary>
            /// Gets the default DOM types.
            /// </summary>
            /// <returns></returns>
            private static string[] GetDefaultDOMTypes() => new[] { DHtml, Fxml, Htm, Html, SHtml, Xaml, XHtml, Xml };

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
