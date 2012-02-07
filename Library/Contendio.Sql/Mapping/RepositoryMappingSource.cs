using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;

namespace Contendio.Sql.Mapping
{
    public class RepositoryMappingSource : MappingSource
    {
        public string Workspace { get; set; }
        private AttributeMappingSource attributeMappingSource = new AttributeMappingSource();

        public RepositoryMappingSource(string workspace)
            : base()
        {
            this.Workspace = workspace;
        }

        protected override MetaModel CreateModel(Type dataContextType)
        {
            MetaModel attributeMetaModel = attributeMappingSource.GetModel(dataContextType);
            MetaModel customMetaModel = new RepositoryMetaModel(Workspace, this, attributeMetaModel);
            return customMetaModel;
        }
    }
}
