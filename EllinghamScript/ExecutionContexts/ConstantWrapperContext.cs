using EllinghamScript.Internal;
using EllinghamScript.Variables;

namespace EllinghamScript.ExecutionContexts
{
    public class ConstantWrapperContext : ExecutionContext
    {
        public VarBase Constant { get; set; }
        public ConstantWrapperContext(ScriptRunner scriptRunner, VarBase constant) : base(scriptRunner)
        {
            Constant = constant;
        }

        public override VarBase Execute()
        {
            return Constant;
        }
    }
}