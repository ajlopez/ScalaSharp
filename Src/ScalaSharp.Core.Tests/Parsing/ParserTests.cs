namespace ScalaSharp.Core.Tests.Parsing
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ScalaSharp.Core.Commands;
    using ScalaSharp.Core.Expressions;
    using ScalaSharp.Core.Language;
    using ScalaSharp.Core.Parsing;

    [TestClass]
    public class ParserTests
    {
        [TestMethod]
        public void ParseEmptyClassCommand()
        {
            Parser parser = new Parser("class Foo { }");

            var result = parser.ParseCommand();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ClassCommand));
            Assert.AreEqual("Foo", ((ClassCommand)result).Name);

            Assert.IsNull(parser.ParseCommand());
        }

        [TestMethod]
        public void ParseClassCommandWithCompositeBody()
        {
            Parser parser = new Parser("class Foo { \r\ndef one = 1\r\n def two = 2\r\n}");

            var result = parser.ParseCommand();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ClassCommand));

            var ccmd = (ClassCommand)result;

            Assert.AreEqual("Foo", ccmd.Name);
            Assert.IsNotNull(ccmd.Body);
            Assert.IsInstanceOfType(ccmd.Body, typeof(CompositeCommand));

            var composite = (CompositeCommand)ccmd.Body;

            Assert.AreEqual(2, composite.Commands.Count);
            Assert.IsInstanceOfType(composite.Commands[0], typeof(DefCommand));
            Assert.IsInstanceOfType(composite.Commands[1], typeof(DefCommand));

            Assert.IsNull(parser.ParseCommand());
        }

        [TestMethod]
        public void ParseClassCommandWithSimpleBody()
        {
            Parser parser = new Parser("class Foo { \r\ndef one = 1\r\n\r\n}");

            var result = parser.ParseCommand();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ClassCommand));

            var ccmd = (ClassCommand)result;

            Assert.AreEqual("Foo", ccmd.Name);
            Assert.IsNotNull(ccmd.Body);
            Assert.IsInstanceOfType(ccmd.Body, typeof(DefCommand));

            Assert.IsNull(parser.ParseCommand());
        }

        [TestMethod]
        public void RaiseIsNoNameInClassCommand()
        {
            Parser parser = new Parser("class { }");

            try
            {
                parser.ParseCommand();
                Assert.Fail();
            }
            catch (ParserException ex)
            {
                Assert.AreEqual("Expected a name", ex.Message);
            }
        }

        [TestMethod]
        public void ParseEmptyObjectCommand()
        {
            Parser parser = new Parser("object Foo { }");

            var result = parser.ParseCommand();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ObjectCommand));
            Assert.AreEqual("Foo", ((ObjectCommand)result).Name);

            Assert.IsNull(parser.ParseCommand());
        }

        [TestMethod]
        public void RaiseIsNoNameInObjectCommand()
        {
            Parser parser = new Parser("object { }");

            try
            {
                parser.ParseCommand();
                Assert.Fail();
            }
            catch (ParserException ex)
            {
                Assert.AreEqual("Expected a name", ex.Message);
            }
        }

        [TestMethod]
        public void ParseSimpleDefCommand()
        {
            Parser parser = new Parser("def foo: unit");

            var result = parser.ParseCommand();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(DefCommand));

            var dcommand = (DefCommand)result;

            Assert.AreEqual("foo", dcommand.Name);
            Assert.IsNotNull(dcommand.TypeInfo);
            Assert.AreEqual("unit", dcommand.TypeInfo.Name);
            Assert.IsNotNull(dcommand.Arguments);
            Assert.AreEqual(0, dcommand.Arguments.Count);
            Assert.IsNull(dcommand.Body);
        }

        [TestMethod]
        public void ParseDefCommandWithConstantExpression()
        {
            Parser parser = new Parser("def foo = 0");

            var result = parser.ParseCommand();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(DefCommand));

            var dcommand = (DefCommand)result;

            Assert.AreEqual("foo", dcommand.Name);
            Assert.IsNotNull(dcommand.Arguments);
            Assert.AreEqual(0, dcommand.Arguments.Count);
            Assert.IsNotNull(dcommand.Body);
            Assert.IsInstanceOfType(dcommand.Body, typeof(ExpressionCommand));

            var body = (ExpressionCommand)dcommand.Body;

            Assert.IsNotNull(body.Expression);
            Assert.IsInstanceOfType(body.Expression, typeof(ConstantExpression));
            Assert.AreEqual(0, ((ConstantExpression)body.Expression).Value);
        }

        [TestMethod]
        public void ParseSimpleDefCommandEmptyParenthesis()
        {
            Parser parser = new Parser("def foo(): unit");

            var result = parser.ParseCommand();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(DefCommand));

            var dcommand = (DefCommand)result;

            Assert.AreEqual("foo", dcommand.Name);
            Assert.IsNotNull(dcommand.TypeInfo);
            Assert.AreEqual("unit", dcommand.TypeInfo.Name);
            Assert.IsNotNull(dcommand.Arguments);
            Assert.AreEqual(0, dcommand.Arguments.Count);
        }

        [TestMethod]
        public void ParseSimpleDefCommandWithTwoIntegerArguments()
        {
            Parser parser = new Parser("def foo(x: Int, y: Int): Int");

            var result = parser.ParseCommand();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(DefCommand));

            var dcommand = (DefCommand)result;

            Assert.AreEqual("foo", dcommand.Name);
            Assert.IsNotNull(dcommand.TypeInfo);
            Assert.AreEqual("Int", dcommand.TypeInfo.Name);
            Assert.IsNotNull(dcommand.Arguments);
            Assert.AreEqual(2, dcommand.Arguments.Count);
            Assert.AreEqual("x", dcommand.Arguments[0].Name);
            Assert.AreEqual("Int", dcommand.Arguments[0].TypeInfo.Name);
            Assert.AreEqual("y", dcommand.Arguments[1].Name);
            Assert.AreEqual("Int", dcommand.Arguments[1].TypeInfo.Name);
        }

        [TestMethod]
        public void RaiseIsNoNameInDefCommand()
        {
            Parser parser = new Parser("def: unit");

            try
            {
                parser.ParseCommand();
                Assert.Fail();
            }
            catch (ParserException ex)
            {
                Assert.AreEqual("Expected a name", ex.Message);
            }
        }

        [TestMethod]
        public void RaiseIsMissingComma()
        {
            Parser parser = new Parser("def foo(x: Int y: Int): unit");

            try
            {
                parser.ParseCommand();
                Assert.Fail();
            }
            catch (ParserException ex)
            {
                Assert.AreEqual("Expected ','", ex.Message);
            }
        }

        [TestMethod]
        public void RaiseIsNoColonInDefCommand()
        {
            Parser parser = new Parser("def name unit");

            try
            {
                parser.ParseCommand();
                Assert.Fail();
            }
            catch (ParserException ex)
            {
                Assert.AreEqual("Expected ':' or '='", ex.Message);
            }
        }

        [TestMethod]
        public void RaiseEndOfInputInDefCommand()
        {
            Parser parser = new Parser("def name");

            try
            {
                parser.ParseCommand();
                Assert.Fail();
            }
            catch (ParserException ex)
            {
                Assert.AreEqual("Expected ':' or '='", ex.Message);
            }
        }

        [TestMethod]
        public void RaiseEndOfInputAfterDefCommand()
        {
            Parser parser = new Parser("def");

            try
            {
                parser.ParseCommand();
                Assert.Fail();
            }
            catch (ParserException ex)
            {
                Assert.AreEqual("Expected a name", ex.Message);
            }
        }

        [TestMethod]
        public void NullIfNoExpression()
        {
            Parser parser = new Parser("}");

            Assert.IsNull(parser.ParseExpression());
        }

        [TestMethod]
        public void NullIfNoCommand()
        {
            Parser parser = new Parser("}");

            Assert.IsNull(parser.ParseCommand());
        }

        [TestMethod]
        public void ParseSimpleValCommandWithIntegerExpression()
        {
            Parser parser = new Parser("val one = 1");

            var cmd = parser.ParseCommand();

            Assert.IsNotNull(cmd);
            Assert.IsInstanceOfType(cmd, typeof(ValCommand));

            var vcmd = (ValCommand)cmd;

            Assert.AreEqual("one", vcmd.Name);
            Assert.IsNotNull(vcmd.Expression);
            Assert.IsInstanceOfType(vcmd.Expression, typeof(ConstantExpression));

            var expr = (ConstantExpression)vcmd.Expression;

            Assert.AreEqual(1, expr.Value);
            Assert.AreEqual(1, expr.Evaluate(null));

            Assert.IsNull(parser.ParseCommand());
        }

        [TestMethod]
        public void ParseSimpleValCommandWithEndOfLine()
        {
            Parser parser = new Parser("val one = 1\n");

            var cmd = parser.ParseCommand();

            Assert.IsNotNull(cmd);
            Assert.IsInstanceOfType(cmd, typeof(ValCommand));

            var vcmd = (ValCommand)cmd;

            Assert.AreEqual("one", vcmd.Name);
            Assert.IsNotNull(vcmd.Expression);
            Assert.IsInstanceOfType(vcmd.Expression, typeof(ConstantExpression));

            var expr = (ConstantExpression)vcmd.Expression;

            Assert.AreEqual(1, expr.Value);
            Assert.AreEqual(1, expr.Evaluate(null));

            Assert.IsNull(parser.ParseCommand());
        }

        [TestMethod]
        public void ParseSimpleValCommandWithSemicolon()
        {
            Parser parser = new Parser("val one = 1;");

            var cmd = parser.ParseCommand();

            Assert.IsNotNull(cmd);
            Assert.IsInstanceOfType(cmd, typeof(ValCommand));

            var vcmd = (ValCommand)cmd;

            Assert.AreEqual("one", vcmd.Name);
            Assert.IsNotNull(vcmd.Expression);
            Assert.IsInstanceOfType(vcmd.Expression, typeof(ConstantExpression));

            var expr = (ConstantExpression)vcmd.Expression;

            Assert.AreEqual(1, expr.Value);
            Assert.AreEqual(1, expr.Evaluate(null));

            Assert.IsNull(parser.ParseCommand());
        }

        [TestMethod]
        public void RaiseIfNoEndOfCommandInSimpleVal()
        {
            Parser parser = new Parser("val one = 1 foo");

            try
            {
                parser.ParseCommand();
                Assert.Fail();
            }
            catch (ParserException ex)
            {
                Assert.AreEqual("Unexpected 'foo'", ex.Message);
            }
        }

        [TestMethod]
        public void ParseSimpleVarCommandWithIntegerExpression()
        {
            Parser parser = new Parser("var one = 1");

            var cmd = parser.ParseCommand();

            Assert.IsNotNull(cmd);
            Assert.IsInstanceOfType(cmd, typeof(VarCommand));

            var vcmd = (VarCommand)cmd;

            Assert.AreEqual("one", vcmd.Name);
            Assert.IsNotNull(vcmd.Expression);
            Assert.IsInstanceOfType(vcmd.Expression, typeof(ConstantExpression));
            Assert.AreSame(TypeInfo.Int, vcmd.TypeInfo);

            var expr = (ConstantExpression)vcmd.Expression;

            Assert.AreEqual(1, expr.Value);
            Assert.AreEqual(1, expr.Evaluate(null));

            Assert.IsNull(parser.ParseCommand());
        }

        [TestMethod]
        public void ParseSimpleVarCommandWithIntegerType()
        {
            Parser parser = new Parser("var one: Int");

            var cmd = parser.ParseCommand();

            Assert.IsNotNull(cmd);
            Assert.IsInstanceOfType(cmd, typeof(VarCommand));

            var vcmd = (VarCommand)cmd;

            Assert.AreEqual("one", vcmd.Name);
            Assert.IsNull(vcmd.Expression);
            Assert.AreSame(TypeInfo.Int, vcmd.TypeInfo);

            Assert.IsNull(parser.ParseCommand());
        }

        [TestMethod]
        public void ParseSimpleVarCommandWithIntegerTypeAndExpression()
        {
            Parser parser = new Parser("var one: Int = 1");

            var cmd = parser.ParseCommand();

            Assert.IsNotNull(cmd);
            Assert.IsInstanceOfType(cmd, typeof(VarCommand));

            var vcmd = (VarCommand)cmd;

            Assert.AreEqual("one", vcmd.Name);
            Assert.AreSame(TypeInfo.Int, vcmd.TypeInfo);
            Assert.IsNotNull(vcmd.Expression);
            Assert.IsInstanceOfType(vcmd.Expression, typeof(ConstantExpression));

            var expr = (ConstantExpression)vcmd.Expression;

            Assert.AreEqual(1, expr.Value);
            Assert.AreEqual(1, expr.Evaluate(null));
            Assert.AreSame(TypeInfo.Int, expr.TypeInfo);

            Assert.IsNull(parser.ParseCommand());
        }

        [TestMethod]
        public void RaiseIfNoTypeAndNotExpressionInVar()
        {
            Parser parser = new Parser("var one");

            try
            {
                parser.ParseCommand();
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Expected ':' or '='", ex.Message);
            }
        }

        [TestMethod]
        public void RaiseIfInvalidTypeInVarExpression()
        {
            Parser parser = new Parser("var one: Int = 1.0");

            try
            {
                parser.ParseCommand();
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.AreEqual("type mismatch", ex.Message);
            }
        }

        [TestMethod]
        public void ParseIntegerExpression()
        {
            Parser parser = new Parser("42");

            var expr = parser.ParseExpression();

            Assert.IsNotNull(expr);
            Assert.IsInstanceOfType(expr, typeof(ConstantExpression));

            var cexpr = (ConstantExpression)expr;

            Assert.AreEqual(42, cexpr.Value);
            Assert.AreSame(TypeInfo.Int, cexpr.TypeInfo);

            Assert.IsNull(parser.ParseExpression());
        }

        [TestMethod]
        public void ParseDoubleExpression()
        {
            Parser parser = new Parser("1.2");

            var expr = parser.ParseExpression();

            Assert.IsNotNull(expr);
            Assert.IsInstanceOfType(expr, typeof(ConstantExpression));

            var cexpr = (ConstantExpression)expr;

            Assert.AreEqual(1.2, cexpr.Value);
            Assert.AreSame(TypeInfo.Double, cexpr.TypeInfo);

            Assert.IsNull(parser.ParseExpression());
        }

        [TestMethod]
        public void ParseVariableExpression()
        {
            Parser parser = new Parser("foo");

            var expr = parser.ParseExpression();

            Assert.IsNotNull(expr);
            Assert.IsInstanceOfType(expr, typeof(VariableExpression));

            var vexpr = (VariableExpression)expr;

            Assert.AreEqual("foo", vexpr.Name);

            Assert.IsNull(parser.ParseExpression());
        }
    }
}
