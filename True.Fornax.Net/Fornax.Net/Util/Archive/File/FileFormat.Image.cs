using System;
using System.Collections;
using System.Linq;

namespace Fornax.Net.Util.Archive.File
{

    public static partial class FileFormat {

        /// <summary>
        /// Image File Extension Handler for Fornax.Net.
        /// Default Image Types include <see cref="Defaults"/>.
        /// </summary>
        /// <seealso cref="IEnumerable" />
        public class Image {

            private Image() { }

            #region Constants
            /// <summary>
            /// <c>Gif</c>: Gif Image file extension.
            /// </summary>
            public const string Gif = @".gif";
            /// <summary>
            /// <c>Jpeg</c>: Jpeg Image file extension.
            /// </summary>
            public const string Jpeg = @".jpeg";
            /// <summary>
            /// <c>Jpg</c>: Jpg Image file extension. 
            /// </summary>
            public const string Jpg = @".jpg";
            /// <summary>
            /// <c>Png</c>: Png document file extension.
            /// </summary>
            public const string Png = @".png";
            /// <summary>
            /// <c>Tiff</c>:Media-Mime type image document file extension.
            /// </summary>
            public const string Tiff = @".tiff";
            #endregion

            /// <summary>
            /// All Images
            /// </summary>
            private static string[] AllImages = new string[ImageExtensions.Count];

            /// <summary>
            /// Gets the format category.
            /// </summary>
            /// <value>
            /// The format category.
            /// </value>
            public static FornaxFormat FormatCategory => FornaxFormat.Image;

            /// <summary>
            /// Gets an array collection of a default Image types supported by Fornax.Net.
            /// [.gif, .jpeg, .jpg, .png, .tiff]
            /// </summary>
            /// <value>
            /// The default Image Types.
            /// </value>
            public static string[] Defaults => GetDefaultImageTypes();

            /// <summary>
            /// Gets an array collection of all Image types existing in Fornax's database.
            /// </summary>
            /// <value>
            /// The default Image Types.
            /// </value>
            public static string[] All => GetAllImageTypes();

            /// <summary>
            /// Gets all Image types.
            /// </summary>
            /// <returns></returns>
            private static string[] GetAllImageTypes() {
                try {
                    ImageExtensions.CopyTo(AllImages, 0);
                    Array.Sort(AllImages);
                    return AllImages;
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
                string str = string.Format(@"{0}: Image Files : Gif, Jpeg, Jpg, Png, Tiff...", nameof(Image));
                return base.ToString();
            }

            /// <summary>
            ///  Adds a valid Image extension to fornax.net database.
            ///  Note: No Restrictions , Excpetion would be thrown at Extraction time if 
            ///  <paramref name="extension"/> is unrecognized by image-mime extractor.
            /// </summary>
            /// <param name="extension">The extension.</param>
            /// <returns>True if Database update succeeds, else: False.</returns>
            public static bool AddImageType(string extension) {

                if (string.IsNullOrWhiteSpace(extension)) { return false; }
                ParseToExtensionFormat(ref extension);
                try {
                    if (ImageExtensions.Contains(extension)) { return false; }
                    ImageExtensions.Add(extension);
                    Fornax.Default.Save();
                    return true;

                } catch (Exception) { return false; }
            }

            /// <summary>
            /// Deletes a specific <see cref="Image"/> type from the Fornax.net database.
            /// Default <c>Image Types </c>cannot be deleted.
            /// For list of Default Image Types see: <see cref="Defaults"/>.
            /// </summary>
            /// <param name="extension">The extension.</param>
            /// <returns>true if Deletion is successful, else : false</returns>
            public static bool DeleteImageType(string extension) {
                try {
                    if (string.IsNullOrWhiteSpace(extension) || !ImageExtensions.Contains(extension)) { return false; }
                    ParseToExtensionFormat(ref extension);
                    if (Defaults.Contains(extension)) { return false; }
                    ImageExtensions.Remove(extension);
                    Fornax.Default.Save();
                    return true;
                } catch (Exception) { return false; }
            }

            /// <summary>
            /// Gets the default Image types.
            /// </summary>
            /// <returns></returns>
            private static string[] GetDefaultImageTypes() => new[] {Gif,Jpeg,Jpg,Png,Tiff};

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
