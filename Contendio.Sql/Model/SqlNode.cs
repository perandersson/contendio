﻿using System;
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
        public SqlObserverManager ObserverManager { get; private set; }
        public NodeEntity Entity { get; private set; }


        public SqlNode()
        {
        }

        public SqlNode(NodeEntity entity, SqlContentRepository contentRepository, SqlObserverManager observerManager)
        {
            this.Entity = entity;
            this.ContentRepository = contentRepository;
            this.ObserverManager = observerManager;
        }

        public Guid Id
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

                var parentQuery = from node in ContentRepository.NodeQueryable where node.Id.Equals(Entity.NodeId.Value) select node;
                var parent = parentQuery.FirstOrDefault();

                if (parent == null)
                    return null;

                return new SqlNode(parent, ContentRepository, ObserverManager);
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
                var typeQuery = from nodeType in ContentRepository.NodeTypeQueryable where nodeType.Id.Equals(Entity.NodeTypeId) select nodeType;
                var type = typeQuery.FirstOrDefault();
                return new SqlNodeType(type, ContentRepository);
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
            sqlNode.NodeTypeId = (new SqlEntityFactory(ContentRepository)).GetNodeType(type).Id;
            sqlNode.Path = CalculatePath(false);
            ContentRepository.Save(sqlNode);

            return new SqlNode(sqlNode, ContentRepository, ObserverManager);
        }

        private string CalculatePath(bool appendSlash)
        {
            if (!Entity.NodeId.HasValue)
                return "/";

            string path = this.Name;
            path = Entity.Path + "/" + path;
            return path;
        }

        public INode GetNode(string path)
        {
            string name = path.Replace("/", "");

            var id = Entity.Id;
            var result = (from node in ContentRepository.NodeQueryable where node.NodeId.HasValue && node.NodeId.Value.Equals(id) && node.Name.Equals(name) select node);
            var resultEntity = result.FirstOrDefault();
            if (resultEntity == null)
                return null;

            return new SqlNode(resultEntity, ContentRepository, ObserverManager);
        }
    }
}
