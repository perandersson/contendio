using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Contendio.Exceptions
{
    public class CannotAddAttributeTwice : ContendioBaseException
    {
        public CannotAddAttributeTwice()
            : base()
        {
        }

        public CannotAddAttributeTwice(string message)
            : base(message)
        {
        }
    }
}
