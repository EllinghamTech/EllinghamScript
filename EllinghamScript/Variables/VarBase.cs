using System.Collections.Generic;
using System.Reflection;
using EllinghamScript.Variables.Attributes;
using EllinghamScript.Variables.Misc;

namespace EllinghamScript.Variables
{
    public class VarBase
    {
        public bool Constant { get; set; } = false;
        public VariableAction VariableAction { get; set; } = VariableAction.None;

        public VarBase()
        {
        }
        
        [VarMethodAvailable(IsAvailable = true)]
        public override string ToString()
        {
            return null;
        }

        [VarMethodAvailable(IsAvailable = true)]
        public virtual bool ToBoolean()
        {
            return false;
        }

        public (MethodInfo, VarMethodAvailableAttribute) GetMethod(string methodName)
        {
            MethodInfo method = this.GetType().GetMethod(methodName);

            if (method == null)
                return (null, null);

            object[] availableAttributes = method.GetCustomAttributes(typeof(VarMethodAvailableAttribute), true);

            if (availableAttributes.Length == 0)
                return (method, null);
            
            return (method, (VarMethodAvailableAttribute)availableAttributes[0]);
        }
        
        public IEnumerable<(MethodInfo, VarMethodAvailableAttribute)> GetMethods(string methodName)
        {
            MethodInfo[] methods = this.GetType().GetMethods();

            List<(MethodInfo, VarMethodAvailableAttribute)> list = new List<(MethodInfo, VarMethodAvailableAttribute)>();

            foreach (MethodInfo method in methods)
            {
                if (method.Name != methodName) continue;
                
                object[] availableAttributes = method.GetCustomAttributes(typeof(VarMethodAvailableAttribute), true);
                VarMethodAvailableAttribute methodAvailableAttribute = null;

                if (availableAttributes.Length > 0)
                    methodAvailableAttribute = (VarMethodAvailableAttribute) availableAttributes[0];
                    
                list.Add((method, methodAvailableAttribute));
            }

            return list;
        }

        public bool HasMethod(string methodName)
        {
            (_, VarMethodAvailableAttribute varMethodAvailableAttribute) = GetMethod(methodName);

            return varMethodAvailableAttribute != null && varMethodAvailableAttribute.IsAvailable;
        }
    }
}