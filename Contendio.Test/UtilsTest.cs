using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Contendio.Sql;
using System.Data.Linq;
using System.Transactions;
using Contendio.Event.Args;
using Contendio.Event;

namespace Contendio.Test
{
    [TestClass]
    public class UtilsTest
    {
        private const string DatabaseSchema = "contendio";
        private const string ConnectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=" + DatabaseSchema + ";Integrated Security=True";

        [TestMethod]
        public void TestConnectionStringToSchema()
        {
            var result = SqlUtils.SchemaNameFromConnectionString(ConnectionString);
            Assert.AreEqual(DatabaseSchema, result);
        }

    }
}
