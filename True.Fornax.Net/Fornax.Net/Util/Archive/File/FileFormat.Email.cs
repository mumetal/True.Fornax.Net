using System;
using System.Collections;
using System.Linq;

namespace Fornax.Net.Util.Archive.File
{
    public static partial class FileFormat
    {
        /// <summary>
        /// Email Document File Extension Handler for Fornax.Net
        /// Default email_Types include <see cref="Defaults"/>
        /// </summary>
        /// <seealso cref="IEnumerable" />
        public class Email
        {

            private Email() { }

            #region Constants
            /// <summary>
            /// <c>Eml</c>:IE Email document file extension.
            /// </summary>
            public const string Eml = @".eml";
            /// <summary>
            /// <c>Dbx</c>: Dbx document file extension.
            /// </summary>
            public const string Dbx = @".dbx";
            /// <summary>
            /// <c>Msg</c>: Email Message document file extension.
            /// </summary>
            public const string Msg = @".msg";
            /// <summary>
            /// <c>Pst</c>: Microsoft Outlook email document file extension.
            /// </summary>
            public const string Pst = @".pst";
            /// <summary>
            /// <c>Vcf</c>: Vcf document file extension.
            /// </summary>
            public const string Vcf = @".vcf";
            #endregion

            /// <summary>
            /// All Emails
            /// </summary>
            private static string[] AllEmail = new string[EmailExtensions.Count];

            /// <summary>
            /// Gets the format category.
            /// </summary>
            /// <value>
            /// The format category.
            /// </value>
            public static FornaxFormat FormatCategory => FornaxFormat.Email;

            /// <summary>
            /// Gets an array collection of a default Email-Document types supported by Fornax.Net.
            /// [.eml, .dbx, .msg, .pst, .vcf].
            /// </summary>
            /// <value>
            /// The default Email Types.
            /// </value>
            public static string[] Defaults => GetDefaultEmailTypes();

            /// <summary>
            /// Gets an array collection of all Email-Document types existing in Fornax's database.
            /// </summary>
            /// <value>
            /// The default Email Types.
            /// </value>
            public static string[] All => GetAllEmailTypes();

            /// <summary>
            /// Gets all Email types.
            /// </summary>
            /// <returns></returns>
            private static string[] GetAllEmailTypes() {
                try {
                    EmailExtensions.CopyTo(AllEmail, 0);
                    Array.Sort(AllEmail);
                    return AllEmail;
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
                string str = string.Format(@"{0}: Email Documents: Eml, Dbx, Msg, Pst, Vcf...", nameof(Email));
                return base.ToString();
            }

            /// <summary>
            ///  Adds a valid Email extension to fornax.net database.
            ///  Example of allowed Email types that are not present in Forax.net [ .ldif , .mht ]<para></para>
            ///  <c>NB: Email type addition to fornax does not state any specific validation rules for emails.</c>
            ///  <c>NB: Exception would be thrown at Extraction time if <paramref name="extension"/> is not of true email type.</c>
            /// </summary>
            /// <param name="extension">The extension.</param>
            /// <returns>True if Database update succeeds, else: False.</returns>
            public static bool AddEmailType(string extension) {

                if (string.IsNullOrWhiteSpace(extension)) { return false; }
                ParseToExtensionFormat(ref extension);
                try {
                    if (EmailExtensions.Contains(extension)) { return false; }
                    EmailExtensions.Add(extension);
                    Fornax.Default.Save();
                    return true;

                } catch (Exception) { return false; }
            }

            /// <summary>
            /// Deletes a specific <see cref="Email"/> type from the Fornax.net database.
            /// Default <c>Email_Types </c>cannot be deleted.
            /// For list of Default Email_Types see: <see cref="Defaults"/>.
            /// </summary>
            /// <param name="extension">The extension.</param>
            /// <returns>true if Deletion is successful, else : false</returns>
            public static bool DeleteEmailType(string extension) {
                try {
                    if (string.IsNullOrWhiteSpace(extension) || !EmailExtensions.Contains(extension)) { return false; }
                    ParseToExtensionFormat(ref extension);
                    if (Defaults.Contains(extension)) { return false; }
                    EmailExtensions.Remove(extension);
                    Fornax.Default.Save();
                    return true;
                } catch (Exception) { return false; }
            }

            /// <summary>
            /// Gets the default Email types.
            /// </summary>
            /// <returns></returns>
            private static string[] GetDefaultEmailTypes() => new[] { Eml, Dbx, Msg, Pst, Vcf };

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
