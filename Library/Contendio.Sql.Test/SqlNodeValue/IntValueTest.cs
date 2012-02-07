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
    public class IntValueTest : BaseTest
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
        public void SqlNodeValue_AddIntValue()
        {
            var contentRepository = ContentRepository;
            var rootNode = GetRootNode(contentRepository);
            var value1 = rootNode.AddValue("value1", 10);

            Assert.IsNotNull(value1);
            Assert.AreEqual("value1", value1.Name);
            Assert.AreEqual(10, value1.GetInteger());
        }

        [TestMethod]
        public void SqlNodeValue_GetIntValue()
        {
            var contentRepository = ContentRepository;
            var rootNode = GetRootNode(contentRepository);
            rootNode.AddValue("value1", 10);
            var value1 = rootNode.GetValue("value1");

            Assert.IsNotNull(value1);
            Assert.AreEqual("value1", value1.Name);
            Assert.AreEqual(10, value1.GetInteger());
        }

        [TestMethod]
        public void SqlNodeValue_GetIntValues_GetAsValueList()
        {
            var contentRepository = ContentRepository;
            var rootNode = GetRootNode(contentRepository);
            rootNode.AddValue("value1", 10);
            var values = rootNode.Values;

            Assert.IsNotNull(values);
            Assert.AreEqual(1, values.Count);

            Assert.AreEqual("value1", values[0].Name);
            Assert.AreEqual(10, values[0].GetInteger());
        }

        [TestMethod]
        public void SqlNodeValue_AddIntValues_GetAsInteger()
        {
            var contentRepository = ContentRepository;
            var rootNode = GetRootNode(contentRepository);
            var value1 = rootNode.AddValue("value1", 10);
            var value2 = rootNode.AddValue("value2", 20);
            var value3 = rootNode.AddValue("value3", 30);

            Assert.IsNotNull(value1);
            Assert.AreEqual("value1", value1.Name);
            Assert.AreEqual(10, value1.GetInteger());

            Assert.IsNotNull(value2);
            Assert.AreEqual("value2", value2.Name);
            Assert.AreEqual(20, value2.GetInteger());

            Assert.IsNotNull(value3);
            Assert.AreEqual("value3", value3.Name);
            Assert.AreEqual(30, value3.GetInteger());
        }

        [TestMethod]
        public void SqlNodeValue_GetIntValues_GetAsInteger()
        {
            var contentRepository = ContentRepository;
            var rootNode = GetRootNode(contentRepository);
            rootNode.AddValue("value1", 10);
            rootNode.AddValue("value2", 20);
            rootNode.AddValue("value3", 30);
            var value1 = rootNode.GetValue("value1");
            var value2 = rootNode.GetValue("value2");
            var value3 = rootNode.GetValue("value3");

            Assert.IsNotNull(value1);
            Assert.AreEqual("value1", value1.Name);
            Assert.AreEqual(10, value1.GetInteger());

            Assert.IsNotNull(value2);
            Assert.AreEqual("value2", value2.Name);
            Assert.AreEqual(20, value2.GetInteger());

            Assert.IsNotNull(value3);
            Assert.AreEqual("value3", value3.Name);
            Assert.AreEqual(30, value3.GetInteger());
        }

        [TestMethod]
        public void SqlNodeValue_AddIntValues_GetAsValueList()
        {
            var contentRepository = ContentRepository;
            var rootNode = GetRootNode(contentRepository);
            rootNode.AddValue("value1", 10);
            rootNode.AddValue("value2", 20);
            rootNode.AddValue("value3", 30);
            var values = rootNode.Values;

            Assert.IsNotNull(values);
            Assert.AreEqual(3, values.Count);

            Assert.AreEqual("value1", values[0].Name);
            Assert.AreEqual(10, values[0].GetInteger());

            Assert.AreEqual("value2", values[1].Name);
            Assert.AreEqual(20, values[1].GetInteger());

            Assert.AreEqual("value3", values[2].Name);
            Assert.AreEqual(30, values[2].GetInteger());
        }

        [TestMethod]
        public void SqlNodeValue_AddIntValuesInTwoParents_GetAsInteger()
        {
            var contentRepository = ContentRepository;
            var rootNode = GetRootNode(contentRepository);
            var childNode1 = rootNode.GetNode("childNode1");
            var childNode2 = rootNode.GetNode("childNode2");

            var value1 = childNode1.AddValue("value1", 10);
            var value2 = childNode2.AddValue("value2", 20);

            Assert.IsNotNull(value1);
            Assert.AreEqual("value1", value1.Name);
            Assert.AreEqual(10, value1.GetInteger());

            Assert.IsNotNull(value2);
            Assert.AreEqual("value2", value2.Name);
            Assert.AreEqual(20, value2.GetInteger());
        }

        [TestMethod]
        public void SqlNodeValue_GetIntValuesInTwoParents_GetAsInteger()
        {
            var contentRepository = ContentRepository;
            var rootNode = GetRootNode(contentRepository);
            var childNode1 = rootNode.GetNode("childNode1");
            var childNode2 = rootNode.GetNode("childNode2");
            childNode1.AddValue("value1", 10);
            childNode2.AddValue("value2", 20);

            var value1 = childNode1.GetValue("value1");
            var value2 = childNode2.GetValue("value2");

            Assert.IsNotNull(value1);
            Assert.AreEqual("value1", value1.Name);
            Assert.AreEqual(10, value1.GetInteger());

            Assert.IsNotNull(value2);
            Assert.AreEqual("value2", value2.Name);
            Assert.AreEqual(20, value2.GetInteger());
        }

        [TestMethod]
        public void SqlNodeValue_AddIntegerValuesInTwoParents_GetAsIntegerFromValues()
        {
            var contentRepository = ContentRepository;
            var rootNode = GetRootNode(contentRepository);
            var childNode1 = rootNode.GetNode("childNode1");
            var childNode2 = rootNode.GetNode("childNode2");
            childNode1.AddValue("value1", 10);
            childNode2.AddValue("value2", 20);

            var values1 = childNode1.Values;
            var values2 = childNode2.Values;

            Assert.IsNotNull(values1);
            Assert.AreEqual(1, values1.Count);

            Assert.IsNotNull(values2);
            Assert.AreEqual(1, values2.Count);

            Assert.AreEqual("value1", values1[0].Name);
            Assert.AreEqual(10, values1[0].GetInteger());

            Assert.AreEqual("value2", values2[0].Name);
            Assert.AreEqual(20, values2[0].GetInteger());
        }

        [TestMethod]
        public void SqlNodeValue_AddValueString_GetAsInt()
        {
            var contentRepository = ContentRepository;
            var rootNode = GetRootNode(contentRepository);
            var value = rootNode.AddValue("value1", "100");

            Assert.IsNotNull(value);
            Assert.AreEqual(100, value.GetInteger());
        }

        [TestMethod]
        public void SqlNodeValue_AddValueBoolTrue_GetAsInt()
        {
            var contentRepository = ContentRepository;
            var rootNode = GetRootNode(contentRepository);
            var value = rootNode.AddValue("value1", true);

            Assert.IsNotNull(value);
            Assert.AreEqual(1, value.GetInteger()); 
        }

        [TestMethod]
        public void SqlNodeValue_AddValueBoolFalse_GetAsInt()
        {
            var contentRepository = ContentRepository;
            var rootNode = GetRootNode(contentRepository);
            var value = rootNode.AddValue("value1", false);

            Assert.IsNotNull(value);
            Assert.AreEqual(0, value.GetInteger());
        }

        [TestMethod]
        public void SqlNodeValue_GetValueString_GetAsInteger()
        {
            var contentRepository = ContentRepository;
            var rootNode = GetRootNode(contentRepository);
            rootNode.AddValue("value1", "100");
            var value = rootNode.GetValue("value1");

            Assert.IsNotNull(value);
            Assert.AreEqual(100, value.GetInteger());
        }

        [TestMethod]
        public void SqlNodeValue_AddAndReplaceInt_GetAsValueList()
        {
            var contentRepository = ContentRepository;
            var rootNode = GetRootNode(contentRepository);
            rootNode.AddValue("value1", 10);
            rootNode.AddValue("value2", 20);
            rootNode.AddValue("value1", 30);

            var values = rootNode.Values;
            Assert.AreEqual(2, values.Count);
            Assert.AreEqual(30, values[0].GetInteger());
            Assert.AreEqual(20, values[1].GetInteger());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidNodeValueTypeException))]
        public void SqlNodeValue_GetInvalidIntFromBinary()
        {
            var contentRepository = ContentRepository;
            var rootNode = GetRootNode(contentRepository);
            var value = rootNode.AddValue("value1", new byte[] { 1, 2, 3, 4, 5 });
            value.GetInteger();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidNodeValueTypeException))]
        public void SqlNodeValue_GetInvalidIntFromDate()
        {
            var contentRepository = ContentRepository;
            var rootNode = GetRootNode(contentRepository);
            var value = rootNode.AddValue("value1", DateTime.Now);
            value.GetInteger();
        }

        [TestMethod]
        public void SqlNodeValue_AddValueType_AsInteger()
        {
            var contentRepository = ContentRepository;
            var rootNode = GetRootNode(contentRepository);
            var value = rootNode.AddValue("value1", 100);
            Assert.AreEqual(NodeValueType.Integer, value.ValueType);
        }

        [TestMethod]
        public void SqlNodeValue_GetValueType_AsInteger()
        {
            var contentRepository = ContentRepository;
            var rootNode = GetRootNode(contentRepository);
            rootNode.AddValue("value1", 100);
            var value = rootNode.GetValue("value1");
            Assert.AreEqual(NodeValueType.Integer, value.ValueType);
        }
    }
}
