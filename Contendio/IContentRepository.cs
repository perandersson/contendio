using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using Contendio.Model;

namespace Contendio
{
    /// <summary>
    /// 
    /// </summary>
    public interface IContentRepository
    {
        /// <summary>
        /// Retrieves the repository name
        /// </summary>
        string Workspace { get; }

        /// <summary>
        /// Retrieves the root node from the database (i.e. that node which doesnt have a parent node)
        /// </summary>
        INode RootNode { get; }

        /// <summary>
        /// 
        /// </summary>
        IQueryManager QueryManager { get; }
    }
}
