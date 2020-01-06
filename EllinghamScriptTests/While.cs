using EllinghamScript;
using EllinghamScript.Internal;
using EllinghamScript.Variables;
using NUnit.Framework;

namespace EllinghamScriptTests
{
    [TestFixture]
    public class While
    {
        [Test]
        public void SimpleWhileLoop()
        {
            Script script = new Script("a = b = 0; while(a < 10) { a = a + 1; b = b + 1; }");
            ScriptRunner scriptRunner = new ScriptRunner(script);
            VarBase result = scriptRunner.Execute();
            TestHelpers.VariableCheck(scriptRunner, "a", 10d, typeof(VarNumber));
            TestHelpers.VariableCheck(scriptRunner, "b", 10d, typeof(VarNumber));
        }
        
        [Test]
        public void SimpleWhileLoopWithContinue()
        {
            // b = b + 1 should never be executed because of the continue
            Script script = new Script("a = b = 0; while(a < 10) { a = a + 1; continue; b = b + 1; }");
            ScriptRunner scriptRunner = new ScriptRunner(script);
            VarBase result = scriptRunner.Execute();
            TestHelpers.VariableCheck(scriptRunner, "a", 10d, typeof(VarNumber));
            TestHelpers.VariableCheck(scriptRunner, "b", 0d, typeof(VarNumber));
        }
        
        [Test]
        public void SimpleWhileLoopWithIfContinue()
        {
            // b = b + 1 should never be executed because of the continue
            Script script = new Script("a = b = 0; while(a < 10) { a = a + 1; if(a > 2) continue; b = b + 1; }");
            ScriptRunner scriptRunner = new ScriptRunner(script);
            VarBase result = scriptRunner.Execute();
            TestHelpers.VariableCheck(scriptRunner, "a", 10d, typeof(VarNumber));
            TestHelpers.VariableCheck(scriptRunner, "b", 2d, typeof(VarNumber));
        }
        
        [Test]
        public void SimpleWhileLoopWithBreak()
        {
            // b = b + 1 should never be executed because of the break
            // and a = a + 1 only once
            Script script = new Script("a = b = 0; while(a < 10) { a = a + 1; break; b = b + 1; }");
            ScriptRunner scriptRunner = new ScriptRunner(script);
            VarBase result = scriptRunner.Execute();
            TestHelpers.VariableCheck(scriptRunner, "a", 1d, typeof(VarNumber));
            TestHelpers.VariableCheck(scriptRunner, "b", 0d, typeof(VarNumber));
        }
        
        [Test]
        public void SimpleWhileLoopWithIfBreak()
        {
            // b = b + 1 should never be executed because of the continue
            Script script = new Script("a = b = 0; while(a < 10) { a = a + 1; if(a > 2) break; b = b + 1; }");
            ScriptRunner scriptRunner = new ScriptRunner(script);
            VarBase result = scriptRunner.Execute();
            TestHelpers.VariableCheck(scriptRunner, "a", 3d, typeof(VarNumber));
            TestHelpers.VariableCheck(scriptRunner, "b", 2d, typeof(VarNumber));
        }
    }
}