namespace ScalaSharp.Core.Tests.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ScalaSharp.Core.Language;

    [TestClass]
    public class DynamicObjectTests
    {
        [TestMethod]
        public void SetAndGetValue()
        {
            DynamicObject obj = new DynamicObject();

            obj.SetValue("foo", "bar");
            Assert.AreEqual("bar", obj.GetValue("foo"));
        }
    }
}
