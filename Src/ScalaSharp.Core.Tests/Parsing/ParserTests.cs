namespace ScalaSharp.Core.Tests.Parsing
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ScalaSharp.Core.Commands;
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
        }

        [TestMethod]
        public void RaiseIsNoNameInDefCommand()
        {
            Parser parser = new Parser("def { }");

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
    }
}
