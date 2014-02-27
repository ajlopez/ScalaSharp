namespace ScalaSharp.Core.Tests.Ast
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ScalaSharp.Core.Ast;

    [TestClass]
    public class InvokeNodeTests
    {
        [TestMethod]
        public void CreateInvokeNode()
        {
            IList<INode> arguments = new List<INode>() { new ConstantNode(42) };
            InvokeNode node = new InvokeNode("append", arguments);

            Assert.AreSame(arguments, node.Arguments);
            Assert.AreEqual("append", node.MethodName);
        }
    }
}
