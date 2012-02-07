using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Contendio.Event;

namespace Contendio
{
    public interface IObserverManager
    {
        /// <summary>
        /// Attach a listener to a specific path
        /// </summary>
        /// <param name="Path"></param>
        /// <param name="value"></param>
        void AttachListener(string Path, RepositoryChangeEventHandler value);

        /// <summary>
        /// Detaches a listener from receiving events
        /// </summary>
        /// <param name="value"></param>
        void DetachListener(RepositoryChangeEventHandler value);
    }
}
