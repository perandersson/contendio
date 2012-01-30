using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;

namespace Contendio.Sql.Mapping
{
    public class RepositoryMappingSource : MappingSource
    {
        public string Repository { get; set; }
        private AttributeMappingSource attributeMappingSource = new AttributeMappingSource();

        public RepositoryMappingSource(string repository) : base()
        {
            this.Repository = repository;
        }

        protected override MetaModel CreateModel(Type dataContextType)
        {
            MetaModel attributeMetaModel = attributeMappingSource.GetModel(dataContextType);
            MetaModel customMetaModel = new RepositoryMetaModel(Repository, this, attributeMetaModel);
            return customMetaModel;
        }
    }
}
