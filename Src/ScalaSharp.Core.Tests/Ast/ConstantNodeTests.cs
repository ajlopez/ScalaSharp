namespace ScalaSharp.Core.Tests.Ast
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ScalaSharp.Core.Ast;

    [TestClass]
    public class ConstantNodeTests
    {
        [TestMethod]
        public void CreateNodeWithIntegerValue()
        {
            ConstantNode node = new ConstantNode(42);

            Assert.IsNotNull(node.Value);
            Assert.AreEqual(42, node.Value);
        }
    }
}
