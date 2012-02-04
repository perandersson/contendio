using System;
using System.Runtime.Serialization;

namespace Contendio.Exceptions
{
    public class ContendioBaseException : Exception
    {
        public ContendioBaseException()
        {
        }

        public ContendioBaseException(string message)
            : base(message)
        {
        }

        public ContendioBaseException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected ContendioBaseException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
