namespace ScalaSharp.Core.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ScalaSharp.Core.Language;

    public class EqualExpression : BinaryBooleanExpression
    {
        private Func<object, object, bool> apply;

        public EqualExpression(IExpression left, IExpression right)
            : base(left, right)
        {
            if (left.TypeInfo == TypeInfo.Int && right.TypeInfo == TypeInfo.Int)
                this.apply = (a, b) => (int)a == (int)b;
            else if (left.TypeInfo == TypeInfo.Double && right.TypeInfo == TypeInfo.Double)
                this.apply = (a, b) => (double)a == (double)b;
            else if (left.TypeInfo == TypeInfo.Int && right.TypeInfo == TypeInfo.Double)
                this.apply = (a, b) => (int)a == (double)b;
            else if (left.TypeInfo == TypeInfo.Double && right.TypeInfo == TypeInfo.Int)
                this.apply = (a, b) => (double)a == (int)b;
        }

        public override object Apply(object leftvalue, object rightvalue)
        {
            if (this.apply != null)
                return this.apply(leftvalue, rightvalue);

            return leftvalue == rightvalue;
        }
    }
}
