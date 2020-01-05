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
        public void SimpleIfElseFalse()
        {
            Script script = new Script("a = b = 0; while(a < 10) { a = a + 1; b = b + 1; }");
            ScriptRunner scriptRunner = new ScriptRunner(script);
            VarBase result = scriptRunner.Execute();
            TestHelpers.VariableCheck(scriptRunner, "a", 10d, typeof(VarNumber));
            TestHelpers.VariableCheck(scriptRunner, "b", 10d, typeof(VarNumber));
        }
    }
}