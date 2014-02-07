namespace ScalaSharp.Core.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ScalaSharp.Core.Parsing;

    [TestClass]
    public class EvaluateTests
    {
        [TestMethod]
        public void EvaluateSimpleAdd()
        {
            Assert.AreEqual(2, this.Evaluate("1+1"));
        }

        [TestMethod]
        public void EvaluateSimpleSubtract()
        {
            Assert.AreEqual(2, this.Evaluate("3-1"));
        }

        [TestMethod]
        public void EvaluateSimpleMultiply()
        {
            Assert.AreEqual(6, this.Evaluate("3*2"));
        }

        [TestMethod]
        public void EvaluateSimpleDivide()
        {
            Assert.AreEqual(3, this.Evaluate("6/2"));
        }

        [TestMethod]
        public void EvaluateSimpleAddMultiply()
        {
            Assert.AreEqual(10, this.Evaluate("4+3*2"));
        }

        private object Evaluate(string text)
        {
            Parser parser = new Parser(text);
            var expr = parser.ParseExpression();

            return expr.Evaluate(null);
        }
    }
}
