using System;
using System.Runtime.Serialization;

namespace Contendio.Exceptions
{
    /// <summary>
    /// Exception thrown when a user tries to retrieve a node value type that isn't convertable.
    /// </summary>
    public class InvalidNodeValueTypeException : ContendioBaseException
    {
        public InvalidNodeValueTypeException()
        {
        }

        public InvalidNodeValueTypeException(string message)
            : base(message)
        {
        }

        public InvalidNodeValueTypeException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected InvalidNodeValueTypeException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
