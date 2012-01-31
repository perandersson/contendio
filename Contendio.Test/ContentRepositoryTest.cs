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
            Assert.AreEqual("/childNode1", childNode.Path);
            Assert.AreEqual("childNode1", childNode.Name);
            Assert.AreEqual("node:node", childNode.NodeType.Name);
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

            Assert.AreEqual(2, rootNode.Children.Count);

            var childNode = rootNode.GetNode("childNode1");
            Assert.IsNotNull(childNode);
            Assert.AreEqual("/childNode1", childNode.Path);
            Assert.AreEqual("childNode1", childNode.Name);

            Assert.AreEqual(childNode.Id, rootNode.Children[0].Id);
            Assert.AreEqual(childNode.Name, rootNode.Children[0].Name);
            Assert.AreEqual(childNode.ParentNode.Id, rootNode.Children[0].ParentNode.Id);

            childNode = rootNode.GetNode("childNode2");
            Assert.IsNotNull(childNode);
            Assert.AreEqual("/childNode2", childNode.Path);
            Assert.AreEqual("childNode2", childNode.Name);

            Assert.AreEqual(childNode.Id, rootNode.Children[1].Id);
            Assert.AreEqual(childNode.Name, rootNode.Children[1].Name);
            Assert.AreEqual(childNode.ParentNode.Id, rootNode.Children[1].ParentNode.Id);
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
            Assert.AreEqual(2, rootNode.Children.Count);

            var childNode = rootNode.GetNode("childNode1");
            Assert.IsNotNull(childNode);
            Assert.AreEqual("/childNode1", childNode.Path);
            Assert.AreEqual("childNode1", childNode.Name);
            Assert.AreEqual(0, childNode.Children.Count);

            using (var transaction = new TransactionScope())
            {
                childNode.AddNode("subChildNode1");
                transaction.Complete();
            }

            childNode = childNode.GetNode("subChildNode1");
            Assert.IsNotNull(childNode);
            Assert.AreEqual("/childNode1/subChildNode1", childNode.Path);
            Assert.AreEqual("subChildNode1", childNode.Name);
            Assert.AreEqual(1, childNode.Children.Count);

            using (var transaction = new TransactionScope())
            {
                rootNode.AddNode("childNode1/subChildNode1/subsubChildNode1");
                transaction.Complete();
            }
        }

        [TestMethod]
        public void TestAddSameNodeTwice()
        {
            IContentRepository contentRepository = ContentRepository;
            var rootNode = contentRepository.RootNode;
            Assert.IsNotNull(rootNode);

            using (var transaction = new TransactionScope())
            {
                rootNode.AddNode("childNode1");
                rootNode.AddNode("childNode1");
                transaction.Complete();
            }

            var rootNodeCollection = rootNode.Children;
            Assert.IsNotNull(rootNodeCollection);
            Assert.AreEqual(2, rootNodeCollection.Count);

            Assert.AreEqual("childNode1", rootNodeCollection[0].Name);
            Assert.AreEqual("childNode1", rootNodeCollection[1].Name);
        }
    }
}
