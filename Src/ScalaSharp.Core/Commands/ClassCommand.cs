namespace ScalaSharp.Core.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class ClassCommand : ICommand
    {
        private string name;

        public ClassCommand(string name)
        {
            this.name = name;
        }

        public string Name { get { return this.name; } }
    }
}
