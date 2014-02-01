namespace ScalaSharp.Core.Expressions
{
    using System;
    using ScalaSharp.Core.Contexts;
    using ScalaSharp.Core.Language;
    
    public interface IExpression
    {
        TypeInfo TypeInfo { get; }

        object Evaluate(IContext context);
    }
}
