namespace ScalaSharp.Core.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ScalaSharp.Core.Language;
    using ScalaSharp.Core.Contexts;

    public class IfExpression : IExpression
    {
        private IExpression conditionexpression;
        private IExpression thenexpression;
        private IExpression elseexpression;
        private TypeInfo typeinfo;

        public IfExpression(IExpression conditionexpression, IExpression thenexpression, IExpression elseexpression)
        {
            this.conditionexpression = conditionexpression;
            this.thenexpression = thenexpression;
            this.elseexpression = elseexpression;
            this.typeinfo = thenexpression.TypeInfo;
        }

        public TypeInfo TypeInfo { get { return this.typeinfo; } }

        public object Evaluate(IContext context)
        {
            bool condition = (bool)this.conditionexpression.Evaluate(context);

            if (condition)
                return this.thenexpression.Evaluate(context);
            else
                return this.elseexpression.Evaluate(context);
        }
    }
}
