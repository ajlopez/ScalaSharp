namespace ScalaSharp.Core.Tests.Ast
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ScalaSharp.Core.Ast;

    [TestClass]
    public class ObjectNodeTests
    {
        [TestMethod]
        public void CreateObjectNodeWithNameAndNullBody()
        {
            ObjectNode node = new ObjectNode("Foo", null);

            Assert.AreEqual("Foo", node.Name);
            Assert.IsNull(node.Body);
            Assert.AreEqual("Foo", node.TypeInfo.Name);
        }
    }
}
