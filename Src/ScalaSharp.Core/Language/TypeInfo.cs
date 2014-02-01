namespace ScalaSharp.Core.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class TypeInfo
    {
        private static TypeInfo tiint = new TypeInfo("Int");
        private static TypeInfo tidouble = new TypeInfo("Double");
        private static TypeInfo tistring = new TypeInfo("String");
        private static TypeInfo tiany = new TypeInfo("Any");
        private static TypeInfo tinull = new TypeInfo("Null");
        private static TypeInfo tiboolean = new TypeInfo("Boolean");

        private string name;

        public TypeInfo(string name)
        {
            this.name = name;
        }

        public string Name { get { return this.name; } }

        public static TypeInfo Int { get { return tiint; } }

        public static TypeInfo Double { get { return tidouble; } }

        public static TypeInfo String { get { return tistring; } }

        public static TypeInfo Any { get { return tiany; } }

        public static TypeInfo Null { get { return tinull; } }

        public static TypeInfo Boolean { get { return tiboolean; } }

        public static TypeInfo Make(object value)
        {
            if (value is int)
                return Int;

            if (value is double)
                return Double;

            if (value is string)
                return String;

            if (value is bool)
                return Boolean;

            if (value == null)
                return Null;

            return new TypeInfo(value.GetType().FullName);
        }
    }
}
