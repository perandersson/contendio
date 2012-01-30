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
        string Repository { get; }

        /// <summary>
        /// Retrieves the root node from the database (i.e. that node which doesnt have a parent node)
        /// </summary>
        INode RootNode { get; }

        /// <summary>
        /// Retrieves a queryable object from the repository. Use this to perform LINQ commands
        /// </summary>
        IQueryable<INode> NodeQueryable { get; }

        /// <summary>
        /// Retrieves a queryable object from the repository. Use this to perform LINQ commands
        /// </summary>
        IQueryable<INodeValue> NodeValueQueryable { get; }

        /// <summary>
        /// Retrieves a queryable object from the repository. Use this to perform LINQ commands
        /// </summary>
        IQueryable<INodeType> NodeTypeQueryable { get; }

        /// <summary>
        /// Saves the node entity into the database
        /// </summary>
        /// <param name="node"></param>
        void Save(INode node);

        /// <summary>
        /// Saves the node value entity into the database
        /// </summary>
        /// <param name="nodeValue"></param>
        void Save(INodeValue nodeValue);

        /// <summary>
        /// Saves the node type entity into the database
        /// </summary>
        /// <param name="nodeType"></param>
        void Save(INodeType nodeType);

        /// <summary>
        /// Deletes the node entity from the database
        /// </summary>
        /// <param name="node"></param>
        void Delete(INode node);

        /// <summary>
        /// Deletes the node value entity from the database
        /// </summary>
        /// <param name="nodeValue"></param>
        void Delete(INodeValue nodeValue);

        /// <summary>
        /// Deletes node type entity from the database
        /// </summary>
        /// <param name="nodeType"></param>
        void Delete(INodeType nodeType);
    }
}
