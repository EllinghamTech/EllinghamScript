using System.Collections.Generic;
using EllinghamScript;
using EllinghamScript.ExecutionContexts;
using EllinghamScript.Internal;
using EllinghamScript.Variables;
using NUnit.Framework;

namespace EllinghamScriptTests
{
    [TestFixture]
    public class Expressions
    {
        [Test]
        public void SillyExpressions()
        {
            // Remember, except for brackets there is no order of operations at this time
            Script script = new Script("a = 1+1; b = (10 + a) * (0.5 + (2 - 0.5) * 1); b = (b * 1) + b - (b + 1) + 1;");
            ScriptRunner scriptRunner = new ScriptRunner(script);

            IEnumerable<ExecutionContext> execution = scriptRunner.GetAllExecutionContexts();

            foreach (ExecutionContext context in execution)
            {
                VarBase result = context.Execute();
            }

            TestHelpers.VariableCheck(scriptRunner, "a", 2d, typeof(VarNumber));
            TestHelpers.VariableCheck(scriptRunner, "b", 24d, typeof(VarNumber));
        }
        
        [Test]
        public void SimpleAssign()
        {
            // Remember, except for brackets there is no order of operations at this time
            Script script = new Script("a = 1+1;");
            ScriptRunner scriptRunner = new ScriptRunner(script);

            IEnumerable<ExecutionContext> execution = scriptRunner.GetAllExecutionContexts();

            foreach (ExecutionContext context in execution)
            {
                VarBase result = context.Execute();
            }

            TestHelpers.VariableCheck(scriptRunner, "a", 2d, typeof(VarNumber));
        }
        
        [Test]
        public void ConcatOperatorSingleQuotedString()
        {
            Script script = new Script("a = 'test' + 'me';");
            ScriptRunner scriptRunner = new ScriptRunner(script);

            IEnumerable<ExecutionContext> execution = scriptRunner.GetAllExecutionContexts();

            foreach (ExecutionContext context in execution)
            {
                VarBase result = context.Execute();
            }

            TestHelpers.VariableCheck(scriptRunner, "a", "testme", typeof(VarString));
        }
        
        [Test]
        public void ConcatOperatorSingleQuotedStringInVariables()
        {
            Script script = new Script("a = 'Platform'; b = 'Script'; c = a + b;");
            ScriptRunner scriptRunner = new ScriptRunner(script);

            IEnumerable<ExecutionContext> execution = scriptRunner.GetAllExecutionContexts();

            foreach (ExecutionContext context in execution)
            {
                VarBase result = context.Execute();
            }

            TestHelpers.VariableCheck(scriptRunner, "a", "Platform", typeof(VarString));
            TestHelpers.VariableCheck(scriptRunner, "b", "Script", typeof(VarString));
            TestHelpers.VariableCheck(scriptRunner, "c", "PlatformScript", typeof(VarString));
        }
    }
}