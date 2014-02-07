namespace ScalaSharp.Core.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ScalaSharp.Core.Expressions;
    using ScalaSharp.Core.Language;

    public class DefCommand : ICommand
    {
        private string name;
        private TypeInfo typeinfo;
        private IList<ArgumentInfo> arguments;
        private ICommand body;

        public DefCommand(string name, IList<ArgumentInfo> arguments, TypeInfo typeinfo, ICommand body)
        {
            this.name = name;
            this.arguments = arguments;
            this.typeinfo = typeinfo;
            this.body = body;
        }

        public string Name { get { return this.name; } }

        public IList<ArgumentInfo> Arguments { get { return this.arguments; } }

        public TypeInfo TypeInfo { get { return this.typeinfo; } }

        public ICommand Body { get { return this.body; } }
    }
}
