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
        public string Workspace { get; private set; }
        public MetaTable Original { get; private set; }
        public MetaModel RepositoryMetaModel { get; private set; }
        public MetaType RepositoryMetaType { get; private set; }

        public RepositoryMetaTable(string workspace, MetaTable orig, MetaModel model)
        {
            this.Workspace = workspace;
            this.Original = orig;
            this.RepositoryMetaModel = model;
            this.RepositoryMetaType = new RepositoryMetaType(workspace, orig.RowType, this, model);
        }

        public override MethodInfo DeleteMethod
        {
            get { return this.Original.DeleteMethod; }
        }

        public override MethodInfo InsertMethod
        {
            get { return this.Original.InsertMethod; }
        }

        public override MetaModel Model
        {
            get { return this.RepositoryMetaModel; }
        }

        public override MetaType RowType
        {
            get { return this.RepositoryMetaType; }
        }

        public override string TableName
        {
            get 
            {
                string tableName = this.Original.TableName;
                return tableName.Replace("replaceme_", Workspace + "_");
            }
        }

        public override MethodInfo UpdateMethod
        {
            get { return Original.UpdateMethod; }
        }
    }
}
