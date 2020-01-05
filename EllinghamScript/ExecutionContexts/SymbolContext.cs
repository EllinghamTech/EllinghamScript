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

            GetContext();
            ContextEndCharacter = ContextToExecute.ContextEndCharacter;
        }

        public void GetContext()
        {
            switch (Symbol.ToLower())
            {
                case Constants.True:
                    ContextToExecute = new ConstantWrapperContext(ScriptRunner, new VarBoolean(true));
                    return;
                case Constants.False:
                    ContextToExecute = new ConstantWrapperContext(ScriptRunner, new VarBoolean(false));
                    return;
                case Constants.Null:
                case Constants.Undefined:
                    ContextToExecute = new ConstantWrapperContext(ScriptRunner, new VarBase());
                    return;
                case Constants.While:
                    ContextToExecute = new WhileContext(ScriptRunner);
                    return;
                case Constants.If:
                    ContextToExecute = new IfContext(ScriptRunner);
                    return;
                case Constants.Else:
                case Constants.ElseIf:
                    throw new Exception("Elseif and Else can only form part of an If control structure");
            }

            if (ScriptRunner.Functions.ContainsKey(Symbol))
                ContextToExecute = new FunctionContext(ScriptRunner, ScriptRunner.Functions[Symbol]);
            else
                ContextToExecute = new VariableContext(ScriptRunner, Symbol);
        }
    }
}