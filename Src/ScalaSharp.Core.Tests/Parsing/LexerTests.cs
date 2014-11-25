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
        public void SkipLineComment()
        {
            Lexer lexer = new Lexer("// a line comment\n  name   ");

            var result = lexer.NextToken();

            Assert.IsNotNull(result);
            Assert.AreEqual("\n", result.Value);
            Assert.AreEqual(TokenType.NewLine, result.Type);

            result = lexer.NextToken();

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

        [TestMethod]
        public void GetTwoIntegers()
        {
            Lexer lexer = new Lexer("123 456");

            var result = lexer.NextToken();

            Assert.IsNotNull(result);
            Assert.AreEqual("123", result.Value);
            Assert.AreEqual(TokenType.Integer, result.Type);

            result = lexer.NextToken();

            Assert.IsNotNull(result);
            Assert.AreEqual("456", result.Value);
            Assert.AreEqual(TokenType.Integer, result.Type);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetInvalidInteger()
        {
            Lexer lexer = new Lexer("123m");

            try
            {
                lexer.NextToken();
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(LexerException));
                Assert.AreEqual("Unexpected 'm'", ex.Message);
            }
        }

        [TestMethod]
        public void GetReal()
        {
            Lexer lexer = new Lexer("123.456");

            var result = lexer.NextToken();

            Assert.IsNotNull(result);
            Assert.AreEqual("123.456", result.Value);
            Assert.AreEqual(TokenType.Real, result.Type);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetInvalidReal()
        {
            Lexer lexer = new Lexer("123.456m");

            try
            {
                lexer.NextToken();
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(LexerException));
                Assert.AreEqual("Unexpected 'm'", ex.Message);
            }
        }

        [TestMethod]
        public void GetCarriageReturnNewLine()
        {
            Lexer lexer = new Lexer("\r\n");

            var result = lexer.NextToken();

            Assert.IsNotNull(result);
            Assert.AreEqual("\r\n", result.Value);
            Assert.AreEqual(TokenType.NewLine, result.Type);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetNewLine()
        {
            Lexer lexer = new Lexer("\n");

            var result = lexer.NextToken();

            Assert.IsNotNull(result);
            Assert.AreEqual("\n", result.Value);
            Assert.AreEqual(TokenType.NewLine, result.Type);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetCarriageReturn()
        {
            Lexer lexer = new Lexer("\r");

            var result = lexer.NextToken();

            Assert.IsNotNull(result);
            Assert.AreEqual("\r", result.Value);
            Assert.AreEqual(TokenType.NewLine, result.Type);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetString()
        {
            Lexer lexer = new Lexer("\"foo\"");

            var result = lexer.NextToken();

            Assert.IsNotNull(result);
            Assert.AreEqual("foo", result.Value);
            Assert.AreEqual(TokenType.String, result.Type);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetUnclosedString()
        {
            Lexer lexer = new Lexer("\"foo");

            try
            {
                lexer.NextToken();
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(LexerException));
                Assert.AreEqual("Unclosed string", ex.Message);
            }
        }

        [TestMethod]
        public void GetDelimiters()
        {
            string punctuations = ",;:{}()[]";

            Lexer lexer = new Lexer(punctuations);

            for (int k = 0; k < punctuations.Length; k++)
            {
                var result = lexer.NextToken();

                Assert.IsNotNull(result);
                Assert.AreEqual(TokenType.Delimiter, result.Type);
                Assert.AreEqual(1, result.Value.Length);
                Assert.AreEqual(punctuations[k], result.Value[0]);
            }

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetNameAndDelimiter()
        {
            Lexer lexer = new Lexer("foo:");

            var token = lexer.NextToken();
            Assert.IsNotNull(token);
            Assert.AreEqual("foo", token.Value);
            Assert.AreEqual(TokenType.Name, token.Type);

            token = lexer.NextToken();
            Assert.IsNotNull(token);
            Assert.AreEqual(":", token.Value);
            Assert.AreEqual(TokenType.Delimiter, token.Type);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetOperators()
        {
            string[] operators = new string[] { "=", "==", "<=", ">=", "<", ">", "<-", "+", "-", "*", "/", "%" };

            Lexer lexer = new Lexer("= == <= >= < > <- + - * / %");

            for (int k = 0; k < operators.Length; k++)
            {
                var result = lexer.NextToken();

                Assert.IsNotNull(result);
                Assert.AreEqual(TokenType.Operator, result.Type);
                Assert.AreEqual(operators[k], result.Value);
            }

            Assert.IsNull(lexer.NextToken());
        }
    }
}
