namespace ScalaSharp.Core.Ast
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ScalaSharp.Core.Commands;
    using ScalaSharp.Core.Contexts;
    using ScalaSharp.Core.Language;

    public class CompositeNode : ICommandNode
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

        public void CheckType(IContext context)
        {
            foreach (var node in this.nodes)
                node.CheckType(context);

            this.typeinfo = this.nodes.Last().TypeInfo;
        }

        public void RegisterInContext(IContext context)
        {
            foreach (var node in this.nodes)
                node.RegisterInContext(context);
        }

        public ICommand GetCommand()
        {
            throw new NotImplementedException();
        }
    }
}
