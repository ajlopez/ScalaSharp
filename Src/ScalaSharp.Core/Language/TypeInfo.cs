namespace ScalaSharp.Core.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class TypeInfo
    {
        private string name;

        public TypeInfo(string name)
        {
            this.name = name;
        }

        public string Name { get { return this.name; } }
    }
}
