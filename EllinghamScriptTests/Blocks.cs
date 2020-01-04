using System.Collections.Generic;
using EllinghamScript;
using EllinghamScript.ExecutionContexts;
using EllinghamScript.Internal;
using EllinghamScript.Variables;
using NUnit.Framework;

namespace EllinghamScriptTests
{
    [TestFixture]
    public class Blocks
    {
        [Test]
        public void RandomBlock()
        {
            Script script = new Script("a = 1+1; b = 1; { a = a + 1; } { b = b + 1; }");
            ScriptRunner scriptRunner = new ScriptRunner(script);
            VarBase result = scriptRunner.Execute();
            TestHelpers.VariableCheck(scriptRunner, "a", 3d, typeof(VarNumber));
            TestHelpers.VariableCheck(scriptRunner, "b", 2d, typeof(VarNumber));
        }
    }
}