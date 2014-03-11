namespace ScalaSharp.Core.Tests.Parsing
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ScalaSharp.Core.Ast;
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
        public void RaiseIfNotImplemented()
        {
            Parser parser = new Parser("[]");

            try
            {
                parser.ParseNode();
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(NotImplementedException));
            }
        }
    }
}
