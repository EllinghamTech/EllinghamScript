using System.Collections.Generic;
using EllinghamScript.Internal;
using EllinghamScript.Variables;

namespace EllinghamScript.ExecutionContexts
{
    public class IfContext : ExecutionContext
    {
        public Dictionary<ExecutionContext, ExecutionContext> IfElseList { get; set; } = new Dictionary<ExecutionContext, ExecutionContext>();
        public IfContext(ScriptRunner scriptRunner) : base(scriptRunner)
        {
            // If, elseif, ..., elseif, else
            
            // We are here from if, the only valid entry point.
            ExecutionContext condition = new ExpressionContext(scriptRunner);
            ExecutionContext conditionalExecution = scriptRunner.CollectContext();
            
            IfElseList.Add(condition, conditionalExecution);

            // Now we check for elseif(s) and else...
            // The context after the expression is either a single statement or block
            while (true)
            {
                string symbol = scriptRunner.TryCollectSymbol(new[] {Constants.ElseIf, Constants.Else});

                switch (symbol)
                {
                    case Constants.ElseIf:
                        condition = new ExpressionContext(scriptRunner);
                        conditionalExecution = scriptRunner.CollectContext();
                        IfElseList.Add(condition, conditionalExecution);
                        continue;
                    case Constants.Else:
                        condition = new AlwaysTrueContext(scriptRunner);
                        conditionalExecution = scriptRunner.CollectContext();
                        IfElseList.Add(condition, conditionalExecution);
                        return;
                    default:
                        return;
                }
            }
        }

        public override VarBase Execute()
        {
            foreach ((ExecutionContext condition, ExecutionContext context) in IfElseList)
            {
                if (condition.Execute().ToBoolean())
                    return context.Execute();
            }
            
            return new VarBase();
        }
    }
}