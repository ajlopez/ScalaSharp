﻿namespace ScalaSharp.Core.Ast
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ScalaSharp.Core.Commands;
    using ScalaSharp.Core.Contexts;
    using ScalaSharp.Core.Language;

    public interface ICommandNode : INode
    {
        ICommand GetCommand();
    }
}
