using System;
using System.Globalization;
using EllinghamScript.Variables.Attributes;

namespace EllinghamScript.Variables
{
    public class VarNumber : VarBase, IVarTypeWrapper<double>
    {
        public double Value { get; set; }

        public VarNumber(double value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return Value.ToString(CultureInfo.InvariantCulture);
        }

        public override bool ToBoolean()
        {
            return Value != 0;
        }

        public double Unwrap()
        {
            return Value;
        }

        [VarMethodAvailable(IsAvailable = true)]
        public VarNumber Abs()
        {
            return new VarNumber(Math.Abs(Value));
        }
        
        [VarMethodAvailable(IsAvailable = true)]
        public VarBoolean IsPositive()
        {
            return new VarBoolean(Value > 0);
        }
        
        [VarMethodAvailable(IsAvailable = true)]
        public VarBoolean IsNegative()
        {
            return new VarBoolean(Value < 0);
        }
        
        [VarMethodAvailable(IsAvailable = true)]
        public VarBoolean IsBetween(VarNumber smallest, VarNumber largest)
        {
            return new VarBoolean(Value >= smallest.Value && Value <= largest.Value);
        }
    }
}