namespace ScalaSharp.Core.Ast
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ScalaSharp.Core.Language;

    public class ConstantNode : INode
    {
        private object value;
        private TypeInfo typeinfo;

        public ConstantNode(object value)
        {
            this.value = value;
            this.typeinfo = TypeInfo.Make(value);
        }

        public object Value { get { return this.value; } }

        public TypeInfo TypeInfo { get { return this.typeinfo; } }
    }
}
