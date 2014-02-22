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
    public class VarNodeTests
    {
        [TestMethod]
        public void CreateVarNode()
        {
            string name = "foo";
            TypeInfo typeinfo = TypeInfo.Int;
            INode expression = new ConstantNode(42);

            VarNode node = new VarNode(name, typeinfo, expression);

            Assert.AreEqual(name, node.Name);
            Assert.AreSame(TypeInfo.Int, node.TypeInfo);
            Assert.AreSame(expression, node.Expression);
        }

        [TestMethod]
        public void CreateVarNodeWithoutTypeInfo()
        {
            string name = "foo";
            TypeInfo typeinfo = TypeInfo.Int;
            INode expression = new ConstantNode(42);

            VarNode node = new VarNode(name, null, expression);

            Assert.IsNull(node.TypeInfo);

            node.CheckType();

            Assert.AreSame(TypeInfo.Int, node.TypeInfo);
        }

        [TestMethod]
        public void RaiseIfTypeMismatch()
        {
            string name = "foo";
            TypeInfo typeinfo = TypeInfo.Int;
            INode expression = new ConstantNode(42);

            VarNode node = new VarNode(name, TypeInfo.Double, expression);

            Assert.AreSame(TypeInfo.Double, node.TypeInfo);

            try
            {
                node.CheckType();
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.AreEqual("type mismatch", ex.Message);
            }
        }
    }
}
