using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;

namespace Contendio.Sql.Mapping
{
    class RepositoryMetaModel : MetaModel
    {
        private RepositoryMappingSource mappingSource;
        private MetaModel attributeMetaModel;
        private Dictionary<Type, MetaTable> cachedMetaTables = new Dictionary<Type, MetaTable>();
        private string workspace;

        public RepositoryMetaModel(string workspace, RepositoryMappingSource mappingSource, MetaModel attributeMetaModel)
        {
            this.workspace = workspace;
            this.mappingSource = mappingSource;
            this.attributeMetaModel = attributeMetaModel;
        }

        public override Type ContextType
        {
            get { return attributeMetaModel.ContextType; }
        }

        public override string DatabaseName
        {
            get { return attributeMetaModel.DatabaseName; }
        }

        public override MetaFunction GetFunction(System.Reflection.MethodInfo method)
        {
            return attributeMetaModel.GetFunction(method);
        }

        public override IEnumerable<MetaFunction> GetFunctions()
        {
            return attributeMetaModel.GetFunctions();
        }

        public override MetaType GetMetaType(Type type)
        {
            return attributeMetaModel.GetMetaType(type);
        }


        public override MetaTable GetTable(Type rowType)
        {
            if (rowType == null) 
                throw new ArgumentNullException("rowType");

            MetaTable metaTable;
            if (!cachedMetaTables.TryGetValue(rowType, out metaTable))
            {
                var originalMetaTable = attributeMetaModel.GetTable(rowType);
                lock (cachedMetaTables)
                {
                    metaTable = new RepositoryMetaTable(workspace, originalMetaTable, this);
                    cachedMetaTables.Add(rowType, metaTable);
                }
            }

            return metaTable;


            //MetaTable tableNoLocks;

            //if (!this._MetaTables.TryGetValue(rowType, out tableNoLocks))
            //{
            //    lock (_MetaTables)
            //    {
            //        if (!this._MetaTables.TryGetValue(rowType, out tableNoLocks))
            //        {
            //            Type key = GetRoot(rowType) ?? rowType;
            //            TableAttribute[] customAttributes = (TableAttribute[])key.GetCustomAttributes(typeof(TableAttribute), true);
            //            if (customAttributes.Length == 0)
            //            {
            //                this._MetaTables.Add(rowType, null);
            //            }
            //            else
            //            {
            //                var tableAttr = CustomizeTableAttribute(key, customAttributes[0]);
            //                tableNoLocks = (MetaTable)Activator.CreateInstance(AttributedMetaTableType, BindingFlags.NonPublic | BindingFlags.Instance, null, new object[] { _orgmodel, tableAttr, key }, System.Threading.Thread.CurrentThread.CurrentCulture);
            //                foreach (MetaType type2 in tableNoLocks.RowType.InheritanceTypes)
            //                {
            //                    this._MetaTables.Add(type2.Type, tableNoLocks);
            //                }
            //            }
            //            if (tableNoLocks.RowType.GetInheritanceType(rowType) == null)
            //            {
            //                this._MetaTables.Add(rowType, null);
            //                tableNoLocks = null;
            //            }
            //        }
            //    }
            //}


            //return tableNoLocks;
        }

        public override IEnumerable<MetaTable> GetTables()
        {
            throw new NotImplementedException();
        }

        public override MappingSource MappingSource
        {
            get { return this.mappingSource; }
        }

        public override Type ProviderType
        {
            get { return attributeMetaModel.ProviderType; }
        }
    }
}
