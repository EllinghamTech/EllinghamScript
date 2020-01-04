using System.Collections.Generic;
using EllinghamScript;
using EllinghamScript.ExecutionContexts;
using EllinghamScript.Internal;
using EllinghamScript.Variables;
using NUnit.Framework;

namespace EllinghamScriptTests
{
    [TestFixture]
    public class StringMethods
    {
        [Test]
        public void ToLower()
        {
            Script script = new Script("a = 'LowerCase'; a = a.ToLower();");
            ScriptRunner scriptRunner = new ScriptRunner(script);
            VarBase result = scriptRunner.Execute();
            TestHelpers.VariableCheck(scriptRunner, "a", "lowercase", typeof(VarString));
        }
        
        [Test]
        public void ToUpper()
        {
            Script script = new Script("a = 'upperCase'; a = a.ToUpper();");
            ScriptRunner scriptRunner = new ScriptRunner(script);
            VarBase result = scriptRunner.Execute();
            TestHelpers.VariableCheck(scriptRunner, "a", "UPPERCASE", typeof(VarString));
        }
        
        [Test]
        public void Trim()
        {
            Script script = new Script("a = '  \n my string needs trimming   '; b = a.Trim();");
            ScriptRunner scriptRunner = new ScriptRunner(script);
            VarBase result = scriptRunner.Execute();
            TestHelpers.VariableCheck(scriptRunner, "a", "  \n my string needs trimming   ", typeof(VarString));
            TestHelpers.VariableCheck(scriptRunner, "b", "my string needs trimming", typeof(VarString));
        }
        
        [Test]
        public void Replace()
        {
            Script script = new Script("a = 'Ellingham Innovations'; b = a.Replace('Innovations', 'Dev');");
            ScriptRunner scriptRunner = new ScriptRunner(script);
            VarBase result = scriptRunner.Execute();
            TestHelpers.VariableCheck(scriptRunner, "a", "Ellingham Innovations", typeof(VarString));
            TestHelpers.VariableCheck(scriptRunner, "b", "Ellingham Dev", typeof(VarString));
        }
        
        [Test]
        public void Contains()
        {
            Script script = new Script("a = 'Ellingham Innovations'; b = a.Contains('Innovations');");
            ScriptRunner scriptRunner = new ScriptRunner(script);
            VarBase result = scriptRunner.Execute();
            TestHelpers.VariableCheck(scriptRunner, "a", "Ellingham Innovations", typeof(VarString));
            TestHelpers.VariableCheck(scriptRunner, "b", true, typeof(VarBoolean));
        }
    }
}