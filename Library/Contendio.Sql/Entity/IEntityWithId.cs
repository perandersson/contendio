using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Contendio.Sql.Entity
{
    public interface IEntityWithId
    {
        Int64 Id { get; }
    }
}
