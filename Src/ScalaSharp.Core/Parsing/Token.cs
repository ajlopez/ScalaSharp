namespace ScalaSharp.Core.Parsing
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Token
    {
        private object value;
        private TokenType type;

        public Token(object value, TokenType type)
        {
            this.value = value;
            this.type = type;
        }

        public object Value { get { return this.value; } }

        public TokenType Type { get { return this.type; } }
    }
}
