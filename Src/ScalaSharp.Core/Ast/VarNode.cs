namespace ScalaSharp.Core.Ast
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ScalaSharp.Core.Language;

    public class VarNode : NamedExpressionNode
    {
        public VarNode(string name, TypeInfo typeinfo, IExpressionNode expression)
            : base(name, typeinfo, expression)
        {
        }
    }
}
