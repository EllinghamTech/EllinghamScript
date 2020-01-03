using EllinghamScript.Internal;
using EllinghamScript.Variables;

namespace EllinghamScript.ExecutionContexts
{
    public class AlwaysTrueContext : ExecutionContext
    {
        public AlwaysTrueContext(ScriptRunner scriptRunner) : base(scriptRunner)
        {
        }

        public override VarBase Execute()
        {
            return new VarBoolean(true);
        }
    }
}