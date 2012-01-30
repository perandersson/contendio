using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;
using System.Data.Linq;

namespace Contendio.Sql.Entity
{
    [Table(Name = "replaceme_NodeValue")]
    public class NodeValueEntity : IEntityWithId
    {
        private EntityRef<NodeEntity> _parentNode;
        private EntityRef<NodeTypeEntity> _nodeType;
        private EntityRef<StringValueEntity> _stringValue;
        private EntityRef<BinaryValueEntity> _binaryValue;

        /// <summary>
        /// 
        /// </summary>
        [Column(IsPrimaryKey = true, IsDbGenerated = true, AutoSync = AutoSync.OnInsert)]
        public Guid Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column]
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column]
        public Guid TypeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column]
        public Guid? StringValueId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column]
        public Guid? BinaryValueId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column]
        public Guid NodeId { get; set; }

        /// <summary>
        /// Retrieves the parent node for this node object
        /// </summary>
        [Association(Storage = "_parentNode", ThisKey = "NodeId", OtherKey = "Id", IsForeignKey = true)]
        public NodeEntity ParentNode
        {
            get
            {
                return _parentNode.Entity;
            }
            set
            {
                _parentNode.Entity = value;
            }
        }

        /// <summary>
        /// Retrieves the parent node for this node object
        /// </summary>
        [Association(Storage = "_nodeType", ThisKey = "TypeId", OtherKey = "Id", IsForeignKey = true)]
        public NodeTypeEntity NodeType
        {
            get
            {
                return _nodeType.Entity;
            }
            set
            {
                _nodeType.Entity = value;
            }
        }

        /// <summary>
        /// Retrieves the parent node for this node object
        /// </summary>
        [Association(Storage = "_stringValue", ThisKey = "StringValueId", OtherKey = "Id")]
        public StringValueEntity StringValue
        {
            get
            {
                return _stringValue.Entity;
            }
            set
            {
                _stringValue.Entity = value;
            }
        }

        /// <summary>
        /// Retrieves the parent node for this node object
        /// </summary>
        [Association(Storage = "_binaryValue", ThisKey = "BinaryValueId", OtherKey = "Id")]
        public BinaryValueEntity BinaryValue
        {
            get
            {
                return _binaryValue.Entity;
            }
            set
            {
                _binaryValue.Entity = value;
            }
        }
    }
}
