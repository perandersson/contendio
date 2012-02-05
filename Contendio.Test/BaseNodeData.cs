using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Contendio.Model;

namespace Contendio.Test
{
    public abstract class BaseNodeData
    {

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
        public INode GetRootNode(IContentRepository contentRepository)
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
    }
}
