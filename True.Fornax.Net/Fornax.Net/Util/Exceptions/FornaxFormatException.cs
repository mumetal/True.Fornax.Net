using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fornax.Net.Util.Exceptions
{
    class FornaxFormatException : FornaxException
    {
        public FornaxFormatException(string message) : base(message) {
        }

        public FornaxFormatException(string message, Exception innerException) : base(message, innerException) {
        }
    }
}
