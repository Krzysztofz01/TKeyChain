using System;
using System.Runtime.Serialization;

namespace TKeyChain.Core.Exceptions
{
    public class VaultLogicException : Exception
    {
        public VaultLogicException()
        {
        }

        public VaultLogicException(string message) : base(message)
        {
        }

        public VaultLogicException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected VaultLogicException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
