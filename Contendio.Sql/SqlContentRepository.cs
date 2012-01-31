using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using Contendio.Model;
using System.Data.Linq.Mapping;
using Contendio.Sql.Mapping;
using Contendio.Sql.Provider;
using Contendio.Sql.Model;
using Contendio.Sql.Entity;

namespace Contendio.Sql
{
    public class SqlContentRepository : IContentRepository
    {
        public string Repository { get; private set; }
        public SqlObserverManager ObserverManager { get; private set; }
        private DataContext DataContext { get; set; }

        public IQueryable<NodeEntity> NodeQueryable
        {
            get { return GetQueryable(nodeTable); }
        }

        public IQueryable<NodeValueEntity> NodeValueQueryable
        {
            get { return GetQueryable(nodeValueTable); }
        }

        public IQueryable<BinaryValueEntity> BinaryValueQueryable
        {
            get { return GetQueryable(binaryValueTable); }
        }

        public IQueryable<StringValueEntity> StringValueQueryable
        {
            get { return GetQueryable(stringValueTable); }
        }

        public IQueryable<NodeTypeEntity> NodeTypeQueryable
        {
            get { return GetQueryable(nodeTypeTable); }
        }

        public INode RootNode
        {
            get
            {
                var node = from n in NodeQueryable where n.NodeId == null select n;
                return new SqlNode(node.FirstOrDefault(), this);
            }

        }

        #region Private members

        private Table<NodeEntity> nodeTable;
        private Table<NodeValueEntity> nodeValueTable;
        private Table<BinaryValueEntity> binaryValueTable;
        private Table<StringValueEntity> stringValueTable;
        private Table<DateValueEntity> dateValueTable;
        private Table<NodeTypeEntity> nodeTypeTable;

        #endregion

        public SqlContentRepository(string repository, string connectionString)
        {            
            this.Repository = repository;
            this.ObserverManager = new SqlObserverManager();

            DataContext = new DataContext(connectionString, new RepositoryMappingSource(Repository));

            this.nodeTypeTable = DataContext.GetTable<NodeTypeEntity>();
            this.nodeTable = DataContext.GetTable<NodeEntity>();
            this.stringValueTable = DataContext.GetTable<StringValueEntity>();
            this.binaryValueTable = DataContext.GetTable<BinaryValueEntity>();
            this.dateValueTable = DataContext.GetTable<DateValueEntity>();
            this.nodeValueTable = DataContext.GetTable<NodeValueEntity>();
        }

        public void Save(NodeEntity node)
        {
            Save(nodeTable, node);
        }

        public void Save(NodeValueEntity nodeValue)
        {
            Save(nodeValueTable, nodeValue);
        }

        public void Save(BinaryValueEntity binaryValue)
        {
            Save(binaryValueTable, binaryValue);
        }

        public void Save(StringValueEntity stringValue)
        {
            Save(stringValueTable, stringValue);
        }

        public void Save(DateValueEntity dateValue)
        {
            Save(dateValueTable, dateValue);
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
            Delete(nodeTable, node);
        }

        public void Delete(NodeValueEntity nodeValue)
        {
            Delete(nodeValueTable, nodeValue);
        }

        public void Delete(BinaryValueEntity binaryValue)
        {
            Delete(binaryValueTable, binaryValue);
        }

        public void Delete(StringValueEntity stringValue)
        {
            Delete(stringValueTable, stringValue);
        }

        public void Delete(NodeTypeEntity nodeType)
        {
            Delete(nodeTypeTable, nodeType);
        }

        public void Delete(DateValueEntity dateValue)
        {
            Delete(dateValueTable, dateValue);
        }

        private void Delete(ITable table, object entity)
        {
            table.DeleteOnSubmit(entity);
            table.Context.SubmitChanges();
        }

        private IQueryable<TSource> GetQueryable<TSource>(ITable<TSource> table) where TSource : class
        {
            IQueryProvider provider = new RepositorySqlProvider(Repository, DataContext.GetCommand, DataContext.ExecuteQuery);
            IQueryable<TSource> source = new RepositoryQueryable<TSource>(Repository, provider, table);
            return source;
        }

        public void Save(INode node)
        {
            throw new NotImplementedException();
        }

        public void Save(INodeValue nodeValue)
        {
            throw new NotImplementedException();
        }

        public void Save(INodeType nodeType)
        {
            throw new NotImplementedException();
        }

        public void Delete(INode node)
        {
            throw new NotImplementedException();
        }

        public void Delete(INodeValue nodeValue)
        {
            throw new NotImplementedException();
        }

        public void Delete(INodeType nodeType)
        {
            throw new NotImplementedException();
        }

        public IQueryManager QueryManager
        {
            get { return new SqlQueryManager(); }
        }
    }
}
