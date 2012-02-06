using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Contendio.Model;
using Contendio.Sql;

namespace Contendio.Test
{
    public abstract class BaseTest
    {
        private const string DatabaseSchema = "contendio";
        private const string Workspace = "test";
        private const string ConnectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=" + DatabaseSchema + ";Integrated Security=True";

        protected IRepositorySetup ContentInstall
        {   
            get
            {
                return new SqlRepositorySetup(Workspace, ConnectionString);
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

        /**
         * node:root
         * |-   childNode1
         *      |-  one
         *          two
         *          three
         *          |- 1_one
         *          four
         *          five
         *      childNode2
         *      |-  one
         *          two
         *          three
         *          |- 2_one
         *          four
         *          five
         *          |- 2_one
         *      childNode3
         *      |-  one
         *          two
         *          |- 3_one
         *          three
         *          four
         *          five
         */
        protected INode GetRootNode(IContentRepository contentRepository)
        {
            var rootNode = contentRepository.RootNode;
            var childNode = rootNode.AddNode("childNode1");
            childNode.AddNode("one");
            childNode.AddNode("two");
            childNode.AddNode("three").AddNode("1_one");
            childNode.AddNode("four");
            childNode.AddNode("five");
            childNode = rootNode.AddNode("childNode2");
            childNode.AddNode("one");
            childNode.AddNode("two");
            childNode.AddNode("three").AddNode("2_one");
            childNode.AddNode("four");
            childNode.AddNode("five").AddNode("2_one");
            childNode = rootNode.AddNode("childNode3");
            childNode.AddNode("one");
            childNode.AddNode("two").AddNode("3_one");
            childNode.AddNode("three");
            childNode.AddNode("four");
            childNode.AddNode("five");
            return rootNode;
        }

        public static bool DateAreEqual(DateTime date, DateTime dateFromDatabase)
        {
            return date.ToString(CultureInfo.InvariantCulture).Equals(dateFromDatabase.ToString(CultureInfo.InvariantCulture));
        }

    }
}
