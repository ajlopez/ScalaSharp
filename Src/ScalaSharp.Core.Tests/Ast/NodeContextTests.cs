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
    }
}
