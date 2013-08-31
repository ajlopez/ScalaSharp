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
            while (this.position < this.length && IsWhiteSpace(this.text[this.position]))
                this.position++;

            if (this.position >= this.length)
                return null;

            char ch = this.text[this.position++];

            if (ch == '\r')
            {
                if (this.position < this.length && this.text[this.position] == '\n')
                {
                    this.position++;
                    return new Token("\r\n", TokenType.NewLine);
                }

                return new Token("\r", TokenType.NewLine);
            }

            if (ch == '\n')
                return new Token("\n", TokenType.NewLine);

            if (char.IsDigit(ch))
                return this.NextInteger(ch);

            return this.NextName(ch);
        }

        private static bool IsWhiteSpace(char ch)
        {
            if (ch == '\r' || ch == '\n')
                return false;

            return char.IsWhiteSpace(ch);
        }

        private Token NextInteger(char ch)
        {
            string value = ch.ToString();

            while (this.position < this.length && char.IsDigit(this.text[this.position]))
                value += this.text[this.position++];

            if (this.position < this.length)
            {
                if (this.text[this.position] == '.')
                    return this.NextReal(value);

                if (!char.IsWhiteSpace(this.text[this.position]))
                    throw new LexerException(string.Format("Unexpected '{0}'", this.text[this.position]));
            }

            return new Token(value, TokenType.Integer);
        }

        private Token NextReal(string integer)
        {
            string value = integer + ".";
            this.position++;

            while (this.position < this.length && char.IsDigit(this.text[this.position]))
                value += this.text[this.position++];

            if (this.position < this.length && !char.IsWhiteSpace(this.text[this.position]))
                throw new LexerException(string.Format("Unexpected '{0}'", this.text[this.position]));

            return new Token(value, TokenType.Real);
        }

        private Token NextName(char ch)
        {
            string value = ch.ToString();

            while (this.position < this.length && !IsWhiteSpace(this.text[this.position]))
                value += this.text[this.position++];

            return new Token(value, TokenType.Name);
        }
    }
}
