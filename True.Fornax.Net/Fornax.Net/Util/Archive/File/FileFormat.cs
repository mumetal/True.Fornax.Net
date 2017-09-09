using System;
using System.Linq;
using System.Collections.Specialized;
using Fornax.Net.Util.Exceptions;

namespace Fornax.Net.Util.Archive.File
{

    /// <summary>
    /// Fornax.Net File Extension Handler. 
    /// </summary>
    public static partial class FileFormat
    {
        /// <summary>
        /// The DOM  file extensions from Fornax Settings. 
        /// [dhtml,fxml,htm,html,shtml,xaml,xhtml,xml]
        /// </summary>
        private static StringCollection DOMExtensions = Fornax.Default.DefaultDOMTypes ?? throw new FornaxException();

        /// <summary>
        /// The Email file extensions from Fornax Settings. 
        /// [eml,dbx,msg,pst,vcf]
        /// </summary>
        private static StringCollection EmailExtensions = Fornax.Default.DefaultEmailTypes ?? throw new FornaxException();

        /// <summary>
        /// The Web document file extensions from Fornax Settings. 
        /// [asp,aspx] 
        /// Note : <see cref="Fornax.DefaultDOMTypes"/> also contains Web doc-files e.g html..
        /// </summary>
        private static StringCollection WebExtensions = Fornax.Default.DefaultWebTypes ?? throw new FornaxException();

        /// <summary>
        /// The Slide-Show file extensions from Fornax Settings. 
        /// [ppt,pptx]
        /// </summary>
        private static StringCollection SlideExtensions = Fornax.Default.DefaultSlideTypes ?? throw new FornaxException();

        /// <summary>
        /// The Spread-Sheet file extensions from Fornax Settings. 
        /// [xl,xlr,xls,xlsm,xlsx]
        /// </summary>
        private static StringCollection SheetExtensions = Fornax.Default.DefaultSSheetTypes ?? throw new FornaxException();

        /// <summary>
        /// The Media type file extensions from Fornax Settings. 
        /// [mp3,mp4]
        /// </summary>
        private static StringCollection MediaExtensions = Fornax.Default.DefaultMediaTypes ?? throw new FornaxException();

        /// <summary>
        /// The image type file extensions from Fornax Settings. 
        /// [gif,jpeg,jpg,png,tiff]
        /// </summary>
        private static StringCollection ImageExtensions = Fornax.Default.DefaultImageTypes ?? throw new FornaxException();

        /// <summary>
        /// The Plain-Text extensions from Fornax Settings. 
        /// [ans,ascii,c,cs,java,txt]
        /// </summary>
        private static StringCollection PlainExtensions = Fornax.Default.DefaultPlainTypes ?? throw new FornaxException();

        /// <summary>
        /// The Document-Text extensions from Fornax Settings. 
        /// [csv,doc,docx,epub,pdf,rtf]
        /// </summary>
        private static StringCollection TextExtensions = Fornax.Default.DefaultTextTypes ?? throw new FornaxException();

        /// <summary>
        /// The Zip file extensions from Fornax Settings. 
        /// [gzip,rar,zip]
        /// </summary>
        private static StringCollection ZipExtensions = Fornax.Default.DefaultZipTypes ?? throw new FornaxException();

        /// <summary>
        /// Parses a specified string to extension format.
        /// </summary>
        /// <param name="extension">The extension.</param>
        private static void ParseToExtensionFormat(ref string extension) {
            try {
                extension = extension.ToLower();
                if (!extension.StartsWith(".")) { extension.Insert(0, "."); }
            } catch (Exception) { }
        }

        /// <summary>
        /// Adds the custom format extension to Fornax database.
        /// </summary>
        /// <param name="extension">The extension.</param>
        /// <returns></returns>
        public static bool AddCustomFormat(string extension) {
            if (string.IsNullOrWhiteSpace(extension)) { return false; }
            ParseToExtensionFormat(ref extension);
            try {
                if (Fornax.Default.CustomTypes.Contains(extension)) { return false; }
                Fornax.Default.CustomTypes.Add(extension);
                Fornax.Default.Save();
                return true;
            } catch (Exception) { return false; }
        }

        /// <summary>
        /// Deletes the custom format from fornax.net database
        /// </summary>
        /// <param name="extension">The extension.</param>
        /// <returns></returns>
        /// <exception cref="FornaxException"></exception>
        public static bool DeleteCustomFormat(string extension) {
            StringCollection custom = Fornax.Default.CustomTypes ?? throw new FornaxException();
            if (string.IsNullOrWhiteSpace(extension) || !custom.Contains(extension)) { return false; }
            try {
                ParseToExtensionFormat(ref extension);
                custom.Remove(extension);
                Fornax.Default.Save();
                return true;
            } catch (Exception) { return false; }

        }

        /// <summary>
        /// Clears the custom formats in fornax.net database.
        /// </summary>
        public static void ClearCustomFormats() {
            try {
                Fornax.Default.CustomTypes.Clear();
                Fornax.Default.Save();
            } catch (Exception) { }
        }

        /// <summary>
        /// Gets all default fornax.net supported file extensions. </summary>
        /// <returns></returns>
        public static string[] GetAllDefaults() {
            try {
                StringCollection defaults = Fornax.Default.DefaultTypes;
                string[] defs = new string[defaults.Count];
                defaults.CopyTo(defs, 0);
                Array.Sort(defs);
                return defs;
            } catch (Exception) { return null; }
        }

        /// <summary>
        /// Gets all custom extensions in fornax.net database.
        /// </summary>
        /// <param name="sorted">if set to <c>true</c> Return a lexographically sorted list of custom extensions.</param>
        /// <returns></returns>
        public static string[] GetAllCustoms(bool sorted = false) {
            try {
                StringCollection customs = Fornax.Default.CustomTypes;
                string[] cust = new string[customs.Count];
                if (customs.Count <= 0) { return cust; }

                customs.CopyTo(cust, 0);
                if (sorted) { Array.Sort(cust);}
                return cust;
            } catch (Exception) { return null; }
        }

        /// <summary>
        /// Initializes the <see cref="FileFormat"/> class.
        /// </summary>
        /// <exception cref="System.NullReferenceException">Settings File as been Corrupted.</exception>
        static FileFormat() {
            {
                if (new Fornax() is null) {
                    throw new NullReferenceException("Settings File as been Corrupted.");
                }
            }
        }
    }
}
