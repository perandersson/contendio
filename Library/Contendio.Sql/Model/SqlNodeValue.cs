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

        public NodeValueType ValueType
        {
            get
            {
                if (Entity.StringValue != null)
                    return NodeValueType.String;
                else if (Entity.IntValue.HasValue)
                    return NodeValueType.Integer;
                else if (Entity.BoolValue.HasValue)
                    return NodeValueType.Boolean;
                else if (Entity.DateTimeValue.HasValue)
                    return NodeValueType.DateTime;
                else
                    return NodeValueType.Binary;
            }
        }

        public string GetString()
        {
            if(Entity.StringValue != null)
                return Entity.StringValue;

            if(Entity.DateTimeValue.HasValue)
            {
                var dateTime = GetDateTime();
                return dateTime.ToString(CultureInfo.InvariantCulture);
            }

            if(Entity.IntValue.HasValue)
            {
                var value = GetInteger();
                return value.ToString();
            }

            if (Entity.BoolValue.HasValue)
            {
                return Entity.BoolValue.Value.ToString();
            }

            if (Entity.BinaryValue != null)
                throw new InvalidNodeValueTypeException("Cannot convert a binary value into a string");

            throw new InvalidNodeValueTypeException("Cannot convert hte current value into a string object type");
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
                throw new InvalidNodeValueTypeException("Cannot convet a DateType value type into a Stream type");

            if (Entity.BoolValue.HasValue)
                throw new InvalidNodeValueTypeException("Cannot convert a Boolean value type into a Stream type");

            throw new InvalidNodeValueTypeException("Cannot convert hte current value into a stream object type");
        }


        public DateTime GetDateTime()
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
                throw new InvalidNodeValueTypeException("Cannot convet a Binary value type into a DateTime type");

            if (Entity.BoolValue.HasValue)
                throw new InvalidNodeValueTypeException("Cannot convert a Boolean value type into a DateTime type");

            throw new InvalidNodeValueTypeException("Cannot convet a nothing into a DateTime object");
        }

        public int GetInteger()
        {
            if(Entity.IntValue.HasValue)
            {
                return Entity.IntValue.Value;
            }

            if(Entity.StringValue != null)
            {
                var str = GetString();
                int value = 0;
                if (!int.TryParse(str, out value))
                    throw new InvalidNodeValueTypeException("Cannot convert the string: '" + str + "' into a int object");

                return value;
            }

            if (Entity.BoolValue.HasValue)
            {
                return GetBool() ? 1 : 0;
            }

            if (Entity.BinaryValue != null)
                throw new InvalidNodeValueTypeException("Cannot convet a Binary value into an int object");

            throw new InvalidNodeValueTypeException("Cannot convet a nothing into an int object");
        }

        public bool GetBool()
        {
            if (Entity.BoolValue.HasValue)
            {
                return Entity.BoolValue.Value;
            }

            if (Entity.IntValue.HasValue)
            {
                var intValue = GetInteger();
                return intValue > 0;
            }

            if (Entity.StringValue != null)
            {
                var strValue = GetString().ToLower();
                if ("true".Equals(strValue) || "yes".Equals(strValue))
                    return true;
                else if ("false".Equals(strValue) || "no".Equals(strValue))
                    return false;

                throw new InvalidNodeValueTypeException("Cannot convert the string value into a boolean object type");
                
            }

            if (Entity.BinaryValue != null)
                throw new InvalidNodeValueTypeException("Cannot convet a Binary value into an boolean type");

            throw new InvalidNodeValueTypeException("Cannot convet a nothing into an boolean type");
        }
    }
}
