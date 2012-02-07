using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Contendio.Sql
{
    public class SqlObserverManager : IObserverManager
    {
        public void AttachListener(string Path, Event.RepositoryChangeEventHandler value)
        {
            throw new NotImplementedException();
        }

        public void DetachListener(Event.RepositoryChangeEventHandler value)
        {
            throw new NotImplementedException();
        }
    }
}
