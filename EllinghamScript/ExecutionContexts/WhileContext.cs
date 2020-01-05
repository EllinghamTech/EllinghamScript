using EllinghamScript.Internal;
using EllinghamScript.Variables;
using EllinghamScript.Variables.Misc;

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
            ContextEndCharacter = LoopExecution.ContextEndCharacter;
        }

        public override VarBase Execute()
        {
            VarBase result = null;
            
            while (Condition.Execute().ToBoolean())
            {
                result = LoopExecution.Execute();

                switch (result.VariableAction)
                {
                    case VariableAction.Break:
                        result.VariableAction = VariableAction.None;
                        return result;
                    case VariableAction.LoopContinue:
                        result.VariableAction = VariableAction.None;
                        continue;
                    case VariableAction.None:
                        break;
                    default:
                        return result;
                }
            }

            return result;
        }
    }
}