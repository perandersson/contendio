using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;

namespace Contendio.Sql.Entity
{
    [Table(Name = "replaceme_NodeAttribute")]
    public class NodeAttributeEntity : IEntityWithId
    {
        /// <summary>
        /// 
        /// </summary>
        [Column(IsPrimaryKey = true, IsDbGenerated = true, AutoSync = AutoSync.OnInsert)]
        public Int64 Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column]
        public Int64 NodeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column]
        public int NodeTypeId { get; set; }
    }
}
