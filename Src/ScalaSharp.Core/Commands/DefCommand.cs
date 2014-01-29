namespace ScalaSharp.Core.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ScalaSharp.Core.Language;

    public class DefCommand : ICommand
    {
        private string name;
        private string type;
        private IList<ArgumentInfo> arguments;

        public DefCommand(string name, IList<ArgumentInfo> arguments, string type)
        {
            this.name = name;
            this.arguments = arguments;
            this.type = type;
        }

        public string Name { get { return this.name; } }

        public IList<ArgumentInfo> Arguments { get { return this.arguments; } }

        public string Type { get { return this.type; } }
    }
}
