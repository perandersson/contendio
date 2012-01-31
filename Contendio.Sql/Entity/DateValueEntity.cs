using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;

namespace Contendio.Sql.Entity
{
    [Table(Name = "replaceme_DateValue")]
    public class DateValueEntity : IEntityWithId
    {
        /// <summary>
        /// 
        /// </summary>
        [Column(IsPrimaryKey = true, IsDbGenerated = true, AutoSync = AutoSync.OnInsert)]
        public long Id { get; set; }

        [Column]
        public DateTime Value { get; set; }
    }
}
