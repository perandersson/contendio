using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Contendio.Model
{
    public interface INodeValue
    {
        /// <summary>
        /// The unique id of this node value
        /// </summary>
        Int64 Id { get; set; }

        /// <summary>
        /// The name of the node value
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// The parent node object
        /// </summary>
        INode ParentNode { get; }

        /// <summary>
        /// The type of this node value.
        /// </summary>
        INodeType NodeType { get; set; }
        
        /// <summary>
        /// The value type (i.e if this is a string, date etc)
        /// </summary>
        NodeValueType ValueType { get; }

        /// <summary>
        /// Retrieves the value as a normal string.
        /// </summary>
        /// <returns>The string value</returns>
        string GetString();

        /// <summary>
        /// Retrieves the value as a stream. This method should be used when the value is a binary type of value
        /// </summary>
        /// <example>
        /// using(var stream = nodeValue.ValueAsStream())
        /// {
        ///     // do stuff with stream here
        /// }
        /// </example>
        /// <returns>A stream. The caller is responsible for closing the stream when the reading is done</returns>
        Stream GetStream();

        /// <summary>
        /// Retrieves the date if it's a value id.
        /// </summary>
        /// <returns></returns>
        DateTime GetDateTime();

        /// <summary>
        /// Retrieve the value as an integer
        /// </summary>
        /// <returns></returns>
        int GetInteger();
    }
}
