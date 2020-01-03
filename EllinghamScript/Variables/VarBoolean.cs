namespace EllinghamScript.Variables
{
    public class VarBoolean : VarBase, IVarTypeWrapper<bool>
    {
        public bool Value { get; set; }

        public VarBoolean(bool value = false)
        {
            Value = value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
        
        public override bool ToBoolean()
        {
            return Value;
        }

        public bool Unwrap()
        {
            return Value;
        }
    }
}