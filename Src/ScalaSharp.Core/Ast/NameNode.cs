﻿namespace ScalaSharp.Core.Ast
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ScalaSharp.Core.Contexts;
    using ScalaSharp.Core.Language;

    public class NameNode : IExpressionNode, IUntypedNode
    {
        private string name;
        private TypeInfo typeinfo;

        public NameNode(string name)
        {
            this.name = name;
        }

        public string Name { get { return this.name; } }

        public TypeInfo TypeInfo { get { return this.typeinfo; } }

        public void CheckType(IContext context)
        {
            var node = (INode)context.GetValue(this.name);

            if (node == null)
                return;

            if (this.TypeInfo == null)
                this.SetTypeInfo(node.TypeInfo);
            else if (this.TypeInfo != node.TypeInfo)
                throw new InvalidOperationException("type mismatch");
        }

        public void SetTypeInfo(TypeInfo typeinfo)
        {
            this.typeinfo = typeinfo;
        }

        public void RegisterInContext(IContext context)
        {
        }

        public Expressions.IExpression GetExpression()
        {
            throw new NotImplementedException();
        }
    }
}
