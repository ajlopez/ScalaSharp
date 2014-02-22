namespace ScalaSharp.Core.Tests.Ast
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ScalaSharp.Core.Ast;

    [TestClass]
    public class ClassNodeTests
    {
        [TestMethod]
        public void CreateClassNodeWithNameAndNullBody()
        {
            VarNode body = new VarNode("a", null, new ConstantNode(42));
            ClassNode node = new ClassNode("Foo", body);

            Assert.AreEqual("Foo", node.Name);
            Assert.IsNotNull(node.Body);
            Assert.AreEqual("Foo", node.TypeInfo.Name);

            node.CheckType();
            Assert.AreEqual("Foo", node.TypeInfo.Name);
        }
    }
}
