using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Contendio.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Contendio.Model;

namespace Contendio.Test.SqlNodeValue
{
    [TestClass]
    public class DateValueTest : BaseTest
    {
        [TestInitialize]
        public void Initialize()
        {
            ContentInstall.Install();
        }

        [TestCleanup]
        public void Cleanup()
        {
            ContentInstall.Uninstall();
        }

        [TestMethod]
        public void SqlNodeValue_AddDateValue()
        {
            var contentRepository = ContentRepository;
            var rootNode = GetRootNode(contentRepository);
            var now = DateTime.Now;
            var value1 = rootNode.AddValue("value1", now);

            Assert.IsNotNull(value1);
            Assert.AreEqual("value1", value1.Name);
            Assert.IsTrue(DateAreEqual(now, value1.GetDateTime()));
        }

        [TestMethod]
        public void SqlNodeValue_GetDateValue()
        {
            var contentRepository = ContentRepository;
            var rootNode = GetRootNode(contentRepository);
            var now = DateTime.Now;
            rootNode.AddValue("value1", now);
            var value1 = rootNode.GetValue("value1");

            Assert.IsNotNull(value1);
            Assert.AreEqual("value1", value1.Name);
            Assert.IsTrue(DateAreEqual(now, value1.GetDateTime()));
        }

        [TestMethod]
        public void SqlNodeValue_GetDateValues_GetAsValueList()
        {
            var contentRepository = ContentRepository;
            var rootNode = GetRootNode(contentRepository);
            var now = DateTime.Now;
            rootNode.AddValue("value1", now);
            var values = rootNode.Values;

            Assert.IsNotNull(values);
            Assert.AreEqual(1, values.Count);

            Assert.AreEqual("value1", values[0].Name);
            Assert.IsTrue(DateAreEqual(now, values[0].GetDateTime()));
        }

        [TestMethod]
        public void SqlNodeValue_AddDateValues_GetAsDate()
        {
            var contentRepository = ContentRepository;
            var rootNode = GetRootNode(contentRepository);
            var date1 = new DateTime(2012, 11, 10);
            var date2 = new DateTime(2011, 5, 29);
            var date3 = new DateTime(2010, 9, 6);
            var value1 = rootNode.AddValue("value1", date1);
            var value2 = rootNode.AddValue("value2", date2);
            var value3 = rootNode.AddValue("value3", date3);

            Assert.IsNotNull(value1);
            Assert.AreEqual("value1", value1.Name);
            Assert.IsTrue(DateAreEqual(date1, value1.GetDateTime()));

            Assert.IsNotNull(value2);
            Assert.AreEqual("value2", value2.Name);
            Assert.IsTrue(DateAreEqual(date2, value2.GetDateTime()));

            Assert.IsNotNull(value3);
            Assert.AreEqual("value3", value3.Name);
            Assert.IsTrue(DateAreEqual(date3, value3.GetDateTime()));
        }

        [TestMethod]
        public void SqlNodeValue_GetDateValues_GetAsDate()
        {
            var contentRepository = ContentRepository;
            var rootNode = GetRootNode(contentRepository);
            var date1 = new DateTime(2012, 11, 10);
            var date2 = new DateTime(2011, 5, 29);
            var date3 = new DateTime(2010, 9, 6);
            rootNode.AddValue("value1", date1);
            rootNode.AddValue("value2", date2);
            rootNode.AddValue("value3", date3);
            var value1 = rootNode.GetValue("value1");
            var value2 = rootNode.GetValue("value2");
            var value3 = rootNode.GetValue("value3");

            Assert.IsNotNull(value1);
            Assert.AreEqual("value1", value1.Name);
            Assert.IsTrue(DateAreEqual(date1, value1.GetDateTime()));

            Assert.IsNotNull(value2);
            Assert.AreEqual("value2", value2.Name);
            Assert.IsTrue(DateAreEqual(date2, value2.GetDateTime()));

            Assert.IsNotNull(value3);
            Assert.AreEqual("value3", value3.Name);
            Assert.IsTrue(DateAreEqual(date3, value3.GetDateTime()));
        }

