using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Contendio.Model;
using System.Data.Linq;
using Contendio.Sql.Entity;
using Contendio.Sql.Provider;

namespace Contendio.Sql
{
    public class SqlQueryManager : IQueryManager
    {
        public DataContext DataContext { get; private set; }
        public string Workspace { get; private set; }

        #region Private members

        private Table<NodeEntity> nodeTable;
        private Table<NodeValueEntity> nodeValueTable;
        private Table<NodeTypeEntity> nodeTypeTable;

        #endregion

        public SqlQueryManager(string workspace, DataContext dataContext)
        {
            this.Workspace = workspace;
            this.DataContext = dataContext;
            this.nodeTypeTable = DataContext.GetTable<NodeTypeEntity>();
            this.nodeTable = DataContext.GetTable<NodeEntity>();
            this.nodeValueTable = DataContext.GetTable<NodeValueEntity>();
        }

        public IQueryable<NodeEntity> NodeQueryable
        {
            get { return GetQueryable(nodeTable); }
        }

        public IQueryable<NodeValueEntity> NodeValueQueryable
        {
            get { return GetQueryable(nodeValueTable); }
        }

        public IQueryable<NodeTypeEntity> NodeTypeQueryable
        {
            get { return GetQueryable(nodeTypeTable); }
        }

        public IQueryable<INode> Nodes
        {
            get { throw new NotImplementedException(); }
        }

        public IQueryable<INodeValue> NodeValues
        {
            get { throw new NotImplementedException(); }
        }

        public IQueryable<INodeType> NodeTypes
        {
            get { throw new NotImplementedException(); }
        }

        private IQueryable<TSource> GetQueryable<TSource>(ITable<TSource> table) where TSource : class
        {
            IQueryProvider provider = new RepositorySqlProvider(Workspace, DataContext.GetCommand, DataContext.ExecuteQuery);
            IQueryable<TSource> source = new RepositoryQueryable<TSource>(Workspace, provider, table);
            return source;
        }

        public void Save(NodeEntity node)
        {
            Save(nodeTable, node);
        }

        public void Save(NodeValueEntity nodeValue)
        {
            Save(nodeValueTable, nodeValue);
        }

        public void Save(NodeTypeEntity nodeType)
        {
            Save(nodeTypeTable, nodeType);
        }

        private void Save(ITable table, IEntityWithId entity)
        {
            if (entity.Id == 0)
            {
                table.InsertOnSubmit(entity);
            }
            else if (table.GetOriginalEntityState(entity) == null)
            {
                table.Attach(entity);
                table.Context.Refresh(RefreshMode.KeepCurrentValues, entity);
            }

            table.Context.SubmitChanges();
        }

        private void Save(ITable table, NodeTypeEntity entity)
        {
            if (entity.Id.Equals(Guid.Empty))
            {
                table.InsertOnSubmit(entity);
            }
            else if (table.GetOriginalEntityState(entity) == null)
            {
                table.Attach(entity);
                table.Context.Refresh(RefreshMode.KeepCurrentValues, entity);
            }

            table.Context.SubmitChanges();
        }

        public void Delete(NodeEntity node)
        {
            DeleteNodeValuesByNode(node.Id);
            Delete(nodeTable, node);
        }

        private void DeleteNodeValuesByNode(Int64 id)
        {
        }

        public void Delete(NodeValueEntity nodeValue)
        {
            Delete(nodeValueTable, nodeValue);
        }

        public void Delete(NodeTypeEntity nodeType)
        {
            Delete(nodeTypeTable, nodeType);
        }
        
        private void Delete(ITable table, object entity)
        {
            table.DeleteOnSubmit(entity);
            DataContext.SubmitChanges();
        }

        public NodeTypeEntity GetNodeType(string name)
        {
            var nodeType = from n in NodeTypeQueryable where n.Name.Equals(name) select n;
            return nodeType.FirstOrDefault();
        }

        public NodeValueEntity GetNodeValueById(Int64 id)
        {
            var nodeValueQuery = from n in NodeValueQueryable where n.Id == id select n;
            return nodeValueQuery.FirstOrDefault();
        }

        public IList<NodeValueEntity> GetNodeValuesForNode(Int64 nodeId)
        {
            var valueQuery = from nodeValue in NodeValueQueryable where nodeValue.NodeId.Equals(nodeId) select nodeValue;
            var values = valueQuery.ToList();
            return values;
        }

        public IList<NodeEntity> GetSubNodesForNode(Int64 nodeId)
        {
            var childrenQuery = from node in NodeQueryable where node.NodeId.HasValue && node.NodeId.Value.Equals(nodeId) select node;
            var children = childrenQuery.ToList();
            return children;
        }
    }
}
