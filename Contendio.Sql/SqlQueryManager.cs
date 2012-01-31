using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Contendio.Model;

namespace Contendio.Sql
{
    public class SqlQueryManager : IQueryManager
    {
        public IQueryable<INode> Nodes
        {
            get { throw new NotImplementedException(); }
        }

        public IQueryable<INodeValue> NodeValues
        {
            get { throw new NotImplementedException(); }
        }

        public IQueryable<INodeType> NodeTypes
        {
            get { throw new NotImplementedException(); }
        }

        public void DeleteBinaryValueById(long id)
        {
        }

        public void DeleteDateValueById(long id)
        {
        }

        public void DeleteStringValueById(long id)
        {
        }
    }
}
