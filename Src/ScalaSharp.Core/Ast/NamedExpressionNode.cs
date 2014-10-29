namespace ScalaSharp.Core.Ast
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ScalaSharp.Core.Contexts;
    using ScalaSharp.Core.Language;

    public abstract class NamedExpressionNode : NamedNode
    {
        private IExpressionNode expression;

        public NamedExpressionNode(string name, TypeInfo typeinfo, IExpressionNode expression)
            : base(name, typeinfo)
        {
            this.expression = expression;
        }

        public IExpressionNode Expression { get { return this.expression; } }

        public override void CheckType(IContext context)
        {
            this.expression.CheckType(context);

            if (this.TypeInfo == null)
                this.SetTypeInfo(this.expression.TypeInfo);
            else if (this.TypeInfo != this.expression.TypeInfo)
                throw new InvalidOperationException("type mismatch");
        }
    }
}
