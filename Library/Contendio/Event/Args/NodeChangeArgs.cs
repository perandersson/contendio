using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Contendio.Model;
using Contendio.Event;

namespace Contendio.Event.Args
{
    public class NodeChangeArgs
    {
        public RepositoryEventType EventType { get; set; }
        public INode Node { get; set; }
    }
}
