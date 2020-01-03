using System;
using EllinghamScript.Internal;
using EllinghamScript.Variables;

namespace EllinghamScript.ExecutionContexts
{
    public class VariableContext : ExecutionContext
    {
        private readonly VarBase _var = null;

        public VariableContext(ScriptRunner scriptRunner, string variableName) : base(scriptRunner)
        {
            if (scriptRunner.Variables.ContainsKey(variableName))
                _var = scriptRunner.Variables[variableName];

            // Collect only . and =, try collect operators will only move the pointer
            // if the operator listed is found.
            string op = scriptRunner.TryCollectOperators(new []{".", "="});

            // If is has no operator and exists, just return it
            if (string.IsNullOrEmpty(op))
            {
                if (_var != null)
                {
                    ContextToExecute = new VariableReturnContext(scriptRunner, variableName);
                    return;
                }
                
                throw new Exception("Undefined Variable, expecting operator");
            }
            
            if (op == "=")
            {
                if(_var == null)
                    ContextToExecute = new AssignmentContext(scriptRunner, variableName);
                else
                    ContextToExecute = new AssignmentContext(scriptRunner, variableName, _var);

                return;
            }
            
            if(_var == null)
                throw new Exception("Undefined Variable, invalid operator"); 
            
            // If it is the accessor, we need to move into the accessor context
            if (op == ".")
            {
                ContextToExecute = new AccessorContext(scriptRunner, variableName);
                return;
            }

            ContextToExecute = new VariableReturnContext(scriptRunner, variableName);
        }
    }
}