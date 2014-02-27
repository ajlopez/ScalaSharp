namespace ScalaSharp.Core.Ast
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class InvokeNode
    {
        private string methodname;
        private IList<INode> arguments;

        public InvokeNode(string methodname, IList<INode> arguments)
        {
            this.methodname = methodname;
            this.arguments = arguments;
        }

        public string MethodName { get { return this.methodname; } }

        public IList<INode> Arguments { get { return this.arguments; } }
    }
}
