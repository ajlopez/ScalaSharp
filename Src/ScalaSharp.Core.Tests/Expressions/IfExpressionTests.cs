namespace ScalaSharp.Core.Tests.Expressions
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ScalaSharp.Core.Expressions;
    using ScalaSharp.Core.Language;

    [TestClass]
    public class IfExpressionTests
    {
        [TestMethod]
        public void EvaluateThenExpression()
        {
            IfExpression ifexpr = new IfExpression(new ConstantExpression(true), new ConstantExpression(1), new ConstantExpression(2));

            Assert.AreSame(TypeInfo.Int, ifexpr.TypeInfo);
            Assert.AreEqual(1, ifexpr.Evaluate(null));
        }

        [TestMethod]
        public void EvaluateElseExpression()
        {
            IfExpression ifexpr = new IfExpression(new ConstantExpression(false), new ConstantExpression(1), new ConstantExpression(2));

            Assert.AreSame(TypeInfo.Int, ifexpr.TypeInfo);
            Assert.AreEqual(2, ifexpr.Evaluate(null));
        }
    }
}
