using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Contendio.Exception
{
    /// <summary>
    /// Exception thrown when a user tries to retrieve a node value type that isn't convertable.
    /// </summary>
    public class InvalidNodeValueTypeException : ContendioException
    {
        public InvalidNodeValueTypeException()
        {
        }

        public InvalidNodeValueTypeException(string message) : base(message)
        {
        }
    }
}
