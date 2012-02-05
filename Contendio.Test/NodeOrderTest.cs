using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Contendio.Exceptions;
using Contendio.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Contendio.Sql;
using System.Data.Linq;
using System.Transactions;

namespace Contendio.Test
{
    [TestClass]
    public class NodeOrderTest : BaseNodeData
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
        public void NodeMoveChildBeforeInSameParent()
        {
            IContentRepository contentRepository = ContentRepository;
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
        public void MoveChildNodeBeforeRoot()
        {
            IContentRepository contentRepository = ContentRepository;
            var rootNode = GetRootNode(contentRepository);
            var childNode = rootNode.GetNode("childNode2");
            childNode.MoveBefore(rootNode);
        }

       
        [TestMethod]
        [ExpectedException(typeof(CannotMoveNodeException))]
        public void MoveRootNodeBeforeChild()
        {
            IContentRepository contentRepository = ContentRepository;
            var rootNode = GetRootNode(contentRepository);
            var childNode = rootNode.GetNode("childNode2");
            rootNode.MoveBefore(childNode);
        }

        [TestMethod]
        public void MoveSameChildNodeBefore()
        {
            IContentRepository contentRepository = ContentRepository;
            var rootNode = GetRootNode(contentRepository);
            var children = rootNode.Children;

            Assert.AreEqual(3, children.Count);
            Assert.AreEqual("childNode2", children[1].Name);

            children = rootNode.Children;
            children[1].MoveBefore(children[1]);
            Assert.AreEqual("childNode2", children[1].Name);
        }

        [TestMethod]
        public void TestMoveBetweenParentsBefore()
        {
            IContentRepository contentRepository = ContentRepository;
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
        public void IsNodeParentOf()
        {
            IContentRepository contentRepository = ContentRepository;
            var rootNode = GetRootNode(contentRepository);
            var childNode1 = rootNode.GetNode("childNode1");
            var childNode2 = rootNode.GetNode("childNode2");

            Assert.IsTrue(rootNode.IsParentOf(childNode1));
            Assert.IsTrue(rootNode.IsParentOf(childNode1.GetNode("one")));
            Assert.IsTrue(childNode1.IsParentOf(childNode1.GetNode("one")));

            Assert.IsTrue(rootNode.IsParentOf(childNode2));
            Assert.IsFalse(childNode2.IsParentOf(childNode1.GetNode("one")));

            Assert.IsFalse(childNode1.IsParentOf(childNode2));
            Assert.IsFalse(childNode2.IsParentOf(childNode1));

            Assert.IsFalse(childNode1.IsParentOf(childNode1));
            Assert.IsFalse(childNode2.IsParentOf(childNode2));
            Assert.IsFalse(rootNode.IsParentOf(rootNode));

        }

        [TestMethod]
        public void IsNodeChildOf()
        {
            IContentRepository contentRepository = ContentRepository;
            var rootNode = GetRootNode(contentRepository);
            var childNode1 = rootNode.GetNode("childNode1");
            var childNode2 = rootNode.GetNode("childNode2");
            var c1One = childNode1.GetNode("one");
            var c1Two = childNode1.GetNode("two");
            var c1Three = childNode1.GetNode("three");
            var c1ThreeOne = c1Three.GetNode("1_one");
            var c2One = childNode2.GetNode("one");
            var c2Two = childNode2.GetNode("two");
            var c2Three = childNode2.GetNode("three");

            Assert.IsTrue(childNode1.IsChildOf(rootNode));
            Assert.IsTrue(childNode2.IsChildOf(rootNode));
            Assert.IsTrue(c1One.IsChildOf(rootNode));
            Assert.IsTrue(c1Two.IsChildOf(rootNode));
            Assert.IsTrue(c1Three.IsChildOf(rootNode));
            Assert.IsTrue(c1ThreeOne.IsChildOf(rootNode));
            Assert.IsTrue(c2One.IsChildOf(rootNode));
            Assert.IsTrue(c2Two.IsChildOf(rootNode));
            Assert.IsTrue(c2Three.IsChildOf(rootNode));

            Assert.IsTrue(c1One.IsChildOf(childNode1));
            Assert.IsTrue(c1Two.IsChildOf(childNode1));
            Assert.IsTrue(c1Three.IsChildOf(childNode1));
            Assert.IsTrue(c1ThreeOne.IsChildOf(childNode1));

            Assert.IsTrue(c2One.IsChildOf(childNode2));
            Assert.IsTrue(c2Two.IsChildOf(childNode2));
            Assert.IsTrue(c2Three.IsChildOf(childNode2));

            Assert.IsFalse(c1One.IsChildOf(childNode2));
            Assert.IsFalse(c1Two.IsChildOf(childNode2));
            Assert.IsFalse(c1Three.IsChildOf(childNode2));
            Assert.IsFalse(c1ThreeOne.IsChildOf(childNode2));

            Assert.IsFalse(c2One.IsChildOf(childNode1));
            Assert.IsFalse(c2Two.IsChildOf(childNode1));
            Assert.IsFalse(c2Three.IsChildOf(childNode1));

            Assert.IsFalse(rootNode.IsChildOf(rootNode));
        }

        [TestMethod]
        public void IsNodeSiblingOf()
        {
            IContentRepository contentRepository = ContentRepository;
            var rootNode = GetRootNode(contentRepository);
            var childNode1 = rootNode.GetNode("childNode1");
            var childNode2 = rootNode.GetNode("childNode2");
            var childNode3 = rootNode.GetNode("childNode3");

            Assert.IsTrue(childNode1.IsSiblingOf(childNode2));
            Assert.IsTrue(childNode1.IsSiblingOf(childNode3));
            Assert.IsTrue(childNode2.IsSiblingOf(childNode1));
            Assert.IsTrue(childNode2.IsSiblingOf(childNode3));
            Assert.IsTrue(childNode3.IsSiblingOf(childNode1));

            Assert.IsFalse(childNode1.IsSiblingOf(childNode1));
            Assert.IsFalse(childNode2.IsSiblingOf(childNode2));
            Assert.IsFalse(childNode3.IsSiblingOf(childNode3));

            Assert.IsFalse(rootNode.IsSiblingOf(childNode1));
            Assert.IsFalse(rootNode.IsSiblingOf(childNode2));
            Assert.IsFalse(rootNode.IsSiblingOf(childNode3));

            Assert.IsFalse(childNode1.IsSiblingOf(rootNode));
            Assert.IsFalse(childNode2.IsSiblingOf(rootNode));
            Assert.IsFalse(childNode3.IsSiblingOf(rootNode));
        }

        [TestMethod]
        public void IsDeepNodeSiblingOf()
        {
            IContentRepository contentRepository = ContentRepository;
            var rootNode = GetRootNode(contentRepository);
            var childNode1 = rootNode.GetNode("childNode1");
            var childNode2 = rootNode.GetNode("childNode2");
            var c1One = childNode1.GetNode("one");
            var c1Two = childNode1.GetNode("two");
            var c1Three = childNode1.GetNode("three");
            var c2One = childNode2.GetNode("one");
            var c2Two = childNode2.GetNode("two");
            var c2Three = childNode2.GetNode("three");

            Assert.IsFalse(rootNode.IsSiblingOf(c1One));
            Assert.IsFalse(rootNode.IsSiblingOf(c1Two));
            Assert.IsFalse(rootNode.IsSiblingOf(c1Three));
            Assert.IsFalse(c1One.IsSiblingOf(rootNode));
            Assert.IsFalse(c1Two.IsSiblingOf(rootNode));
            Assert.IsFalse(c1Three.IsSiblingOf(rootNode));

            Assert.IsFalse(rootNode.IsSiblingOf(c2One));
            Assert.IsFalse(rootNode.IsSiblingOf(c2Two));
            Assert.IsFalse(rootNode.IsSiblingOf(c2Three));
            Assert.IsFalse(c2One.IsSiblingOf(rootNode));
            Assert.IsFalse(c2Two.IsSiblingOf(rootNode));
            Assert.IsFalse(c2Three.IsSiblingOf(rootNode));

            Assert.IsTrue(c1One.IsSiblingOf(c1Two));
            Assert.IsTrue(c1One.IsSiblingOf(c1Three));
            Assert.IsTrue(c1Two.IsSiblingOf(c1One));
            Assert.IsTrue(c1Two.IsSiblingOf(c1Three));
            Assert.IsTrue(c1Three.IsSiblingOf(c1One));
            Assert.IsTrue(c1Three.IsSiblingOf(c1Two));

            Assert.IsFalse(c2One.IsSiblingOf(c1One));
            Assert.IsFalse(c2One.IsSiblingOf(c1Two));
            Assert.IsFalse(c2One.IsSiblingOf(c1Three));
            Assert.IsFalse(c2Two.IsSiblingOf(c1One));
            Assert.IsFalse(c2Two.IsSiblingOf(c1Two));
            Assert.IsFalse(c2Two.IsSiblingOf(c1Three));
            Assert.IsFalse(c2Three.IsSiblingOf(c1One));
            Assert.IsFalse(c2Three.IsSiblingOf(c1Two));
            Assert.IsFalse(c2Three.IsSiblingOf(c1Three));
                
            Assert.IsFalse(c1One.IsSiblingOf(c2One));
            Assert.IsFalse(c1One.IsSiblingOf(c2Two));
            Assert.IsFalse(c1One.IsSiblingOf(c2Three));
            Assert.IsFalse(c1Two.IsSiblingOf(c2One));
            Assert.IsFalse(c1Two.IsSiblingOf(c2Two));
            Assert.IsFalse(c1Two.IsSiblingOf(c2Three));
            Assert.IsFalse(c1Three.IsSiblingOf(c2One));
            Assert.IsFalse(c1Three.IsSiblingOf(c2Two));
            Assert.IsFalse(c1Three.IsSiblingOf(c2Three));
        }

        [TestMethod]
        [ExpectedException(typeof(CannotMoveNodeException))]
        public void MoveParentBeforeChildNode()
        {
            IContentRepository contentRepository = ContentRepository;
            var rootNode = GetRootNode(contentRepository);
            var childNode2 = rootNode.GetNode("childNode2");
            var two = childNode2.GetNode("two");
            childNode2.MoveBefore(two);
        }

        [TestMethod]
        [ExpectedException(typeof(CannotMoveNodeException))]
        public void MoveParentBeforeDeepChildNode()
        {
            IContentRepository contentRepository = ContentRepository;
            var rootNode = GetRootNode(contentRepository);
            var childNode3 = rootNode.GetNode("childNode3");
            var two = childNode3.GetNode("two");
            var one = two.GetNode("3_one");
            childNode3.MoveBefore(one);
        }
    }
}
