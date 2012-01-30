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
        public NodeValueEntity Original { get; private set; }
        public SqlContentRepository ContentRepository { get; private set; }
        public IObserverManager ObserverManager { get; private set; }

        public SqlNodeValue()
        {
        }

        public SqlNodeValue(NodeValueEntity original, SqlContentRepository contentRepository, IObserverManager observerManager)
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
                return new SqlNode(Original.ParentNode, ContentRepository, ObserverManager);
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

        public string ValueAsString()
        {
            if (Original.StringValueId.HasValue)
            {
                return Original.StringValue.Value;
            }

            return string.Empty;
        }

        public System.IO.Stream ValueAsStream()
        {
            if (Original.BinaryValueId.HasValue)
            {
                return new MemoryStream(Original.BinaryValue.Value.ToArray());
            }

            return null;
        }
    }
}
