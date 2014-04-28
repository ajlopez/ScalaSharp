namespace ScalaSharp.Core.Tests.Ast
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ScalaSharp.Core.Ast;

    [TestClass]
    public class NodeContextTests
    {
        [TestMethod]
        public void GetUndefinedNode()
        {
            NodeContext context = new NodeContext();

            Assert.IsNull(context.GetNode("foo"));
        }

        [TestMethod]
        public void SetAndGetNode()
        {
            NodeContext context = new NodeContext();
            INode node = new NameNode("foo");

            context.SetNode("foo", node);

            var result = context.GetNode("foo");
            Assert.IsNotNull(result);
            Assert.AreSame(node, result);
        }
    }
}
