namespace ScalaSharp.Core.Tests.Ast
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ScalaSharp.Core.Ast;
    using ScalaSharp.Core.Commands;
    using ScalaSharp.Core.Contexts;

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

        [TestMethod]
        public void RegisterInContext()
        {
            VarNode body = new VarNode("a", null, new ConstantNode(42));
            ClassNode node = new ClassNode("Foo", body);
            IContext context = new Context();

            node.RegisterInContext(context);

            Assert.AreSame(node, context.GetValue("Foo"));
        }

        [TestMethod]
        public void GetCommand()
        {
            VarNode body = new VarNode("a", null, new ConstantNode(42));
            ClassNode node = new ClassNode("Foo", body);

            var result = node.GetCommand();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ClassCommand));

            var ccmd = (ClassCommand)result;
            Assert.AreEqual("Foo", ccmd.Name);
            Assert.IsNotNull(ccmd.Body);
        }
    }
}
