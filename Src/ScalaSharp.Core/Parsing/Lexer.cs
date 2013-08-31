namespace ScalaSharp.Core.Parsing
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Lexer
    {
        private string text;
        private int position;
        private int length;

        public Lexer(string text)
        {
            this.text = text;
            this.length = text.Length;
        }

        public Token NextToken()
        {
            while (this.position < this.length && char.IsWhiteSpace(text[position]))
                this.position++;

            if (this.position >= this.length)
                return null;

            char ch = this.text[this.position++];

            if (char.IsDigit(ch))
                return NextInteger(ch);

            return NextName(ch);
        }

        private Token NextInteger(char ch)
        {
            string value = ch.ToString();

            while (this.position < this.length && char.IsDigit(text[position]))
                value += text[position++];

            if (this.position < this.length)
            {
                if (text[position] == '.')
                    return NextReal(value);

                if (!char.IsWhiteSpace(text[position]))
                    throw new LexerException(string.Format("Unexpected '{0}'", text[position]));
            }

            return new Token(value, TokenType.Integer);
        }

        private Token NextReal(string integer)
        {
            string value = integer + ".";
            this.position++;

            while (this.position < this.length && char.IsDigit(text[position]))
                value += text[position++];

            if (this.position < this.length && !char.IsWhiteSpace(text[position]))
                throw new LexerException(string.Format("Unexpected '{0}'", text[position]));

            return new Token(value, TokenType.Real);
        }

        private Token NextName(char ch)
        {
            string value = ch.ToString();

            while (this.position < this.length && !char.IsWhiteSpace(text[position]))
                value += text[position++];

            return new Token(value, TokenType.Name);
        }
    }
}
