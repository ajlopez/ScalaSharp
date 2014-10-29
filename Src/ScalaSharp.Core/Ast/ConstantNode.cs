namespace ScalaSharp.Core.Ast
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ScalaSharp.Core.Contexts;
    using ScalaSharp.Core.Language;
    using ScalaSharp.Core.Expressions;

    public class ConstantNode : IExpressionNode
    {
        private object value;
        private TypeInfo typeinfo;

        public ConstantNode(object value)
        {
            this.value = value;
            this.typeinfo = TypeInfo.Make(value);
        }

        public object Value { get { return this.value; } }

        public TypeInfo TypeInfo { get { return this.typeinfo; } }

        public void CheckType(IContext context)
        {
        }

        public void RegisterInContext(IContext context)
        {
        }

        public IExpression GetExpression()
        {
            return new ConstantExpression(this.value);
        }
    }
}
