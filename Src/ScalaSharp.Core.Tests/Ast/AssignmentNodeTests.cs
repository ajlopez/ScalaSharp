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
    public class AssignmentNodeTests
    {
        [TestMethod]
        public void CreateAssignmentNode()
        {
            NameNode target = new NameNode("foo");
            ConstantNode expr = new ConstantNode(42);

            AssignmentNode node = new AssignmentNode(target, expr);

            Assert.AreSame(target, node.Target);
            Assert.AreSame(expr, node.Expression);
            Assert.AreSame(TypeInfo.Int, node.TypeInfo);
        }

        [TestMethod]
        public void CheckType()
        {
            NameNode target = new NameNode("foo");
            ConstantNode expr = new ConstantNode(42);

            AssignmentNode node = new AssignmentNode(target, expr);
            node.CheckType();

            Assert.AreSame(TypeInfo.Int, target.TypeInfo);
        }

        [TestMethod]
        public void RaiseIfTypeMismatch()
        {
            NameNode target = new NameNode("foo");
            target.SetTypeInfo(TypeInfo.Double);
            ConstantNode expr = new ConstantNode(42);

            AssignmentNode node = new AssignmentNode(target, expr);

            try
            {
                node.CheckType();
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.AreEqual("type mismatch", ex.Message);
            }
        }
    }
}

