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
    public class AddExpressionTests
    {
        [TestMethod]
        public void AddTwoIntegers()
        {
            AddExpression expr = new AddExpression(new ConstantExpression(1), new ConstantExpression(2));

            Assert.AreSame(TypeInfo.Int, expr.TypeInfo);
            Assert.AreEqual(3, expr.Evaluate(null));
        }

        [TestMethod]
        public void AddIntegerToDouble()
        {
            AddExpression expr = new AddExpression(new ConstantExpression(1), new ConstantExpression(2.5));

            Assert.AreSame(TypeInfo.Double, expr.TypeInfo);
            Assert.AreEqual(1 + 2.5, expr.Evaluate(null));
        }

        [TestMethod]
        public void AddDoubleToInteger()
        {
            AddExpression expr = new AddExpression(new ConstantExpression(2.5), new ConstantExpression(1));

            Assert.AreSame(TypeInfo.Double, expr.TypeInfo);
            Assert.AreEqual(2.5 + 1, expr.Evaluate(null));
        }

        [TestMethod]
        public void AddTwoDoubles()
        {
            AddExpression expr = new AddExpression(new ConstantExpression(2.5), new ConstantExpression(3.7));

            Assert.AreSame(TypeInfo.Double, expr.TypeInfo);
            Assert.AreEqual(2.5 + 3.7, expr.Evaluate(null));
        }

        [TestMethod]
        public void AddStringToInteger()
        {
            AddExpression expr = new AddExpression(new ConstantExpression("42"), new ConstantExpression(1));

            Assert.AreSame(TypeInfo.String, expr.TypeInfo);
            Assert.AreEqual("421", expr.Evaluate(null));
        }

        [TestMethod]
        public void AddIntegerToString()
        {
            AddExpression expr = new AddExpression(new ConstantExpression(42), new ConstantExpression("1"));

            Assert.AreSame(TypeInfo.String, expr.TypeInfo);
            Assert.AreEqual("421", expr.Evaluate(null));
        }
    }
}
