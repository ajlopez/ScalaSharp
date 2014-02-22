namespace ScalaSharp.Core.Tests.Ast
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ScalaSharp.Core.Ast;
    using ScalaSharp.Core.Language;

    [TestClass]
    public class ConstantNodeTests
    {
        [TestMethod]
        public void CreateNodeWithIntegerValue()
        {
            ConstantNode node = new ConstantNode(42);

            Assert.IsNotNull(node.Value);
            Assert.AreEqual(42, node.Value);
            Assert.AreSame(TypeInfo.Int, node.TypeInfo);

            node.CheckType();
            Assert.AreSame(TypeInfo.Int, node.TypeInfo);
        }
    }
}
