using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Contendio.Sql;
using System.Data.Linq;
using System.Transactions;
using Contendio.Event.Args;
using Contendio.Event;

namespace Contendio.Test
{
    [TestClass]
    public class EventTest
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

        //private bool testSimpleNodeEvent1_triggered = false;

        //[TestMethod]
        //public void TestSimpleNodeEvent1()
        //{
        //    IContentRepository contentRepository = ContentRepository;
        //    var rootNode = contentRepository.RootNode;
        //    Assert.IsNotNull(rootNode);

        //    rootNode.OnNodeChanged += new NodeChangeEventHandler(EventTestSimpleNodeEvent1);
        //    using (var transaction = new TransactionScope())
        //    {
        //        rootNode.CreateNode("childNode1");
        //        transaction.Complete();
        //    }

        //    Assert.IsTrue(testSimpleNodeEvent1_triggered);
        //}

        //void EventTestSimpleNodeEvent1(object sender, NodeChangeArgs args)
        //{
        //    Assert.IsNotNull(args.Node);
        //    Assert.AreEqual(RepositoryEventType.NodeAdded, args.EventType);

        //    testSimpleNodeEvent1_triggered = true;
        //}

        //private int testSimpleNodeEvent2_triggered = 0;

        //[TestMethod]
        //public void TestSimpleNodeEvent2()
        //{
        //    IContentRepository contentRepository = ContentRepository;
        //    var rootNode = contentRepository.RootNode;
        //    Assert.IsNotNull(rootNode);

        //    rootNode.OnNodeChanged += new NodeChangeEventHandler(EventTestSimpleNodeEvent2);
        //    using (var transaction = new TransactionScope())
        //    {
        //        rootNode.CreateNode("childNode1");
        //        transaction.Complete();
        //    }

        //    Assert.AreEqual(1, testSimpleNodeEvent2_triggered);

        //    using (var transaction = new TransactionScope())
        //    {
        //        rootNode.CreateNode("childNode2");
        //        transaction.Complete();
        //    }

        //    Assert.AreEqual(1, testSimpleNodeEvent2_triggered);
        //}

        //void EventTestSimpleNodeEvent2(object sender, NodeChangeArgs args)
        //{
        //    Assert.IsNotNull(args.Node);
        //    Assert.AreEqual(RepositoryEventType.NodeAdded, args.EventType);

        //    testSimpleNodeEvent2_triggered++;
        //}
    }
}
