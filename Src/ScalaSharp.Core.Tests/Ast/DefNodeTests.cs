namespace ScalaSharp.Core.Tests.Ast
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ScalaSharp.Core.Ast;
    using ScalaSharp.Core.Commands;
    using ScalaSharp.Core.Expressions;
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
            IExpressionNode expression = new ConstantNode(42);

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
            IExpressionNode expression = new ConstantNode(42);

            DefNode node = new DefNode(name, arguments, null, expression);

            Assert.IsNull(node.TypeInfo);

            node.CheckType();

            Assert.AreSame(TypeInfo.Int, node.TypeInfo);
        }

        [TestMethod]
        public void GetCommand()
        {
            string name = "foo";
            IList<ArgumentInfo> arguments = new List<ArgumentInfo>() { new ArgumentInfo("a", TypeInfo.Int), new ArgumentInfo("b", TypeInfo.Double) };
            TypeInfo typeinfo = TypeInfo.Int;
            IExpressionNode expression = new ConstantNode(42);

            DefNode node = new DefNode(name, arguments, typeinfo, expression);

            var result = node.GetCommand();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(DefCommand));

            var dcmd = (DefCommand)result;

            Assert.AreEqual(name, dcmd.Name);
            Assert.AreEqual(TypeInfo.Int, dcmd.TypeInfo);
            Assert.IsNotNull(dcmd.Arguments);
            Assert.AreEqual(2, dcmd.Arguments.Count);
            Assert.AreSame(arguments, dcmd.Arguments);

            Assert.IsNotNull(dcmd.Body);
            Assert.IsInstanceOfType(dcmd.Body, typeof(ExpressionCommand));

            var body = (ExpressionCommand)dcmd.Body;
            Assert.IsInstanceOfType(body.Expression, typeof(ConstantExpression));
            Assert.AreEqual(42, ((ConstantExpression)body.Expression).Value);
        }

        [TestMethod]
        public void RaiseIfTypeMismatch()
        {
            string name = "foo";
            IList<ArgumentInfo> arguments = new List<ArgumentInfo>() { new ArgumentInfo("a", TypeInfo.Int), new ArgumentInfo("b", TypeInfo.Double) };
            TypeInfo typeinfo = TypeInfo.String;
            IExpressionNode expression = new ConstantNode(42);

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
