using EllinghamScript.Internal;
using EllinghamScript.Variables;

namespace EllinghamScript.ExecutionContexts
{
    public class WhileContext : ExecutionContext
    {
        public ExpressionContext Condition { get; set; }
        public ExecutionContext LoopExecution { get; set; }
        
        public WhileContext(ScriptRunner scriptRunner) : base(scriptRunner)
        {
            Condition = new ExpressionContext(scriptRunner);
            LoopExecution = scriptRunner.CollectContext();
        }

        public override VarBase Execute()
        {
            VarBase result = null;
            
            while (Condition.Execute().ToBoolean())
            {
                result = LoopExecution.Execute();
            }

            return result;
        }
    }
}