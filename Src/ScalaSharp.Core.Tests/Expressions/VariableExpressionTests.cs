namespace ScalaSharp.Core.Tests.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ScalaSharp.Core.Contexts;
    using ScalaSharp.Core.Expressions;
    using ScalaSharp.Core.Language;

    [TestClass]
    public class VariableExpressionTests
    {
        [TestMethod]
        public void CreateAndEvaluateVariableExpression()
        {
            Context context = new Context();
            context.SetValue("one", 1);

            VariableExpression expr = new VariableExpression("one");

            Assert.AreEqual("one", expr.Name);
            Assert.AreEqual(1, expr.Evaluate(context));
            Assert.AreSame(TypeInfo.Any, expr.TypeInfo);
        }
    }
}
