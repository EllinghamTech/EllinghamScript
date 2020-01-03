using System.Collections.Generic;
using EllinghamScript.Internal;
using EllinghamScript.Variables;

namespace EllinghamScript.ExecutionContexts
{
    /// <summary>
    /// Blocks are run almost in a separate scope, though not really as
    /// it shares the same ScriptRunner and therefore function and variable
    /// sets.  Blocks however can be wrapped and therefore controlled just
    /// like any other context.
    ///
    /// A block only really makes sense after a control-flow statement but
    /// there is no harm in having random blocks except possibly lower
    /// performance. 
    /// </summary>
    public class BlockContext : ExecutionContext
    {
        public IEnumerable<ExecutionContext> ExecutionContexts { get; set; } = null;
        
        public BlockContext(ScriptRunner scriptRunner) : base(scriptRunner)
        {
            ExecutionContexts = scriptRunner.GetAllExecutionContexts(false, new[] {Constants.BracesClose});
        }

        public override VarBase Execute()
        {
            if(ExecutionContexts == null)
                return new VarBase();
            
            VarBase returnVar = new VarBase();

            foreach (ExecutionContext context in ExecutionContexts)
            {
                returnVar = context.Execute();
            }

            return returnVar;
        }
    }
}