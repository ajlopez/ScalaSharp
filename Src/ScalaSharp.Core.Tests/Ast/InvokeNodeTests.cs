namespace ScalaSharp.Core.Tests.Ast
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ScalaSharp.Core.Ast;
    using ScalaSharp.Core.Contexts;
    using ScalaSharp.Core.Language;

    [TestClass]
    public class InvokeNodeTests
    {
        [TestMethod]
        public void CreateInvokeNode()
        {
            IList<INode> arguments = new List<INode>() { new ConstantNode(42) };
            InvokeNode node = new InvokeNode(new NameNode("append"), arguments);

            node.RegisterInContext(null);
            Assert.AreSame(arguments, node.Arguments);
            Assert.IsNotNull(node.Target);
            Assert.IsInstanceOfType(node.Target, typeof(NameNode));
            Assert.AreEqual("append", ((NameNode)node.Target).Name);
        }

        [TestMethod]
        public void CheckType()
        {
            Context context = new Context();
            DefNode defnode = new DefNode("append", null, TypeInfo.Int, null);
            defnode.RegisterInContext(context);
            IList<INode> arguments = new List<INode>() { new ConstantNode(42) };
            InvokeNode node = new InvokeNode(new NameNode("append"), arguments);

            Assert.IsNull(node.TypeInfo);
            node.CheckType(context);
            Assert.AreEqual(TypeInfo.Int, node.TypeInfo);
        }
    }
}
