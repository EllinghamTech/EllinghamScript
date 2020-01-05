using System;
using EllinghamScript;
using EllinghamScript.Internal;
using NUnit.Framework;

namespace EllinghamScriptTests
{
    [TestFixture]
    public class InvalidCode
    {
        [Test]
        public void InvalidCode_1()
        {
            // 1(5) is not valid
            string code = "a = 1 + 1(5);";
            Script script = new Script(code);
            Assert.Throws<Exception>(() =>
            {
                ScriptRunner scriptRunner = new ScriptRunner(script);
                scriptRunner.Execute();
            });
        }
        
        [Test]
        public void InvalidCode_2()
        {
            // No final semi-colon
            string code = "a = 1 + 1; if(a == 1) a = 3";
            Script script = new Script(code);
            Assert.Throws<Exception>(() =>
            {
                ScriptRunner scriptRunner = new ScriptRunner(script);
                scriptRunner.Execute();
            });
        }
        
        [Test]
        public void InvalidCode_3()
        {
            // b + 1, b does not exist so it is expecting an assignment
            string code = "a = 1 + 1; if(a == 1) a = 3; else { b + 1; }";
            Script script = new Script(code);
            Assert.Throws<Exception>(() =>
            {
                ScriptRunner scriptRunner = new ScriptRunner(script);
                scriptRunner.Execute();
            });
        }
        
        [Test]
        public void InvalidCode_4()
        {
            // a = 1, a does not have a semi colon on the end so is invalid
            string code = "a = 1 + 1; if(a == 1) { a = 3; } else { a = 1 }";
            Script script = new Script(code);
            Assert.Throws<Exception>(() =>
            {
                ScriptRunner scriptRunner = new ScriptRunner(script);
                scriptRunner.Execute();
            });
        }
    }
}