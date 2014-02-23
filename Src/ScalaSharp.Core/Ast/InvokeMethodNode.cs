namespace ScalaSharp.Core.Ast
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class InvokeMethodNode
    {
        private INode target;
        private string methodname;
        private IList<INode> arguments;

        public InvokeMethodNode(INode target, string methodname, IList<INode> arguments)
        {
            this.target = target;
            this.methodname = methodname;
            this.arguments = arguments;
        }

        public INode Target { get { return this.target; } }

        public string MethodName { get { return this.methodname; } }

        public IList<INode> Arguments { get { return this.arguments; } }
    }
}
