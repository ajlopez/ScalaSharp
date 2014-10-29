namespace ScalaSharp.Core.Ast
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ScalaSharp.Core.Contexts;
    using ScalaSharp.Core.Expressions;
    using ScalaSharp.Core.Language;

    public class IfNode : IExpressionNode
    {
        private IExpressionNode condition;
        private IExpressionNode then;
        private IExpressionNode @else;

        public IfNode(IExpressionNode condition, IExpressionNode then, IExpressionNode @else)
        {
            this.condition = condition;
            this.then = then;
            this.@else = @else;
        }

        public IExpressionNode Condition { get { return this.condition; } }

        public IExpressionNode Then { get { return this.then; } }

        public IExpressionNode Else { get { return this.@else; } }

        public TypeInfo TypeInfo { get { return this.then.TypeInfo; } }

        public void CheckType(IContext context)
        {
            if (this.then != null)
                this.then.CheckType(context);

            if (this.@else != null)
                this.@else.CheckType(context);

            this.condition.CheckType(context);

            if (this.condition.TypeInfo != TypeInfo.Boolean)
                throw new InvalidOperationException("condition must be boolean");

            if (this.then.TypeInfo != this.@else.TypeInfo)
                throw new InvalidOperationException("type mismatch");
        }

        public void RegisterInContext(IContext context)
        {
            if (this.then != null)
                this.then.RegisterInContext(context);

            if (this.@else != null)
                this.@else.RegisterInContext(context);
        }

        public IExpression GetExpression()
        {
            return new IfExpression(this.condition.GetExpression(), this.then.GetExpression(), this.@else.GetExpression());
        }
    }
}
