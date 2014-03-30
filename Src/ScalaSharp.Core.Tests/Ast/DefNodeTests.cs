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
    public class DefNodeTests
    {
        [TestMethod]
        public void CreateDefNode()
        {
            string name = "foo";
            IList<ArgumentInfo> arguments = new List<ArgumentInfo>() { new ArgumentInfo("a", TypeInfo.Int), new ArgumentInfo("b", TypeInfo.Double) };
            TypeInfo typeinfo = TypeInfo.Int;
            INode expression = new ConstantNode(42);

            DefNode node = new DefNode(name, arguments, typeinfo, expression);

            Assert.AreEqual(name, node.Name);
            Assert.AreSame(arguments, node.Arguments);
            Assert.AreSame(TypeInfo.Int, node.TypeInfo);
            Assert.AreSame(expression, node.Expression);
        }

        [TestMethod]
        public void CreateDefNodeWithoutTypeInfo()
        {
            string name = "foo";
            IList<ArgumentInfo> arguments = new List<ArgumentInfo>() { new ArgumentInfo("a", TypeInfo.Int), new ArgumentInfo("b", TypeInfo.Double) };
            TypeInfo typeinfo = TypeInfo.Int;
            INode expression = new ConstantNode(42);

            DefNode node = new DefNode(name, arguments, null, expression);

            Assert.IsNull(node.TypeInfo);

            node.CheckType();

            Assert.AreSame(TypeInfo.Int, node.TypeInfo);
        }

        [TestMethod]
        public void RaiseIfTypeMismatch()
        {
            string name = "foo";
            IList<ArgumentInfo> arguments = new List<ArgumentInfo>() { new ArgumentInfo("a", TypeInfo.Int), new ArgumentInfo("b", TypeInfo.Double) };
            TypeInfo typeinfo = TypeInfo.String;
            INode expression = new ConstantNode(42);

            DefNode node = new DefNode(name, arguments, typeinfo, expression);

            Assert.IsNotNull(node.TypeInfo);
            Assert.AreSame(TypeInfo.String, node.TypeInfo);

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
