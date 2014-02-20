namespace ScalaSharp.Core.Tests.Ast
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ScalaSharp.Core.Ast;
    using ScalaSharp.Core.Language;

    [TestClass]
    public class IfNodeTest
    {
        [TestMethod]
        public void CreateIfNode()
        {
            var condition = new ConstantNode(true);
            var then = new ConstantNode(42);

            var node = new IfNode(condition, then, null);

            Assert.AreSame(condition, node.Condition);
            Assert.AreSame(then, node.Then);
            Assert.IsNull(node.Else);
            Assert.AreSame(TypeInfo.Int, node.TypeInfo);
        }
    }
}
