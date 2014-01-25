namespace ScalaSharp.Core.Expressions
{
    using System;
    using ScalaSharp.Core.Contexts;
    
    public interface IExpression
    {
        object Evaluate(IContext context);
    }
}
