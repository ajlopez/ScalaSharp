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
        public void CreateObjectNode()
        {
            VarNode varnode = new VarNode("a", null, new ConstantNode(42));
            ObjectNode node = new ObjectNode("Foo", varnode);

            Assert.AreEqual("Foo", node.Name);
            Assert.IsNotNull(node.Body);
            Assert.AreSame(varnode, node.Body);
            Assert.AreEqual("Foo", node.TypeInfo.Name);
            node.CheckType();
            Assert.AreEqual("Foo", node.TypeInfo.Name);
        }
    }
}
