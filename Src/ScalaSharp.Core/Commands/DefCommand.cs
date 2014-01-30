namespace ScalaSharp.Core.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ScalaSharp.Core.Language;
    using ScalaSharp.Core.Expressions;

    public class DefCommand : ICommand
    {
        private string name;
        private string type;
        private IList<ArgumentInfo> arguments;
        private ICommand body;

        public DefCommand(string name, IList<ArgumentInfo> arguments, string type, ICommand body)
        {
            this.name = name;
            this.arguments = arguments;
            this.type = type;
            this.body = body;
        }

        public string Name { get { return this.name; } }

        public IList<ArgumentInfo> Arguments { get { return this.arguments; } }

        public string Type { get { return this.type; } }

        public ICommand Body { get { return this.body; } }
    }
}
