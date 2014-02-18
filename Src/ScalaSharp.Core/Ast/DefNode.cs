namespace ScalaSharp.Core.Ast
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ScalaSharp.Core.Language;

    public class DefNode
    {
        private string name;
        private IList<ArgumentInfo> arguments; 
        private TypeInfo typeinfo;
        private INode expression;

        public DefNode(string name, IList<ArgumentInfo> arguments, TypeInfo typeinfo, INode expression)
        {
            this.name = name;
            this.arguments = arguments;
            this.typeinfo = typeinfo;
            this.expression = expression;
        }

        public string Name { get { return this.name; } }

        public IList<ArgumentInfo> Arguments { get { return this.arguments; } }

        public TypeInfo TypeInfo { get { return this.typeinfo; } }

        public INode Expression { get { return this.expression; } }
    }
}
