namespace ScalaSharp.Core.Parsing
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ScalaSharp.Core.Commands;

    public class Parser
    {
        private Lexer lexer;

        public Parser(string text)
        {
            this.lexer = new Lexer(text);
        }

        public ICommand ParseCommand()
        {
            var token = this.NextToken();

            if (token == null)
                return null;

            string name;

            if (token.Type == TokenType.Name && token.Value == "def")
            {
                name = this.ParseName();
                this.ParseToken(TokenType.Punctuation, ":");
                string type = this.ParseName();
                return new DefCommand(name, type);
            }

            if (token.Type == TokenType.Name && token.Value == "object")
            {
                name = this.ParseName();
                this.ParseToken(TokenType.Punctuation, "{");
                this.ParseToken(TokenType.Punctuation, "}");
                return new ObjectCommand(name);
            }

            if (token.Type == TokenType.Name && token.Value == "class")
            {
                name = this.ParseName();
                this.ParseToken(TokenType.Punctuation, "{");
                this.ParseToken(TokenType.Punctuation, "}");

                return new ClassCommand(name);
            }

            throw new ParserException(string.Format("Unexpected '{0}'", token.Value));
        }

        private string ParseName()
        {
            Token token = this.NextToken();

            if (token == null || token.Type != TokenType.Name)
                throw new ParserException("Expected a name");

            return token.Value;
        }

        private void ParseToken(TokenType type, string value)
        {
            Token token = this.NextToken();

            if (token == null || token.Type != type || token.Value != value)
                throw new ParserException(string.Format("Expected '{0}'", value));
        }

        private Token NextToken()
        {
            return lexer.NextToken();
        }
    }
}
