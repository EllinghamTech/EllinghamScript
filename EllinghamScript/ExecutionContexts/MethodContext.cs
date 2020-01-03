using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using EllinghamScript.Internal;
using EllinghamScript.Misc;
using EllinghamScript.Variables;
using EllinghamScript.Variables.Attributes;

namespace EllinghamScript.ExecutionContexts
{
    public class MethodContext : ExecutionContext
    {
        private string VariableNameName { get; set; }
        private string MethodName { get; set; }
        private VarBase Variable { get; set; }
        private IEnumerable<ExecutionContext> Arguments { get; set; }

        public MethodContext(ScriptRunner scriptRunner, string variableName, string methodName, IEnumerable<ExecutionContext> arguments) : base(scriptRunner)
        {
            VariableNameName = variableName;
            MethodName = methodName;
            Arguments = arguments;
        }

        public MethodContext(ScriptRunner scriptRunner, VarBase variable, string methodName, IEnumerable<ExecutionContext> arguments) : base(scriptRunner)
        {
            Variable = variable;
            MethodName = methodName;
            Arguments = arguments;
        }
        
        public override VarBase Execute()
        {
            VarBase variable = null;
            
            if(Variable == null)
                variable = ScriptRunner.Variables[VariableNameName];

            return Execute(variable ?? Variable);
        }

        public VarBase Execute(VarBase variable)
        {
            List<MethodSignature> methodSignatures = new List<MethodSignature>();
            
            IEnumerable<(MethodInfo, VarMethodAvailableAttribute)> methods = variable.GetMethods(MethodName);
            IEnumerable<(MethodInfo, VarMethodAvailableAttribute)> methodsArray = methods as (MethodInfo, VarMethodAvailableAttribute)[] ?? methods.ToArray();
            
            if(!methodsArray.Any()) throw new Exception("Method on object not found");
            
            // Inspect the method arguments for types
            // Accept VarBase, VarNumeric, VarString, VarObject
            // or their unwrapped types VarNumber => double, VarString => string
            foreach ((MethodInfo methodInfo, _) in
                methodsArray.Where(t => t.Item2.IsAvailable))
            {
                methodSignatures.Add(new MethodSignature(methodInfo));
            }
            
            if(methodSignatures == null)
                throw new Exception("No methods available on variableName");

            List<VarBase> arguments = Arguments.Select(m => m.Execute()).ToList();

            // Now lets go through the signatures
            foreach (MethodSignature signature in methodSignatures)
            {
                // We can only call the method if the number of provided arguments is equal to or greater than
                // the required arguments.
                // We can also only call the method if the method takes at least as many arguments as provided.
                if (signature.Types.Count(m => m.Item2) >= arguments.Count &&
                    signature.Types.Count >= arguments.Count)
                {
                    bool isValid = true;
                        
                    for (int i = 0; i < arguments.Count; i++)
                    {
                        if (!isValid) break;
                        
                        // Compare type
                        if (signature.Types[i].Item1 != arguments[i].GetType())
                            isValid = false;
                    }

                    if (isValid)
                    {
                        return signature.Invoke(variable, arguments.Select(m => m as object).ToArray());
                    }
                }
            }
            
            throw new Exception("Method not found");
        }
    }
}