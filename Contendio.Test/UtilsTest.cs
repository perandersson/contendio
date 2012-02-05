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

        [TestMethod]
        public void TestMoveBefore()
        {
            var items = new List<string>() { "one", "two", "three", "four", "five" };
            var movedItems = ArrayUtils.MoveItemBefore(items, 0, 1);

            Assert.AreEqual(items[0], movedItems[0]);
            Assert.AreEqual(items[1], movedItems[1]);
            Assert.AreEqual(items[2], movedItems[2]);
            Assert.AreEqual(items[3], movedItems[3]);
            Assert.AreEqual(items[4], movedItems[4]);

            movedItems = ArrayUtils.MoveItemBefore(items, 0, 2);

            Assert.AreEqual(items[1], movedItems[0]);
            Assert.AreEqual(items[0], movedItems[1]);
            Assert.AreEqual(items[2], movedItems[2]);
            Assert.AreEqual(items[3], movedItems[3]);
            Assert.AreEqual(items[4], movedItems[4]);

            movedItems = ArrayUtils.MoveItemBefore(items, 0, 3);

            Assert.AreEqual(items[1], movedItems[0]);
            Assert.AreEqual(items[2], movedItems[1]);
            Assert.AreEqual(items[0], movedItems[2]);
            Assert.AreEqual(items[3], movedItems[3]);
            Assert.AreEqual(items[4], movedItems[4]);

            movedItems = ArrayUtils.MoveItemBefore(items, 0, 4);

            Assert.AreEqual(items[1], movedItems[0]);
            Assert.AreEqual(items[2], movedItems[1]);
            Assert.AreEqual(items[3], movedItems[2]);
            Assert.AreEqual(items[0], movedItems[3]);
            Assert.AreEqual(items[4], movedItems[4]);

            movedItems = ArrayUtils.MoveItemBefore(items, 1, 0);

            Assert.AreEqual(items[1], movedItems[0]);
            Assert.AreEqual(items[0], movedItems[1]);
            Assert.AreEqual(items[2], movedItems[2]);
            Assert.AreEqual(items[3], movedItems[3]);
            Assert.AreEqual(items[4], movedItems[4]);

            movedItems = ArrayUtils.MoveItemBefore(items, 1, 1);

            Assert.AreEqual(items[0], movedItems[0]);
            Assert.AreEqual(items[1], movedItems[1]);
            Assert.AreEqual(items[2], movedItems[2]);
            Assert.AreEqual(items[3], movedItems[3]);
            Assert.AreEqual(items[4], movedItems[4]);

            movedItems = ArrayUtils.MoveItemBefore(items, 1, 2);

            Assert.AreEqual(items[0], movedItems[0]);
            Assert.AreEqual(items[1], movedItems[1]);
            Assert.AreEqual(items[2], movedItems[2]);
            Assert.AreEqual(items[3], movedItems[3]);
            Assert.AreEqual(items[4], movedItems[4]);

            movedItems = ArrayUtils.MoveItemBefore(items, 1, 3);

            Assert.AreEqual(items[0], movedItems[0]);
            Assert.AreEqual(items[2], movedItems[1]);
            Assert.AreEqual(items[1], movedItems[2]);
            Assert.AreEqual(items[3], movedItems[3]);
            Assert.AreEqual(items[4], movedItems[4]);

            movedItems = ArrayUtils.MoveItemBefore(items, 1, 4);

            Assert.AreEqual(items[0], movedItems[0]);
            Assert.AreEqual(items[2], movedItems[1]);
            Assert.AreEqual(items[3], movedItems[2]);
            Assert.AreEqual(items[1], movedItems[3]);
            Assert.AreEqual(items[4], movedItems[4]);

            movedItems = ArrayUtils.MoveItemBefore(items, 4, 4);

            Assert.AreEqual(items[0], movedItems[0]);
            Assert.AreEqual(items[1], movedItems[1]);
            Assert.AreEqual(items[2], movedItems[2]);
            Assert.AreEqual(items[3], movedItems[3]);
            Assert.AreEqual(items[4], movedItems[4]);

            movedItems = ArrayUtils.MoveItemBefore(items, 4, 3);

            Assert.AreEqual(items[0], movedItems[0]);
            Assert.AreEqual(items[1], movedItems[1]);
            Assert.AreEqual(items[2], movedItems[2]);
            Assert.AreEqual(items[4], movedItems[3]);
            Assert.AreEqual(items[3], movedItems[4]);
        }

        [TestMethod]
        public void ArrayUtilMoveAfter()
        {
            var items = new List<string>() { "one", "two", "three", "four", "five" };
            var movedItems = ArrayUtils.MoveItemAfter(items, 0, 0);

            Assert.AreEqual(items[0], movedItems[0]);
            Assert.AreEqual(items[1], movedItems[1]);
            Assert.AreEqual(items[2], movedItems[2]);
            Assert.AreEqual(items[3], movedItems[3]);
            Assert.AreEqual(items[4], movedItems[4]);

            movedItems = ArrayUtils.MoveItemAfter(items, 0, 1);
            Assert.AreEqual(items[1], movedItems[0]);
            Assert.AreEqual(items[0], movedItems[1]);
            Assert.AreEqual(items[2], movedItems[2]);
            Assert.AreEqual(items[3], movedItems[3]);
            Assert.AreEqual(items[4], movedItems[4]);

            movedItems = ArrayUtils.MoveItemAfter(items, 0, 2);
            Assert.AreEqual(items[1], movedItems[0]);
            Assert.AreEqual(items[2], movedItems[1]);
            Assert.AreEqual(items[0], movedItems[2]);
            Assert.AreEqual(items[3], movedItems[3]);
            Assert.AreEqual(items[4], movedItems[4]);

            movedItems = ArrayUtils.MoveItemAfter(items, 0, 3);
            Assert.AreEqual(items[1], movedItems[0]);
            Assert.AreEqual(items[2], movedItems[1]);
            Assert.AreEqual(items[3], movedItems[2]);
            Assert.AreEqual(items[0], movedItems[3]);
            Assert.AreEqual(items[4], movedItems[4]);

            movedItems = ArrayUtils.MoveItemAfter(items, 0, 4);
            Assert.AreEqual(items[1], movedItems[0]);
            Assert.AreEqual(items[2], movedItems[1]);
            Assert.AreEqual(items[3], movedItems[2]);
            Assert.AreEqual(items[4], movedItems[3]);
            Assert.AreEqual(items[0], movedItems[4]);

            movedItems = ArrayUtils.MoveItemAfter(items, 4, 0);
            Assert.AreEqual(items[4], movedItems[0]);
            Assert.AreEqual(items[0], movedItems[1]);
            Assert.AreEqual(items[1], movedItems[2]);
            Assert.AreEqual(items[2], movedItems[3]);
            Assert.AreEqual(items[3], movedItems[4]);

            movedItems = ArrayUtils.MoveItemAfter(items, 4, 4);
            Assert.AreEqual(items[0], movedItems[0]);
            Assert.AreEqual(items[1], movedItems[1]);
            Assert.AreEqual(items[2], movedItems[2]);
            Assert.AreEqual(items[3], movedItems[3]);
            Assert.AreEqual(items[4], movedItems[4]);
        }

    }
}
