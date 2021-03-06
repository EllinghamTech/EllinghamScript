using EllinghamScript;
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
            VarBase result = scriptRunner.Execute();
            TestHelpers.VariableCheck(scriptRunner, "a", 10d, typeof(VarNumber));
        }
        
        [Test]
        public void SimpleIfElseFalse()
        {
            Script script = new Script("if(false) { a = 10; } else { a = 5; }");
            ScriptRunner scriptRunner = new ScriptRunner(script);
            VarBase result = scriptRunner.Execute();
            TestHelpers.VariableCheck(scriptRunner, "a", 5d, typeof(VarNumber));
        }
    }
}