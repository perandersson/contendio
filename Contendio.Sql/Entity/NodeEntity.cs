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
        private string _path = String.Empty;
        
        /// <summary>
        /// 
        /// </summary>
        [Column(IsPrimaryKey = true, IsDbGenerated = true, AutoSync = AutoSync.OnInsert)]
        public Int64 Id { get; set; }

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
        public Int64? NodeId { get; set; }

        /// <summary>
        /// The id of this nodes type
        /// </summary>
        [Column]
        public int NodeTypeId { get; set; }

        /// <summary>
        /// The date when this node is added into the database
        /// </summary>
        [Column(UpdateCheck = UpdateCheck.Never)]
        public DateTime AddedDate { get; set; }

        /// <summary>
        /// The date when this node is changed
        /// </summary>
        [Column(UpdateCheck = UpdateCheck.Never)]
        public DateTime ChangedDate { get; set; }
    }
}
