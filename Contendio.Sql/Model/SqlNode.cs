using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Contendio.Exceptions;
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
                var name = Entity.Name;
                if (name == null)
                    return string.Empty;

                return name;
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
            get
            {
                return CalculatePath();
            }
        }

        private string CalculatePath()
        {
            //"/" + Name + "/" + Name;
            if (!Entity.NodeId.HasValue)
                return "/";

            var sb = new StringBuilder();
            var parent = ParentNode as SqlNode;
            if (parent != null && parent.Entity.NodeId.HasValue)
                sb.Append(parent.Path);

            sb.Append("/");
            sb.Append(Name);
            return sb.ToString();
        }

        public INode AddNode(string name)
        {
            return AddNode(name, "node:node");
        }

        public INode AddNode(string name, string type)
        {
            ValidateRelativeValue(name, "name");

            var names = name.Split('/');
            if (names.Length > 1)
            {
                INode lastNode = AddNode(names[0], type);
                for (int i = 1; i < names.Length; ++i)
                {
                    lastNode = lastNode.AddNode(names[i], type);
                }
                return lastNode;
            }

            var sqlNode = new NodeEntity();
            sqlNode.Name = name;
            sqlNode.NodeId = Entity.Id;
            sqlNode.NodeTypeId = QueryManager.GetNodeType(type).Id;
            sqlNode.AddedDate = DateTime.Now;
            sqlNode.ChangedDate = DateTime.Now;
            sqlNode.Index = GetNumChildren();
            
            QueryManager.Save(sqlNode);

            return new SqlNode(sqlNode, ContentRepository);
        }

        private int GetNumChildren()
        {
            return Children.Count;
        }

        public INode GetNode(string path)
        {
            ValidateRelativeValue(path, "path");

            var paths = path.Split('/');
            if (paths.Length > 1)
            {
                var node = GetNode(paths[0]);
                for(int i = 1; i < paths.Length; ++i)
                {
                    node = node.GetNode(paths[i]);
                }

                return node;
            }

            var id = Entity.Id;
            var result = (from node in QueryManager.NodeQueryable where node.NodeId.HasValue && node.NodeId.Value.Equals(id) && node.Name.Equals(path) select node);
            var resultEntity = result.FirstOrDefault();
            if (resultEntity == null)
                return null;

            var nodeModel = new SqlNode(resultEntity, ContentRepository);
            return nodeModel;
        }

        public void MoveBefore(INode node)
        {
            ValidateNodeAsRootNode(this);
            ValidateNodeAsRootNode(node);

            if (IsNodeSame(node))
                return;

            var thisIndex = Entity.Index;
            var otherIndex = ((SqlNode) node).Entity.Index;

            var movedItems = ArrayUtils.MoveItemBefore(ParentNode.Children, thisIndex, otherIndex);

            for (int i = 0; i < movedItems.Count; ++i)
            {
                SetIndexToNode(movedItems[i] as SqlNode, i);
            }
        }

        private bool IsNodeSame(INode node)
        {
            if (node == null)
                return false;

            return Entity.Id == node.Id;
        }

        private void SetIndexToNode(SqlNode node, int index)
        {
            node.Entity.Index = index;
            QueryManager.Save(node.Entity);
        }

        public void MoveAfter(INode node)
        {
            ValidateNodeAsRootNode(this);
            ValidateNodeAsRootNode(node);
            ValidateNodeMovementCirculation(node as SqlNode);

            if (IsNodeSame(node))
                return;

            throw new NotImplementedException();
        }

        private void ValidateNodeMovementCirculation(SqlNode node)
        {
            if(IsParentOf(node))
                throw new CannotMoveNodeException("Cannot move supplied node because the movement would create circulated node hierarchy");
        }

        private void ValidateNodeAsRootNode(INode node)
        {
            if (node == null)
                throw new ArgumentNullException("node");

            if(node.IsRootNode())
                throw new CannotMoveNodeException("Cannot move root node");
        }

        public void Delete()
        {
            using (var transaction = new TransactionScope())
            {
                DeleteAllRelativeNodesAndValues();
                QueryManager.Delete(Entity);
                transaction.Complete();
            }
        }

        private void DeleteAllRelativeNodesAndValues()
        {
            foreach(var child in Children)
            {
                child.Delete();
            }
        }

        public void Delete(string path)
        {
            ValidateRelativeValue(path, "path");
            var paths = path.Split('/');
            if (paths.Length > 1)
            {
                var node = GetNode(paths[0]);
                for (int i = 1; i < paths.Length; ++i)
                {
                    node = node.GetNode(paths[i]);
                }

                node.Delete();
                return;
            }

            GetNode(path).Delete();
        }

        private void ValidateRelativeValue(string path, string variableName)
        {
            ValidateNonEmpty(path, variableName);
            if(path[0] == '/')
                throw new ArgumentException("The '" + variableName + "' argument is invalid. Only relative values are allowed.");
        }

        private void ValidateNonEmpty(string value, string valueName)
        {
            if(value.Length == 0)
                throw new ArgumentException("The '" + valueName + "' argument cannot be empty");
        }
        
        private void ValidateNonEmpty(byte[] value, string valueName)
        {
            if (value == null)
                throw new ArgumentNullException(valueName);

            if (value.Length == 0)
                throw new ArgumentException("The '" + valueName + "' argument cannot be empty");
        }

        public INodeValue AddValue(string name, string value)
        {
            return AddValue(name, value, "value:string");
        }

        public INodeValue AddValue(string name, string value, string type)
        {
            ValidateNonEmpty(name, "name");
            ValidateNonEmpty(value, "value");

            var valueEntity = GetNodeValueByName(name) ?? CreateNewValueEntity(name, type);

            valueEntity.StringValue = value;
            valueEntity.DateTimeValue = null;
            valueEntity.BinaryValue = null;
            valueEntity.ChangedDate = DateTime.Now;

            QueryManager.Save(valueEntity);
            return new SqlNodeValue(valueEntity, ContentRepository);
        }

        public INodeValue AddValue(string name, DateTime date)
        {
            return AddValue(name, date, "value:date");
        }

        public INodeValue AddValue(string name, DateTime date, string type)
        {
            ValidateNonEmpty(name, "name");
            ValidateNonEmpty(type, "type");

            var valueEntity = GetNodeValueByName(name) ?? CreateNewValueEntity(name, type);

            valueEntity.StringValue = null;
            valueEntity.DateTimeValue = date;
            valueEntity.BinaryValue = null;
            valueEntity.ChangedDate = DateTime.Now;

            QueryManager.Save(valueEntity);
            return new SqlNodeValue(valueEntity, ContentRepository);
        }

        public INodeValue AddValue(string name, byte[] array)
        {
            return AddValue(name, array, "value:binary");
        }

        public INodeValue AddValue(string name, byte[] array, string type)
        {
            ValidateNonEmpty(name, "name");
            ValidateNonEmpty(array, "array");
            ValidateNonEmpty(type, "type");

            var valueEntity = GetNodeValueByName(name) ?? CreateNewValueEntity(name, type);

            valueEntity.StringValue = null;
            valueEntity.DateTimeValue = null;
            valueEntity.BinaryValue = array;
            valueEntity.ChangedDate = DateTime.Now;

            QueryManager.Save(valueEntity);
            return new SqlNodeValue(valueEntity, ContentRepository);
        }

        public bool IsParentOf(INode node)
        {
            if (node == null)
                return false;

            if (node.IsRootNode())
                return false;

            if (Id.Equals(node.Id))
                return false;

            var parent = node.ParentNode;
            do
            {
                if (parent.Id == Id)
                    return true;
            } while ((parent = parent.ParentNode) != null);

            return false;
        }

        public bool IsRootNode()
        {
            return Entity.NodeId.HasValue == false;
        }

        public bool IsSiblingOf(INode node)
        {
            if (node == null)
                return false;

            if (Id.Equals(node.Id))
                return false;

            if (IsRootNode())
                return false;

            var sqlNode = node as SqlNode;
            if (sqlNode == null)
                return false;

            if(sqlNode.IsRootNode())
                return false;

            return sqlNode.Entity.NodeId.Value.Equals(Entity.NodeId.Value);
        }

        public bool IsChildOf(INode node)
        {
            throw new NotImplementedException();
        }

        private NodeValueEntity CreateNewValueEntity(string name, string type)
        {
            var entity = new NodeValueEntity();
            entity.Name = name;
            entity.NodeId = Entity.Id;
            entity.NodeTypeId = QueryManager.GetNodeType(type).Id;
            entity.AddedDate = DateTime.Now;
            entity.StringValue = null;
            entity.BinaryValue = null;
            entity.DateTimeValue = null;
            return entity;
        }

        private NodeValueEntity GetNodeValueByName(string name)
        {
            var checkForValue = from nodeValue in QueryManager.NodeValueQueryable where nodeValue.NodeId == Id && nodeValue.Name.Equals(name) select nodeValue;
            var valueEntity = checkForValue.FirstOrDefault();
            return valueEntity;
        }
        
    }
}
