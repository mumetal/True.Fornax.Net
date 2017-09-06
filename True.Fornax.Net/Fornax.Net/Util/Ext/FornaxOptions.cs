using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fornax.Net.Util.Ext
{
    static class FornaxOptions
    {
        //contains only enumerations and value handlers for options
        internal enum TraversalMode { Minimal, Normal, Detailed, Absolute }
        internal enum SearchMode { Native /*AND = _WS_*/ , Normal /*OR = _WS_*/, Natural /*Natural Laguage */}


    }


}
