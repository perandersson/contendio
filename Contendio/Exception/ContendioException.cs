using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Contendio.Exception
{
    public class ContendioException : System.Exception
    {
        public ContendioException()
        {
        }

        public ContendioException(string message)
            : base(message)
        {
        }
    }
}
