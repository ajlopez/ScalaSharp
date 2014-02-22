namespace ScalaSharp.Core.Ast
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ScalaSharp.Core.Language;

    public interface IUntypedNode
    {
        void SetTypeInfo(TypeInfo typeinfo);
    }
}
