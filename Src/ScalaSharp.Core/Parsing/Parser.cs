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
                name = this.NextToken().Value;
                this.NextToken();
                string type = this.NextToken().Value;
                return new DefCommand(name, type);
            }

            name = this.NextToken().Value;
            this.NextToken();
            this.NextToken();

            return new ClassCommand(name);
        }

        private Token NextToken()
        {
            return lexer.NextToken();
        }
    }
}
