using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Contendio.Exceptions
{
    public class CannotAddAttributeOnRootNode : ContendioBaseException
    {
        public CannotAddAttributeOnRootNode()
        {
        }

        public CannotAddAttributeOnRootNode(string message) : base(message)
        {
        }

        public CannotAddAttributeOnRootNode(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected CannotAddAttributeOnRootNode(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
