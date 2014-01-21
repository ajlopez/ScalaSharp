namespace ScalaSharp.Core.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class ObjectCommand : ICommand
    {
        private string name;

        public ObjectCommand(string name)
        {
            this.name = name;
        }

        public string Name { get { return this.name; } }
    }
}
