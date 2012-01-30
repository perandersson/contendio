using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;
using System.Data.Linq;

namespace Contendio.Sql.Entity
{
    [Table(Name = "replaceme_StringValue")]
    public class StringValueEntity : IEntityWithId
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
        public Guid TypeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column]
        public string Value { get; set; }
    }
}
