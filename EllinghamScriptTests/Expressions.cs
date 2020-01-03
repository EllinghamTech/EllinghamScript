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
            
            Assert.IsTrue(scriptRunner.Variables.ContainsKey("a"));
            VarNumber a = (VarNumber)scriptRunner.Variables["a"];
            Assert.AreEqual(2, a.Value);
            
            Assert.IsTrue(scriptRunner.Variables.ContainsKey("b"));
            VarNumber b = (VarNumber)scriptRunner.Variables["b"];
            Assert.AreEqual(24, b.Value);
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
            
            Assert.IsTrue(scriptRunner.Variables.ContainsKey("a"));
            VarNumber a = (VarNumber)scriptRunner.Variables["a"];
            Assert.AreEqual(2, a.Value);
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
            
            Assert.IsTrue(scriptRunner.Variables.ContainsKey("a"));
            VarString a = (VarString)scriptRunner.Variables["a"];
            Assert.AreEqual("testme", a.Value);
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
            
            Assert.IsTrue(scriptRunner.Variables.ContainsKey("a"));
            VarString a = (VarString)scriptRunner.Variables["a"];
            Assert.AreEqual("Platform", a.Value);
            
            Assert.IsTrue(scriptRunner.Variables.ContainsKey("b"));
            VarString b = (VarString)scriptRunner.Variables["b"];
            Assert.AreEqual("Script", b.Value);
            
            Assert.IsTrue(scriptRunner.Variables.ContainsKey("c"));
            VarString c = (VarString)scriptRunner.Variables["c"];
            Assert.AreEqual("PlatformScript", c.Value);
        }
    }
}