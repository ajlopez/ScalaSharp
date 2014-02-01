namespace ScalaSharp.Core.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ScalaSharp.Core.Contexts;
    using ScalaSharp.Core.Language;

    public class ConstantExpression : IExpression
    {
        private object value;
        private TypeInfo typeinfo;

        public ConstantExpression(object value)
        {
            this.value = value;
            this.typeinfo = TypeInfo.Make(value);
        }

        public object Value { get { return this.value; } }

        public TypeInfo TypeInfo { get { return this.typeinfo; } }

        public object Evaluate(IContext context)
        {
            return this.value;
        }
    }
}
