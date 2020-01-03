using EllinghamScript.Internal;
using EllinghamScript.Variables;

namespace EllinghamScript.ExecutionContexts
{
    public class OperationContext : ExecutionContext
    {
        public ExecutionContext First { get; set; }
        public ExecutionContext Second { get; set; }
        public string OperationString { get; set; }
        public Operations Operations { get; set; }
        
        public OperationContext(ScriptRunner scriptRunner, ExecutionContext first, ExecutionContext second, string op) : base(scriptRunner)
        {
            First = first;
            Second = second;
            OperationString = op;
            Operations = scriptRunner.Operations;
        }

        public override VarBase Execute()
        {
            if (ContextToExecute == null)
            {
                VarBase first = First.Execute();
                VarBase second = Second.Execute();
                    
                return Operations.FindBest(first.GetType(), second.GetType(), OperationString).Compile()(first, second);
            }

            return ContextToExecute.Execute();
        }
    }
}