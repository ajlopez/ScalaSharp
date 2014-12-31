namespace ScalaSharp.Core.Ast
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ScalaSharp.Core.Contexts;
    using ScalaSharp.Core.Expressions;
    using ScalaSharp.Core.Language;

    public class DotNameNode : IExpressionNode
    {
        private INode target;
        private string name;

        public DotNameNode(INode target, string name)
        {
            this.target = target;
            this.name = name;
        }

        public INode Target { get { return this.target; } }

        public string Name { get { return this.name; } }

        public TypeInfo TypeInfo
        {
            get { throw new NotImplementedException(); }
        }

        public void CheckType(IContext context)
        {
            throw new NotImplementedException();
        }

        public void RegisterInContext(IContext context)
        {
        }

        public IExpression GetExpression()
        {
            throw new NotImplementedException();
        }
    }
}
