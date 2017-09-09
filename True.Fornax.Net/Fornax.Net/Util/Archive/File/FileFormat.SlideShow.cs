using System;
using System.Collections;
using System.Linq;

namespace Fornax.Net.Util.Archive.File
{

    public static partial class FileFormat {

        /// <summary>
        /// SlideShow Document File Extension Handler for Fornax.Net.
        /// Default Slide Types include <see cref="Defaults"/>.
        /// </summary>
        /// <seealso cref="IEnumerable" />
        public class SlideShow { 

            private SlideShow() { }

            #region Constants
            /// <summary>
            /// <c>Ppt</c>: Microsoft PowerPoint(97-2003) Slide-Show document file extension.
            /// </summary>
            public const string Ppt = @".ppt";
            /// <summary>
            /// <c>Pptx</c>: Microsoft PowerPoint(2007-2017) Slide-Show document file extension.
            /// </summary>
            public const string Pptx = @".pptx";
            #endregion

            /// <summary>
            /// All Slides
            /// </summary>
            private static string[] AllSlides = new string[SlideExtensions.Count];

            /// <summary>
            /// Gets the format category.
            /// </summary>
            /// <value>
            /// The format category.
            /// </value>
            public static FornaxFormat FormatCategory => FornaxFormat.Slide;

            /// <summary>
            /// Gets an array collection of a default Slide types supported by Fornax.Net.
            /// [.ppt, .pptx].
            /// </summary>
            /// <value>
            /// The default Slide Types.
            /// </value>
            public static string[] Defaults => GetDefaultSlideTypes();

            /// <summary>
            /// Gets an array collection of all SlideShow Doc-types existing in Fornax's database.
            /// </summary>
            /// <value>
            /// The default Slide Types.
            /// </value>
            public static string[] All => GetAllSlideTypes();

            /// <summary>
            /// Gets all Slide types.
            /// </summary>
            /// <returns></returns>
            private static string[] GetAllSlideTypes() {
                try {
                    SlideExtensions.CopyTo(AllSlides, 0);
                    Array.Sort(AllSlides);
                    return AllSlides;
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
                string str = string.Format(@"{0}: Power point Slide-Show Documents: Ppt, Pptx ..", nameof(SlideShow));
                return base.ToString();
            }

            /// <summary>
            ///  Adds a valid SlideShow document extension to fornax.net database.
            ///  Note: Fornax.net only allows users to add only Microsoft PowerPoint Supported types. 
            ///  <code>extension must StartWith("pp");</code> e.g. ppt .. 
            /// </summary>
            /// <param name="extension">The extension.</param>
            /// <returns>True if Database update succeeds, else: False.</returns>
            public static bool AddSlideType(string extension) {

                if (string.IsNullOrWhiteSpace(extension) || !extension.StartsWith("pp")) { return false; }
                ParseToExtensionFormat(ref extension);
                try {
                    if (SlideExtensions.Contains(extension)) { return false; }
                    SlideExtensions.Add(extension);
                    Fornax.Default.Save();
                    return true;

                } catch (Exception) { return false; }
            }

            /// <summary>
            /// Deletes a specific <see cref="SlideShow"/> type from the Fornax.net database.
            /// Default <c>Slide Types </c>cannot be deleted.
            /// For list of Default Slide Types see: <see cref="Defaults"/>.
            /// </summary>
            /// <param name="extension">The extension.</param>
            /// <returns>true if Deletion is successful, else : false</returns>
            public static bool DeleteSlideType(string extension) {
                try {
                    if (string.IsNullOrWhiteSpace(extension) || !SlideExtensions.Contains(extension)) { return false; }
                    ParseToExtensionFormat(ref extension);
                    if (Defaults.Contains(extension)) { return false; }
                    SlideExtensions.Remove(extension);
                    Fornax.Default.Save();
                    return true;
                } catch (Exception) { return false; }
            }

            /// <summary>
            /// Gets the default Slide types.
            /// </summary>
            /// <returns></returns>
            private static string[] GetDefaultSlideTypes() => new[] { Ppt, Pptx};

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
