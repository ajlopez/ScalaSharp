namespace ScalaSharp.Core.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class CompositeCommand : ICommand
    {
        private IList<ICommand> commands;

        public CompositeCommand(IList<ICommand> commands)
        {
            this.commands = commands;
        }

        public IList<ICommand> Commands { get { return this.commands; } }
    }
}
