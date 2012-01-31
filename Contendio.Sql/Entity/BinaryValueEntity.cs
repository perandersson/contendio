using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;
using System.Data.Linq;

namespace Contendio.Sql.Entity
{
    [Table(Name = "replaceme_BinaryValue")]
    public class BinaryValueEntity : IEntityWithId
    {
        /// <summary>
        /// 
        /// </summary>
        [Column(IsPrimaryKey = true, IsDbGenerated = true, AutoSync = AutoSync.OnInsert)]
        public long Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(UpdateCheck = UpdateCheck.Never)]
        public Binary Value { get; set; }
    }
}
