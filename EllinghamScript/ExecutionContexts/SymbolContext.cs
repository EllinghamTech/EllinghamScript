using System;
using EllinghamScript.Internal;
using EllinghamScript.Variables;

namespace EllinghamScript.ExecutionContexts
{
    /// <summary>
    /// A symbol context represents either a function call
    /// or a variable.  From here we activate either a FunctionContext
    /// or VariableContext.
    /// </summary>
    public class SymbolContext : ExecutionContext
    {
        protected string Symbol { get; set; }

        public SymbolContext(ScriptRunner scriptRunner, string symbol) : base(scriptRunner)
        {
            Symbol = symbol;

            switch (symbol.ToLower())
            {
                case "true":
                    ContextToExecute = new ConstantWrapperContext(scriptRunner, new VarBoolean(true));
                    return;
                case "false":
                    ContextToExecute = new ConstantWrapperContext(scriptRunner, new VarBoolean(false));
                    return;
                case "null":
                case "undefined":
                    ContextToExecute = new ConstantWrapperContext(scriptRunner, new VarBase());
                    return;
                case Constants.While:
                    ContextToExecute = new WhileContext(scriptRunner);
                    return;
                case Constants.If:
                    ContextToExecute = new IfContext(scriptRunner);
                    return;
                case Constants.Else:
                case Constants.ElseIf:
                    throw new Exception("Elseif and Else can only form part of an If control structure");
            }

            if (scriptRunner.Functions.ContainsKey(symbol))
                ContextToExecute = new FunctionContext(scriptRunner, scriptRunner.Functions[symbol]);
            else
                ContextToExecute = new VariableContext(scriptRunner, symbol);
        }
    }
}