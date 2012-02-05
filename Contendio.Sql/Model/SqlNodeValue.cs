using System;
using System.Globalization;
using System.Linq;
using Contendio.Exceptions;
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
                if (value.Contains("/"))
                    throw new InvalidNameException("The name can't contain the reserved character '/'");

                Entity.Name = value;
                QueryManager.Save(Entity);
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

        public string GetString()
        {
            if(Entity.StringValue != null)
                return Entity.StringValue;

            if(Entity.DateTimeValue.HasValue)
            {
                var dateTime = GetDateTime();
                if (dateTime != null)
                    return dateTime.Value.ToString(CultureInfo.InvariantCulture);
            }

            if (Entity.BinaryValue != null)
                throw new InvalidNodeValueTypeException("Cannot convert a binary value into a string");

            return String.Empty;
        }

        public System.IO.Stream GetStream()
        {
            if (Entity.BinaryValue != null)
            {
                return new MemoryStream(Entity.BinaryValue.ToArray());
            }

            if (Entity.StringValue != null)
            {
                var str = GetString();
                var bytes = System.Text.Encoding.Default.GetBytes(str);
                return new MemoryStream(bytes);
            }

            if (Entity.DateTimeValue.HasValue)
                throw new InvalidNodeValueTypeException("Cannot convet a DateType value into a Stream");

            return null;
        }


        public DateTime? GetDateTime()
        {
            if (Entity.DateTimeValue.HasValue)
            {
                return Entity.DateTimeValue.Value;
            }

            if (Entity.StringValue != null)
            {
                var str = GetString();
                DateTime value;
                if(!DateTime.TryParse(str, out value))
                    throw new InvalidNodeValueTypeException("Cannot convert the string: '" + str + "' into a DateTime object");

                return value;
            }

            if (Entity.BinaryValue != null)
                throw new InvalidNodeValueTypeException("Cannot convet a Binary value into a DateTime object");

            return null;
        }
    }
}
