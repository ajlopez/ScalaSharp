namespace ScalaSharp.Core.Ast
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ScalaSharp.Core.Contexts;
    using ScalaSharp.Core.Language;

    public interface INode
    {
        TypeInfo TypeInfo { get; }

        void CheckType(IContext context);

        void RegisterInContext(IContext context);
    }
}
