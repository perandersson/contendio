using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Contendio.Exceptions
{
    /// <summary>
    /// Exception thrown when an invalid name is set on a node or a node value (such as '/', which is reserved)
    /// </summary>
    public class InvalidNameException : ContendioBaseException
    {
        public InvalidNameException()
        {
        }

        public InvalidNameException(string message)
            : base(message)
        {
        }

        public InvalidNameException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected InvalidNameException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
