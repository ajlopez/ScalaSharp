namespace ScalaSharp.Core.Ast
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
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
    }
}
