namespace ScalaSharp.Core.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class DefCommand : ICommand
    {
        private string name;
        private string type;

        public DefCommand(string name, string type)
        {
            this.name = name;
            this.type = type;
        }

        public string Name { get { return this.name; } }

        public string Type { get { return this.type; } }
    }
}
