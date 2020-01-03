using System;
using EllinghamScript.Variables;

namespace EllinghamScript.VariableOperations
{
    public class BooleanOperations
    {
        public static VarBoolean CompareEquals(VarBase v1, VarBase v2)
        {
            return new VarBoolean(v1.ToBoolean() == v2.ToBoolean());
        }
        
        public static VarBoolean CompareNotEquals(VarBase v1, VarBase v2)
        {
            return new VarBoolean(v1.ToBoolean() != v2.ToBoolean());
        }
        
        public static VarBoolean CompareAnd(VarBase v1, VarBase v2)
        {
            return new VarBoolean(v1.ToBoolean() && v2.ToBoolean());
        }
        
        public static VarBoolean CompareOr(VarBase v1, VarBase v2)
        {
            return new VarBoolean(v1.ToBoolean() || v2.ToBoolean());
        }
        
        public static void Register(Operations operations)
        {
            Type t1 = typeof(VarBoolean);
            Type t2 = typeof(VarBoolean);
            
            operations.Register(t1, t2, "==", (v1, v2) => CompareEquals(v1, v2));
            operations.Register(t1, t2, "!=", (v1, v2) => CompareNotEquals(v1, v2));
            operations.Register(t1, t2, "&&", (v1, v2) => CompareAnd(v1, v2));
            operations.Register(t1, t2, "||", (v1, v2) => CompareOr(v1, v2));
        }
    }
}