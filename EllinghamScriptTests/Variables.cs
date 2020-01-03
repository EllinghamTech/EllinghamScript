using System.Collections.Generic;
using EllinghamScript;
using EllinghamScript.ExecutionContexts;
using EllinghamScript.Internal;
using EllinghamScript.Variables;
using NUnit.Framework;

namespace EllinghamScriptTests
{
    [TestFixture]
    public class Variables
    {
        [Test]
        public void AssignSingleQuotedString()
        {
            Script script = new Script("a = 'single quoted string';");
            ScriptRunner scriptRunner = new ScriptRunner(script);

            IEnumerable<ExecutionContext> execution = scriptRunner.GetAllExecutionContexts();

            foreach (ExecutionContext context in execution)
            {
                VarBase result = context.Execute();
            }
            
            Assert.IsTrue(scriptRunner.Variables.ContainsKey("a"));
            VarString a = (VarString)scriptRunner.Variables["a"];
            Assert.AreEqual("single quoted string", a.Value);
        }
        
        [Test]
        public void AssignDoubleQuotedString()
        {
            Script script = new Script(@"a = ""double quoted string"";");
            ScriptRunner scriptRunner = new ScriptRunner(script);

            IEnumerable<ExecutionContext> execution = scriptRunner.GetAllExecutionContexts();

            foreach (ExecutionContext context in execution)
            {
                VarBase result = context.Execute();
            }
            
            Assert.IsTrue(scriptRunner.Variables.ContainsKey("a"));
            VarString a = (VarString)scriptRunner.Variables["a"];
            Assert.AreEqual("double quoted string", a.Value);
        }
    }
}