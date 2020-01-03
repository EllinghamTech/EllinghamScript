using System.Collections.Generic;

namespace EllinghamScript.Variables
{
    public class VarObject : VarBase
    {
        public Dictionary<string, VarBase> Properties = new Dictionary<string, VarBase>();
        public bool HasProperties { get; set; } = true;

        public override string ToString()
        {
            return "{Object}";
        }

        public override bool ToBoolean()
        {
            return Properties.Count != 0;
        }
    }
}