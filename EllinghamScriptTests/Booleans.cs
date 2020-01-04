using System.Collections.Generic;
using EllinghamScript;
using EllinghamScript.ExecutionContexts;
using EllinghamScript.Internal;
using EllinghamScript.Variables;
using NUnit.Framework;

namespace EllinghamScriptTests
{
    [TestFixture]
    public class Booleans
    {
        [Test]
        public void SimpleAssign()
        {
            // Remember, except for brackets there is no order of operations at this time
            Script script = new Script("a = true; b = false;");
            ScriptRunner scriptRunner = new ScriptRunner(script);
            VarBase result = scriptRunner.Execute();
            
            Assert.IsTrue(scriptRunner.Variables.ContainsKey("a"));
            VarBoolean a = (VarBoolean)scriptRunner.Variables["a"];
            Assert.True(a.Value);
            
            Assert.IsTrue(scriptRunner.Variables.ContainsKey("b"));
            VarBoolean b = (VarBoolean)scriptRunner.Variables["b"];
            Assert.False(b.Value);
            
            TestHelpers.VariableCheck(scriptRunner, "a", true, typeof(VarBoolean));
            TestHelpers.VariableCheck(scriptRunner, "b", false, typeof(VarBoolean));
        }
        
        [Test]
        public void ExpressionAssign()
        {
            // Remember, except for brackets there is no order of operations at this time
            Script script = new Script("a = (1 == 1); b = 1 < 1;");
            ScriptRunner scriptRunner = new ScriptRunner(script);
            VarBase result = scriptRunner.Execute();
            
            Assert.IsTrue(scriptRunner.Variables.ContainsKey("a"));
            VarBoolean a = (VarBoolean)scriptRunner.Variables["a"];
            Assert.True(a.Value);
            
            Assert.IsTrue(scriptRunner.Variables.ContainsKey("b"));
            VarBoolean b = (VarBoolean)scriptRunner.Variables["b"];
            Assert.False(b.Value);
            
            TestHelpers.VariableCheck(scriptRunner, "a", true, typeof(VarBoolean));
            TestHelpers.VariableCheck(scriptRunner, "b", false, typeof(VarBoolean));
        }
        
        [Test]
        public void SimpleAssignAndOr()
        {
            // Remember, except for brackets there is no order of operations at this time
            Script script = new Script("a = false || true; b = true && false;");
            ScriptRunner scriptRunner = new ScriptRunner(script);
            VarBase result = scriptRunner.Execute();
            TestHelpers.VariableCheck(scriptRunner, "a", true, typeof(VarBoolean));
            TestHelpers.VariableCheck(scriptRunner, "b", false, typeof(VarBoolean));
        }
    }
}