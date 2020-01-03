using System.Collections.Generic;
using EllinghamScript;
using EllinghamScript.ExecutionContexts;
using EllinghamScript.Internal;
using EllinghamScript.Variables;
using NUnit.Framework;

namespace EllinghamScriptTests
{
    [TestFixture]
    public class While
    {
        [Test]
        public void SimpleIfElseFalse()
        {
            Script script = new Script("a = b = 0; while(a < 10) { a = a + 1; b = b + 1; }");
            ScriptRunner scriptRunner = new ScriptRunner(script);

            IEnumerable<ExecutionContext> execution = scriptRunner.GetAllExecutionContexts();

            foreach (ExecutionContext context in execution)
            {
                VarBase result = context.Execute();
            }
            
            Assert.IsTrue(scriptRunner.Variables.ContainsKey("a"));
            VarNumber a = (VarNumber)scriptRunner.Variables["a"];
            Assert.AreEqual(10, a.Value);
            
            Assert.IsTrue(scriptRunner.Variables.ContainsKey("b"));
            VarNumber b = (VarNumber)scriptRunner.Variables["b"];
            Assert.AreEqual(10, b.Value);
        }
    }
}