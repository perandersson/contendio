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
        Int64 Id { get; set; }

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
        IList<INodeValue> Values { get; set; }

        /// <summary>
        /// This nodes child nodes
        /// </summary>
        IList<INode> Children { get; set; }

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

        /// <summary>
        /// Moves this node before the supplied node
        /// </summary>
        /// <param name="node"></param>
        void MoveBefore(INode node);

        /// <summary>
        /// Moves this node after the supplied node
        /// </summary>
        /// <param name="node"></param>
        void MoveAfter(INode node);

        /// <summary>
        /// Removes this node from the repository
        /// </summary>
        void Delete();

        /// <summary>
        /// Removes a node with the specific path
        /// </summary>
        /// <param name="path">Deletes a node with the specific path</param>
        void Delete(string path);

        /// <summary>
        /// Adds a string value to this node
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        INodeValue AddValue(string name, string value);

        /// <summary>
        /// Adds a string value to this node
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        INodeValue AddValue(string name, string value, string type);

        /// <summary>
        /// Adds a date value to this node
        /// </summary>
        /// <param name="name"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        INodeValue AddValue(string name, DateTime date);

        /// <summary>
        /// Adds a date value to this node
        /// </summary>
        /// <param name="name"></param>
        /// <param name="date"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        INodeValue AddValue(string name, DateTime date, string type);

        /// <summary>
        /// Adds a binary value to this node
        /// </summary>
        /// <param name="name"></param>
        /// <param name="array"></param>
        /// <returns></returns>
        INodeValue AddValue(string name, byte[] array);

        /// <summary>
        /// Adds a binary value to this node
        /// </summary>
        /// <param name="name"></param>
        /// <param name="array"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        INodeValue AddValue(string name, byte[] array, string type);

        /// <summary>
        /// Retrieves the value in this node by a specific name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        INodeValue GetValue(string name);

        /// <summary>
        /// Checks if this node is the parent node of the supplied node object
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        bool IsParentOf(INode node);

        /// <summary>
        /// Checks if this node is the child node of the supplied node object
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        bool IsChildOf(INode node);

        /// <summary>
        /// Checks if the this node and the supplied node is siblings
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        bool IsSiblingOf(INode node);

        /// <summary>
        /// Checks if this node is a root node
        /// </summary>
        /// <returns></returns>
        bool IsRootNode();
    }
}
