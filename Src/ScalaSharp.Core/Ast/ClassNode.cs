namespace ScalaSharp.Core.Ast
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ScalaSharp.Core.Contexts;
    using ScalaSharp.Core.Language;

    public class ClassNode : INode
    {
        private string name;
        private INode body;
        private TypeInfo typeinfo;

        public ClassNode(string name, INode body)
        {
            this.name = name;
            this.body = body;
            this.typeinfo = TypeInfo.MakeByName(name);
        }

        public string Name { get { return this.name; } }

        public INode Body { get { return this.body; } }

        public TypeInfo TypeInfo { get { return this.typeinfo; } }

        public void CheckType()
        {
            if (this.body != null)
                this.body.CheckType();
        }

        public void RegisterInContext(IContext context)
        {
            context.SetValue(this.name, this);
        }
    }
}
