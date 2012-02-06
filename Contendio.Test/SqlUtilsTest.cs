using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Contendio.Sql;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Contendio.Test
{
    [TestClass]
    public class SqlUtilsTest
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
