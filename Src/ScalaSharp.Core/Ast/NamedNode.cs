namespace ScalaSharp.Core.Ast
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ScalaSharp.Core.Contexts;
    using ScalaSharp.Core.Language;

    public abstract class NamedNode : INode, IUntypedNode
    {
        private string name;
        private TypeInfo typeinfo;

        public NamedNode(string name, TypeInfo typeinfo)
        {
            this.name = name;
            this.typeinfo = typeinfo;
        }

        public string Name { get { return this.name; } }

        public TypeInfo TypeInfo { get { return this.typeinfo; } }

        public abstract void CheckType();

        public void SetTypeInfo(TypeInfo typeinfo)
        {
            this.typeinfo = typeinfo;
        }

        public void RegisterInContext(IContext context)
        {
            context.SetValue(this.name, this);
        }
    }
}
