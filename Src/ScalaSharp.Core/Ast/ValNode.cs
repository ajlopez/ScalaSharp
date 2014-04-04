namespace ScalaSharp.Core.Ast
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ScalaSharp.Core.Language;

    public class ValNode : NamedExpressionNode
    {
        public ValNode(string name, TypeInfo typeinfo, INode expression)
            : base(name, typeinfo, expression)
        {
        }
    }
}
