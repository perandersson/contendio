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
        public Guid NodeTypeId { get; set; }

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
    }
}
