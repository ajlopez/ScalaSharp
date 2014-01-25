﻿namespace ScalaSharp.Core.Tests.Parsing
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ScalaSharp.Core.Commands;
    using ScalaSharp.Core.Parsing;
    using ScalaSharp.Core.Expressions;

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
            Assert.AreEqual("unit", dcommand.Type);
            Assert.IsNotNull(dcommand.Arguments);
            Assert.AreEqual(0, dcommand.Arguments.Count);
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
            Assert.AreEqual("unit", dcommand.Type);
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
            Assert.AreEqual("Int", dcommand.Type);
            Assert.IsNotNull(dcommand.Arguments);
            Assert.AreEqual(2, dcommand.Arguments.Count);
            Assert.AreEqual("x", dcommand.Arguments[0].Name);
            Assert.AreEqual("Int", dcommand.Arguments[0].Type);
            Assert.AreEqual("y", dcommand.Arguments[1].Name);
            Assert.AreEqual("Int", dcommand.Arguments[1].Type);
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
                Assert.AreEqual("Expected ':'", ex.Message);
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
                Assert.AreEqual("Expected ':'", ex.Message);
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
        public void RaiseIfUnexpectedOpenSquareBracket()
        {
            Parser parser = new Parser("[");

            try
            {
                parser.ParseCommand();
                Assert.Fail();
            }
            catch (ParserException ex)
            {
                Assert.AreEqual("Unexpected '['", ex.Message);
            }
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
        public void ParseIntegerExpression()
        {
            Parser parser = new Parser("42");

            var expr = parser.ParseExpression();

            Assert.IsNotNull(expr);
            Assert.IsInstanceOfType(expr, typeof(ConstantExpression));

            var cexpr = (ConstantExpression)expr;

            Assert.AreEqual(42, cexpr.Value);

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
