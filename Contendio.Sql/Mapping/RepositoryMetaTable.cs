using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;
using System.Reflection;

namespace Contendio.Sql.Mapping
{
    class RepositoryMetaTable : MetaTable
    {
        private MetaTable originalTable;
        private MetaModel model;
        private string repository;

        public RepositoryMetaTable(string repository, MetaTable orig, MetaModel model)
        {
            this.repository = repository;
            this.originalTable = orig;
            this.model = model;
        }

        public override MethodInfo DeleteMethod
        {
            get { return this.originalTable.DeleteMethod; }
        }

        public override MethodInfo InsertMethod
        {
            get { return this.originalTable.InsertMethod; }
        }

        public override MetaModel Model
        {
            get { return this.model; }
        }

        public override MetaType RowType
        {
            get { return this.originalTable.RowType; }
        }

        public override string TableName
        {
            get 
            {
                string tableName = this.originalTable.TableName;
                return tableName.Replace("replaceme_", repository + "_");
            }
        }

        public override MethodInfo UpdateMethod
        {
            get { return originalTable.UpdateMethod; }
        }
    }
}
