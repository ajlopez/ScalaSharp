namespace ScalaSharp.Core.Tests.Contexts
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ScalaSharp.Core.Contexts;

    [TestClass]
    public class ContextTests
    {
        [TestMethod]
        public void GetUnknownValueAsNull()
        {
            Context context = new Context();

            Assert.IsNull(context.GetValue("unknown"));
        }

        [TestMethod]
        public void SetAndGetValue()
        {
            Context context = new Context();

            context.SetValue("one", 1);

            Assert.AreEqual(1, context.GetValue("one"));
        }
    }
}
