using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Contendio.Test;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Contendio.Exceptions;

namespace Contendio.Sql.Test.SqlNode
{
    [TestClass]
    public class NodeAttributeTest : BaseTest
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
        public void SqlNode_VerifyRootNodeAttribute()
        {
            var contentRepository = ContentRepository;
            var rootNode = GetRootNode(contentRepository);
            var attributes = rootNode.Attributes;

            Assert.AreEqual(1, attributes.Count);
            Assert.AreEqual("attribute:unmovable", attributes[0].Name);
        }

        [TestMethod]
        [ExpectedException(typeof(CannotAddAttributeOnRootNode))]
        public void SqlNode_AddAttributeOnRoot_ExpectFailure()
        {
            var contentRepository = ContentRepository;
            var rootNode = GetRootNode(contentRepository);

            rootNode.AddAttribute("attribute:readonly");
        }
    }
}
