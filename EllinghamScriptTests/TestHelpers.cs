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
            
            Assert.NotNull(variable);

            Assert.AreEqual(variableType, variable.GetType());

            switch (variable)
            {
                case VarString varString:
                    Assert.AreEqual((string)value, varString.Value);
                    break;
                case VarBoolean varBoolean:
                    Assert.AreEqual((bool)value, varBoolean.Value);
                    break;
                case VarNumber varNumber:
                    Assert.AreEqual((double)value, varNumber.Value);
                    break;
            }
        }
    }
}