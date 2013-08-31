namespace ScalaSharp.Core.Parsing
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Lexer
    {
        private string text;

        public Lexer(string text)
        {
            this.text = text;
        }

        public Token NextToken()
        {
            if (this.text == null)
                return null;

            Token token;

            this.text = this.text.Trim();

            if (char.IsDigit(this.text[0]))
                token = new Token(this.text, TokenType.Integer);
            else
                token = new Token(this.text.Trim(), TokenType.Name);

            this.text = null;

            return token;
        }
    }
}
