namespace ScalaSharp.Core.Tests.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ScalaSharp.Core.Expressions;
    using ScalaSharp.Core.Language;

    [TestClass]
    public class EqualExpressionTests
    {
        [TestMethod]
        public void EqualIntegers()
        {
            EqualExpression expr = new EqualExpression(new ConstantExpression(1), new ConstantExpression(1));
            Assert.AreEqual(true, expr.Evaluate(null));
            Assert.AreSame(TypeInfo.Boolean, expr.TypeInfo);
        }

        [TestMethod]
        public void NotEqualIntegers()
        {
            EqualExpression expr = new EqualExpression(new ConstantExpression(1), new ConstantExpression(2));
            Assert.AreEqual(false, expr.Evaluate(null));
        }

        [TestMethod]
        public void EqualReals()
        {
            EqualExpression expr = new EqualExpression(new ConstantExpression(1.2), new ConstantExpression(1.2));
            Assert.AreEqual(true, expr.Evaluate(null));
            Assert.AreSame(TypeInfo.Boolean, expr.TypeInfo);
        }

        [TestMethod]
        public void NotEqualReals()
        {
            EqualExpression expr = new EqualExpression(new ConstantExpression(1.3), new ConstantExpression(2.4));
            Assert.AreEqual(false, expr.Evaluate(null));
        }

        [TestMethod]
        public void EqualRealInteger()
        {
            EqualExpression expr = new EqualExpression(new ConstantExpression(1.0), new ConstantExpression(1));
            Assert.AreEqual(true, expr.Evaluate(null));
            Assert.AreSame(TypeInfo.Boolean, expr.TypeInfo);
        }

        [TestMethod]
        public void EqualIntegerReal()
        {
            EqualExpression expr = new EqualExpression(new ConstantExpression(1), new ConstantExpression(1.0));
            Assert.AreEqual(true, expr.Evaluate(null));
            Assert.AreSame(TypeInfo.Boolean, expr.TypeInfo);
        }

        [TestMethod]
        public void EqualStrings()
        {
            EqualExpression expr = new EqualExpression(new ConstantExpression("foo"), new ConstantExpression("foo"));
            Assert.AreEqual(true, expr.Evaluate(null));
        }

        [TestMethod]
        public void NotEqualStrings()
        {
            EqualExpression expr = new EqualExpression(new ConstantExpression("foo"), new ConstantExpression("bar"));
            Assert.AreEqual(false, expr.Evaluate(null));
        }
    }
}
