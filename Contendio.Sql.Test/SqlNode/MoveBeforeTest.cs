using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Contendio.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Contendio.Test.SqlNode
{
    [TestClass]
    public class MoveBeforeTest : BaseTest
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
        public void SqlNode_MoveBefore_InSameParent()
        {
            var contentRepository = ContentRepository;
            var rootNode = GetRootNode(contentRepository);
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
        public void SqlNode_MoveBefore_ChildNodeBeforeRoot()
        {
            var contentRepository = ContentRepository;
            var rootNode = GetRootNode(contentRepository);
            var childNode = rootNode.GetNode("childNode2");
            childNode.MoveBefore(rootNode);
        }


        [TestMethod]
        [ExpectedException(typeof(CannotMoveNodeException))]
        public void SqlNode_MoveBefore_RootNodeBeforeChild()
        {
            var contentRepository = ContentRepository;
            var rootNode = GetRootNode(contentRepository);
            var childNode = rootNode.GetNode("childNode2");
            rootNode.MoveBefore(childNode);
        }

        [TestMethod]
        public void SqlNode_MoveBefore_SameNode()
        {
            var contentRepository = ContentRepository;
            var rootNode = GetRootNode(contentRepository);
            var children = rootNode.Children;

            Assert.AreEqual(3, children.Count);
            Assert.AreEqual("childNode2", children[1].Name);

            children = rootNode.Children;
            children[1].MoveBefore(children[1]);
            Assert.AreEqual("childNode2", children[1].Name);
        }

        [TestMethod]
        public void SqlNode_MoveBefore_BetweenParents()
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

            childNode2Children[2].MoveBefore(childNode1Children[2]);

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
            Assert.AreEqual("2_one", childNode1Children[2].Children[0].Name);
            Assert.AreEqual("three", childNode1Children[3].Name);
            Assert.AreEqual(1, childNode1Children[3].Children.Count);
            Assert.AreEqual("1_one", childNode1Children[3].Children[0].Name);
            Assert.AreEqual("four", childNode1Children[4].Name);
            Assert.AreEqual("five", childNode1Children[5].Name);

            Assert.AreEqual("one", childNode2Children[0].Name);
            Assert.AreEqual("two", childNode2Children[1].Name);
            Assert.AreEqual("four", childNode2Children[2].Name);
            Assert.AreEqual("five", childNode2Children[3].Name);
        }

        [TestMethod]
        [ExpectedException(typeof(CannotMoveNodeException))]
        public void SqlNode_MoveBefore_ParentBeforeChildNode()
        {
            var contentRepository = ContentRepository;
            var rootNode = GetRootNode(contentRepository);
            var childNode2 = rootNode.GetNode("childNode2");
            var two = childNode2.GetNode("two");
            childNode2.MoveBefore(two);
        }

        [TestMethod]
        [ExpectedException(typeof(CannotMoveNodeException))]
        public void SqlNode_MoveBefore_ParentBeforeDeepChildNode()
        {
            var contentRepository = ContentRepository;
            var rootNode = GetRootNode(contentRepository);
            var childNode3 = rootNode.GetNode("childNode3");
            var two = childNode3.GetNode("two");
            var one = two.GetNode("3_one");
            childNode3.MoveBefore(one);
        }
    }
}
