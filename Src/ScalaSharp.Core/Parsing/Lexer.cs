namespace ScalaSharp.Core.Parsing
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Lexer
    {
        private static string punctuation = ",.;:{}()[]";
        private string text;
        private int position;
        private int length;

        public Lexer(string text)
        {
            this.text = text;
            this.length = text == null ? 0 : text.Length;
        }

        public Token NextToken()
        {
            this.SkipWhiteSpaces();

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

            if (ch == '"')
                return this.NextString();

            if (IsOperator(ch))
                return this.NextOperator(ch);

            if (punctuation.Contains(ch))
                return new Token(ch.ToString(), TokenType.Delimiter);

            if (char.IsDigit(ch))
                return this.NextInteger(ch);

            return this.NextName(ch);
        }

        private void SkipWhiteSpaces()
        {
            while (true)
            {
                while (this.position < this.length && IsWhiteSpace(this.text[this.position]))
                    this.position++;

                if (this.position >= this.length)
                    return;

                if (this.text[this.position] == '/' && this.position < this.length - 1 && this.text[position + 1] == '/')
                {
                    while (this.position < this.length && this.text[position] != '\r' && this.text[position] != '\n')
                        this.position++;
                }
                else
                    return;
            }
        }

        private static bool IsWhiteSpace(char ch)
        {
            if (ch == '\r' || ch == '\n')
                return false;

            return char.IsWhiteSpace(ch);
        }

        private static bool IsOperator(char ch)
        {
            if (char.IsWhiteSpace(ch))
                return false;

            if (char.IsLetterOrDigit(ch))
                return false;

            if (punctuation.Contains(ch))
                return false;

            return true;
        }

        private Token NextString()
        {
            string value = string.Empty;

            while (this.position < this.length && this.text[this.position] != '"')
                value += this.text[this.position++];

            if (this.position >= this.length)
                throw new LexerException("Unclosed string");

            this.position++;

            return new Token(value, TokenType.String);
        }

        private Token NextOperator(char ch)
        {
            string value = ch.ToString();

            while (this.position < this.length)
            {
                var ch2 = this.text[this.position];

                if (!IsOperator(ch2))
                    break;

                value += ch2;
                this.position++;
            }

            return new Token(value, TokenType.Operator);
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

                if (char.IsLetter(this.text[this.position]))
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

            while (this.position < this.length && char.IsLetter(this.text[this.position]))
                value += this.text[this.position++];

            return new Token(value, TokenType.Name);
        }
    }
}
