namespace ScalaSharp.Core.Tests.Ast
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ScalaSharp.Core.Ast;

    [TestClass]
    public class InvokeMethodNodeTests
    {
        [TestMethod]
        public void CreateInvokeMethodNode()
        {
            INode target = new NameNode("foo");
            IList<INode> arguments = new List<INode>() { new ConstantNode(42) };
            InvokeMethodNode node = new InvokeMethodNode(target, "append", arguments);

            node.RegisterInContext(null);
            Assert.AreSame(target, node.Target);
            Assert.AreSame(arguments, node.Arguments);
            Assert.AreEqual("append", node.MethodName);
        }
    }
}
