namespace ScalaSharp.Core.Tests.Ast
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ScalaSharp.Core.Ast;
    using ScalaSharp.Core.Expressions;
    using ScalaSharp.Core.Language;

    [TestClass]
    public class ConstantNodeTests
    {
        [TestMethod]
        public void CreateNodeWithIntegerValue()
        {
            ConstantNode node = new ConstantNode(42);

            Assert.IsNotNull(node.Value);
            Assert.AreEqual(42, node.Value);
            Assert.AreSame(TypeInfo.Int, node.TypeInfo);

            node.RegisterInContext(null);
            node.CheckType(null);
            Assert.AreSame(TypeInfo.Int, node.TypeInfo);
        }

        [TestMethod]
        public void GetExpression()
        {
            ConstantNode node = new ConstantNode(42);

            var expr = node.GetExpression();

            Assert.IsNotNull(expr);
            Assert.IsInstanceOfType(expr, typeof(ConstantExpression));
            Assert.AreEqual(42, ((ConstantExpression)expr).Value);
        }
    }
}
