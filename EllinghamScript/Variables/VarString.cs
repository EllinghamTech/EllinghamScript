using System;
using EllinghamScript.Variables.Attributes;

namespace EllinghamScript.Variables
{
    public class VarString : VarBase, IVarTypeWrapper<string>
    {
        public string Value { get; set; }

        public VarString()
        {
            
        }

        public VarString(string value, bool isParsed = true)
        {
            Value = value;
        }

        public override string ToString()
        {
            return Value;
        }

        public string Unwrap()
        {
            return Value;
        }
        
        [VarMethodAvailable(IsAvailable = true)]
        public VarNumber Length()
        {
            return new VarNumber(Value.Length);
        }
        
        [VarMethodAvailable(IsAvailable = true)]
        public VarBoolean IsEmpty()
        {
            return new VarBoolean(string.IsNullOrEmpty(Value));
        }
        
        [VarMethodAvailable(IsAvailable = true)]
        public VarBoolean Contains(VarString subString)
        {
            return new VarBoolean(Value.Contains(subString.Value));
        }
        
        [VarMethodAvailable(IsAvailable = true)]
        public VarString ToUpper()
        {
            return new VarString(Value.ToUpper());
        }
        
        [VarMethodAvailable(IsAvailable = true)]
        public VarString ToLower()
        {
            return new VarString(Value.ToLower());
        }
        
        [VarMethodAvailable(IsAvailable = true)]
        public VarString Trim()
        {
            return new VarString(Value.Trim());
        }
        
        [VarMethodAvailable(IsAvailable = true)]
        public VarString Replace(VarString occurrence, VarString replaceWith)
        {
            return new VarString(Value.Replace(occurrence.Value, replaceWith.Value, StringComparison.InvariantCulture));
        }
    }
}