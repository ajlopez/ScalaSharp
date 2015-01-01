namespace ScalaSharp.Core.Ast
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ScalaSharp.Core.Contexts;
    using ScalaSharp.Core.Expressions;
    using ScalaSharp.Core.Language;

    public class InvokeNode : IExpressionNode
    {
        private INode target;
        private IList<INode> arguments;
        private TypeInfo typeinfo;

        public InvokeNode(INode target, IList<INode> arguments)
        {
            this.target = target;
            this.arguments = arguments;
        }

        public INode Target { get { return this.target; } }

        public IList<INode> Arguments { get { return this.arguments; } }

        public TypeInfo TypeInfo
        {
            get { return this.typeinfo; }
        }

        public void CheckType(IContext context)
        {
            this.target.CheckType(context);
            this.typeinfo = this.target.TypeInfo;
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
