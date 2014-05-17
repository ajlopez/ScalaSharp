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
    public class NameNodeTests
    {
        [TestMethod]
        public void CreateNodeWithName()
        {
            NameNode node = new NameNode("foo");

            Assert.IsNotNull(node.Name);
            Assert.AreEqual("foo", node.Name);
            Assert.IsNull(node.TypeInfo);
            node.CheckType();
        }

        [TestMethod]
        public void RegisterInContext()
        {
            NameNode node = new NameNode("foo");
            Context context = new Context();

            node.RegisterInContext(context);

            Assert.IsNotNull(context.GetValue("foo"));
            Assert.AreSame(node, context.GetValue("foo"));
        }
    }
}
