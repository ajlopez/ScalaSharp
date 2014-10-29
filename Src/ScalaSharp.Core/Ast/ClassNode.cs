namespace ScalaSharp.Core.Ast
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ScalaSharp.Core.Commands;
    using ScalaSharp.Core.Contexts;
    using ScalaSharp.Core.Language;

    public class ClassNode : ICommandNode
    {
        private string name;
        private ICommandNode body;
        private TypeInfo typeinfo;

        public ClassNode(string name, ICommandNode body)
        {
            this.name = name;
            this.body = body;
            this.typeinfo = TypeInfo.MakeByName(name);
        }

        public string Name { get { return this.name; } }

        public ICommandNode Body { get { return this.body; } }

        public TypeInfo TypeInfo { get { return this.typeinfo; } }

        public void CheckType(IContext context)
        {
            if (this.body != null)
                this.body.CheckType(context);
        }

        public void RegisterInContext(IContext context)
        {
            context.SetValue(this.name, this);
        }

        public ICommand GetCommand()
        {
            return new ClassCommand(this.name, this.body.GetCommand());
            throw new NotImplementedException();
        }
    }
}
