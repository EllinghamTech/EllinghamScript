using System.Collections.Generic;
using EllinghamScript;
using EllinghamScript.ExecutionContexts;
using EllinghamScript.Internal;
using EllinghamScript.Variables;
using NUnit.Framework;

namespace EllinghamScriptTests
{
    [TestFixture]
    public class Methods
    {
        [Test]
        public void AbsOnNumber()
        {
            Script script = new Script("a = 5 * (0 - 1); b = a.Abs();");
            ScriptRunner scriptRunner = new ScriptRunner(script);

            IEnumerable<ExecutionContext> execution = scriptRunner.GetAllExecutionContexts();

            foreach (ExecutionContext context in execution)
            {
                VarBase result = context.Execute();
            }
            
            Assert.IsTrue(scriptRunner.Variables.ContainsKey("a"));
            VarNumber a = (VarNumber)scriptRunner.Variables["a"];
            Assert.AreEqual(-5, a.Value);
            
            Assert.IsTrue(scriptRunner.Variables.ContainsKey("b"));
            VarNumber b = (VarNumber)scriptRunner.Variables["b"];
            Assert.AreEqual(5, b.Value);
        }
    }
}