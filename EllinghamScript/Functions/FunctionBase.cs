using EllinghamScript.ExecutionContexts;
using EllinghamScript.Internal;
using EllinghamScript.Variables;

namespace EllinghamScript.Functions
{
    public class FunctionBase : ExecutionContext
    {
        public virtual VarBase ExecuteInternal()
        {
            return new VarBase();
        }

        public FunctionBase(ScriptRunner scriptRunner) : base(scriptRunner)
        {
        }
    }
}