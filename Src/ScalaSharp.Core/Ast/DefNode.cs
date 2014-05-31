namespace ScalaSharp.Core.Ast
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ScalaSharp.Core.Commands;
    using ScalaSharp.Core.Language;

    public class DefNode : NamedExpressionNode, ICommandNode
    {
        private IList<ArgumentInfo> arguments; 

        public DefNode(string name, IList<ArgumentInfo> arguments, TypeInfo typeinfo, IExpressionNode expression)
            : base(name, typeinfo, expression)
        {
            this.arguments = arguments;
        }

        public IList<ArgumentInfo> Arguments { get { return this.arguments; } }

        public ICommand GetCommand()
        {
            return new DefCommand(this.Name, this.arguments, this.TypeInfo, new ExpressionCommand(this.Expression.GetExpression()));
            throw new NotImplementedException();
        }
    }
}
