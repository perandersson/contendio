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

        /**
         * node:root
         * |-   childNode1
         *      |-  one
         *          two
         *          three
         *          |- one
         *          four
         *          five
         *      childNode2
         *      |-  one
         *          two
         *          three
         *          |- one
         *          four
         *          five
         *          |- one
         *      childNode3
         *      |-  one
         *          two
         *          |- one
         *          three
         *          four
         *          five
         */
        public INode GetRootNode(IContentRepository contentRepository)
        {
            var rootNode = contentRepository.RootNode;
            var childNode = rootNode.AddNode("childNode1");
            childNode.AddNode("one");
            childNode.AddNode("two");
            childNode.AddNode("three").AddNode("one");
            childNode.AddNode("four");
            childNode.AddNode("five");
            childNode = rootNode.AddNode("childNode2");
            childNode.AddNode("one");
            childNode.AddNode("two");
            childNode.AddNode("three").AddNode("one");
            childNode.AddNode("four");
            childNode.AddNode("five").AddNode("one");
            childNode = rootNode.AddNode("childNode3");
            childNode.AddNode("one");
            childNode.AddNode("two").AddNode("one");
            childNode.AddNode("three");
            childNode.AddNode("four");
            childNode.AddNode("five");
            return rootNode;
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
            IContentRepository contentRepository = ContentRepository;
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
        public void MoveChildNodeAfterRoot()
        {
            IContentRepository contentRepository = ContentRepository;
            var rootNode = GetRootNode(contentRepository);
            var childNode = rootNode.GetNode("childNode2");
            childNode.MoveAfter(rootNode);
        }

        [TestMethod]
        [ExpectedException(typeof(CannotMoveNodeException))]
        public void MoveRootNodeAfterChild()
        {
            IContentRepository contentRepository = ContentRepository;
            var rootNode = GetRootNode(contentRepository);
            var childNode = rootNode.GetNode("childNode2");
            rootNode.MoveAfter(childNode);
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
        public void MoveSameChildNodeAfter()
        {

            IContentRepository contentRepository = ContentRepository;
            var rootNode = GetRootNode(contentRepository);
            var children = rootNode.Children;

            Assert.AreEqual(3, children.Count);
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

        [TestMethod]
        public void IsNodeParentOf()
        {
            IContentRepository contentRepository = ContentRepository;
            var rootNode = GetRootNode(contentRepository);

            var childNode1 = rootNode.GetNode("childNode1");
            Assert.IsTrue(rootNode.IsParentOf(childNode1));
            Assert.IsTrue(rootNode.IsParentOf(childNode1.GetNode("one")));
            Assert.IsTrue(childNode1.IsParentOf(childNode1.GetNode("one")));

            var childNode2 = rootNode.GetNode("childNode2");
            Assert.IsTrue(rootNode.IsParentOf(childNode2));
            Assert.IsFalse(childNode2.IsParentOf(childNode1.GetNode("one")));

            Assert.IsFalse(childNode1.IsParentOf(childNode2));
            Assert.IsFalse(childNode2.IsParentOf(childNode1));

            Assert.IsFalse(childNode1.IsParentOf(childNode1));
            Assert.IsFalse(childNode2.IsParentOf(childNode2));
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
            var one = two.GetNode("one");
            childNode3.MoveBefore(one);
        }
    }
}