        [TestMethod]
        public void SqlNodeValue_AddDateValues_GetAsValueList()
        {
            var contentRepository = ContentRepository;
            var rootNode = GetRootNode(contentRepository);
            var date1 = new DateTime(2012, 11, 10);
            var date2 = new DateTime(2011, 5, 29);
            var date3 = new DateTime(2010, 9, 6);
            rootNode.AddValue("value1", date1);
            rootNode.AddValue("value2", date2);
            rootNode.AddValue("value3", date3);
            var values = rootNode.Values;

            Assert.IsNotNull(values);
            Assert.AreEqual(3, values.Count);

            Assert.AreEqual("value1", values[0].Name);
            Assert.IsTrue(DateAreEqual(date1, values[0].GetDateTime()));

            Assert.AreEqual("value2", values[1].Name);
            Assert.IsTrue(DateAreEqual(date2, values[1].GetDateTime()));

            Assert.AreEqual("value3", values[2].Name);
            Assert.IsTrue(DateAreEqual(date3, values[2].GetDateTime()));
        }

        [TestMethod]
        public void SqlNodeValue_AddDateValuesInTwoParents_GetAsDate()
        {
            var contentRepository = ContentRepository;
            var rootNode = GetRootNode(contentRepository);
            var childNode1 = rootNode.GetNode("childNode1");
            var childNode2 = rootNode.GetNode("childNode2");

            var date1 = new DateTime(2012, 11, 10);
            var date2 = new DateTime(2011, 5, 29);
            var value1 = childNode1.AddValue("value1", date1);
            var value2 = childNode2.AddValue("value2", date2);

            Assert.IsNotNull(value1);
            Assert.AreEqual("value1", value1.Name);
            Assert.IsTrue(DateAreEqual(date1, value1.GetDateTime()));

            Assert.IsNotNull(value2);
            Assert.AreEqual("value2", value2.Name);
            Assert.IsTrue(DateAreEqual(date2, value2.GetDateTime()));
        }

        [TestMethod]
        public void SqlNodeValue_GetDateValuesInTwoParents_GetAsDate()
        {
            var contentRepository = ContentRepository;
            var rootNode = GetRootNode(contentRepository);
            var childNode1 = rootNode.GetNode("childNode1");
            var childNode2 = rootNode.GetNode("childNode2");
            var date1 = new DateTime(2012, 11, 10);
            var date2 = new DateTime(2011, 5, 29);
            childNode1.AddValue("value1", date1);
            childNode2.AddValue("value2", date2);

            var value1 = childNode1.GetValue("value1");
            var value2 = childNode2.GetValue("value2");

            Assert.IsNotNull(value1);
            Assert.AreEqual("value1", value1.Name);
            Assert.IsTrue(DateAreEqual(date1, value1.GetDateTime()));

            Assert.IsNotNull(value2);
            Assert.AreEqual("value2", value2.Name);
            Assert.IsTrue(DateAreEqual(date2, value2.GetDateTime()));
        }

        [TestMethod]
        public void SqlNodeValue_AddDateValuesInTwoParents_GetAsDateFromValues()
        {
            var contentRepository = ContentRepository;
            var rootNode = GetRootNode(contentRepository);
            var childNode1 = rootNode.GetNode("childNode1");
            var childNode2 = rootNode.GetNode("childNode2");
            var date1 = new DateTime(2012, 11, 10);
            var date2 = new DateTime(2011, 5, 29);
            childNode1.AddValue("value1", date1);
            childNode2.AddValue("value2", date2);

            var values1 = childNode1.Values;
            var values2 = childNode2.Values;

            Assert.IsNotNull(values1);
            Assert.AreEqual(1, values1.Count);

            Assert.IsNotNull(values2);
            Assert.AreEqual(1, values2.Count);

            Assert.AreEqual("value1", values1[0].Name);
            Assert.IsTrue(DateAreEqual(date1, values1[0].GetDateTime()));

            Assert.AreEqual("value2", values2[0].Name);
            Assert.AreEqual(date2, values2[0].GetDateTime());
            Assert.IsTrue(DateAreEqual(date2, values2[0].GetDateTime()));
        }

