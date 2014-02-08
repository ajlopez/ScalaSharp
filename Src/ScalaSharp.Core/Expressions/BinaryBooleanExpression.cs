namespace ScalaSharp.Core.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ScalaSharp.Core.Contexts;
    using ScalaSharp.Core.Language;

    public abstract class BinaryBooleanExpression : BinaryExpression
    {
        public BinaryBooleanExpression(IExpression left, IExpression right)
            : base(left, right)
        {
            this.TypeInfo = TypeInfo.Boolean;
        }
    }
}
