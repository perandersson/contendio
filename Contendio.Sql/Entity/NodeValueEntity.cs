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
        public Int64 Id { get; set; }

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
        public Int64 NodeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(UpdateCheck = UpdateCheck.Never)]
        public DateTime AddedDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(UpdateCheck = UpdateCheck.Never)]
        public DateTime ChangedDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column]
        public string StringValue { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(UpdateCheck = UpdateCheck.Never)]
        public Binary BinaryValue { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column]
        public DateTime? DateTimeValue { get; set; }
    }
}
