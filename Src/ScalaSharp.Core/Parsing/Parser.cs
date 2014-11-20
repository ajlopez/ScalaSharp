namespace ScalaSharp.Core.Parsing
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using ScalaSharp.Core.Ast;
    using ScalaSharp.Core.Commands;
    using ScalaSharp.Core.Expressions;
    using ScalaSharp.Core.Language;

    public class Parser
    {
        private static string[][] binaryoperators = new string[][] { new string[] { "==" }, new string[] { "+", "-" }, new string[] { "*", "/", "%" } };
        private Lexer lexer;
        private Stack<Token> tokens = new Stack<Token>();

        public Parser(string text)
        {
            this.lexer = new Lexer(text);
        }

        public ICommandNode ParseNodes()
        {
            IList<INode> nodes = new List<INode>();

            for (var node = this.ParseNode(); node != null; node = this.ParseNode())
                nodes.Add(node);

            if (nodes.Count == 0)
                return null;

            if (nodes.Count == 1)
                return (ICommandNode)nodes[0];

            return new CompositeNode(nodes);
        }

        public INode ParseNode()
        {
            var node = this.ParseSimpleNode();

            if (node == null)
                return null;

            this.ParseEndOfCommand();

            return node;
        }

        public ICommand ParseCommands()
        {
            IList<ICommand> commands = new List<ICommand>();

            for (var cmd = this.ParseCommand(); cmd != null; cmd = this.ParseCommand())
                commands.Add(cmd);

            if (commands.Count == 0)
                return null;

            if (commands.Count == 1)
                return commands[0];

            return new CompositeCommand(commands);
        }

        public ICommand ParseCommand()
        {
            var token = this.NextToken();

            while (token != null && token.Type == TokenType.NewLine)
                token = this.NextToken();

            if (token == null)
                return null;

            string name;

            if (token.Type == TokenType.Name && token.Value == "def")
                return this.ParseDefCommand();

            if (token.Type == TokenType.Name && token.Value == "object")
            {
                name = this.ParseName();
                this.ParseToken(TokenType.Delimiter, "{");
                ICommand body = this.ParseCommands();
                this.ParseToken(TokenType.Delimiter, "}");
                this.ParseEndOfCommand();

                return new ObjectCommand(name, body);
            }

            if (token.Type == TokenType.Name && token.Value == "class")
            {
                name = this.ParseName();
                this.ParseToken(TokenType.Delimiter, "{");
                ICommand body = this.ParseCommands();
                this.ParseToken(TokenType.Delimiter, "}");
                this.ParseEndOfCommand();

                return new ClassCommand(name, body);
            }

            if (token.Type == TokenType.Name && token.Value == "val")
            {
                name = this.ParseName();
                this.ParseToken(TokenType.Operator, "=");
                IExpression expr = this.ParseExpression();
                this.ParseEndOfCommand();

                return new ValCommand(name, expr);
            }

            if (token.Type == TokenType.Name && token.Value == "var")
            {
                name = this.ParseName();
                TypeInfo typeinfo = null;
                IExpression expr = null;

                if (this.TryParseToken(TokenType.Delimiter, ":"))
                    typeinfo = this.ParseTypeInfo();
                if (this.TryParseToken(TokenType.Operator, "="))
                    expr = this.ParseExpression();

                if (typeinfo == null && expr == null)
                    throw new ParserException("Expected ':' or '='");

                this.ParseEndOfCommand();

                return new VarCommand(name, typeinfo, expr);
            }

            this.PushToken(token);

            return null;
        }

        public IExpression ParseExpression()
        {
            return this.ParseBinaryExpression(0);
        }

        private INode ParseSimpleNode()
        {
            var token = this.NextToken();

            if (token == null)
                return null;

            if (token.Type == TokenType.String)
                return new ConstantNode(token.Value);

            if (token.Type == TokenType.Integer)
                return new ConstantNode(int.Parse(token.Value, CultureInfo.InvariantCulture));

            if (token.Type == TokenType.Real)
                return new ConstantNode(double.Parse(token.Value, CultureInfo.InvariantCulture));

            if (token.Type == TokenType.Name && token.Value == "class")
            {
                string name = this.ParseName();
                this.ParseToken(TokenType.Delimiter, "{");
                ICommandNode body = this.ParseNodes();
                this.ParseToken(TokenType.Delimiter, "}");

                return new ClassNode(name, body);
            }

            if (token.Type == TokenType.Name && token.Value == "object")
            {
                string name = this.ParseName();
                this.ParseToken(TokenType.Delimiter, "{");
                ICommandNode body = this.ParseNodes();
                this.ParseToken(TokenType.Delimiter, "}");

                return new ObjectNode(name, body);
            }

            if (token.Type == TokenType.Name && token.Value == "val")
            {
                string name = this.ParseName();
                TypeInfo tinfo = null;
                IExpressionNode expr = null;

                if (this.TryParseToken(TokenType.Delimiter, ":"))
                    tinfo = this.ParseTypeInfo();

                if (this.TryParseToken(TokenType.Operator, "="))
                    expr = this.ParseExpressionNode();

                return new ValNode(name, tinfo, expr);
            }

            if (token.Type == TokenType.Name && token.Value == "var")
            {
                string name = this.ParseName();
                TypeInfo tinfo = null;
                IExpressionNode expr = null;

                if (this.TryParseToken(TokenType.Delimiter, ":"))
                    tinfo = this.ParseTypeInfo();

                if (this.TryParseToken(TokenType.Operator, "="))
                    expr = this.ParseExpressionNode();

                return new VarNode(name, tinfo, expr);
            }

            if (token.Type == TokenType.Name && token.Value == "def")
                return this.ParseDefNode();

            if (token.Type == TokenType.Name)
            {
                INode target = null;

                if (this.TryParseToken(TokenType.Delimiter, "."))
                {
                    target = new NameNode(token.Value);
                    token = new Token(this.ParseName(), TokenType.Name);
                }

                if (this.TryParseToken(TokenType.Delimiter, "("))
                {
                    IList<INode> arguments = new List<INode>();

                    while (!this.TryParseToken(TokenType.Delimiter, ")"))
                    {
                        if (arguments.Count > 0)
                            this.ParseToken(TokenType.Delimiter, ",");

                        arguments.Add(this.ParseSimpleNode());
                    }

                    if (target != null)
                        return new InvokeMethodNode(target, token.Value, arguments);

                    return new InvokeNode(token.Value, arguments);
                }
                else
                    return new NameNode(token.Value);
            }

            this.PushToken(token);

            return null;
        }

        private IExpressionNode ParseExpressionNode()
        {
            var token = this.NextToken();

            if (token == null)
                return null;

            if (token.Type == TokenType.String)
                return new ConstantNode(token.Value);

            if (token.Type == TokenType.Integer)
                return new ConstantNode(int.Parse(token.Value, CultureInfo.InvariantCulture));

            if (token.Type == TokenType.Real)
                return new ConstantNode(double.Parse(token.Value, CultureInfo.InvariantCulture));

            if (token.Type == TokenType.Name)
            {
                if (this.TryParseToken(TokenType.Delimiter, "("))
                {
                    IList<INode> arguments = new List<INode>();

                    while (!this.TryParseToken(TokenType.Delimiter, ")"))
                    {
                        if (arguments.Count > 0)
                            this.ParseToken(TokenType.Delimiter, ",");

                        arguments.Add(this.ParseSimpleNode());
                    }

                    return new InvokeNode(token.Value, arguments);
                }
                else
                    return new NameNode(token.Value);
            }

            this.PushToken(token);

            return null;
        }

        private DefNode ParseDefNode()
        {
            string name = this.ParseName();
            IList<ArgumentInfo> arguments = new List<ArgumentInfo>();

            if (this.TryParseToken(TokenType.Delimiter, "("))
                while (!this.TryParseToken(TokenType.Delimiter, ")"))
                {
                    if (arguments.Count > 0)
                        this.ParseToken(TokenType.Delimiter, ",");

                    string argname = this.ParseName();
                    this.ParseToken(TokenType.Delimiter, ":");
                    TypeInfo ti = this.ParseTypeInfo();
                    arguments.Add(new ArgumentInfo(argname, ti));
                }

            TypeInfo typeinfo = null;

            if (this.TryParseToken(TokenType.Delimiter, ":"))
                typeinfo = this.ParseTypeInfo();

            IExpressionNode expr = null;

            if (this.TryParseToken(TokenType.Operator, "="))
                expr = this.ParseExpressionNode();

            if (typeinfo == null && expr == null)
                throw new ParserException("Expected ':' or '='");

            return new DefNode(name, arguments, typeinfo, expr);
        }

        private IExpression ParseBinaryExpression(int level)
        {
            if (level >= binaryoperators.Length)
                return this.ParseTerm();

            IExpression expr = this.ParseBinaryExpression(level + 1);

            if (expr == null)
                return null;

            Token token;

            for (token = this.NextToken(); token != null && this.IsBinaryOperator(level, token); token = this.NextToken())
            {
                if (token.Value == "+")
                    expr = new AddExpression(expr, this.ParseBinaryExpression(level + 1));
                else if (token.Value == "-")
                    expr = new SubtractExpression(expr, this.ParseBinaryExpression(level + 1));
                else if (token.Value == "*")
                    expr = new MultiplyExpression(expr, this.ParseBinaryExpression(level + 1));
                else if (token.Value == "/")
                    expr = new DivideExpression(expr, this.ParseBinaryExpression(level + 1));
                else if (token.Value == "%")
                    expr = new ModulusExpression(expr, this.ParseBinaryExpression(level + 1));
                else if (token.Value == "==")
                    expr = new EqualExpression(expr, this.ParseBinaryExpression(level + 1));
            }

            if (token != null)
                this.PushToken(token);

            return expr;
        }

        private IExpression ParseTerm()
        {
            Token token = this.NextToken();

            if (token == null)
                return null;

            if (token.Type == TokenType.Name)
                return new VariableExpression(token.Value);

            if (token.Type == TokenType.String)
                return new ConstantExpression(token.Value);

            if (token.Type == TokenType.Integer)
                return new ConstantExpression(int.Parse(token.Value, CultureInfo.InvariantCulture));

            if (token.Type == TokenType.Real)
                return new ConstantExpression(double.Parse(token.Value, CultureInfo.InvariantCulture));

            this.PushToken(token);

            return null;
        }

        private void ParseEndOfCommand()
        {
            Token token = this.NextToken();

            if (token == null)
                return;

            if (token.Type == TokenType.NewLine)
                return;

            if (token.Type == TokenType.Delimiter && token.Value == ";")
                return;

            if (token.Type == TokenType.Delimiter && token.Value == "}")
            {
                this.PushToken(token);
                return;
            }

            throw new ParserException(string.Format("Unexpected '{0}'", token.Value));
        }

        private DefCommand ParseDefCommand()
        {
            string name = this.ParseName();
            IList<ArgumentInfo> arguments = new List<ArgumentInfo>();

            if (this.TryParseToken(TokenType.Delimiter, "("))
                while (!this.TryParseToken(TokenType.Delimiter, ")"))
                {
                    if (arguments.Count > 0)
                        this.ParseToken(TokenType.Delimiter, ",");

                    string argname = this.ParseName();
                    this.ParseToken(TokenType.Delimiter, ":");
                    TypeInfo ti = this.ParseTypeInfo();
                    arguments.Add(new ArgumentInfo(argname, ti));
                }

            TypeInfo typeinfo = null;

            if (this.TryParseToken(TokenType.Delimiter, ":"))
                typeinfo = this.ParseTypeInfo();

            ICommand body = null;

            if (this.TryParseToken(TokenType.Operator, "="))
                body = new ExpressionCommand(this.ParseExpression());

            if (typeinfo == null && body == null)
                throw new ParserException("Expected ':' or '='");

            this.ParseEndOfCommand();

            return new DefCommand(name, arguments, typeinfo, body);
        }

        private TypeInfo ParseTypeInfo()
        {
            return TypeInfo.MakeByName(this.ParseName());
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

        private bool IsBinaryOperator(int level, Token token)
        {
            return token.Type == TokenType.Operator && binaryoperators[level].Contains(token.Value);
        }
    }
}
