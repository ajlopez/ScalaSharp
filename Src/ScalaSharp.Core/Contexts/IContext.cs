namespace ScalaSharp.Core.Contexts
{
    using System;

    public interface IContext
    {
        object GetValue(string name);
        void SetValue(string name, object value);
    }
}
