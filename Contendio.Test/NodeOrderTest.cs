using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Contendio.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Contendio.Sql;
using System.Data.Linq;
using System.Transactions;

namespace Contendio.Test
{
    [TestClass]
    public class NodeOrderTest
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
                var contentRepository = new SqlContentRepository("test", ConnectionString);
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
        public void TestMoveChildAfterTest()
        {
            IContentRepository contentRepository = ContentRepository;

            var rootNode = contentRepository.RootNode;
            Assert.IsNotNull(rootNode);

            rootNode.AddNode("childNode1");
            rootNode.AddNode("childNode2");
            rootNode.AddNode("childNode3");

            var children = rootNode.Children;
            Assert.AreEqual(3, children.Count);
            Assert.AreEqual("childNode1", children[0].Name);
            Assert.AreEqual("childNode2", children[1].Name);
            Assert.AreEqual("childNode3", children[2].Name);

            children[0].MoveAfter(children[1]);
            children = rootNode.Children;
            Assert.AreEqual(3, children.Count);
            Assert.AreEqual("childNode2", children[0].Name);
            Assert.AreEqual("childNode1", children[1].Name);
            Assert.AreEqual("childNode3", children[2].Name);

            children[0].MoveAfter(children[1]);
            children = rootNode.Children;
            Assert.AreEqual(3, children.Count);
            Assert.AreEqual("childNode1", children[0].Name);
            Assert.AreEqual("childNode2", children[1].Name);
            Assert.AreEqual("childNode3", children[2].Name);

            children[0].MoveAfter(children[2]);
            children = rootNode.Children;
            Assert.AreEqual(3, children.Count);
            Assert.AreEqual("childNode2", children[0].Name);
            Assert.AreEqual("childNode3", children[1].Name);
            Assert.AreEqual("childNode1", children[2].Name);

            children[1].MoveAfter(children[2]);
            children = rootNode.Children;
            Assert.AreEqual(3, children.Count);
            Assert.AreEqual("childNode2", children[0].Name);
            Assert.AreEqual("childNode1", children[1].Name);
            Assert.AreEqual("childNode3", children[2].Name);
        }

        [TestMethod]
        public void TestMoveChildBeforeTest()
        {
            IContentRepository contentRepository = ContentRepository;

            var rootNode = contentRepository.RootNode;
            Assert.IsNotNull(rootNode);

            rootNode.AddNode("childNode1");
            rootNode.AddNode("childNode2");
            rootNode.AddNode("childNode3");

            var children = rootNode.Children;
            Assert.AreEqual(3, children.Count);
            Assert.AreEqual("childNode1", children[0].Name);
            Assert.AreEqual("childNode2", children[1].Name);
            Assert.AreEqual("childNode3", children[2].Name);

            children[0].MoveBefore(children[1]);
            children = rootNode.Children;
            Assert.AreEqual(3, children.Count);
            Assert.AreEqual("childNode1", children[0].Name);
            Assert.AreEqual("childNode2", children[1].Name);
            Assert.AreEqual("childNode3", children[2].Name);

            children[1].MoveBefore(children[0]);
            children = rootNode.Children;
            Assert.AreEqual(3, children.Count);
            Assert.AreEqual("childNode2", children[0].Name);
            Assert.AreEqual("childNode1", children[1].Name);
            Assert.AreEqual("childNode3", children[2].Name);

            children[1].MoveBefore(children[0]);
            children = rootNode.Children;
            Assert.AreEqual(3, children.Count);
            Assert.AreEqual("childNode1", children[0].Name);
            Assert.AreEqual("childNode2", children[1].Name);
            Assert.AreEqual("childNode3", children[2].Name);

            children[0].MoveBefore(children[2]);
            children = rootNode.Children;
            Assert.AreEqual(3, children.Count);
            Assert.AreEqual("childNode2", children[0].Name);
            Assert.AreEqual("childNode1", children[1].Name);
            Assert.AreEqual("childNode3", children[2].Name);

            children[2].MoveBefore(children[1]);
            children = rootNode.Children;
            Assert.AreEqual(3, children.Count);
            Assert.AreEqual("childNode2", children[0].Name);
            Assert.AreEqual("childNode3", children[1].Name);
            Assert.AreEqual("childNode1", children[2].Name);
        }

