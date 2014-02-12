namespace ScalaSharp.Core.Ast
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class ClassNode : INode
    {
        private string name;
        private INode body;

        public ClassNode(string name, INode body)
        {
            this.name = name;
            this.body = body;
        }

        public string Name { get { return this.name; } }

        public INode Body { get { return this.body; } }
    }
}
