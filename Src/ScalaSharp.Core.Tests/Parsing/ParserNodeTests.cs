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
    using ScalaSharp.Core.Contexts;
    using ScalaSharp.Core.Language;

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
        public void ParseSimpleDefNodeWithTypeAndValue()
        {
            Parser parser = new Parser("def foo: Int = 2");

            var result = parser.ParseNode();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(DefNode));

            var dnode = (DefNode)result;

            Assert.AreEqual("foo", dnode.Name);
            Assert.IsNotNull(dnode.TypeInfo);
            Assert.AreEqual(TypeInfo.Int, dnode.TypeInfo);
            Assert.IsNotNull(dnode.Arguments);
            Assert.AreEqual(0, dnode.Arguments.Count);
            Assert.IsNotNull(dnode.Expression);
            Assert.IsInstanceOfType(dnode.Expression, typeof(ConstantNode));

            var expr = (ConstantNode)dnode.Expression;

            Assert.AreEqual(2, expr.Value);

            dnode.CheckType(null);
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
        public void ParseSimpleVarNodeWithTypeAndIntegerExpression()
        {
            Parser parser = new Parser("var one: Int = 1");

            var node = parser.ParseNode();

            Assert.IsNotNull(node);
            Assert.IsInstanceOfType(node, typeof(VarNode));

            var vnode = (VarNode)node;

            Assert.AreEqual("one", vnode.Name);
            Assert.IsNotNull(vnode.Expression);
            Assert.IsInstanceOfType(vnode.Expression, typeof(ConstantNode));
            Assert.AreEqual(TypeInfo.Int, vnode.TypeInfo);

            var expr = (ConstantNode)vnode.Expression;

            Assert.AreEqual(1, expr.Value);

            Assert.IsNull(parser.ParseNode());
        }

        [TestMethod]
        public void ParseSimpleValNodeWithTypeAndIntegerExpression()
        {
            Parser parser = new Parser("val one: Int = 1");

            var node = parser.ParseNode();

            Assert.IsNotNull(node);
            Assert.IsInstanceOfType(node, typeof(ValNode));

            var vnode = (ValNode)node;

            Assert.AreEqual("one", vnode.Name);
            Assert.IsNotNull(vnode.Expression);
            Assert.IsInstanceOfType(vnode.Expression, typeof(ConstantNode));
            Assert.AreEqual(TypeInfo.Int, vnode.TypeInfo);

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

        [TestMethod]
        public void ParseSimpleInvokeNodeWithOneArgument()
        {
            Parser parser = new Parser("fibo(1)");

            var node = parser.ParseNode();
            Assert.IsNotNull(node);
            Assert.IsInstanceOfType(node, typeof(InvokeNode));

            var inode = (InvokeNode)node;

            Assert.AreEqual("fibo", inode.MethodName);
            Assert.IsNotNull(inode.Arguments);
            Assert.AreEqual(1, inode.Arguments.Count);
            Assert.IsInstanceOfType(inode.Arguments[0], typeof(ConstantNode));
            Assert.AreEqual(1, ((ConstantNode)inode.Arguments[0]).Value);
        }

        [TestMethod]
        public void ParseSimpleInvokeNodeWithNoArgument()
        {
            Parser parser = new Parser("foo()");

            var node = parser.ParseNode();
            Assert.IsNotNull(node);
            Assert.IsInstanceOfType(node, typeof(InvokeNode));

            var inode = (InvokeNode)node;

            Assert.AreEqual("foo", inode.MethodName);
            Assert.IsNotNull(inode.Arguments);
            Assert.AreEqual(0, inode.Arguments.Count);
        }

        [TestMethod]
        public void ParseSimpleInvokeMethodNodeWithNoArgument()
        {
            Parser parser = new Parser("foo.bar()");

            var node = parser.ParseNode();
            Assert.IsNotNull(node);
            Assert.IsInstanceOfType(node, typeof(InvokeMethodNode));

            var imnode = (InvokeMethodNode)node;

            Assert.IsNotNull(imnode.Target);
            Assert.IsInstanceOfType(imnode.Target, typeof(NameNode));
            Assert.AreEqual("foo", ((NameNode)imnode.Target).Name);
            Assert.AreEqual("bar", imnode.MethodName);
            Assert.IsNotNull(imnode.Arguments);
            Assert.AreEqual(0, imnode.Arguments.Count);
        }

        [TestMethod]
        public void ParseSimpleInvokeNodeWithTwoArguments()
        {
            Parser parser = new Parser("add(1, 2)");

            var node = parser.ParseNode();
            Assert.IsNotNull(node);
            Assert.IsInstanceOfType(node, typeof(InvokeNode));

            var inode = (InvokeNode)node;

            Assert.AreEqual("add", inode.MethodName);
            Assert.IsNotNull(inode.Arguments);
            Assert.AreEqual(2, inode.Arguments.Count);
            Assert.IsInstanceOfType(inode.Arguments[0], typeof(ConstantNode));
            Assert.AreEqual(1, ((ConstantNode)inode.Arguments[0]).Value);
            Assert.IsInstanceOfType(inode.Arguments[1], typeof(ConstantNode));
            Assert.AreEqual(2, ((ConstantNode)inode.Arguments[1]).Value);
        }

        [TestMethod]
        public void ParseNodes()
        {
            Parser parser = new Parser("class Foo { }\nclass Bar { }");

            var result = parser.ParseNodes();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CompositeNode));
            Assert.AreEqual(2, ((CompositeNode)result).Nodes.Count);

            Assert.IsNull(parser.ParseNode());
        }

        [TestMethod]
        public void ParseNodesAndRegisterInContext()
        {
            Parser parser = new Parser("class Foo { }\nclass Bar { }");

            var result = parser.ParseNodes();
            var context = new Context();
            result.RegisterInContext(context);

            var cfoo = context.GetValue("Foo");

            Assert.IsNotNull(cfoo);
            Assert.IsInstanceOfType(cfoo, typeof(ClassNode));

            var cbar = context.GetValue("Bar");

            Assert.IsNotNull(cbar);
            Assert.IsInstanceOfType(cbar, typeof(ClassNode));

            Assert.IsNull(parser.ParseNode());
        }

        [TestMethod]
        public void ParseNodesRegisterInContextCheckTypes()
        {
            Parser parser = new Parser("var a = 1\nvar b = a");

            var result = parser.ParseNodes();
            var context = new Context();
            result.RegisterInContext(context);
            result.CheckType(context);

            var vara = context.GetValue("a");

            Assert.IsNotNull(vara);
            Assert.IsInstanceOfType(vara, typeof(VarNode));
            Assert.AreEqual(TypeInfo.Int, ((VarNode)vara).TypeInfo);

            var varb = context.GetValue("b");

            Assert.IsNotNull(varb);
            Assert.IsInstanceOfType(varb, typeof(VarNode));
            Assert.AreEqual(TypeInfo.Int, ((VarNode)varb).TypeInfo);

            Assert.IsNull(parser.ParseNode());
        }

        [TestMethod]
        public void ParseNodesRegisterInContextCheckTypesUsingDef()
        {
            Parser parser = new Parser("var a = 1\ndef b = a");

            var result = parser.ParseNodes();
            var context = new Context();
            result.RegisterInContext(context);
            result.CheckType(context);

            var vara = context.GetValue("a");

            Assert.IsNotNull(vara);
            Assert.IsInstanceOfType(vara, typeof(VarNode));
            Assert.AreEqual(TypeInfo.Int, ((VarNode)vara).TypeInfo);

            var defb = context.GetValue("b");

            Assert.IsNotNull(defb);
            Assert.IsInstanceOfType(defb, typeof(DefNode));
            Assert.AreEqual(TypeInfo.Int, ((DefNode)defb).TypeInfo);

            Assert.IsNull(parser.ParseNode());
        }
    }
}
