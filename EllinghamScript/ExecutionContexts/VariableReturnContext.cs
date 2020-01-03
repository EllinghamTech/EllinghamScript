using EllinghamScript.Internal;
using EllinghamScript.Variables;

namespace EllinghamScript.ExecutionContexts
{
    public class VariableReturnContext : ExecutionContext
    {
        private readonly string _variableName;
        
        public VariableReturnContext(ScriptRunner scriptRunner, string variableName) : base(scriptRunner)
        {
            _variableName = variableName;
        }

        public override VarBase Execute()
        {
            return ScriptRunner.Variables[_variableName];
        }
    }
}