        [TestMethod]
        public void SqlNodeValue_AddValueString_GetAsDate()
        {
            var contentRepository = ContentRepository;
            var rootNode = GetRootNode(contentRepository);
            var now = DateTime.Now;
            var value = rootNode.AddValue("date1", now.ToString(CultureInfo.InvariantCulture));

            Assert.IsNotNull(value);
            Assert.IsTrue(DateAreEqual(now, value.GetDateTime()));
        }

        [TestMethod]
        public void SqlNodeValue_GetValueString_GetAsDate()
        {
            var contentRepository = ContentRepository;
            var rootNode = GetRootNode(contentRepository);
            var now = DateTime.Now;
            rootNode.AddValue("date1", now.ToString(CultureInfo.InvariantCulture));
            var value = rootNode.GetValue("date1");

            Assert.IsNotNull(value);
            Assert.IsTrue(DateAreEqual(now, value.GetDateTime()));
        }

        [TestMethod]
        public void SqlNodeValue_AddAndReplaceDate_GetAsValueList()
        {
            var contentRepository = ContentRepository;
            var rootNode = GetRootNode(contentRepository);
            var date1 = new DateTime(2012, 11, 10);
            var date2 = new DateTime(2011, 5, 29);
            var date3 = new DateTime(2010, 9, 6);
            rootNode.AddValue("value1", date1);
            rootNode.AddValue("value2", date2);
            rootNode.AddValue("value1", date3);

            var values = rootNode.Values;
            Assert.AreEqual(2, values.Count);

            Assert.AreEqual("value1", values[0].Name);
            Assert.IsTrue(DateAreEqual(date3, values[0].GetDateTime()));

            Assert.AreEqual("value2", values[1].Name);
            Assert.IsTrue(DateAreEqual(date2, values[1].GetDateTime()));
            
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void SqlNodeValue_AddInvalidDateName()
        {
            var contentRepository = ContentRepository;
            var rootNode = GetRootNode(contentRepository);
            rootNode.AddValue("/value1", DateTime.Now);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidNodeValueTypeException))]
        public void SqlNodeValue_GetInvalidDateTime()
        {
            var contentRepository = ContentRepository;
            var rootNode = GetRootNode(contentRepository);
            var value = rootNode.AddValue("value1", "stringvalue");
            value.GetDateTime();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidNodeValueTypeException))]
        public void SqlNodeValue_GetInvalidDateFromInt()
        {
            var contentRepository = ContentRepository;
            var rootNode = GetRootNode(contentRepository);
            var value = rootNode.AddValue("value1", 100);
            value.GetDateTime();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidNodeValueTypeException))]
        public void SqlNodeValue_GetInvalidDateFromBinary()
        {
            var contentRepository = ContentRepository;
            var rootNode = GetRootNode(contentRepository);
            var value = rootNode.AddValue("value1", new byte[] {1, 2, 3, 4, 5});
            value.GetDateTime();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidNodeValueTypeException))]
        public void SqlNodeValue_GetInvalidDateFromBool()
        {
            var contentRepository = ContentRepository;
            var rootNode = GetRootNode(contentRepository);
            var value = rootNode.AddValue("value1", true);
            value.GetDateTime();
        }

        [TestMethod]
        public void SqlNodeValue_AddValueType_AsDate()
        {
            var contentRepository = ContentRepository;
            var rootNode = GetRootNode(contentRepository);
            var value = rootNode.AddValue("value1", DateTime.Now);
            Assert.AreEqual(NodeValueType.DateTime, value.ValueType);
        }

        [TestMethod]
        public void SqlNodeValue_GetValueType_AsDate()
        {
            var contentRepository = ContentRepository;
            var rootNode = GetRootNode(contentRepository);
            rootNode.AddValue("value1", DateTime.Now);
            var value = rootNode.GetValue("value1");
            Assert.AreEqual(NodeValueType.DateTime, value.ValueType);
        }
    }
}
