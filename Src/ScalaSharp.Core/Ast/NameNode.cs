﻿namespace ScalaSharp.Core.Ast
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ScalaSharp.Core.Language;

    public class NameNode : INode, IUntypedNode
    {
        private string name;
        private TypeInfo typeinfo;

        public NameNode(string name)
        {
            this.name = name;
        }

        public string Name { get { return this.name; } }

        public TypeInfo TypeInfo { get { return this.typeinfo; } }

        public void CheckType()
        {
        }

        public void SetTypeInfo(TypeInfo typeinfo)
        {
            this.typeinfo = typeinfo;
        }
    }
}
