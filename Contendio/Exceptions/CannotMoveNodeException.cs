using System;
using System.Runtime.Serialization;

namespace Contendio.Exceptions
{
    public class CannotMoveNodeException : ContendioBaseException
    {
        public CannotMoveNodeException()
        {
        }

        public CannotMoveNodeException(string message) : base(message)
        {
        }

        public CannotMoveNodeException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected CannotMoveNodeException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
