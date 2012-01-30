using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Contendio.Sql.Entity
{
    [Table(Name = "replaceme_Node")]
    public class NodeEntity : IEntityWithId
    {
        private EntityRef<NodeEntity> _parentNode;
        private EntityRef<NodeTypeEntity> _nodeType;
        private EntitySet<NodeValueEntity> _nodeValue = new EntitySet<NodeValueEntity>();
        private string _path = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        [Column(IsPrimaryKey = true, IsDbGenerated = true, AutoSync = AutoSync.OnInsert)]
        public Guid Id { get; set; }

        /// <summary>
        /// The name of the current node
        /// </summary>
        [Column]
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column]
        public string Path { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(CanBeNull = true)]
        public Guid? NodeId { get; set; }

        /// <summary>
        /// The id of this nodes type
        /// </summary>
        [Column]
        public Guid NodeTypeId { get; set; }

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
        /// Retrieves all node data for this node
        /// </summary>
        [Association(Storage = "_nodeValue", ThisKey = "Id", OtherKey = "NodeId")]
        public EntitySet<NodeValueEntity> NodeValue
        {
            get
            {
                return _nodeValue;
            }
            set
            {
                _nodeValue.Assign(value);
            }
        }

        /// <summary>
        /// Retrieves the parent node for this node object
        /// </summary>
        [Association(Storage = "_nodeType", ThisKey = "NodeTypeId", OtherKey = "Id", IsForeignKey = true)]
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
    }
}
