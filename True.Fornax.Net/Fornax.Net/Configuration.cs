#region Enumeration Options

/// <summary>
///  Traversal Mode specifies the mode at which fornax network crawler (<seealso cref="Fornax.Net.Bot.Net.FornaxNetBot" />)
///  crawls the web.
/// </summary>
public enum TraversalMode
{
    /// <summary>
    /// When <c>Minimal</c> Traversion mode is set, <seealso cref="Fornax.Net.Bot.Net.FornaxNetBot" /> implements the minimalist approach to 
    /// web crawling.<para> No Document scoring and page priority weighting as pages are fetched, this mode excludes immediate ranking of pages as they are acquired i.e. 
    /// the pages are stored in a Regular <code>Queue</code>.</para>
    /// Fetch Depth: (Default: 10, Maximum: 15, Minimum: 2).
    /// </summary>
    Minimal = 2,
    /// <summary>
    /// 
    /// </summary>
    Normal = 3,
    /// <summary>
    /// 
    /// </summary>
    Detailed = 1,
    /// <summary>
    /// 
    /// </summary>
    Absolute = 0
}

/// <summary>
/// 
/// </summary>
public enum SearchMode
{
    /// <summary>
    /// The native *AND = _WS_*
    /// </summary>
    Native = 1,
    /// <summary>
    /// The normal *OR =_WS_
    /// </summary>
    Normal = 0,
    /// <summary>
    /// The natural
    /// </summary>
    Natural = -1
}

/// <summary>
/// 
/// </summary>
public enum FileFetchMode
{
    /// <summary>
    /// The polite
    /// </summary>
    Polite = 0,
    /// <summary>
    /// The robust
    /// </summary>
    Robust = ~Polite
}

/// <summary>
/// 
/// </summary>
public enum QueryMode : byte
{
    /// <summary>
    /// The simple
    /// </summary>
    Simple = 0,
    /// <summary>
    /// The full
    /// </summary>
    Full = 1
}

/// <summary>
/// 
/// </summary>
public enum FieldScope : sbyte
{
    /// <summary>
    /// The content
    /// </summary>
    Content = 0x0000,
    /// <summary>
    /// The date
    /// </summary>
    Date = 0x0001,
    /// <summary>
    /// The modified
    /// </summary>
    Modified = 0x0002,
    /// <summary>
    /// The name
    /// </summary>
    Name = 0x0003,
    /// <summary>
    /// The path
    /// </summary>
    Path = 0x0004,
    /// <summary>
    /// The type
    /// </summary>
    Type = 0x0005,
    /// <summary>
    /// The meta data
    /// </summary>
    MetaData = 0x0006
}

/// <summary>
/// 
/// </summary>
internal enum QueryType
{
    /// <summary>
    /// The boolean
    /// </summary>
    Boolean,
    /// <summary>
    /// The fieldscope
    /// </summary>
    Fieldscope,
    /// <summary>
    /// The frequency
    /// </summary>
    Frequency,
    /// <summary>
    /// The phrase
    /// </summary>
    Phrase,
    /// <summary>
    /// The proximity
    /// </summary>
    Proximity,
    /// <summary>
    /// The regex
    /// </summary>
    Regex,
    /// <summary>
    /// The term
    /// </summary>
    Term,
    /// <summary>
    /// The text
    /// </summary>
    Text,
    /// <summary>
    /// The wildcard
    /// </summary>
    Wildcard,
}

/// <summary>
/// 
/// </summary>
public enum ExpandQuery
{
    /// <summary>
    /// The yes
    /// </summary>
    YES,
    /// <summary>
    /// No: Do not Expand 
    /// </summary>
    NO = ~YES
}

/// <summary>
/// 
/// </summary>
public enum Caching
{
    /// <summary>
    /// The on
    /// </summary>
    ON,
    /// <summary>
    /// The off
    /// </summary>
    OFF = ~ON
}

/// <summary>
/// 
/// </summary>
public enum CacheType : sbyte
{
    /// <summary>
    /// The default
    /// </summary>
    Default,
    /// <summary>
    /// The reduced
    /// </summary>
    Reduced,
    /// <summary>
    /// The network
    /// </summary>
    Network,
    /// <summary>
    /// The local
    /// </summary>
    Local,
    /// <summary>
    /// The remote
    /// </summary>
    Remote,
    /// <summary>
    /// The absolute
    /// </summary>
    Absolute = ~Reduced
}

/// <summary>
/// 
/// </summary>
public enum FornaxFormat : byte
{
    /// <summary>
    /// The default
    /// </summary>
    Default,
    /// <summary>
    /// All
    /// </summary>
    All,
    /// <summary>
    /// The image
    /// </summary>
    Image,
    /// <summary>
    /// The text
    /// </summary>
    Text,
    /// <summary>
    /// The slide
    /// </summary>     
    Slide,
    /// <summary>
    /// The spread sheet
    /// </summary>
    SpreadSheet,
    /// <summary>
    /// The email
    /// </summary>
    Email,
    /// <summary>
    /// The DOM
    /// </summary>
    DOM,
    /// <summary>
    /// The web
    /// </summary>
    Web,
    /// <summary>
    /// The media
    /// </summary>
    Media,
    /// <summary>
    /// The plain
    /// </summary>
    Plain,
    /// <summary>
    /// The zip
    /// </summary>
    Zip
}

/// <summary>
/// 
/// </summary>
public enum Suggestion {
    /// <summary>
    /// The on
    /// </summary>
    ON = 0,
    /// <summary>
    /// The off
    /// </summary>
    OFF = ~ON

}

/// <summary>
/// 
/// </summary>
public enum SearchFilter {
    /// <summary>
    /// The bias
    /// </summary>
    Bias = 0x0000,
    /// <summary>
    /// The explicit
    /// </summary>
    Explicit = 0x0002,
    /// <summary>
    /// The safe
    /// </summary>
    Safe = 0x0004
}

/// <summary>
/// 
/// </summary>
public enum AutoCorrection {
    /// <summary>
    /// All
    /// </summary>
    All,
    /// <summary>
    /// Any
    /// </summary>
    Any = ~ All
}
#endregion 



namespace Fornax.Net
{
    public sealed class Configuration : FornaxConfig
    {

        private Configuration() { }


        public static FornaxConfig SetConfiguration() {
            

            return null;
        }

        public static FornaxConfig LoadConfiguration() {
           
            return null;
        }

    }
}
