namespace ScalaSharp.Core.Tests.Ast
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ScalaSharp.Core.Ast;
    using ScalaSharp.Core.Language;

    [TestClass]
    public class ValNodeTests
    {
        [TestMethod]
        public void CreateValNode()
        {
            string name = "foo";
            TypeInfo typeinfo = TypeInfo.Int;
            IExpressionNode expression = new ConstantNode(42);

            ValNode node = new ValNode(name, typeinfo, expression);

            Assert.AreEqual(name, node.Name);
            Assert.AreSame(TypeInfo.Int, node.TypeInfo);
            Assert.AreSame(expression, node.Expression);
        }

        [TestMethod]
        public void CreateValNodeWithoutTypeInfo()
        {
            string name = "foo";
            TypeInfo typeinfo = TypeInfo.Int;
            IExpressionNode expression = new ConstantNode(42);

            ValNode node = new ValNode(name, null, expression);

            Assert.IsNull(node.TypeInfo);

            node.CheckType(null);

            Assert.AreSame(TypeInfo.Int, node.TypeInfo);
        }
    }
}
