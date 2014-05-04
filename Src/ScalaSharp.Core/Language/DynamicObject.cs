namespace ScalaSharp.Core.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ScalaSharp.Core.Contexts;

    public class DynamicObject : IContext
    {
        private IDictionary<string, object> values = new Dictionary<string, object>();

        public object GetValue(string name)
        {
            return this.values[name];
        }

        public void SetValue(string name, object value)
        {
            this.values[name] = value;
        }
    }
}
