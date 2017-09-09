using System;
using System.Collections;
using System.Linq;

namespace Fornax.Net.Util.Archive.File
{

    public static partial class FileFormat
    {

        /// <summary>
        /// Plain-Text File Extension Handler for Fornax.Net.
        /// Default Plain Types include <see cref="Defaults"/>.
        /// </summary>
        /// <seealso cref="IEnumerable" />
        public class Plain
        {

            private Plain() { }

            #region Constants
            /// <summary>
            /// <c>Ansi</c>: Ansi Encoded Text Document file extension.
            /// </summary>
            public const string Ansi = @".ans";
            /// <summary>
            /// <c>Ascii</c>: Ascii Encoded Text Document file extension.
            /// </summary>
            public const string Ascii = @".ascii";
            /// <summary>
            /// <c>C</c>: C Program source document file extension.
            /// </summary>
            public const string C = @".c";
            /// <summary>
            /// <c>Csharp(Cs)</c>: C#/C-Sharp Program document file extension.
            /// </summary>
            public const string CSharp = @".cs";
            /// <summary>
            /// <c>Java</c>: Java Program document file extension.
            /// </summary>
            public const string Java = @".java";
            /// <summary>
            /// <c>Txt(Text)</c>: Plain Text Document file extension.
            /// </summary>
            public const string Txt = @".txt";
            #endregion

            /// <summary>
            /// All Plains
            /// </summary>
            private static string[] AllPlains = new string[PlainExtensions.Count];

            /// <summary>
            /// Gets the format category.
            /// </summary>
            /// <value>
            /// The format category.
            /// </value>
            public static FornaxFormat FormatCategory => FornaxFormat.Plain;

            /// <summary>
            /// Gets an array collection of a default Plain Text-File types supported by Fornax.Net.
            /// [.ans, .ascii, .c, .cs, .java, .txt].
            /// </summary>
            /// <value>
            /// The default Plain Types.
            /// </value>
            public static string[] Defaults => GetDefaultPlainTypes();

            /// <summary>
            /// Gets an array collection of all Plain text-file types existing in Fornax's database.
            /// </summary>
            /// <value>
            /// The default Plain Types.
            /// </value>
            public static string[] All => GetAllPlainTypes();

            /// <summary>
            /// Gets all Plain types.
            /// </summary>
            /// <returns></returns>
            private static string[] GetAllPlainTypes() {
                try {
                    PlainExtensions.CopyTo(AllPlains, 0);
                    Array.Sort(AllPlains);
                    return AllPlains;
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
                string str = string.Format(@"{0}: Plain Text-Files: Ans, Ascii, C, Cs, Java, Txt...", nameof(Plain));
                return base.ToString();
            }

            /// <summary>
            ///  Adds a valid Plain extension to fornax.net database.
            ///  Note: Fornax.net only allows any sort of plain text-files.<para></para>
            ///  Note: <seealso cref="FornaxFormatException"/> would be thrown at Extraction time if 
            ///  <paramref name="extension"/> is not supported.
            /// </summary>
            /// <param name="extension">The extension.</param>
            /// <returns>True if Database update succeeds, else: False.</returns>
            public static bool AddPlainType(string extension) {

                if (string.IsNullOrWhiteSpace(extension)) { return false; }
                ParseToExtensionFormat(ref extension);
                try {
                    if (PlainExtensions.Contains(extension)) { return false; }
                    PlainExtensions.Add(extension);
                    Fornax.Default.Save();
                    return true;

                } catch (Exception) { return false; }
            }

            /// <summary>
            /// Deletes a specific <see cref="Plain"/> type from the Fornax.net database.
            /// Default <c>Plain Types </c>cannot be deleted.
            /// For list of Default Plain Types see: <see cref="Defaults"/>.
            /// </summary>
            /// <param name="extension">The extension.</param>
            /// <returns>true if Deletion is successful, else : false</returns>
            public static bool DeletePlainType(string extension) {
                try {
                    if (string.IsNullOrWhiteSpace(extension) || !PlainExtensions.Contains(extension)) { return false; }
                    ParseToExtensionFormat(ref extension);
                    if (Defaults.Contains(extension)) { return false; }
                    PlainExtensions.Remove(extension);
                    Fornax.Default.Save();
                    return true;
                } catch (Exception) { return false; }
            }

            /// <summary>
            /// Gets the default Plain-Text file types.
            /// </summary>
            /// <returns></returns>
            private static string[] GetDefaultPlainTypes() => new[] { Ansi, Ascii, C, CSharp, Java, Txt };

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
