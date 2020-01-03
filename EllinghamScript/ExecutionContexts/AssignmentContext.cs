using System;
using EllinghamScript.Internal;
using EllinghamScript.Variables;

namespace EllinghamScript.ExecutionContexts
{
    public class AssignmentContext : ExecutionContext
    {
        private readonly string _variableName = null;
        
        public AssignmentContext(ScriptRunner scriptRunner, string symbol) : base(scriptRunner)
        {
            _variableName = symbol;
            
            if (!scriptRunner.Variables.ContainsKey(symbol))
                scriptRunner.Variables.Add(symbol, new VarBase());
            
            ContextToExecute = new ExpressionContext(scriptRunner, false);
        }
        
        public AssignmentContext(ScriptRunner scriptRunner, string symbol, VarBase varBase) : base(scriptRunner)
        {
            _variableName = symbol;
            if(varBase.Constant) throw new Exception("Cannot assign value to a constant");
            ContextToExecute = new ExpressionContext(scriptRunner, false);
        }
        
        public override VarBase Execute()
        {
            VarBase value = ContextToExecute.Execute(); 
            ScriptRunner.Variables[_variableName] = value;
            return value;
        }
    }
}