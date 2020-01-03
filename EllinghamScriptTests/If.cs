using System.Collections.Generic;
using EllinghamScript;
using EllinghamScript.ExecutionContexts;
using EllinghamScript.Internal;
using EllinghamScript.Variables;
using NUnit.Framework;

namespace EllinghamScriptTests
{
    [TestFixture]
    public class If
    {
        [Test]
        public void SimpleIfElseTrue()
        {
            Script script = new Script("if(true) { a = 10; } else { a = 5; }");
            ScriptRunner scriptRunner = new ScriptRunner(script);

            IEnumerable<ExecutionContext> execution = scriptRunner.GetAllExecutionContexts();

            foreach (ExecutionContext context in execution)
            {
                VarBase result = context.Execute();
            }
            
            TestHelpers.VariableCheck(scriptRunner, "a", 10d, typeof(VarNumber));
        }
        
        [Test]
        public void SimpleIfElseFalse()
        {
            Script script = new Script("if(false) { a = 10; } else { a = 5; }");
            ScriptRunner scriptRunner = new ScriptRunner(script);

            IEnumerable<ExecutionContext> execution = scriptRunner.GetAllExecutionContexts();

            foreach (ExecutionContext context in execution)
            {
                VarBase result = context.Execute();
            }
            
            TestHelpers.VariableCheck(scriptRunner, "a", 5d, typeof(VarNumber));
        }
    }
}