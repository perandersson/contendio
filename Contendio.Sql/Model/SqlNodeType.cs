using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Contendio.Sql.Entity;
using Contendio.Model;

namespace Contendio.Sql.Model
{
    public class SqlNodeType : INodeType
    {
        public NodeTypeEntity Original { get; private set; }
        public SqlContentRepository ContentRepository { get; private set; }

        public SqlNodeType()
        {
        }

        public SqlNodeType(NodeTypeEntity original, SqlContentRepository contentRepository)
        {
            this.Original = original;
            this.ContentRepository = contentRepository;
        }

        public Guid Id
        {
            get
            {
                return Original.Id;
            }
            set
            {
                Original.Id = value;
            }
        }

        public string Name
        {
            get
            {
                return Original.Name;
            }
            set
            {
                Original.Name = value;
            }
        }
    }
}
