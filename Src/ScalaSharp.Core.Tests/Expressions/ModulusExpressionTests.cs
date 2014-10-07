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
    public class ModulusExpressionTests
    {
        [TestMethod]
        public void ModulusTwoIntegers()
        {
            ModulusExpression expr = new ModulusExpression(new ConstantExpression(6), new ConstantExpression(2));

            Assert.AreEqual(0, expr.Evaluate(null));
            Assert.AreSame(TypeInfo.Int, expr.TypeInfo);
        }

        [TestMethod]
        public void ModulusIntegerByDouble()
        {
            ModulusExpression expr = new ModulusExpression(new ConstantExpression(2), new ConstantExpression(2.5));

            try
            {
                expr.Evaluate(null);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(InvalidOperationException));
            }
        }

        [TestMethod]
        public void ModulusDoubleByInteger()
        {
            ModulusExpression expr = new ModulusExpression(new ConstantExpression(2.5), new ConstantExpression(3));

            try
            {
                expr.Evaluate(null);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(InvalidOperationException));
            }
        }

        [TestMethod]
        public void ModulusTwoDoubles()
        {
            ModulusExpression expr = new ModulusExpression(new ConstantExpression(2.5), new ConstantExpression(3.7));

            try
            {
                expr.Evaluate(null);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(InvalidOperationException));
            }
        }
    }
}
