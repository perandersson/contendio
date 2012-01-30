using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Contendio.Event.Args;

namespace Contendio.Event
{
    [Serializable]
    public delegate void RepositoryChangeEventHandler(object sender, NodeChangeArgs args);
}
