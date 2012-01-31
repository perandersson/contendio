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
        public long Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column]
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column]
        public int NodeTypeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column]
        public long? StringValueId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column]
        public long? BinaryValueId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column]
        public long? DateValueId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column]
        public long NodeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column]
        public DateTime AddedDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column]
        public DateTime ChangedDate { get; set; }
    }
}
