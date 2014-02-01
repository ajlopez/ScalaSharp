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
    public class MultiplyExpressionTests
    {
        [TestMethod]
        public void MultiplyTwoIntegers()
        {
            MultiplyExpression expr = new MultiplyExpression(new ConstantExpression(3), new ConstantExpression(2));

            Assert.AreEqual(6, expr.Evaluate(null));
            Assert.AreSame(TypeInfo.Int, expr.TypeInfo);
        }

        [TestMethod]
        public void MultiplyIntegerByDouble()
        {
            MultiplyExpression expr = new MultiplyExpression(new ConstantExpression(2), new ConstantExpression(2.5));

            Assert.AreEqual(2 * 2.5, expr.Evaluate(null));
            Assert.AreSame(TypeInfo.Double, expr.TypeInfo);
        }

        [TestMethod]
        public void MultiplyDoubleByInteger()
        {
            MultiplyExpression expr = new MultiplyExpression(new ConstantExpression(2.5), new ConstantExpression(3));

            Assert.AreEqual(2.5 * 3, expr.Evaluate(null));
            Assert.AreSame(TypeInfo.Double, expr.TypeInfo);
        }

        [TestMethod]
        public void MultiplyTwoDoubles()
        {
            MultiplyExpression expr = new MultiplyExpression(new ConstantExpression(2.5), new ConstantExpression(3.7));

            Assert.AreEqual(2.5 * 3.7, expr.Evaluate(null));
            Assert.AreSame(TypeInfo.Double, expr.TypeInfo);
        }
    }
}
