namespace ScalaSharp.Core.Ast
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
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
    }
}
