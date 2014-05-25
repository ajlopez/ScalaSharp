namespace ScalaSharp.Core.Ast
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ScalaSharp.Core.Contexts;
    using ScalaSharp.Core.Language;

    public class NameNode : IExpressionNode, IUntypedNode
    {
        private string name;
        private TypeInfo typeinfo;

        public NameNode(string name)
        {
            this.name = name;
        }

        public string Name { get { return this.name; } }

        public TypeInfo TypeInfo { get { return this.typeinfo; } }

        public void CheckType()
        {
        }

        public void SetTypeInfo(TypeInfo typeinfo)
        {
            this.typeinfo = typeinfo;
        }

        public void RegisterInContext(IContext context)
        {
            context.SetValue(this.name, this);
        }

        public Expressions.IExpression GetExpression()
        {
            throw new NotImplementedException();
        }
    }
}
