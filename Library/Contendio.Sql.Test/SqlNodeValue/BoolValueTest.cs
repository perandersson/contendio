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
    public class BoolValueTest : BaseTest
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
        public void SqlNodeValue_AddBoolValue()
        {
            var contentRepository = ContentRepository;
            var rootNode = GetRootNode(contentRepository);
            var value1 = rootNode.AddValue("value1", true);

            Assert.IsNotNull(value1);
            Assert.AreEqual("value1", value1.Name);
            Assert.AreEqual(true, value1.GetBool());
        }

        [TestMethod]
        public void SqlNodeValue_GetBoolValue()
        {
            var contentRepository = ContentRepository;
            var rootNode = GetRootNode(contentRepository);
            rootNode.AddValue("value1", true);
            var value1 = rootNode.GetValue("value1");

            Assert.IsNotNull(value1);
            Assert.AreEqual("value1", value1.Name);
            Assert.AreEqual(true, value1.GetBool());
        }

        [TestMethod]
        public void SqlNodeValue_GetBoolValues_GetAsValueList()
        {
            var contentRepository = ContentRepository;
            var rootNode = GetRootNode(contentRepository);
            rootNode.AddValue("value1", true);
            var values = rootNode.Values;

            Assert.IsNotNull(values);
            Assert.AreEqual(1, values.Count);

            Assert.AreEqual("value1", values[0].Name);
            Assert.AreEqual(true, values[0].GetBool());
        }

        [TestMethod]
        public void SqlNodeValue_AddBoolValues_GetAsBool()
        {
            var contentRepository = ContentRepository;
            var rootNode = GetRootNode(contentRepository);
            var value1 = rootNode.AddValue("value1", true);
            var value2 = rootNode.AddValue("value2", false);
            var value3 = rootNode.AddValue("value3", true);

            Assert.IsNotNull(value1);
            Assert.AreEqual("value1", value1.Name);
            Assert.AreEqual(true, value1.GetBool());

            Assert.IsNotNull(value2);
            Assert.AreEqual("value2", value2.Name);
            Assert.AreEqual(false, value2.GetBool());

            Assert.IsNotNull(value3);
            Assert.AreEqual("value3", value3.Name);
            Assert.AreEqual(true, value3.GetBool());
        }

        [TestMethod]
        public void SqlNodeValue_GetBoolValues_GetAsBool()
        {
            var contentRepository = ContentRepository;
            var rootNode = GetRootNode(contentRepository);
            rootNode.AddValue("value1", true);
            rootNode.AddValue("value2", false);
            rootNode.AddValue("value3", true);
            var value1 = rootNode.GetValue("value1");
            var value2 = rootNode.GetValue("value2");
            var value3 = rootNode.GetValue("value3");

            Assert.IsNotNull(value1);
            Assert.AreEqual("value1", value1.Name);
            Assert.AreEqual(true, value1.GetBool());

            Assert.IsNotNull(value2);
            Assert.AreEqual("value2", value2.Name);
            Assert.AreEqual(false, value2.GetBool());

            Assert.IsNotNull(value3);
            Assert.AreEqual("value3", value3.Name);
            Assert.AreEqual(true, value3.GetBool());
        }

        [TestMethod]
        public void SqlNodeValue_AddBoolValues_GetAsValueList()
        {
            var contentRepository = ContentRepository;
            var rootNode = GetRootNode(contentRepository);
            rootNode.AddValue("value1", true);
            rootNode.AddValue("value2", false);
            rootNode.AddValue("value3", true);
            var values = rootNode.Values;

            Assert.IsNotNull(values);
            Assert.AreEqual(3, values.Count);

            Assert.AreEqual("value1", values[0].Name);
            Assert.AreEqual(true, values[0].GetBool());

            Assert.AreEqual("value2", values[1].Name);
            Assert.AreEqual(false, values[1].GetBool());

            Assert.AreEqual("value3", values[2].Name);
            Assert.AreEqual(true, values[2].GetBool());
        }

        [TestMethod]
        public void SqlNodeValue_AddBoolValuesInTwoParents_GetAsBool()
        {
            var contentRepository = ContentRepository;
            var rootNode = GetRootNode(contentRepository);
            var childNode1 = rootNode.GetNode("childNode1");
            var childNode2 = rootNode.GetNode("childNode2");

            var value1 = childNode1.AddValue("value1", true);
            var value2 = childNode2.AddValue("value2", false);

            Assert.IsNotNull(value1);
            Assert.AreEqual("value1", value1.Name);
            Assert.AreEqual(true, value1.GetBool());

            Assert.IsNotNull(value2);
            Assert.AreEqual("value2", value2.Name);
            Assert.AreEqual(false, value2.GetBool());
        }

        [TestMethod]
        public void SqlNodeValue_GetBoolValuesInTwoParents_GetAsBool()
        {
            var contentRepository = ContentRepository;
            var rootNode = GetRootNode(contentRepository);
            var childNode1 = rootNode.GetNode("childNode1");
            var childNode2 = rootNode.GetNode("childNode2");
            childNode1.AddValue("value1", true);
            childNode2.AddValue("value2", false);

            var value1 = childNode1.GetValue("value1");
            var value2 = childNode2.GetValue("value2");

            Assert.IsNotNull(value1);
            Assert.AreEqual("value1", value1.Name);
            Assert.AreEqual(true, value1.GetBool());

            Assert.IsNotNull(value2);
            Assert.AreEqual("value2", value2.Name);
            Assert.AreEqual(false, value2.GetBool());
        }

        [TestMethod]
        public void SqlNodeValue_AddBoolValuesInTwoParents_GetAsBoolFromValues()
        {
            var contentRepository = ContentRepository;
            var rootNode = GetRootNode(contentRepository);
            var childNode1 = rootNode.GetNode("childNode1");
            var childNode2 = rootNode.GetNode("childNode2");
            childNode1.AddValue("value1", true);
            childNode2.AddValue("value2", false);

            var values1 = childNode1.Values;
            var values2 = childNode2.Values;

            Assert.IsNotNull(values1);
            Assert.AreEqual(1, values1.Count);

            Assert.IsNotNull(values2);
            Assert.AreEqual(1, values2.Count);

            Assert.AreEqual("value1", values1[0].Name);
            Assert.AreEqual(true, values1[0].GetBool());

            Assert.AreEqual("value2", values2[0].Name);
            Assert.AreEqual(false, values2[0].GetBool());
        }

        [TestMethod]
        public void SqlNodeValue_AddValueString_GetAsBool()
        {
            var contentRepository = ContentRepository;
            var rootNode = GetRootNode(contentRepository);
            var value = rootNode.AddValue("value1", "True");

            Assert.IsNotNull(value);
            Assert.AreEqual(true, value.GetBool());
        }

        [TestMethod]
        public void SqlNodeValue_AddValueInt1_GetAsBool()
        {
            var contentRepository = ContentRepository;
            var rootNode = GetRootNode(contentRepository);
            var value = rootNode.AddValue("value1", 1);

            Assert.IsNotNull(value);
            Assert.AreEqual(true, value.GetBool()); 
        }

        [TestMethod]
        public void SqlNodeValue_AddValueInt0_GetAsInt()
        {
            var contentRepository = ContentRepository;
            var rootNode = GetRootNode(contentRepository);
            var value = rootNode.AddValue("value1", 0);

            Assert.IsNotNull(value);
            Assert.AreEqual(false, value.GetBool());
        }

        [TestMethod]
        public void SqlNodeValue_GetValueString_GetAsBool()
        {
            var contentRepository = ContentRepository;
            var rootNode = GetRootNode(contentRepository);
            rootNode.AddValue("value1", "False");
            var value = rootNode.GetValue("value1");

            Assert.IsNotNull(value);
            Assert.AreEqual(false, value.GetBool());
        }

        [TestMethod]
        public void SqlNodeValue_AddAndReplaceBool_GetAsValueList()
        {
            var contentRepository = ContentRepository;
            var rootNode = GetRootNode(contentRepository);
            rootNode.AddValue("value1", true);
            rootNode.AddValue("value2", true);
            rootNode.AddValue("value1", false);

            var values = rootNode.Values;
            Assert.AreEqual(2, values.Count);
            Assert.AreEqual(false, values[0].GetBool());
            Assert.AreEqual(true, values[1].GetBool());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidNodeValueTypeException))]
        public void SqlNodeValue_GetInvalidBoolFromBinary()
        {
            var contentRepository = ContentRepository;
            var rootNode = GetRootNode(contentRepository);
            var value = rootNode.AddValue("value1", new byte[] { 1, 2, 3, 4, 5 });
            value.GetBool();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidNodeValueTypeException))]
        public void SqlNodeValue_GetInvalidBoolFromDate()
        {
            var contentRepository = ContentRepository;
            var rootNode = GetRootNode(contentRepository);
            var value = rootNode.AddValue("value1", DateTime.Now);
            value.GetBool();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidNodeValueTypeException))]
        public void SqlNodeValue_GetInvalidBoolFromString()
        {
            var contentRepository = ContentRepository;
            var rootNode = GetRootNode(contentRepository);
            var value = rootNode.AddValue("value1", "non-bool value here");
            value.GetBool();
        }

        [TestMethod]
        public void SqlNodeValue_AddValueType_AsBoolean()
        {
            var contentRepository = ContentRepository;
            var rootNode = GetRootNode(contentRepository);
            var value = rootNode.AddValue("value1", true);
            Assert.AreEqual(NodeValueType.Boolean, value.ValueType);
        }

        [TestMethod]
        public void SqlNodeValue_GetValueType_AsBoolean()
        {
            var contentRepository = ContentRepository;
            var rootNode = GetRootNode(contentRepository);
            rootNode.AddValue("value1", true);
            var value = rootNode.GetValue("value1");
            Assert.AreEqual(NodeValueType.Boolean, value.ValueType);
        }
    }
}
