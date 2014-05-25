namespace ScalaSharp.Core.Ast
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ScalaSharp.Core.Contexts;
    using ScalaSharp.Core.Language;
    using ScalaSharp.Core.Expressions;

    public interface IExpressionNode : INode
    {
        IExpression GetExpression();
    }
}
