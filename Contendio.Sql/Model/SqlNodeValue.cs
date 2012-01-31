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
        public SqlQueryManager QueryManager { get; private set; }

        public SqlNodeValue()
        {
        }

        public SqlNodeValue(NodeValueEntity entity, SqlContentRepository contentRepository)
        {
            this.Entity = entity;
            this.ContentRepository = contentRepository;
            this.ObserverManager = contentRepository.ObserverManager as SqlObserverManager;
            this.QueryManager = contentRepository.QueryManager as SqlQueryManager;
        }

        public long Id
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
                var typeQuery = from nodeType in QueryManager.NodeTypeQueryable where nodeType.Id.Equals(Entity.NodeTypeId) select nodeType;
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

            var result = QueryManager.GetStringValueById(Entity.StringValueId.Value);
            if (result == null)
                return string.Empty;

            return result.Value;
        }

        public System.IO.Stream ValueAsStream()
        {
            if (!Entity.BinaryValueId.HasValue)
                return null;

            var result = QueryManager.GetBinaryValueById(Entity.BinaryValueId.Value);
            if (result == null)
                return null;

            return new MemoryStream(result.Value.ToArray());
        }


        public DateTime? ValueAsDate()
        {
            if (!Entity.DateValueId.HasValue)
                return null;

            var result = QueryManager.GetDateValueById(Entity.DateValueId.Value);
            if (result == null)
                return null;

            return result.Value;
        }
    }
}
