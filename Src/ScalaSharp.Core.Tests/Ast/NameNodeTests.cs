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
            IContext context = new Context();
            NameNode node = new NameNode("foo");
            node.RegisterInContext(context);

            Assert.IsNotNull(node.Name);
            Assert.AreEqual("foo", node.Name);
            Assert.IsNull(node.TypeInfo);
            node.CheckType(context);
        }
    }
}
