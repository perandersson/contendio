using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Contendio.Sql;

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
        public void TestRemoveSubChildNode()
        {
            IContentRepository contentRepository = ContentRepository;

            var rootNode = contentRepository.RootNode;
            Assert.IsNotNull(rootNode);

            var nodeLevel3 = rootNode.AddNode("childNode/subChildNode/nodeLevel3");
            Assert.IsNotNull(nodeLevel3);
            rootNode.Delete("childNode/subChildNode/nodeLevel3");

            var subChildNode = rootNode.GetNode("childNode/subChildNode");
            Assert.IsNotNull(subChildNode);
            Assert.AreEqual(0, subChildNode.Children.Count);
        }

        [TestMethod]
        public void TestRemoveRecursiveSubChildNode()
        {
            IContentRepository contentRepository = ContentRepository;

            var rootNode = contentRepository.RootNode;
            Assert.IsNotNull(rootNode);

            var nodeLevel3 = rootNode.AddNode("childNode/subChildNode/nodeLevel3");
            Assert.IsNotNull(nodeLevel3);
            rootNode.Delete("childNode/subChildNode");
            Assert.AreEqual(1, rootNode.Children.Count);
            var childNode = rootNode.GetNode("childNode");
            Assert.IsNotNull(childNode);
            Assert.AreEqual(0, childNode.Children.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddAbsoluteNodePath()
        {
            IContentRepository contentRepository = ContentRepository;

            var rootNode = contentRepository.RootNode;
            Assert.IsNotNull(rootNode);
            rootNode.AddNode("/childNode/subChildNode/nodeLevel3");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestGetAbsoluteNodePath()
        {
            IContentRepository contentRepository = ContentRepository;

            var rootNode = contentRepository.RootNode;
            Assert.IsNotNull(rootNode);
            rootNode.AddNode("childNode/subChildNode/nodeLevel3");
            rootNode.GetNode("/childNode/subChildNode/nodeLevel3");
        }

        [TestMethod]
        public void TestGetRelativeDeepNodePath()
        {
            throw new NotImplementedException();
        }
    }
}
