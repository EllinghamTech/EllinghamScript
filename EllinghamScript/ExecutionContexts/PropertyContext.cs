using System;
using EllinghamScript.Internal;
using EllinghamScript.Variables;

namespace EllinghamScript.ExecutionContexts
{
    public class PropertyContext : ExecutionContext
    {
        public string VariableName { get; set; }
        public string Property { get; set; }

        public PropertyContext(ScriptRunner scriptRunner, string variableName, string property) : base(scriptRunner)
        {
            VariableName = variableName;
            Property = property;
            
            throw new NotImplementedException("Not yet implemented...  Sorry...");
        }

        public override VarBase Execute()
        {
            return base.Execute();
        }
    }
}