using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Contendio.Event;

namespace Contendio.Model
{
    public interface INode
    {
        /// <summary>
        /// The unique identifier for this node.
        /// </summary>
        Guid Id { get; set; }

        /// <summary>
        /// The name of this node
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// The parent node if one exists. The root node doesn't have a parent node (i.e. it's null)
        /// </summary>
        INode ParentNode { get; set; }

        /// <summary>
        /// The type of this node
        /// </summary>
        INodeType NodeType { get; set; }

        /// <summary>
        /// The values of this node
        /// </summary>
        ICollection<INodeValue> Values { get; set; }

        /// <summary>
        /// This nodes child nodes
        /// </summary>
        ICollection<INode> Children { get; set; }

        /// <summary>
        /// Event triggered when this- or a child node is changed somehow (added, deleted or simply updated)
        /// </summary>
        event RepositoryChangeEventHandler OnNodeChanged;

        /// <summary>
        /// Retrieves the path of the node
        /// </summary>
        string Path { get; }

        /// <summary>
        /// Creates a new node and adds it to this node as a child
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        INode AddNode(string name);

        /// <summary>
        /// Creates a new node and adds it to this node as a child
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        INode AddNode(string name, string type);

        /// <summary>
        /// Retrieves a node with a specific path
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        INode GetNode(string path);
    }
}
