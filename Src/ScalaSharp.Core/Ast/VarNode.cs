namespace ScalaSharp.Core.Ast
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ScalaSharp.Core.Commands;
    using ScalaSharp.Core.Language;

    public class VarNode : NamedExpressionNode, ICommandNode
    {
        public VarNode(string name, TypeInfo typeinfo, IExpressionNode expression)
            : base(name, typeinfo, expression)
        {
        }

        public ICommand GetCommand()
        {
            return new VarCommand(this.Name, this.TypeInfo, this.Expression.GetExpression());
        }
    }
}
