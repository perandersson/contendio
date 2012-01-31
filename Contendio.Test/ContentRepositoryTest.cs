using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Contendio.Sql;
using System.Data.Linq;
using System.Transactions;

namespace Contendio.Test
{
    [TestClass]
    public class ContentRepositoryTest
    {
        private const string DatabaseSchema = "contendio";
        private const string ConnectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=" + DatabaseSchema + ";Integrated Security=True";

        private IRepositorySetup ContentInstall
        {
            get
            {
                return new SqlRepositorySetup(DatabaseSchema, "test", ConnectionString);
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
        public void TestRepositoryInstallation()
        {
            IContentRepository contentRepository = ContentRepository;

            var rootNode = contentRepository.RootNode;
            Assert.IsNotNull(rootNode);
            Assert.AreEqual("/", rootNode.Path);
            Assert.IsNotNull(rootNode.NodeType);
            Assert.AreEqual("node:root", rootNode.NodeType.Name);
        }

        [TestMethod]
        public void TestAddChildNode()
        {
            IContentRepository contentRepository = ContentRepository;
            var rootNode = contentRepository.RootNode;
            Assert.IsNotNull(rootNode);

            using (var transaction = new TransactionScope())
            {
                var tmp = rootNode.AddNode("childNode1");
                transaction.Complete();
            }

            var childNode = rootNode.GetNode("childNode1");
            Assert.IsNotNull(childNode);
            Assert.AreEqual(childNode.Path, "/childNode1");
            Assert.AreEqual(childNode.Name, "childNode1");
        }

        [TestMethod]
        public void TestAddMultipleChildNodes()
        {
            IContentRepository contentRepository = ContentRepository;
            var rootNode = contentRepository.RootNode;
            Assert.IsNotNull(rootNode);

            using (var transaction = new TransactionScope())
            {
                rootNode.AddNode("childNode1");
                rootNode.AddNode("childNode2");
                transaction.Complete();
            }

            var childNode = rootNode.GetNode("childNode1");
            Assert.IsNotNull(childNode);
            Assert.AreEqual("/childNode1", childNode.Path);
            Assert.AreEqual("childNode1", childNode.Name);

            childNode = rootNode.GetNode("childNode2");
            Assert.IsNotNull(childNode);
            Assert.AreEqual("/childNode2", childNode.Path);
            Assert.AreEqual("childNode2", childNode.Name);
        }

        [TestMethod]
        public void TestAddSubChildNodes()
        {
            IContentRepository contentRepository = ContentRepository;
            var rootNode = contentRepository.RootNode;
            Assert.IsNotNull(rootNode);

            using (var transaction = new TransactionScope())
            {
                rootNode.AddNode("childNode1");
                rootNode.AddNode("childNode2");
                transaction.Complete();
            }

            var childNode = rootNode.GetNode("childNode1");
            Assert.IsNotNull(childNode);
            Assert.AreEqual("/childNode1", childNode.Path);
            Assert.AreEqual("childNode1", childNode.Name);

            using (var transaction = new TransactionScope())
            {
                childNode.AddNode("subChildNode1");
                transaction.Complete();
            }

            childNode = childNode.GetNode("subChildNode1");
            Assert.IsNotNull(childNode);
            Assert.AreEqual("/childNode1/subChildNode1", childNode.Path);
            Assert.AreEqual("subChildNode1", childNode.Name);
        }

        /*
        [TestMethod]
        public void TryToSaveSameNodeTwice()
        {
            IContentRepository contentRepository = ContentRepository;
            using (var transaction = new TransactionScope())
            {
                var rootNode = contentRepository.RootNode;
                Assert.IsNotNull(rootNode);

                rootNode.AddNode("childNode1");
                rootNode.AddNode("childNode1");
                transaction.Complete();
            }

            using (var transaction = new TransactionScope())
            {
                var rootNode = contentRepository.RootNode;
                Assert.IsNotNull(rootNode);

                var node = rootNode.GetNode("childNode1");
                Assert.IsNotNull(node);
                Assert.AreEqual(node.Path, "/childNode1");
                Assert.AreEqual(node.Name, "childNode1");

                node = rootNode.GetNode("childNode1_0");
                Assert.IsNotNull(node);
                Assert.AreEqual(node.Path, "/childNode1_0");
                Assert.AreEqual(node.Name, "childNode1_0");
            }
        }
         * */
    }
}
