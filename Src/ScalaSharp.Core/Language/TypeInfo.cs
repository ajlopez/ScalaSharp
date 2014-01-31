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

        public static TypeInfo Make(object value)
        {
            if (value is int)
                return new TypeInfo("Int");

            if (value is double)
                return new TypeInfo("Double");

            if (value is string)
                return new TypeInfo("String");

            if (value == null)
                return new TypeInfo("Null");

            return new TypeInfo(value.GetType().FullName);
        }
    }
}
