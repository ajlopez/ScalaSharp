namespace ScalaSharp.Core.Tests.Parsing
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ScalaSharp.Core.Ast;
    using ScalaSharp.Core.Commands;
    using ScalaSharp.Core.Parsing;

    [TestClass]
    public class ParserNodeTests
    {
        [TestMethod]
        public void ParseNullAsNull()
        {
            Parser parser = new Parser(null);

            Assert.IsNull(parser.ParseNode());
        }

        [TestMethod]
        public void ParseEmptyStringNull()
        {
            Parser parser = new Parser(string.Empty);

            Assert.IsNull(parser.ParseNode());
        }

        [TestMethod]
        public void ParseSpacesStringNull()
        {
            Parser parser = new Parser("  ");

            Assert.IsNull(parser.ParseNode());
        }

        [TestMethod]
        public void ParseStringNode()
        {
            Parser parser = new Parser("\"foo\"");

            var result = parser.ParseNode();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ConstantNode));

            var node = (ConstantNode)result;

            Assert.AreEqual("foo", node.Value);

            Assert.IsNull(parser.ParseNode());
        }

        [TestMethod]
        public void ParseIntegerNode()
        {
            Parser parser = new Parser("123");

            var result = parser.ParseNode();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ConstantNode));

            var node = (ConstantNode)result;

            Assert.AreEqual(123, node.Value);

            Assert.IsNull(parser.ParseNode());
        }

        [TestMethod]
        public void ParseRealNode()
        {
            Parser parser = new Parser("123.45");

            var result = parser.ParseNode();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ConstantNode));

            var node = (ConstantNode)result;

            Assert.AreEqual(123.45, node.Value);

            Assert.IsNull(parser.ParseNode());
        }

        [TestMethod]
        public void ParseNameNode()
        {
            Parser parser = new Parser("name");

            var result = parser.ParseNode();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NameNode));

            var node = (NameNode)result;

            Assert.AreEqual("name", node.Name);

            Assert.IsNull(parser.ParseNode());
        }

        [TestMethod]
        public void ParseEmptyClassNode()
        {
            Parser parser = new Parser("class Foo { }");

            var result = parser.ParseNode();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ClassNode));

            var node = (ClassNode)result;

            Assert.AreEqual("Foo", node.Name);
            Assert.IsNull(node.Body);
            Assert.AreEqual("Foo", node.TypeInfo.Name);

            Assert.IsNull(parser.ParseCommand());
        }

        [TestMethod]
        public void ParseClassNodeWithOneDefNode()
        {
            Parser parser = new Parser("class Foo { def one = 1 }");

            var result = parser.ParseNode();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ClassNode));

            var node = (ClassNode)result;

            Assert.AreEqual("Foo", node.Name);
            Assert.IsNotNull(node.Body);
            Assert.IsInstanceOfType(node.Body, typeof(DefNode));
            Assert.AreEqual("Foo", node.TypeInfo.Name);

            Assert.IsNull(parser.ParseCommand());
        }

        [TestMethod]
        public void ParseClassNodeWithTwoDefNodes()
        {
            Parser parser = new Parser("class Foo { def one = 1; def two = 2 }");

            var result = parser.ParseNode();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ClassNode));

            var node = (ClassNode)result;

            Assert.AreEqual("Foo", node.Name);
            Assert.IsNotNull(node.Body);
            Assert.IsInstanceOfType(node.Body, typeof(CompositeNode));
            Assert.AreEqual("Foo", node.TypeInfo.Name);

            Assert.IsNull(parser.ParseCommand());
        }

        [TestMethod]
        public void RaiseIsNoNameInClassCommand()
        {
            Parser parser = new Parser("class { }");

            try
            {
                parser.ParseNode();
                Assert.Fail();
            }
            catch (ParserException ex)
            {
                Assert.AreEqual("Expected a name", ex.Message);
            }
        }

        [TestMethod]
        public void ParseEmptyObjectNode()
        {
            Parser parser = new Parser("object Foo { }");

            var result = parser.ParseNode();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ObjectNode));

            var node = (ObjectNode)result;

            Assert.AreEqual("Foo", node.Name);
            Assert.IsNull(node.Body);
            Assert.AreEqual("Foo", node.TypeInfo.Name);

            Assert.IsNull(parser.ParseCommand());
        }

        [TestMethod]
        public void ParseEmptyObjectNodeWithOneDefNode()
        {
            Parser parser = new Parser("object Foo { def one = 1 }");

            var result = parser.ParseNode();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ObjectNode));

            var node = (ObjectNode)result;

            Assert.AreEqual("Foo", node.Name);
            Assert.IsNotNull(node.Body);
            Assert.IsInstanceOfType(node.Body, typeof(DefNode));
            Assert.AreEqual("Foo", node.TypeInfo.Name);

            Assert.IsNull(parser.ParseCommand());
        }

        [TestMethod]
        public void ParseEmptyObjectNodeWithTwoDefNodes()
        {
            Parser parser = new Parser("object Foo { def one = 1; def two = 2 }");

            var result = parser.ParseNode();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ObjectNode));

            var node = (ObjectNode)result;

            Assert.AreEqual("Foo", node.Name);
            Assert.IsNotNull(node.Body);
            Assert.IsInstanceOfType(node.Body, typeof(CompositeNode));
            Assert.AreEqual("Foo", node.TypeInfo.Name);

            Assert.IsNull(parser.ParseCommand());
        }

        [TestMethod]
        public void RaiseIsNoNameInObjectCommand()
        {
            Parser parser = new Parser("object { }");

            try
            {
                parser.ParseNode();
                Assert.Fail();
            }
            catch (ParserException ex)
            {
                Assert.AreEqual("Expected a name", ex.Message);
            }
        }

        [TestMethod]
        public void ParseSimpleDefNode()
        {
            Parser parser = new Parser("def foo: unit");

            var result = parser.ParseNode();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(DefNode));

            var dnode = (DefNode)result;

            Assert.AreEqual("foo", dnode.Name);
            Assert.IsNotNull(dnode.TypeInfo);
            Assert.AreEqual("unit", dnode.TypeInfo.Name);
            Assert.IsNotNull(dnode.Arguments);
            Assert.AreEqual(0, dnode.Arguments.Count);
            Assert.IsNull(dnode.Expression);
        }

        [TestMethod]
        public void ParseDefNodeWithConstantNode()
        {
            Parser parser = new Parser("def foo = 0");

            var result = parser.ParseNode();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(DefNode));

            var dnode = (DefNode)result;

            Assert.AreEqual("foo", dnode.Name);
            Assert.IsNotNull(dnode.Arguments);
            Assert.AreEqual(0, dnode.Arguments.Count);
            Assert.IsNotNull(dnode.Expression);
            Assert.IsInstanceOfType(dnode.Expression, typeof(ConstantNode));

            var expr = (ConstantNode)dnode.Expression;

            Assert.AreEqual(0, expr.Value);
        }

        [TestMethod]
        public void ParseSimpleDefNodeEmptyParenthesis()
        {
            Parser parser = new Parser("def foo(): unit");

            var result = parser.ParseNode();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(DefNode));

            var dnode = (DefNode)result;

            Assert.AreEqual("foo", dnode.Name);
            Assert.IsNotNull(dnode.TypeInfo);
            Assert.AreEqual("unit", dnode.TypeInfo.Name);
            Assert.IsNotNull(dnode.Arguments);
            Assert.AreEqual(0, dnode.Arguments.Count);
        }

        [TestMethod]
        public void ParseSimpleDefNodeWithTwoIntegerArguments()
        {
            Parser parser = new Parser("def foo(x: Int, y: Int): Int");

            var result = parser.ParseNode();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(DefNode));

            var dnode = (DefNode)result;

            Assert.AreEqual("foo", dnode.Name);
            Assert.IsNotNull(dnode.TypeInfo);
            Assert.AreEqual("Int", dnode.TypeInfo.Name);
            Assert.IsNotNull(dnode.Arguments);
            Assert.AreEqual(2, dnode.Arguments.Count);
            Assert.AreEqual("x", dnode.Arguments[0].Name);
            Assert.AreEqual("Int", dnode.Arguments[0].TypeInfo.Name);
            Assert.AreEqual("y", dnode.Arguments[1].Name);
            Assert.AreEqual("Int", dnode.Arguments[1].TypeInfo.Name);
        }

        [TestMethod]
        public void RaiseIsNoNameInDefNode()
        {
            Parser parser = new Parser("def: unit");

            try
            {
                parser.ParseNode();
                Assert.Fail();
            }
            catch (ParserException ex)
            {
                Assert.AreEqual("Expected a name", ex.Message);
            }
        }

        [TestMethod]
        public void RaiseEndOfInputInDefNode()
        {
            Parser parser = new Parser("def name");

            try
            {
                parser.ParseNode();
                Assert.Fail();
            }
            catch (ParserException ex)
            {
                Assert.AreEqual("Expected ':' or '='", ex.Message);
            }
        }

        [TestMethod]
        public void ParseSimpleValNodeWithIntegerExpression()
        {
            Parser parser = new Parser("val one = 1");

            var node = parser.ParseNode();

            Assert.IsNotNull(node);
            Assert.IsInstanceOfType(node, typeof(ValNode));

            var vnode = (ValNode)node;

            Assert.AreEqual("one", vnode.Name);
            Assert.IsNotNull(vnode.Expression);
            Assert.IsInstanceOfType(vnode.Expression, typeof(ConstantNode));

            var expr = (ConstantNode)vnode.Expression;

            Assert.AreEqual(1, expr.Value);

            Assert.IsNull(parser.ParseNode());
        }

        [TestMethod]
        public void ParseSimpleValNodeWithEndOfLine()
        {
            Parser parser = new Parser("val one = 1\n");

            var node = parser.ParseNode();

            Assert.IsNotNull(node);
            Assert.IsInstanceOfType(node, typeof(ValNode));

            var vnode = (ValNode)node;

            Assert.AreEqual("one", vnode.Name);
            Assert.IsNotNull(vnode.Expression);
            Assert.IsInstanceOfType(vnode.Expression, typeof(ConstantNode));

            var expr = (ConstantNode)vnode.Expression;

            Assert.AreEqual(1, expr.Value);

            Assert.IsNull(parser.ParseNode());
        }

        [TestMethod]
        public void ParseSimpleValNodeWithSemicolon()
        {
            Parser parser = new Parser("val one = 1;");

            var node = parser.ParseNode();

            Assert.IsNotNull(node);
            Assert.IsInstanceOfType(node, typeof(ValNode));

            var vnode = (ValNode)node;

            Assert.AreEqual("one", vnode.Name);
            Assert.IsNotNull(vnode.Expression);
            Assert.IsInstanceOfType(vnode.Expression, typeof(ConstantNode));

            var expr = (ConstantNode)vnode.Expression;

            Assert.AreEqual(1, expr.Value);

            Assert.IsNull(parser.ParseNode());
        }

        [TestMethod]
        public void ParseSimpleVarNodeWithIntegerExpression()
        {
            Parser parser = new Parser("var one = 1");

            var node = parser.ParseNode();

            Assert.IsNotNull(node);
            Assert.IsInstanceOfType(node, typeof(VarNode));

            var vnode = (VarNode)node;

            Assert.AreEqual("one", vnode.Name);
            Assert.IsNotNull(vnode.Expression);
            Assert.IsInstanceOfType(vnode.Expression, typeof(ConstantNode));

            var expr = (ConstantNode)vnode.Expression;

            Assert.AreEqual(1, expr.Value);

            Assert.IsNull(parser.ParseNode());
        }

        [TestMethod]
        public void ParseSimpleVarNodeWithEndOfLine()
        {
            Parser parser = new Parser("var one = 1\n");

            var node = parser.ParseNode();

            Assert.IsNotNull(node);
            Assert.IsInstanceOfType(node, typeof(VarNode));

            var vnode = (VarNode)node;

            Assert.AreEqual("one", vnode.Name);
            Assert.IsNotNull(vnode.Expression);
            Assert.IsInstanceOfType(vnode.Expression, typeof(ConstantNode));

            var expr = (ConstantNode)vnode.Expression;

            Assert.AreEqual(1, expr.Value);

            Assert.IsNull(parser.ParseNode());
        }

        [TestMethod]
        public void ParseSimpleVarNodeWithSemicolon()
        {
            Parser parser = new Parser("var one = 1;");

            var node = parser.ParseNode();

            Assert.IsNotNull(node);
            Assert.IsInstanceOfType(node, typeof(VarNode));

            var vnode = (VarNode)node;

            Assert.AreEqual("one", vnode.Name);
            Assert.IsNotNull(vnode.Expression);
            Assert.IsInstanceOfType(vnode.Expression, typeof(ConstantNode));

            var expr = (ConstantNode)vnode.Expression;

            Assert.AreEqual(1, expr.Value);

            Assert.IsNull(parser.ParseNode());
        }
    }
}
