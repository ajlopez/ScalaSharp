namespace ScalaSharp.Core.Ast
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ScalaSharp.Core.Language;

    public abstract class NamedExpressionNode : NamedNode
    {
        private INode expression;

        public NamedExpressionNode(string name, TypeInfo typeinfo, INode expression)
            : base(name, typeinfo)
        {
            this.expression = expression;
        }

        public INode Expression { get { return this.expression; } }

        public override void CheckType()
        {
            this.expression.CheckType();

            if (this.TypeInfo == null)
                this.SetTypeInfo(this.expression.TypeInfo);
            else if (this.TypeInfo != this.expression.TypeInfo)
                throw new InvalidOperationException("type mismatch");
        }
    }
}
