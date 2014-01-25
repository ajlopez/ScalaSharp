namespace ScalaSharp.Core.Parsing
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using ScalaSharp.Core.Commands;
    using ScalaSharp.Core.Expressions;
    using ScalaSharp.Core.Language;

    public class Parser
    {
        private Lexer lexer;
        private Stack<Token> tokens = new Stack<Token>();

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
                return this.ParseDefCommand();

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

            if (token.Type == TokenType.Name && token.Value == "val")
            {
                name = this.ParseName();
                this.ParseToken(TokenType.Operator, "=");
                IExpression expr = this.ParseExpression();

                return new ValCommand(name, expr);
            }

            throw new ParserException(string.Format("Unexpected '{0}'", token.Value));
        }

        public IExpression ParseExpression()
        {
            Token token = this.NextToken();

            if (token == null)
                return null;

            if (token.Type == TokenType.Name)
                return new VariableExpression(token.Value);

            return new ConstantExpression(int.Parse(token.Value, CultureInfo.InvariantCulture));
        }

        private DefCommand ParseDefCommand()
        {
            string name = this.ParseName();
            IList<Argument> arguments = new List<Argument>();

            if (this.TryParseToken(TokenType.Punctuation, "("))
                while (!this.TryParseToken(TokenType.Punctuation, ")"))
                {
                    if (arguments.Count > 0)
                        this.ParseToken(TokenType.Punctuation, ",");

                    string argname = this.ParseName();
                    this.ParseToken(TokenType.Punctuation, ":");
                    string argtype = this.ParseName();
                    arguments.Add(new Argument(argname, argtype));
                }

            this.ParseToken(TokenType.Punctuation, ":");
            string type = this.ParseName();
            return new DefCommand(name, arguments, type);
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

        private bool TryParseToken(TokenType type, string value)
        {
            Token token = this.NextToken();

            if (token != null && token.Type == type && token.Value == value)
                return true;

            this.PushToken(token);

            return false;
        }

        private Token NextToken()
        {
            if (this.tokens.Count > 0)
                return this.tokens.Pop();

            return this.lexer.NextToken();
        }

        private void PushToken(Token token)
        {
            this.tokens.Push(token);            
        }
    }
}
