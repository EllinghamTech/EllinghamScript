using System;
using EllinghamScript.Internal;
using EllinghamScript.Variables;
using NUnit.Framework;

namespace EllinghamScriptTests
{
    public static class TestHelpers
    {
        public static void VariableCheck(ScriptRunner scriptRunner, string variableName, object value, Type variableType)
        {
            Assert.IsTrue(scriptRunner.Variables.ContainsKey(variableName));

            VarBase variable = scriptRunner.Variables[variableName];
            
            Assert.AreEqual(variableType, variable.GetType());

            if (variable is VarString varString)
                Assert.AreEqual((string)value, varString.Value);
            
            if (variable is VarBoolean varBoolean)
                Assert.AreEqual((bool)value, varBoolean.Value);
            
            if(variable is VarNumber varNumber)
                Assert.AreEqual((double)value, varNumber.Value);
        }
    }
}