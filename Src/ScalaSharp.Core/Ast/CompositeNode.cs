namespace ScalaSharp.Core.Ast
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ScalaSharp.Core.Language;

    public class CompositeNode : INode
    {
        private IList<INode> nodes;
        private TypeInfo typeinfo;

        public CompositeNode(IList<INode> nodes)
        {
            this.nodes = nodes;
            this.typeinfo = nodes.Last().TypeInfo;
        }

        public IList<INode> Nodes { get { return this.nodes; } }

        public TypeInfo TypeInfo { get { return this.typeinfo; } }

        public void CheckType()
        {
            foreach (var node in this.nodes)
                node.CheckType();

            this.typeinfo = this.nodes.Last().TypeInfo;
        }
    }
}
