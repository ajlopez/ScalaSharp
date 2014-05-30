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
        private string methodname;
        private IList<INode> arguments;

        public InvokeNode(string methodname, IList<INode> arguments)
        {
            this.methodname = methodname;
            this.arguments = arguments;
        }

        public string MethodName { get { return this.methodname; } }

        public IList<INode> Arguments { get { return this.arguments; } }

        public TypeInfo TypeInfo
        {
            get { throw new NotImplementedException(); }
        }

        public void CheckType()
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
