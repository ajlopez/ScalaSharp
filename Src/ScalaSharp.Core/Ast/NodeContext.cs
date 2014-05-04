namespace ScalaSharp.Core.Ast
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class NodeContext
    {
        private IDictionary<string, INode> nodes = new Dictionary<string, INode>();

        public INode GetNode(string name)
        {
            if (!this.nodes.ContainsKey(name))
                return null;

            return this.nodes[name];
        }

        public void SetNode(string name, INode node)
        {
            this.nodes[name] = node;
        }
    }
}
