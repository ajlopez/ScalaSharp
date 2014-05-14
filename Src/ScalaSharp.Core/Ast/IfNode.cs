namespace ScalaSharp.Core.Ast
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ScalaSharp.Core.Contexts;
    using ScalaSharp.Core.Language;

    public class IfNode : INode
    {
        private INode condition;
        private INode then;
        private INode @else;

        public IfNode(INode condition, INode then, INode @else)
        {
            this.condition = condition;
            this.then = then;
            this.@else = @else;
        }

        public INode Condition { get { return this.condition; } }

        public INode Then { get { return this.then; } }

        public INode Else { get { return this.@else; } }

        public TypeInfo TypeInfo { get { return this.then.TypeInfo; } }

        public void CheckType()
        {
            if (this.then != null)
                this.then.CheckType();

            if (this.@else != null)
                this.@else.CheckType();

            this.condition.CheckType();

            if (this.condition.TypeInfo != TypeInfo.Boolean)
                throw new InvalidOperationException("condition must be boolean");

            if (this.then.TypeInfo != this.@else.TypeInfo)
                throw new InvalidOperationException("type mismatch");
        }

        public void RegisterInContext(IContext context)
        {
            if (this.then != null)
                this.then.RegisterInContext(context);

            if (this.@else != null)
                this.@else.RegisterInContext(context);
        }
    }
}
