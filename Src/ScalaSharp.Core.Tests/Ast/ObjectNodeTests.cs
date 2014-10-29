namespace ScalaSharp.Core.Tests.Ast
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ScalaSharp.Core.Ast;
    using ScalaSharp.Core.Contexts;

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
            node.CheckType(null);
            Assert.AreEqual("Foo", node.TypeInfo.Name);
        }

        [TestMethod]
        public void RegisterInContext()
        {
            VarNode varnode = new VarNode("a", null, new ConstantNode(42));
            ObjectNode node = new ObjectNode("Foo", varnode);

            Context context = new Context();

            node.RegisterInContext(context);

            Assert.IsNotNull(context.GetValue("Foo"));
            Assert.IsNull(context.GetValue("a"));
            Assert.AreSame(node, context.GetValue("Foo"));
        }
    }
}