        [TestMethod]
        [ExpectedException(typeof(CannotMoveNodeException))]
        public void TestTryToMoveNodeBeforeRoot()
        {
            IContentRepository contentRepository = ContentRepository;

            var rootNode = contentRepository.RootNode;
            Assert.IsNotNull(rootNode);

            rootNode.AddNode("childNode1");
            rootNode.AddNode("childNode2");
            rootNode.AddNode("childNode3");

            var childNode = rootNode.GetNode("childNode2");
            childNode.MoveBefore(rootNode);
        }

        [TestMethod]
        [ExpectedException(typeof(CannotMoveNodeException))]
        public void TestTryToMoveNodeAfterRoot()
        {
            IContentRepository contentRepository = ContentRepository;

            var rootNode = contentRepository.RootNode;
            Assert.IsNotNull(rootNode);

            rootNode.AddNode("childNode1");
            rootNode.AddNode("childNode2");
            rootNode.AddNode("childNode3");

            var childNode = rootNode.GetNode("childNode2");
            childNode.MoveAfter(rootNode);
        }

        [TestMethod]
        [ExpectedException(typeof(CannotMoveNodeException))]
        public void TestTryToMoveRootAfterNode()
        {
            IContentRepository contentRepository = ContentRepository;

            var rootNode = contentRepository.RootNode;
            Assert.IsNotNull(rootNode);

            rootNode.AddNode("childNode1");
            rootNode.AddNode("childNode2");
            rootNode.AddNode("childNode3");

            var childNode = rootNode.GetNode("childNode2");
            rootNode.MoveAfter(childNode);
        }

        [TestMethod]
        [ExpectedException(typeof(CannotMoveNodeException))]
        public void TestTryToMoveRootBeforeNode()
        {
            IContentRepository contentRepository = ContentRepository;

            var rootNode = contentRepository.RootNode;
            Assert.IsNotNull(rootNode);

            rootNode.AddNode("childNode1");
            rootNode.AddNode("childNode2");
            rootNode.AddNode("childNode3");

            var childNode = rootNode.GetNode("childNode2");
            rootNode.MoveBefore(childNode);
        }

        [TestMethod]
        public void TestMoveSameNodeBefore()
        {
            IContentRepository contentRepository = ContentRepository;

            var rootNode = contentRepository.RootNode;
            Assert.IsNotNull(rootNode);

            rootNode.AddNode("childNode1");
            rootNode.AddNode("childNode2");

            var children = rootNode.Children;
            Assert.AreEqual(2, children.Count);
            Assert.AreEqual("childNode2", children[1].Name);

            children = rootNode.Children;
            children[1].MoveBefore(children[1]);
            Assert.AreEqual("childNode2", children[1].Name);
        }

        [TestMethod]
        public void TestMoveSameNodeAfter()
        {
            IContentRepository contentRepository = ContentRepository;

            var rootNode = contentRepository.RootNode;
            Assert.IsNotNull(rootNode);

            rootNode.AddNode("childNode1");
            rootNode.AddNode("childNode2");

            var children = rootNode.Children;
            Assert.AreEqual(2, children.Count);
            Assert.AreEqual("childNode2", children[1].Name);

            children = rootNode.Children;
            children[1].MoveAfter(children[1]);
            Assert.AreEqual("childNode2", children[1].Name);
        }

        [TestMethod]
        public void TestMoveBetweenParentsBefore()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void TestMoveBetweenParentsAfter()
        {
            throw new NotImplementedException();
        }
    }
}
