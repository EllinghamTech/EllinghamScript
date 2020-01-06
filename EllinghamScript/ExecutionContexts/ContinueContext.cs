using EllinghamScript.Internal;
using EllinghamScript.Variables;
using EllinghamScript.Variables.Misc;

namespace EllinghamScript.ExecutionContexts
{
    public class ContinueContext : ExecutionContext
    {
        public ContinueContext(ScriptRunner scriptRunner) : base(scriptRunner)
        {
        }

        public override VarBase Execute()
        {
            return new VarBase {VariableAction = VariableAction.LoopContinue};
        }
    }
}