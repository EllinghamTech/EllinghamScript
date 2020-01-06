using EllinghamScript.Internal;
using EllinghamScript.Variables;
using EllinghamScript.Variables.Misc;

namespace EllinghamScript.ExecutionContexts
{
    public class BreakContext : ExecutionContext
    {
        public BreakContext(ScriptRunner scriptRunner) : base(scriptRunner)
        {
        }

        public override VarBase Execute()
        {
            return new VarBase {VariableAction = VariableAction.Break};
        }
    }
}