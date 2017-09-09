using System;

namespace Fornax.Net.Util.Exceptions
{
    class FornaxException : Exception
    {
        public FornaxException(string message) : base(message) {
        }

        public FornaxException(string message, Exception innerException) : base(message, innerException) {
        }

        public FornaxException() : base() {

        }
    }
}
