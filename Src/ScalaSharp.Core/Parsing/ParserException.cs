namespace ScalaSharp.Core.Parsing
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class ParserException : FieldAccessException
    {
        public ParserException(string message)
            : base(message)
        {
        }
    }
}
