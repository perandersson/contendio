using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Contendio.Sql.Test.ContentRepository
{
    [TestClass]
    public class MultipleWorkspaces
    {
        private const string DatabaseSchema = "contendio";
        private const string Workspace = "test";
        private const string Workspace2 = "other";
        private const string ConnectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=" + DatabaseSchema + ";Integrated Security=True";

        protected IRepositorySetup ContentInstall
        {
            get
            {
                return new SqlRepositorySetup(Workspace, ConnectionString);
            }
        }

        protected IRepositorySetup ContentInstall2
        {
            get
            {
                return new SqlRepositorySetup(Workspace2, ConnectionString);
            }
        }

        protected IContentRepository ContentRepository
        {
            get
            {
                var contentRepository = new SqlContentRepository(Workspace, ConnectionString);
                return contentRepository;
            }
        }

        protected IContentRepository ContentRepository2
        {
            get
            {
                var contentRepository = new SqlContentRepository(Workspace2, ConnectionString);
                return contentRepository;
            }
        }

        [TestInitialize]
        public void Initialize()
        {
            ContentInstall.Install();
            ContentInstall2.Install();
        }

        [TestCleanup]
        public void Cleanup()
        {
            ContentInstall.Uninstall();
            ContentInstall2.Uninstall();
        }

        [TestMethod]
        public void SqlContentRepository_MultipleWorkspaces_GetRootNode()
        {
            var contentRepository1 = ContentRepository;
            var contentRepository2 = ContentRepository2;

            var root1 = contentRepository1.RootNode;
            var root2 = contentRepository2.RootNode;

            Assert.IsNotNull(root1);
            Assert.IsNotNull(root2);
            Assert.AreEqual("/", root1.Path);
            Assert.AreEqual("/", root2.Path);
        }

        [TestMethod]
        public void SqlContentRepository_MultipleWorkspaces_AddChildNode()
        {
            var contentRepository1 = ContentRepository;
            var contentRepository2 = ContentRepository2;
            var root1 = contentRepository1.RootNode;
            var root2 = contentRepository2.RootNode;
            root1.AddNode("childNode1");
            var children1 = root1.Children;
            var children2 = root2.Children;

            Assert.IsNotNull(children1);
            Assert.IsNotNull(children2);

            Assert.AreEqual(1, children1.Count);
            Assert.AreEqual(0, children2.Count);


        }
    }
}
