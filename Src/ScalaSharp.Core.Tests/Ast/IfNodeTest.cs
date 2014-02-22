﻿namespace ScalaSharp.Core.Tests.Ast
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ScalaSharp.Core.Ast;
    using ScalaSharp.Core.Language;

    [TestClass]
    public class IfNodeTest
    {
        [TestMethod]
        public void CreateIfNodeWithThen()
        {
            var condition = new ConstantNode(true);
            var then = new ConstantNode(42);

            var node = new IfNode(condition, then, null);

            Assert.AreSame(condition, node.Condition);
            Assert.AreSame(then, node.Then);
            Assert.IsNull(node.Else);
            Assert.AreSame(TypeInfo.Int, node.TypeInfo);
        }

        [TestMethod]
        public void CreateIfNodeWithThenAndElse()
        {
            var condition = new ConstantNode(true);
            var then = new ConstantNode(42);
            var @else = new ConstantNode(0);

            var node = new IfNode(condition, then, @else);

            Assert.AreSame(condition, node.Condition);
            Assert.AreSame(then, node.Then);
            Assert.AreSame(@else, node.Else);
            Assert.AreSame(TypeInfo.Int, node.TypeInfo);
            node.CheckType();
            Assert.AreSame(TypeInfo.Int, node.TypeInfo);
        }

        [TestMethod]
        public void RaiseIfTypeMismatch()
        {
            var condition = new ConstantNode(true);
            var then = new ConstantNode(42);
            var @else = new ConstantNode(42.0);

            var node = new IfNode(condition, then, @else);

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

        [TestMethod]
        public void RaiseIfInvalidCondition()
        {
            var condition = new ConstantNode(1);
            var then = new ConstantNode(42);

            try
            {
                var node = new IfNode(condition, then, null);
                node.CheckType();
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.AreEqual("condition must be boolean", ex.Message);
            }
        }
    }
}
