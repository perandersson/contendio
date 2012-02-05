using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Contendio.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Contendio.Sql;
using System.Data.Linq;
using System.Transactions;

namespace Contendio.Test.MoveNode
{
    [TestClass]
    public class MoveNodeAfter : BaseNodeData
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
                return new SqlContentRepository("test", ConnectionString);
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
        public void NodeMoveChildAfterInSameParent()
        {
            var contentRepository = ContentRepository;
            var rootNode = GetRootNode(contentRepository);
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
        [ExpectedException(typeof(CannotMoveNodeException))]
        public void MoveChildNodeAfterRoot()
        {
            var contentRepository = ContentRepository;
            var rootNode = GetRootNode(contentRepository);
            var childNode = rootNode.GetNode("childNode2");
            childNode.MoveAfter(rootNode);
        }

        [TestMethod]
        [ExpectedException(typeof(CannotMoveNodeException))]
        public void MoveRootNodeAfterChild()
        {
            var contentRepository = ContentRepository;
            var rootNode = GetRootNode(contentRepository);
            var childNode = rootNode.GetNode("childNode2");
            rootNode.MoveAfter(childNode);
        }

        [TestMethod]
        public void MoveSameChildNodeAfter()
        {
            var contentRepository = ContentRepository;
            var rootNode = GetRootNode(contentRepository);
            var children = rootNode.Children;

            Assert.AreEqual(3, children.Count);
            Assert.AreEqual("childNode2", children[1].Name);

            children = rootNode.Children;
            children[1].MoveAfter(children[1]);
            Assert.AreEqual("childNode2", children[1].Name);
        }

        [TestMethod]
        public void TestMoveBetweenParentsAfter()
        {
            var contentRepository = ContentRepository;
            var rootNode = GetRootNode(contentRepository);
            var childNode1 = rootNode.GetNode("childNode1");
            var childNode1Children = childNode1.Children;
            var childNode2 = rootNode.GetNode("childNode2");
            var childNode2Children = childNode2.Children;

            Assert.IsNotNull(childNode1Children);
            Assert.IsNotNull(childNode2Children);

            Assert.AreEqual(5, childNode1Children.Count);
            Assert.AreEqual(5, childNode2Children.Count);

            childNode2Children[2].MoveAfter(childNode1Children[2]);

            childNode1Children = childNode1.Children;
            childNode2Children = childNode2.Children;

            Assert.IsNotNull(childNode1Children);
            Assert.IsNotNull(childNode2Children);

            Assert.AreEqual(6, childNode1Children.Count);
            Assert.AreEqual(4, childNode2Children.Count);

            Assert.AreEqual("one", childNode1Children[0].Name);
            Assert.AreEqual("two", childNode1Children[1].Name);
            Assert.AreEqual("three", childNode1Children[2].Name);
            Assert.AreEqual(1, childNode1Children[2].Children.Count);
            Assert.AreEqual("1_one", childNode1Children[2].Children[0].Name);
            Assert.AreEqual("three", childNode1Children[3].Name);
            Assert.AreEqual(1, childNode1Children[3].Children.Count);
            Assert.AreEqual("2_one", childNode1Children[3].Children[0].Name);
            Assert.AreEqual("four", childNode1Children[4].Name);
            Assert.AreEqual("five", childNode1Children[5].Name);

            Assert.AreEqual("one", childNode2Children[0].Name);
            Assert.AreEqual("two", childNode2Children[1].Name);
            Assert.AreEqual("four", childNode2Children[2].Name);
            Assert.AreEqual("five", childNode2Children[3].Name);
        }

        [TestMethod]
        public void MoveAfterLastItemInOtherParent()
        {
            var contentRepository = ContentRepository;
            var rootNode = GetRootNode(contentRepository);
            var childNode1 = rootNode.GetNode("childNode1");
            var childNode1Children = childNode1.Children;
            var childNode2 = rootNode.GetNode("childNode2");
            var childNode2Children = childNode2.Children;

            childNode2Children[4].MoveAfter(childNode1Children[4]);

            childNode1Children = childNode1.Children;
            childNode2Children = childNode2.Children;

            Assert.IsNotNull(childNode1Children);
            Assert.IsNotNull(childNode2Children);

            Assert.AreEqual(6, childNode1Children.Count);
            Assert.AreEqual(4, childNode2Children.Count);

            Assert.AreEqual("one", childNode1Children[0].Name);
            Assert.AreEqual("two", childNode1Children[1].Name);
            Assert.AreEqual("three", childNode1Children[2].Name);
            Assert.AreEqual("four", childNode1Children[3].Name);
            Assert.AreEqual("five", childNode1Children[4].Name);
            Assert.AreEqual("five", childNode1Children[5].Name);

            Assert.AreEqual("one", childNode2Children[0].Name);
            Assert.AreEqual("two", childNode2Children[1].Name);
            Assert.AreEqual("three", childNode2Children[2].Name);
            Assert.AreEqual("four", childNode2Children[3].Name);
        }

        [TestMethod]
        public void MoveAfterFirstItemInOtherParent()
        {
            var contentRepository = ContentRepository;
            var rootNode = GetRootNode(contentRepository);
            var childNode1 = rootNode.GetNode("childNode1");
            var childNode1Children = childNode1.Children;
            var childNode2 = rootNode.GetNode("childNode2");
            var childNode2Children = childNode2.Children;

            childNode2Children[4].MoveAfter(childNode1Children[0]);

            childNode1Children = childNode1.Children;
            childNode2Children = childNode2.Children;

            Assert.AreEqual(6, childNode1Children.Count);
            Assert.AreEqual(4, childNode2Children.Count);

            Assert.AreEqual("one", childNode1Children[0].Name);
            Assert.AreEqual("five", childNode1Children[1].Name);
            Assert.AreEqual("two", childNode1Children[2].Name);
            Assert.AreEqual("three", childNode1Children[3].Name);
            Assert.AreEqual("four", childNode1Children[4].Name);
            Assert.AreEqual("five", childNode1Children[5].Name);

            Assert.AreEqual("one", childNode2Children[0].Name);
            Assert.AreEqual("two", childNode2Children[1].Name);
            Assert.AreEqual("three", childNode2Children[2].Name);
            Assert.AreEqual("four", childNode2Children[3].Name);
        }

    }
}
