namespace ScalaSharp.Core.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ScalaSharp.Core.Contexts;

    public class VariableExpression : IExpression
    {
        private string name;

        public VariableExpression(string name)
        {
            this.name = name;
        }

        public string Name { get { return this.name; } }

        public object Evaluate(IContext context)
        {
            return context.GetValue(this.name);
        }
    }
}
