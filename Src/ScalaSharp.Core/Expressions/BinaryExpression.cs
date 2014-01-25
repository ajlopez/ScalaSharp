namespace ScalaSharp.Core.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ScalaSharp.Core.Contexts;

    public abstract class BinaryExpression : IExpression
    {
        private IExpression left;
        private IExpression right;

        public BinaryExpression(IExpression left, IExpression right)
        {
            this.left = left;
            this.right = right;
        }

        public IExpression LeftExpression { get { return this.left; } }

        public IExpression RightExpression { get { return this.right; } }

        public object Evaluate(IContext context)
        {
            var lvalue = this.left.Evaluate(context);
            var rvalue = this.right.Evaluate(context);

            return this.Apply(lvalue, rvalue);
        }

        public abstract object Apply(object leftvalue, object rightvalue);
    }
}
