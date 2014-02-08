namespace ScalaSharp.Core.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class EqualExpression : BinaryExpression
    {
        private Func<object, object, bool> apply;

        public EqualExpression(IExpression left, IExpression right)
            : base(left, right)
        {
            this.TypeInfo = Language.TypeInfo.Boolean;

            if (left.TypeInfo == Language.TypeInfo.Int && right.TypeInfo == Language.TypeInfo.Int)
                this.apply = (a, b) => (int)a == (int)b;
            else if (left.TypeInfo == Language.TypeInfo.Double && right.TypeInfo == Language.TypeInfo.Double)
                this.apply = (a, b) => (double)a == (double)b;
            else if (left.TypeInfo == Language.TypeInfo.Int && right.TypeInfo == Language.TypeInfo.Double)
                this.apply = (a, b) => (int)a == (double)b;
            else if (left.TypeInfo == Language.TypeInfo.Double && right.TypeInfo == Language.TypeInfo.Int)
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
