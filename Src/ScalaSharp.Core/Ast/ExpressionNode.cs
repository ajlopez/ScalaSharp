namespace ScalaSharp.Core.Ast
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ScalaSharp.Core.Expressions;
    using ScalaSharp.Core.Language;

    public class ExpressionNode : IExpressionNode
    {
        private IExpression expression;

        public ExpressionNode(IExpression expression)
        {
            this.expression = expression;
        }

        public TypeInfo TypeInfo { get { return this.expression.TypeInfo; } }

        public IExpression GetExpression()
        {
            return this.expression;
        }

        public void CheckType(Contexts.IContext context)
        {
            throw new NotImplementedException();
        }

        public void RegisterInContext(Contexts.IContext context)
        {
            throw new NotImplementedException();
        }
    }
}
