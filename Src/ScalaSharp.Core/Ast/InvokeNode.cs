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
        private TypeInfo typeinfo;

        public InvokeNode(string methodname, IList<INode> arguments)
        {
            this.methodname = methodname;
            this.arguments = arguments;
        }

        public string MethodName { get { return this.methodname; } }

        public IList<INode> Arguments { get { return this.arguments; } }

        public TypeInfo TypeInfo
        {
            get { return this.typeinfo; }
        }

        public void CheckType(IContext context)
        {
            this.typeinfo = ((INode)context.GetValue(this.methodname)).TypeInfo;
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
