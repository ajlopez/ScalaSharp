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
    }
}
