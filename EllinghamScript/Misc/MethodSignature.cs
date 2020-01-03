using System;
using System.Collections.Generic;
using System.Reflection;
using EllinghamScript.Variables;

namespace EllinghamScript.Misc
{
    public class MethodSignature
    {
        public string Name => MethodInfo.Name;
        public List<(Type, bool)> Types { get; set; } = new List<(Type, bool)>(); // Parameter type, parameter required
        public MethodInfo MethodInfo { get; set; }

        public MethodSignature(MethodInfo methodInfo)
        {
            ParameterInfo[] parameterInfos = methodInfo.GetParameters();
            MethodInfo = methodInfo;

            foreach (ParameterInfo parameterInfo in parameterInfos)
            {
                Types.Add((parameterInfo.ParameterType, !parameterInfo.IsOptional));
            }
        }

        public VarBase Invoke(object obj, object[] arguments)
        {
            return (VarBase)MethodInfo.Invoke(obj, arguments);
        }
    }
}