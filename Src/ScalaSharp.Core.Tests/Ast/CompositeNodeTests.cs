﻿namespace ScalaSharp.Core.Tests.Ast
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ScalaSharp.Core.Ast;
    using ScalaSharp.Core.Language;

    [TestClass]
    public class CompositeNodeTests
    {
        [TestMethod]
        public void CreateCompositeNodeWithTwoConstantNodes()
        {
            INode node1 = new ConstantNode(42);
            INode node2 = new ConstantNode("foo");

            CompositeNode node = new CompositeNode(new INode[] { node1, node2 });

            Assert.IsNotNull(node.Nodes);
            Assert.AreEqual(2, node.Nodes.Count);
            Assert.AreSame(node1, node.Nodes[0]);
            Assert.AreSame(node2, node.Nodes[1]);
            Assert.AreSame(TypeInfo.String, node.TypeInfo);
        }
    }
}
