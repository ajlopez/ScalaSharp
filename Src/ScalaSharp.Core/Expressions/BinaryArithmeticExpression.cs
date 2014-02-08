namespace ScalaSharp.Core.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ScalaSharp.Core.Contexts;
    using ScalaSharp.Core.Language;

    public abstract class BinaryArithmeticExpression : BinaryExpression
    {
        public BinaryArithmeticExpression(IExpression left, IExpression right)
            : base(left, right)
        {
            if (left.TypeInfo == TypeInfo.Int && right.TypeInfo == TypeInfo.Int)
                this.TypeInfo = TypeInfo.Int;
            else
                this.TypeInfo = TypeInfo.Double;
        }
    }
}
