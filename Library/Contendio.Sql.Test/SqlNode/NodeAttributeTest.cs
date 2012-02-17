using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Contendio.Test;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Contendio.Exceptions;

namespace Contendio.Sql.Test.SqlNode
{
    [TestClass]
    public class NodeAttributeTest : BaseTest
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
        public void SqlNode_VerifyRootNodeAttribute()
        {
            var contentRepository = ContentRepository;
            var rootNode = GetRootNode(contentRepository);
            var attributes = rootNode.Attributes;

            Assert.AreEqual(1, attributes.Count);
            Assert.AreEqual("attribute:unmovable", attributes[0].Name);
        }

        [TestMethod]
        [ExpectedException(typeof(CannotAddAttributeOnRootNode))]
        public void SqlNode_AddAttributeOnRoot_ExpectFailure()
        {
            var contentRepository = ContentRepository;
            var rootNode = GetRootNode(contentRepository);

            rootNode.AddAttribute("attribute:readonly");
        }

        [TestMethod]
        public void SqlNode_AddOneAttributeOnNode_GetAttribute()
        {
            var contentRepository = ContentRepository;
            var rootNode = GetRootNode(contentRepository);
            var childNode1 = rootNode.GetNode("childNode1");

            childNode1.AddAttribute("attribute:readonly");

            var attributes = childNode1.Attributes;
            Assert.AreEqual(1, attributes.Count);
            Assert.AreEqual("attribute:readonly", attributes[0].Name);
        }

        [TestMethod]
        public void SqlNode_AddTwoAttributesOnNode_GetAttributes()
        {
            var contentRepository = ContentRepository;
            var rootNode = GetRootNode(contentRepository);
            var childNode1 = rootNode.GetNode("childNode1");

            childNode1.AddAttribute("attribute:readonly");
            childNode1.AddAttribute("attribute:unmovable");

            var attributes = childNode1.Attributes;
            Assert.AreEqual(2, attributes.Count);
            Assert.AreEqual("attribute:readonly", attributes[0].Name);
            Assert.AreEqual("attribute:unmovable", attributes[1].Name);
        }

        [TestMethod]
        [ExpectedException(typeof(CannotAddAttributeTwice))]
        public void SqlNode_AddSameAttributeTwice_ExpectFailure()
        {
            var contentRepository = ContentRepository;
            var rootNode = GetRootNode(contentRepository);
            var childNode1 = rootNode.GetNode("childNode1");

            childNode1.AddAttribute("attribute:readonly");
            childNode1.AddAttribute("attribute:readonly");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SqlNode_AddEmptyAttributeOnNode_ExpectFailure()
        {
            var contentRepository = ContentRepository;
            var rootNode = GetRootNode(contentRepository);
            var childNode1 = rootNode.GetNode("childNode1");

            childNode1.AddAttribute("");
        }

        [TestMethod]
        public void SqlNode_AddAttributeOnMultipleNodes_GetAttributes()
        {
            var contentRepository = ContentRepository;
            var rootNode = GetRootNode(contentRepository);
            var childNode1 = rootNode.GetNode("childNode1");
            var childNode2 = rootNode.GetNode("childNode2");

            childNode1.AddAttribute("attribute:readonly");
            childNode2.AddAttribute("attribute:unmovable");

            var attributes1 = childNode1.Attributes;
            var attributes2 = childNode2.Attributes;

            Assert.AreEqual(1, attributes1.Count);
            Assert.AreEqual(1, attributes2.Count);

            Assert.AreEqual("attribute:readonly", attributes1[0].Name);
            Assert.AreEqual("attribute:unmovable", attributes2[0].Name);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidNodeValueTypeException))]
        public void SqlNode_AddAttributeThatDoesNotExist_ExpectFailure()
        {
            var contentRepository = ContentRepository;
            var rootNode = GetRootNode(contentRepository);
            var childNode1 = rootNode.GetNode("childNode1");

            childNode1.AddAttribute("attribute:nonexisting");
        }
    }
}
