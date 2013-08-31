namespace ScalaSharp.Core.Tests.Parsing
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ScalaSharp.Core.Parsing;

    [TestClass]
    public class LexerTests
    {
        [TestMethod]
        public void GetName()
        {
            Lexer lexer = new Lexer("name");

            var result = lexer.NextToken();

            Assert.IsNotNull(result);
            Assert.AreEqual("name", result.Value);
            Assert.AreEqual(TokenType.Name, result.Type);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetNameWithWhitespaces()
        {
            Lexer lexer = new Lexer("  name   ");

            var result = lexer.NextToken();

            Assert.IsNotNull(result);
            Assert.AreEqual("name", result.Value);
            Assert.AreEqual(TokenType.Name, result.Type);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetInteger()
        {
            Lexer lexer = new Lexer("123");

            var result = lexer.NextToken();

            Assert.IsNotNull(result);
            Assert.AreEqual("123", result.Value);
            Assert.AreEqual(TokenType.Integer, result.Type);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetIntegerWithWhitespaces()
        {
            Lexer lexer = new Lexer("  123   ");

            var result = lexer.NextToken();

            Assert.IsNotNull(result);
            Assert.AreEqual("123", result.Value);
            Assert.AreEqual(TokenType.Integer, result.Type);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetTwoNames()
        {
            Lexer lexer = new Lexer("foo bar");

            var result = lexer.NextToken();

            Assert.IsNotNull(result);
            Assert.AreEqual("foo", result.Value);
            Assert.AreEqual(TokenType.Name, result.Type);

            result = lexer.NextToken();

            Assert.IsNotNull(result);
            Assert.AreEqual("bar", result.Value);
            Assert.AreEqual(TokenType.Name, result.Type);

            Assert.IsNull(lexer.NextToken());
        }
    }
}
