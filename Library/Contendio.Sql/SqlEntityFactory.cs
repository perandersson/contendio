using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Contendio.Sql.Entity;

namespace Contendio.Sql
{
    class SqlEntityFactory
    {
        public SqlContentRepository ContentRepository { get; private set; }

        public SqlEntityFactory(SqlContentRepository contentRepository)
        {
            this.ContentRepository = contentRepository;
        }

        public NodeTypeEntity GetNodeType(string name)
        {
            var nodeType = from n in ContentRepository.NodeTypeQueryable where n.Name.Equals(name) select n;
            return nodeType.FirstOrDefault();
        }

    }
}
