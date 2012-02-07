using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Contendio.Test.SqlNode
{
    [TestClass]
    public class SqlNodeTest : BaseTest
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
        public void SqlNode_IsNodeParentOf()
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
        public void SqlNode_IsNodeChildOf()
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
        public void SqlNode_IsNodeSiblingOf()
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
        public void SqlNode_IsDeepNodeSiblingOf()
        {
            var contentRepository = ContentRepository;
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
        public void SqlNode_RemoveChildNode()
        {
            var contentRepository = ContentRepository;

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
        public void SqlNode_RemoveSubChildNode()
        {
            var contentRepository = ContentRepository;

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
        public void SqlNode_RemoveRecursiveSubChildNode()
        {
            var contentRepository = ContentRepository;

            var rootNode = contentRepository.RootNode;
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
        public void SqlNode_AddAbsoluteNodePath()
        {
            var contentRepository = ContentRepository;

            var rootNode = contentRepository.RootNode;
            Assert.IsNotNull(rootNode);
            rootNode.AddNode("/childNode/subChildNode/nodeLevel3");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SqlNode_GetAbsoluteNodePath()
        {
            var contentRepository = ContentRepository;

            var rootNode = contentRepository.RootNode;
            Assert.IsNotNull(rootNode);
            rootNode.AddNode("childNode/subChildNode/nodeLevel3");
            rootNode.GetNode("/childNode/subChildNode/nodeLevel3");
        }

        [TestMethod]
        public void SqlNode_GetRelativeDeepNodePath()
        {
            var contentRepository = ContentRepository;

            var rootNode = contentRepository.RootNode;
            Assert.IsNotNull(rootNode);
            var three = rootNode.AddNode("childNode/one/two/three");
            Assert.AreEqual("three", three.Name);
            Assert.AreEqual("/childNode/one/two/three", three.Path);

            three = rootNode.GetNode("childNode/one/two/three");
            Assert.AreEqual("three", three.Name);
            Assert.AreEqual("/childNode/one/two/three", three.Path);

            var childNode = rootNode.GetNode("childNode");
            three = childNode.GetNode("one/two/three");
            Assert.AreEqual("three", three.Name);
            Assert.AreEqual("/childNode/one/two/three", three.Path);

            var four = childNode.GetNode("one/two/three/four");
            Assert.IsNull(four);
        }

        [TestMethod]
        public void SqlNode_SetParentUsingProperty()
        {
            var contentRepository = ContentRepository;

            var rootNode = contentRepository.RootNode;
            Assert.IsNotNull(rootNode);
            var childNode1 = rootNode.AddNode("childNode1");
            var childNode2 = rootNode.AddNode("childNode2");
            var subChildNode1 = childNode1.AddNode("subChildNode1");

            var children = childNode1.Children;
            Assert.AreEqual(1, children.Count);
            Assert.AreEqual("subChildNode1", children[0].Name);
            Assert.AreEqual("/childNode1/subChildNode1", children[0].Path);

            children = childNode2.Children;
            Assert.AreEqual(0, children.Count);

            children = childNode1.Children;
            children[0].ParentNode = childNode2;

            children = childNode1.Children;
            Assert.AreEqual(0, children.Count);

            children = childNode2.Children;
            Assert.AreEqual(1, children.Count);
            Assert.AreEqual("subChildNode1", children[0].Name);
            Assert.AreEqual("/childNode2/subChildNode1", children[0].Path);
        }

        [TestMethod]
        public void SqlNode_AddChildNode()
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
        public void SqlNode_AddMultipleChildNodes()
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
        public void SqlNode_AddSubChildNodes()
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

            var subChildNode1 = childNode.GetNode("subChildNode1");
            Assert.IsNotNull(subChildNode1);
            Assert.AreEqual("/childNode1/subChildNode1", subChildNode1.Path);
            Assert.AreEqual("subChildNode1", subChildNode1.Name);
            Assert.AreEqual(1, childNode.Children.Count);
        }

        [TestMethod]
        public void SqlNode_MultipleAddOneLine()
        {
            IContentRepository contentRepository = ContentRepository;
            var rootNode = contentRepository.RootNode;
            Assert.IsNotNull(rootNode);

            using (var transaction = new TransactionScope())
            {
                var result = rootNode.AddNode("childNode1/subChildNode1/subsubChildNode1");
                Assert.IsNotNull(result);
                Assert.AreEqual("/childNode1/subChildNode1/subsubChildNode1", result.Path);
                Assert.AreEqual("subsubChildNode1", result.Name);
                transaction.Complete();
            }


            var childNode1 = rootNode.GetNode("childNode1");
            Assert.AreEqual("/childNode1", childNode1.Path);
            Assert.AreEqual("childNode1", childNode1.Name);
            Assert.AreEqual(1, childNode1.Children.Count);

            var subChildNode1 = childNode1.GetNode("subChildNode1");
            Assert.AreEqual("/childNode1/subChildNode1", subChildNode1.Path);
            Assert.AreEqual("subChildNode1", subChildNode1.Name);
            Assert.AreEqual(1, subChildNode1.Children.Count);

            var subsubChildNode1 = subChildNode1.GetNode("subsubChildNode1");
            Assert.AreEqual("/childNode1/subChildNode1/subsubChildNode1", subsubChildNode1.Path);
            Assert.AreEqual("subsubChildNode1", subsubChildNode1.Name);
            Assert.AreEqual(0, subsubChildNode1.Children.Count);
        }

        [TestMethod]
        public void SqlNode_AddSameNodeTwice()
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
