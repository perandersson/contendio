using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Contendio.Model;

namespace Contendio
{
    public interface IQueryManager
    {
        /// <summary>
        /// 
        /// </summary>
        IQueryable<INode> Nodes { get; }

        /// <summary>
        /// 
        /// </summary>
        IQueryable<INodeValue> NodeValues { get; }

        /// <summary>
        /// 
        /// </summary>
        IQueryable<INodeType> NodeTypes { get; }
    }
}
