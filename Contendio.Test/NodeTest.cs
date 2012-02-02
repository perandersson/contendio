using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Contendio.Sql;
using System.Data.Linq;
using System.Transactions;
using Contendio.Exception;

namespace Contendio.Test
{
    [TestClass]
    public class NodeTest
    {
        private const string DatabaseSchema = "contendio";
        private const string ConnectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=" + DatabaseSchema + ";Integrated Security=True";

        private IRepositorySetup ContentInstall
        {
            get
            {
                return new SqlRepositorySetup("test", ConnectionString);
            }
        }

        private IContentRepository ContentRepository
        {
            get
            {
                SqlContentRepository contentRepository = new SqlContentRepository("test", ConnectionString);
                return contentRepository;
            }
        }

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
        public void TestRemoveChildNode()
        {
            IContentRepository contentRepository = ContentRepository;

            var rootNode = contentRepository.RootNode;
            Assert.IsNotNull(rootNode);
            Assert.AreEqual("/", rootNode.Path);
            Assert.IsNotNull(rootNode.NodeType);
            Assert.AreEqual("node:root", rootNode.NodeType.Name);

            var childNode1 = rootNode.AddNode("childNode1");
            Assert.AreEqual(1, rootNode.Children.Count);
            childNode1.Delete();
            Assert.AreEqual(0, rootNode.Children.Count);
        }

        [TestMethod]
        public void TestDeleteChildNodeFromParent()
        {
            IContentRepository contentRepository = ContentRepository;

            var rootNode = contentRepository.RootNode;
            Assert.IsNotNull(rootNode);
            var testNode1 = rootNode.AddNode("testNode1");
            Assert.AreEqual(1, rootNode.Children.Count);
            rootNode.Delete(testNode1);
            Assert.AreEqual(0, rootNode.Children.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ContendioException))]
        public void TestAddAbsoluteNodePath()
        {
            IContentRepository contentRepository = ContentRepository;

            var rootNode = contentRepository.RootNode;
            Assert.IsNotNull(rootNode);
            var testNode1 = rootNode.AddNode("testNode1");
            Assert.AreEqual(1, rootNode.Children.Count);
            rootNode.Delete("testNode1");
            Assert.AreEqual(0, rootNode.Children.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ContendioException))]
        public void TestGetAbsoluteNodePath()
        {
        }

        [TestMethod]
        public void TestGetRelativeDeepNodePath()
        {
        }
    }
}
