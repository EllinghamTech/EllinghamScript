using System;
using EllinghamScript.Internal;
using EllinghamScript.Variables;

namespace EllinghamScript.ExecutionContexts
{
    public class AccessorContext : ExecutionContext
    {
        private string VariableName { get; set; }
        private string AccessorSymbolName { get; set; }
        
        public AccessorContext(ScriptRunner scriptRunner, string variable) : base(scriptRunner)
        {
            string symbol = scriptRunner.CollectSymbol();
            
            if(string.IsNullOrEmpty(symbol))
                throw new Exception("Accessor invalid");

            VariableName = variable;
            AccessorSymbolName = symbol;
            
            // Determine if method by next symbol ( or )
            if(scriptRunner.CurChar == '(')
                ContextToExecute = new MethodContext(ScriptRunner, VariableName, AccessorSymbolName, scriptRunner.CollectArguments());
            else
                ContextToExecute = null; //new PropertyContext(ScriptRunner, variable, AccessorSymbolName);
        }

        public override VarBase Execute()
        {
            return ContextToExecute?.Execute();
        }
    }
}