using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Contendio.Sql.Entity;
using System.IO;
using Contendio.Model;

namespace Contendio.Sql.Model
{
    public class SqlNodeValue : INodeValue
    {
        public NodeValueEntity Entity { get; private set; }
        public SqlContentRepository ContentRepository { get; private set; }
        public SqlObserverManager ObserverManager { get; private set; }

        public SqlNodeValue()
        {
        }

        public SqlNodeValue(NodeValueEntity entity, SqlContentRepository contentRepository, SqlObserverManager observerManager)
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

        public INode ParentNode { get; set; }

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

        public string ValueAsString()
        {
            if (!Entity.StringValueId.HasValue)
                return string.Empty;

            var valueQuery = from value in ContentRepository.StringValueQueryable where value.Id.Equals(Entity.StringValueId.Value) select value;
            var result = valueQuery.FirstOrDefault();
            if (result == null)
                return string.Empty;

            return result.Value;
        }

        public System.IO.Stream ValueAsStream()
        {
            if (!Entity.BinaryValueId.HasValue)
                return null;

            var valueQuery = from value in ContentRepository.BinaryValueQueryable where value.Id.Equals(Entity.BinaryValueId.Value) select value;
            var result = valueQuery.FirstOrDefault();
            if (result == null)
                return null;

            return new MemoryStream(result.Value.ToArray());
        }
    }
}
