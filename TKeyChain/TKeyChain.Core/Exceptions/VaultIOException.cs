using System;
using System.IO;
using System.Runtime.Serialization;

namespace TKeyChain.Core.Exceptions
{
    class VaultIOException : IOException
    {
        public VaultIOException()
        {
        }

        public VaultIOException(string message) : base(message)
        {
        }

        public VaultIOException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public VaultIOException(string message, int hresult) : base(message, hresult)
        {
        }

        protected VaultIOException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
