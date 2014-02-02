namespace ScalaSharp.Core.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ScalaSharp.Core.Expressions;
    using ScalaSharp.Core.Language;

    public class VarCommand : ICommand
    {
        private string name;
        private TypeInfo typeinfo;
        private IExpression expression;

        public VarCommand(string name, TypeInfo typeinfo, IExpression expression)
        {
            this.name = name;
            this.typeinfo = typeinfo;
            this.expression = expression;

            if (typeinfo == null)
                this.typeinfo = expression.TypeInfo;
        }

        public string Name { get { return this.name; } }

        public TypeInfo TypeInfo { get { return this.typeinfo; } }

        public IExpression Expression { get { return this.expression; } }
    }
}
