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
    public class AssignmentNodeTests
    {
        [TestMethod]
        public void CreateAssignmentNode()
        {
            NameNode target = new NameNode("foo");
            ConstantNode expr = new ConstantNode(42);
            AssignmentNode node = new AssignmentNode(target, expr);

            Assert.AreSame(target, node.Target);
            Assert.AreSame(expr, node.Expression);
            Assert.AreSame(TypeInfo.Int, node.TypeInfo);
        }
    }
}

