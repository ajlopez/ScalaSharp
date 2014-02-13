namespace ScalaSharp.Core.Ast
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ScalaSharp.Core.Language;

    public class VarNode
    {
        private string name;
        private TypeInfo typeinfo;
        private INode expression;

        public VarNode(string name, TypeInfo typeinfo, INode expression)
        {
            this.name = name;
            this.typeinfo = typeinfo;
            this.expression = expression;
        }

        public string Name { get { return this.name; } }

        public TypeInfo TypeInfo { get { return this.typeinfo; } }

        public INode Expression { get { return this.expression; } }
    }
}
