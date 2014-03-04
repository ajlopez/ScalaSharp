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
        public void RaiseIfNotImplemented()
        {
            Parser parser = new Parser("123");

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
