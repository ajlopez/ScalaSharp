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
    public class ConstantExpressionTests
    {
        [TestMethod]
        public void CreateAndEvaluateConstantExpression()
        {
            ConstantExpression expr = new ConstantExpression(1);

            Assert.AreEqual(1, expr.Value);
            Assert.AreEqual(1, expr.Evaluate(null));
            Assert.AreSame(TypeInfo.Int, expr.TypeInfo);
        }
    }
}
