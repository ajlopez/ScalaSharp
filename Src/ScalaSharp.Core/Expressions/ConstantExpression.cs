namespace ScalaSharp.Core.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class ConstantExpression : ScalaSharp.Core.Expressions.IExpression
    {
        private object value;

        public ConstantExpression(object value)
        {
            this.value = value;
        }

        public object Value { get { return this.value; } }

        public object Evaluate()
        {
            return this.value;
        }
    }
}
