namespace ScalaSharp.Core.Ast
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ScalaSharp.Core.Commands;
    using ScalaSharp.Core.Contexts;
    using ScalaSharp.Core.Language;

    public class ObjectNode : ICommandNode
    {
        private string name;
        private ICommandNode body;
        private TypeInfo typeinfo;

        public ObjectNode(string name, ICommandNode body)
        {
            this.name = name;
            this.body = body;
            this.typeinfo = TypeInfo.MakeByName(name);
        }

        public string Name { get { return this.name; } }

        public INode Body { get { return this.body; } }

        public TypeInfo TypeInfo { get { return this.typeinfo; } }

        public void CheckType(IContext context)
        {
            if (this.Body != null)
                this.Body.CheckType(context);
        }

        public void RegisterInContext(IContext context)
        {
            context.SetValue(this.name, this);
        }

        public ICommand GetCommand()
        {
            return new ObjectCommand(this.name, this.body.GetCommand());
        }
    }
}
