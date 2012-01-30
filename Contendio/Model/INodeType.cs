using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Contendio.Model
{
    public interface INodeType
    {
        /// <summary>
        /// The unique id of this node type
        /// </summary>
        Guid Id { get; set; }

        /// <summary>
        /// The name of this node type.
        /// </summary>
        /// <example>
        /// var name = rootNode.NodeType.Name;
        /// Console.WriteLine(name); // This prints "node:rootnode"
        /// </example>
        string Name { get; set; }
    }
}
