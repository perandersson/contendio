using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;

namespace Contendio.Sql.Entity
{
    [Table(Name = "replaceme_NodeType")]
    public class NodeTypeEntity
    {
        /// <summary>
        /// 
        /// </summary>
        [Column(IsPrimaryKey = true, IsDbGenerated = true, AutoSync = AutoSync.OnInsert)]
        public int Id { get; set; }

        /// <summary>
        /// The name of the current node
        /// </summary>
        [Column]
        public string Name { get; set; }
    }
}
