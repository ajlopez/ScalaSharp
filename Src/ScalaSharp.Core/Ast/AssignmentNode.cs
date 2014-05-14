namespace ScalaSharp.Core.Ast
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ScalaSharp.Core.Contexts;
    using ScalaSharp.Core.Language;

    public class AssignmentNode : INode
    {
        private INode target;
        private INode expression;

        public AssignmentNode(INode target, INode expression)
        {
            this.target = target;
            this.expression = expression;
        }

        public INode Target { get { return this.target; } }

        public INode Expression { get { return this.expression; } }

        public TypeInfo TypeInfo { get { return this.expression.TypeInfo; } }

        public void CheckType()
        {
            this.expression.CheckType();
            this.target.CheckType();

            if (this.target.TypeInfo == null)
                ((IUntypedNode)this.target).SetTypeInfo(this.expression.TypeInfo);
            else if (this.target.TypeInfo != this.expression.TypeInfo)
                throw new InvalidOperationException("type mismatch");
        }

        public void RegisterInContext(IContext context)
        {
        }
    }
}
