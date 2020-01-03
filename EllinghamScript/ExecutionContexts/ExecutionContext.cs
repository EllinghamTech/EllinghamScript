using EllinghamScript.Internal;
using EllinghamScript.Variables;

namespace EllinghamScript.ExecutionContexts
{
    public class ExecutionContext
    {
        protected ScriptRunner ScriptRunner { get; set; } 
        protected ExecutionContext ContextToExecute { get; set; }

        public ExecutionContext(ScriptRunner scriptRunner)
        {
            ScriptRunner = scriptRunner;
        }

        public virtual VarBase Execute()
        {
            return ContextToExecute != null ? ContextToExecute.Execute() : new VarBase();
        }
    }
}