using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Contendio.Sql.Entity;
using Contendio.Event;
using Contendio.Model;

namespace Contendio.Sql.Model
{
    public class SqlNode : INode
    {
        public SqlContentRepository ContentRepository { get; private set; }
        public IObserverManager ObserverManager { get; private set; }
        public NodeEntity Original { get; private set; }


        public SqlNode()
        {
        }

        public SqlNode(NodeEntity original, SqlContentRepository contentRepository, IObserverManager observerManager)
        {
            this.Original = original;
            this.ContentRepository = contentRepository;
            this.ObserverManager = observerManager;
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

        public INode ParentNode
        {
            get
            {
                if (Original.ParentNode == null)
                    return null;

                return new SqlNode(Original.ParentNode, ContentRepository, ObserverManager);
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
                return new SqlNodeType(Original.NodeType, ContentRepository);
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public ICollection<INodeValue> Values
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public ICollection<INode> Children
        {
            get
            {
                throw new NotImplementedException();
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
            get { return Original.Path; }
        }

        public INode AddNode(string name)
        {
            return AddNode(name, "node:node");
        }

        public INode AddNode(string name, string type)
        {
            var sqlNode = new NodeEntity();
            sqlNode.Name = name;
            sqlNode.NodeId = Original.Id;
            sqlNode.NodeTypeId = (new SqlEntityFactory(ContentRepository)).GetNodeType(type).Id;
            sqlNode.Path = CalculatePath(false);
            ContentRepository.Save(sqlNode);

            return new SqlNode(sqlNode, ContentRepository, ObserverManager);
        }

        private string CalculatePath(bool appendSlash)
        {
            if (!Original.NodeId.HasValue)
                return "/";

            string path = this.Name;
            path = Original.Path + "/" + path;
            return path;
        }

        public INode GetNode(string path)
        {
            string name = path.Replace("/", "");

            var id = Original.Id;
            var result = (from node in ContentRepository.NodeQueryable where node.NodeId.HasValue && node.NodeId.Value.Equals(id) && node.Name.Equals(name) select node);
            var resultEntity = result.FirstOrDefault();
            if (resultEntity == null)
                return null;

            return new SqlNode(resultEntity, ContentRepository, ObserverManager);
        }
    }
}
