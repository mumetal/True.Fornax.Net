using System;
using System.Collections;
using System.Linq;

namespace Fornax.Net.Util.Archive.File
{

    public static partial class FileFormat
    {

        /// <summary>
        /// SpreadSheet File Extension Handler for Fornax.Net.
        /// Default Sheet Types include <see cref="Defaults"/>.
        /// </summary>
        /// <seealso cref="IEnumerable" />
        public class SpreadSheet
        {

            private SpreadSheet() { }
            
            #region Constants
            /// <summary>
            /// <c>Xl</c>: Microsoft Excel, Spreadsheet template file extension.
            /// </summary>
            public const string Xl = @".xl";
            /// <summary>
            /// <c>Xlr</c>: Xlr Sheet file extension.
            /// </summary>
            public const string Xlr = @".xlr";
            /// <summary>
            /// <c>Xls</c>: Microsoft Excel(97-2003), Spreadsheet document file extension.
            /// </summary>
            public const string Xls = @".xls";
            /// <summary>
            /// <c>Xlsm</c>: Xlsm Sheet document File Extension.
            /// </summary>
            public const string Xlsm = @".xlsm";
            /// <summary>
            /// <c>Xlsx</c>: Microsoft Excel(2007-2017), Spreadsheet document file extension.
            /// </summary>
            public const string Xlsx = @".xlsx";
            #endregion

            /// <summary>
            /// All Sheets
            /// </summary>
            private static string[] AllSheets = new string[SheetExtensions.Count];

            /// <summary>
            /// Gets the format category.
            /// </summary>
            /// <value>
            /// The format category.
            /// </value>
            public static FornaxFormat FormatCategory => FornaxFormat.SpreadSheet;

            /// <summary>
            /// Gets an array collection of a default SpreadSheet document types supported by Fornax.Net.
            /// [.xl, .xlr, .xls, .xlsm, .xlsx].
            /// </summary>
            /// <value>
            /// The default Sheet Types.
            /// </value>
            public static string[] Defaults => GetDefaultSheetTypes();

            /// <summary>
            /// Gets an array collection of all SpreadSheet document/template types existing in Fornax's database.
            /// </summary>
            /// <value>
            /// The default Sheet Types.
            /// </value>
            public static string[] All => GetAllSheetTypes();

            /// <summary>
            /// Gets all Sheet types.
            /// </summary>
            /// <returns></returns>
            private static string[] GetAllSheetTypes() {
                try {
                    SheetExtensions.CopyTo(AllSheets, 0);
                    Array.Sort(AllSheets);
                    return AllSheets;
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
                string str = string.Format(@"{0}: Spread-Sheet Documents: Xl, Xlr, Xls, Xlsm, Xlsx, ...", nameof(SpreadSheet));
                return base.ToString();
            }

            /// <summary>
            ///  Adds a valid SpreadSheet extension to fornax.net database.
            ///  Note: Fornax.net only allows users to add only Microsoft Excel suppported Spread-Sheet types. 
            ///  i.e. <code>extension must Startwith("x");</code> e.g. xlsb,xltx,xls ... 
            /// </summary>
            /// <param name="extension">The extension.</param>
            /// <returns>True if Database update succeeds, else: False.</returns>
            public static bool AddSheetType(string extension) {

                if (string.IsNullOrWhiteSpace(extension) || !extension.StartsWith("x")) { return false; }
                ParseToExtensionFormat(ref extension);
                try {
                    if (SheetExtensions.Contains(extension)) { return false; }
                    SheetExtensions.Add(extension);
                    Fornax.Default.Save();
                    return true;

                } catch (Exception) { return false; }
            }

            /// <summary>
            /// Deletes a specific <see cref="SpreadSheet"/> type from the Fornax.net database.
            /// Default <c>Sheet Types </c>cannot be deleted.
            /// For list of Default Sheet Types see: <see cref="Defaults"/>.
            /// </summary>
            /// <param name="extension">The extension.</param>
            /// <returns>true if Deletion is successful, else : false</returns>
            public static bool DeleteSheetType(string extension) {
                try {
                    if (string.IsNullOrWhiteSpace(extension) || !SheetExtensions.Contains(extension)) { return false; }
                    ParseToExtensionFormat(ref extension);
                    if (Defaults.Contains(extension)) { return false; }
                    SheetExtensions.Remove(extension);
                    Fornax.Default.Save();
                    return true;
                } catch (Exception) { return false; }
            }

            /// <summary>
            /// Gets the default Sheet types.
            /// </summary>
            /// <returns></returns>
            private static string[] GetDefaultSheetTypes() => new[] {Xl, Xlr, Xls, Xlsm, Xlsx };


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
