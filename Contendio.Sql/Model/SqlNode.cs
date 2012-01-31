using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Contendio.Sql.Entity;
using Contendio.Event;
using Contendio.Model;
using System.Transactions;

namespace Contendio.Sql.Model
{
    public class SqlNode : INode
    {
        public SqlContentRepository ContentRepository { get; private set; }
        public SqlObserverManager ObserverManager { get; private set; }
        public SqlQueryManager QueryManager { get; private set; }
        public NodeEntity Entity { get; private set; }

        public SqlNode()
        {
        }

        public SqlNode(NodeEntity entity, SqlContentRepository contentRepository)
        {
            this.Entity = entity;
            this.ContentRepository = contentRepository;
            this.ObserverManager = contentRepository.ObserverManager as SqlObserverManager;
            this.QueryManager = contentRepository.QueryManager as SqlQueryManager;
        }

        public Int64 Id
        {
            get
            {
                return Entity.Id;
            }
            set
            {
                Entity.Id = value;
            }
        }

        public string Name
        {
            get
            {
                return Entity.Name;
            }
            set
            {
                Entity.Name = value;
            }
        }

        public INode ParentNode
        {
            get
            {
                if (!Entity.NodeId.HasValue)
                    return null;

                var parentQuery = from node in QueryManager.NodeQueryable where node.Id.Equals(Entity.NodeId.Value) select node;
                var parent = parentQuery.FirstOrDefault();

                if (parent == null)
                    return null;

                return new SqlNode(parent, ContentRepository);
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public INodeType NodeType
        {
            get
            {
                var typeQuery = from nodeType in QueryManager.NodeTypeQueryable where nodeType.Id.Equals(Entity.NodeTypeId) select nodeType;
                var type = typeQuery.FirstOrDefault();
                return new SqlNodeType(type, ContentRepository);
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public IList<INodeValue> Values
        {
            get
            {
                var values = QueryManager.GetNodeValuesForNode(Entity.Id);
                var results = new List<INodeValue>();
                foreach (var value in values)
                {
                    results.Add(new SqlNodeValue(value, ContentRepository));
                }
                return results;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public IList<INode> Children
        {
            get
            {
                var children = QueryManager.GetSubNodesForNode(Entity.Id);
                var results = new List<INode>();
                foreach (var child in children)
                {
                    results.Add(new SqlNode(child, ContentRepository));
                }
                return results;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public event RepositoryChangeEventHandler OnNodeChanged
        {
            add
            {
                if (ObserverManager != null)
                {
                    ObserverManager.AttachListener(Path, value);
                }
            }
            remove
            {
                if (ObserverManager != null)
                {
                    ObserverManager.DetachListener(value);
                }
            }
        }

        public string Path
        {
            get { return Entity.Path; }
        }

        public INode AddNode(string name)
        {
            return AddNode(name, "node:node");
        }

        public INode AddNode(string name, string type)
        {
            var sqlNode = new NodeEntity();
            sqlNode.Name = name;
            sqlNode.NodeId = Entity.Id;
            sqlNode.NodeTypeId = QueryManager.GetNodeType(type).Id;
            sqlNode.AddedDate = DateTime.Now;
            sqlNode.ChangedDate = DateTime.Now;

            string appendName = Path;
            if (!Entity.NodeId.HasValue)
                appendName = "";
            string path = appendName + "/" + name;

            sqlNode.Path = path;
            QueryManager.Save(sqlNode);

            return new SqlNode(sqlNode, ContentRepository);
        }

        public INode GetNode(string path)
        {
            string name = path.Replace("/", "");

            var id = Entity.Id;
            var result = (from node in QueryManager.NodeQueryable where node.NodeId.HasValue && node.NodeId.Value.Equals(id) && node.Name.Equals(name) select node);
            var resultEntity = result.FirstOrDefault();
            if (resultEntity == null)
                return null;

            var nodeModel = new SqlNode(resultEntity, ContentRepository);
            return nodeModel;
        }

        public void Delete()
        {
            QueryManager.Delete(Entity);
        }

        public void Delete(string path)
        {
            throw new NotImplementedException();
        }

        public INodeValue AddValue(string name, string value)
        {
            return AddValue(name, value, "value:string");
        }

        public INodeValue AddValue(string name, string value, string type)
        {
          //  using (var transaction = new TransactionScope())
          //  {
                var valueEntity = CheckAndDeleteValue(name);
                if (valueEntity == null)
                    valueEntity = CreateNewValueEntity(name, type);

                var stringEntity = new StringValueEntity();
                stringEntity.Value = value;
                QueryManager.Save(stringEntity);

                valueEntity.StringValueId = stringEntity.Id;
                valueEntity.ChangedDate = DateTime.Now;

                QueryManager.Save(valueEntity);
            //    transaction.Complete();
                return new SqlNodeValue(valueEntity, ContentRepository);
           // }
        }

        public INodeValue AddValue(string name, DateTime date)
        {
            return AddValue(name, date, "value:date");
        }

        public INodeValue AddValue(string name, DateTime date, string type)
        {
            using (var transaction = new TransactionScope())
            {
                var valueEntity = CheckAndDeleteValue(name);
                if (valueEntity == null)
                    valueEntity = CreateNewValueEntity(name, type);

                var dateEntity = new DateValueEntity();
                dateEntity.Value = date;
                QueryManager.Save(dateEntity);

                valueEntity.DateValueId = dateEntity.Id;
                valueEntity.ChangedDate = DateTime.Now;

                QueryManager.Save(valueEntity);
                transaction.Complete();
                return new SqlNodeValue(valueEntity, ContentRepository);
            }
        }

        public INodeValue AddValue(string name, byte[] array)
        {
            return AddValue(name, array, "value:binary");
        }

        public INodeValue AddValue(string name, byte[] array, string type)
        {
            using (var transaction = new TransactionScope())
            {
                var valueEntity = CheckAndDeleteValue(name);
                if (valueEntity == null)
                    valueEntity = CreateNewValueEntity(name, type);

                var binaryEntity = new BinaryValueEntity();
                binaryEntity.Value = array;
                QueryManager.Save(binaryEntity);

                valueEntity.BinaryValueId = binaryEntity.Id;
                valueEntity.ChangedDate = DateTime.Now;

                QueryManager.Save(valueEntity);
                transaction.Complete();
                return new SqlNodeValue(valueEntity, ContentRepository);
            }
        }

        private NodeValueEntity CreateNewValueEntity(string name, string type)
        {
            var entity = new NodeValueEntity();
            entity.Name = name;
            entity.NodeId = Entity.Id;
            entity.NodeTypeId = QueryManager.GetNodeType(type).Id;
            entity.AddedDate = DateTime.Now;
            entity.StringValueId = null;
            entity.BinaryValueId = null;
            entity.DateValueId = null;
            return entity;
        }
        
        private NodeValueEntity CheckAndDeleteValue(string name)
        {
            var checkForValue = from nodeValue in QueryManager.NodeValueQueryable where nodeValue.NodeId == Id && nodeValue.Name.Equals(name) select nodeValue;
            var valueEntity = checkForValue.FirstOrDefault();
            if (valueEntity != null)
            {
                if (valueEntity.StringValueId.HasValue)
                    QueryManager.DeleteStringValueById(valueEntity.StringValueId.Value);
                else if(valueEntity.BinaryValueId.HasValue)
                    QueryManager.DeleteBinaryValueById(valueEntity.BinaryValueId.Value);
                else if(valueEntity.DateValueId.HasValue)
                    QueryManager.DeleteDateValueById(valueEntity.DateValueId.Value);
            }

            return valueEntity;
        }

    }
}
