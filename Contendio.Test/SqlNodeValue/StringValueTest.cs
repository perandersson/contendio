using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Contendio.Test.SqlNodeValue
{
    [TestClass]
    public class StringValueTest : BaseTest
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
        public void SqlNodeValue_AddStringValue()
        {
            var contentRepository = ContentRepository;
            var rootNode = GetRootNode(contentRepository);
            var value1 = rootNode.AddValue("value1", "value1value");

            Assert.IsNotNull(value1);
            Assert.AreEqual("value1", value1.Name);
            Assert.AreEqual("value1value", value1.GetString());
        }

        [TestMethod]
        public void SqlNodeValue_GetStringValue()
        {
            var contentRepository = ContentRepository;
            var rootNode = GetRootNode(contentRepository);
            rootNode.AddValue("value1", "value1value");
            var value1 = rootNode.GetValue("value1");

            Assert.IsNotNull(value1);
            Assert.AreEqual("value1", value1.Name);
            Assert.AreEqual("value1value", value1.GetString());
        }

        [TestMethod]
        public void SqlNodeValue_GetStringValues_GetAsValueList()
        {
            var contentRepository = ContentRepository;
            var rootNode = GetRootNode(contentRepository);
            rootNode.AddValue("value1", "value1value");
            var values = rootNode.Values;

            Assert.IsNotNull(values);
            Assert.AreEqual(1, values.Count);

            Assert.AreEqual("value1", values[0].Name);
            Assert.AreEqual("value1value", values[0].GetString());
        }

        [TestMethod]
        public void SqlNodeValue_AddStringValues_GetAsString()
        {
            var contentRepository = ContentRepository;
            var rootNode = GetRootNode(contentRepository);
            var value1 = rootNode.AddValue("value1", "value1value");
            var value2 = rootNode.AddValue("value2", "value2value");
            var value3 = rootNode.AddValue("value3", "value3value");

            Assert.IsNotNull(value1);
            Assert.AreEqual("value1", value1.Name);
            Assert.AreEqual("value1value", value1.GetString());

            Assert.IsNotNull(value2);
            Assert.AreEqual("value2", value2.Name);
            Assert.AreEqual("value2value", value2.GetString());

            Assert.IsNotNull(value3);
            Assert.AreEqual("value3", value3.Name);
            Assert.AreEqual("value3value", value3.GetString());
        }

        [TestMethod]
        public void SqlNodeValue_GetStringValues_GetAsString()
        {
            var contentRepository = ContentRepository;
            var rootNode = GetRootNode(contentRepository);
            rootNode.AddValue("value1", "value1value");
            rootNode.AddValue("value2", "value2value");
            rootNode.AddValue("value3", "value3value");
            var value1 = rootNode.GetValue("value1");
            var value2 = rootNode.GetValue("value2");
            var value3 = rootNode.GetValue("value3");

            Assert.IsNotNull(value1);
            Assert.AreEqual("value1", value1.Name);
            Assert.AreEqual("value1value", value1.GetString());

            Assert.IsNotNull(value2);
            Assert.AreEqual("value2", value2.Name);
            Assert.AreEqual("value2value", value2.GetString());

            Assert.IsNotNull(value3);
            Assert.AreEqual("value3", value3.Name);
            Assert.AreEqual("value3value", value3.GetString());
        }

        [TestMethod]
        public void SqlNodeValue_AddStringValues_GetAsValueList()
        {
            var contentRepository = ContentRepository;
            var rootNode = GetRootNode(contentRepository);
            rootNode.AddValue("value1", "value1value");
            rootNode.AddValue("value2", "value2value");
            rootNode.AddValue("value3", "value3value");
            var values = rootNode.Values;

            Assert.IsNotNull(values);
            Assert.AreEqual(3, values.Count);

            Assert.AreEqual("value1", values[0].Name);
            Assert.AreEqual("value1value", values[0].GetString());

            Assert.AreEqual("value2", values[1].Name);
            Assert.AreEqual("value2value", values[1].GetString());

            Assert.AreEqual("value3", values[2].Name);
            Assert.AreEqual("value3value", values[2].GetString());
        }

        [TestMethod]
        public void SqlNodeValue_AddStringValuesInTwoParents_GetAsString()
        {
            var contentRepository = ContentRepository;
            var rootNode = GetRootNode(contentRepository);
            var childNode1 = rootNode.GetNode("childNode1");
            var childNode2 = rootNode.GetNode("childNode2");

            var value1 = childNode1.AddValue("value1", "value1value");
            var value2 = childNode2.AddValue("value2", "value2value");

            Assert.IsNotNull(value1);
            Assert.AreEqual("value1", value1.Name);
            Assert.AreEqual("value1value", value1.GetString());

            Assert.IsNotNull(value2);
            Assert.AreEqual("value2", value2.Name);
            Assert.AreEqual("value2value", value2.GetString());
        }

        [TestMethod]
        public void SqlNodeValue_GetStringValuesInTwoParents_GetAsString()
        {
            var contentRepository = ContentRepository;
            var rootNode = GetRootNode(contentRepository);
            var childNode1 = rootNode.GetNode("childNode1");
            var childNode2 = rootNode.GetNode("childNode2");
            childNode1.AddValue("value1", "value1value");
            childNode2.AddValue("value2", "value2value");

            var value1 = childNode1.GetValue("value1");
            var value2 = childNode2.GetValue("value2");

            Assert.IsNotNull(value1);
            Assert.AreEqual("value1", value1.Name);
            Assert.AreEqual("value1value", value1.GetString());

            Assert.IsNotNull(value2);
            Assert.AreEqual("value2", value2.Name);
            Assert.AreEqual("value2value", value2.GetString());
        }

        [TestMethod]
        public void SqlNodeValue_AddStringValuesInTwoParents_GetAsStringFromValues()
        {
            var contentRepository = ContentRepository;
            var rootNode = GetRootNode(contentRepository);
            var childNode1 = rootNode.GetNode("childNode1");
            var childNode2 = rootNode.GetNode("childNode2");
            childNode1.AddValue("value1", "value1value");
            childNode2.AddValue("value2", "value2value");

            var values1 = childNode1.Values;
            var values2 = childNode2.Values;

            Assert.IsNotNull(values1);
            Assert.AreEqual(1, values1.Count);

            Assert.IsNotNull(values2);
            Assert.AreEqual(1, values2.Count);

            Assert.AreEqual("value1", values1[0].Name);
            Assert.AreEqual("value1value", values1[0].GetString());

            Assert.AreEqual("value2", values2[0].Name);
            Assert.AreEqual("value2value", values2[0].GetString());
        }

        [TestMethod]
        public void SqlNodeValue_AddValueDate_GetAsString()
        {
            var contentRepository = ContentRepository;
            var rootNode = GetRootNode(contentRepository);
            var now = DateTime.Now;
            var value = rootNode.AddValue("date1", now);
            
            Assert.IsNotNull(value);
            Assert.AreEqual(now.ToString(CultureInfo.InvariantCulture), value.GetString());
        }

        [TestMethod]
        public void SqlNodeValue_GetValueDate_GetAsString()
        {
            var contentRepository = ContentRepository;
            var rootNode = GetRootNode(contentRepository);
            var now = DateTime.Now;
            rootNode.AddValue("date1", now);
            var value = rootNode.GetValue("date1");

            Assert.IsNotNull(value);
            Assert.AreEqual(now.ToString(CultureInfo.InvariantCulture), value.GetString());
        }

        [TestMethod]
        public void SqlNodeValue_AddAndReplaceString_GetAsValueList()
        {
            var contentRepository = ContentRepository;
            var rootNode = GetRootNode(contentRepository);
            rootNode.AddValue("mystringvalue", "hello world!!!");
            rootNode.AddValue("mystringvalue2", "goodbye world!!!");
            rootNode.AddValue("mystringvalue", "replacedValue!!!");

            var values = rootNode.Values;
            Assert.AreEqual(2, values.Count);
            Assert.AreEqual("replacedValue!!!", values[0].GetString());
            Assert.AreEqual("goodbye world!!!", values[1].GetString());
        }
    }
}
