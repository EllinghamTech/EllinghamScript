using EllinghamScript;
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
            VarBase result = scriptRunner.Execute();
            TestHelpers.VariableCheck(scriptRunner, "a", -5d, typeof(VarNumber));
            TestHelpers.VariableCheck(scriptRunner, "b", 5d, typeof(VarNumber));
        }
    }
}