using System;
using System.Collections;
using System.Linq;

namespace Fornax.Net.Util.Archive.File
{

    public static partial class FileFormat {

        /// <summary>
        /// Media File Extension Handler for Fornax.Net.
        /// Default Media Types include <see cref="Defaults"/>.
        /// </summary>
        /// <seealso cref="System.Collections.IEnumerable" />
        public class Media {

            private Media() { }

            #region Constants
            /// <summary>
            /// <c>Mp3</c>: Mpeg-3. Audio Media file extension.
            /// </summary>
            public const string Mp3 = @".mp3";
            /// <summary>
            /// <c>Mp4</c>: Mpeg-4 (Mp4) Vedio Media file extension.
            /// </summary>
            public const string Mp4 = @".mp4";
            #endregion

            /// <summary>
            /// All Medias
            /// </summary>
            private static string[] AllMedias = new string[MediaExtensions.Count];

            /// <summary>
            /// Gets the format category.
            /// </summary>
            /// <value>
            /// The format category.
            /// </value>
            public static FornaxFormat FormatCategory => FornaxFormat.Media;

            /// <summary>
            /// Gets an array collection of a default Media types supported by Fornax.Net.
            /// [.mp3, .mp4].
            /// </summary>
            /// <value>
            /// The default Media Types.
            /// </value>
            public static string[] Defaults => GetDefaultMediaTypes();

            /// <summary>
            /// Gets an array collection of all Media types existing in Fornax's database.
            /// </summary>
            /// <value>
            /// The default Media Types.
            /// </value>
            public static string[] All => GetAllMediaTypes();

            /// <summary>
            /// Gets all Media types.
            /// </summary>
            /// <returns></returns>
            private static string[] GetAllMediaTypes() {
                try {
                    MediaExtensions.CopyTo(AllMedias, 0);
                    Array.Sort(AllMedias);
                    return AllMedias;
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
                string str = string.Format(@"{0}:Meda Types Mp4. Mp3...", nameof(Media));
                return base.ToString();
            }

            /// <summary>
            ///  <para>Adds a valid Media extension to fornax.net database.</para>
            ///  Note: No Restrictions , Excpetion would be thrown at Extraction time if 
            ///  <paramref name="extension"/> is unrecognized by media-mime extractor.
            /// </summary>
            /// <param name="extension">The extension.</param>
            /// <returns>True if Database update succeeds, else: False.</returns>
            public static bool AddMediaType(string extension) {

                if (string.IsNullOrWhiteSpace(extension)) { return false; }
                ParseToExtensionFormat(ref extension);
                try {
                    if (MediaExtensions.Contains(extension)) { return false; }
                    MediaExtensions.Add(extension);
                    Fornax.Default.Save();
                    return true;

                } catch (Exception) { return false; }
            }

            /// <summary>
            /// Deletes a specific <see cref="Media"/> type from the Fornax.net database.
            /// Default <c>Media Types </c>cannot be deleted.
            /// For list of Default Media Types see: <see cref="Defaults"/>.
            /// </summary>
            /// <param name="extension">The extension.</param>
            /// <returns>true if Deletion is successful, else : false</returns>
            public static bool DeleteMediaType(string extension) {
                try {
                    if (string.IsNullOrWhiteSpace(extension) || !MediaExtensions.Contains(extension)) { return false; }
                    ParseToExtensionFormat(ref extension);
                    if (Defaults.Contains(extension)) { return false; }
                    MediaExtensions.Remove(extension);
                    Fornax.Default.Save();
                    return true;
                } catch (Exception) { return false; }
            }

            /// <summary>
            /// Gets the default Media types.
            /// </summary>
            /// <returns></returns>
            private static string[] GetDefaultMediaTypes() => new[] {Mp3,Mp4};

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
