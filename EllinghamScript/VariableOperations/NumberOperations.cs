using System;
using EllinghamScript.Variables;

namespace EllinghamScript.VariableOperations
{
    public static class NumberOperations
    {
        public static VarNumber Add(VarBase v1, VarBase v2)
        {
            (VarNumber r1, VarNumber r2) = CheckTypes(v1, v2);
            
            return new VarNumber(r1.Value + r2.Value);
        }

        public static VarNumber Subtract(VarBase v1, VarBase v2)
        {
            (VarNumber r1, VarNumber r2) = CheckTypes(v1, v2);
            
            return new VarNumber(r1.Value - r2.Value);
        }

        public static VarNumber Multiply(VarBase v1, VarBase v2)
        {
            (VarNumber r1, VarNumber r2) = CheckTypes(v1, v2);
            
            return new VarNumber(r1.Value * r2.Value);
        }

        public static VarNumber Divide(VarBase v1, VarBase v2)
        {
            (VarNumber r1, VarNumber r2) = CheckTypes(v1, v2);
            
            return new VarNumber(r1.Value / r2.Value);
        }

        public static VarNumber Modulo(VarBase v1, VarBase v2)
        {
            (VarNumber r1, VarNumber r2) = CheckTypes(v1, v2);
            
            return new VarNumber(r1.Value % r2.Value);
        }

        public static VarBoolean CompareEquals(VarBase v1, VarBase v2)
        {
            (VarNumber r1, VarNumber r2) = CheckTypes(v1, v2);
            
            return new VarBoolean(r1.Value == r2.Value);
        }
        
        public static VarBoolean CompareNotEquals(VarBase v1, VarBase v2)
        {
            (VarNumber r1, VarNumber r2) = CheckTypes(v1, v2);
            
            return new VarBoolean(r1.Value != r2.Value);
        }
        
        public static VarBoolean CompareGreaterThan(VarBase v1, VarBase v2)
        {
            (VarNumber r1, VarNumber r2) = CheckTypes(v1, v2);
            
            return new VarBoolean(r1.Value > r2.Value);
        }
        
        public static VarBoolean CompareGreaterThanEquals(VarBase v1, VarBase v2)
        {
            (VarNumber r1, VarNumber r2) = CheckTypes(v1, v2);
            
            return new VarBoolean(r1.Value >= r2.Value);
        }
        
        public static VarBoolean CompareLessThan(VarBase v1, VarBase v2)
        {
            (VarNumber r1, VarNumber r2) = CheckTypes(v1, v2);
            
            return new VarBoolean(r1.Value < r2.Value);
        }
        
        public static VarBoolean CompareLessThanEquals(VarBase v1, VarBase v2)
        {
            (VarNumber r1, VarNumber r2) = CheckTypes(v1, v2);
            
            return new VarBoolean(r1.Value <= r2.Value);
        }

        public static void Register(Operations operations)
        {
            Type t1 = typeof(VarNumber);
            Type t2 = typeof(VarNumber);
            
            operations.Register(t1, t2, "+", (v1, v2) => Add(v1, v2));
            operations.Register(t1, t2, "-", (v1, v2) => Subtract(v1, v2));
            operations.Register(t1, t2, "/", (v1, v2) => Divide(v1, v2));
            operations.Register(t1, t2, "*", (v1, v2) => Multiply(v1, v2));
            operations.Register(t1, t2, "%", (v1, v2) => Modulo(v1, v2));
            
            operations.Register(t1, t2, "==", (v1, v2) => CompareEquals(v1, v2));
            operations.Register(t1, t2, "!=", (v1, v2) => CompareNotEquals(v1, v2));
            operations.Register(t1, t2, ">", (v1, v2) => CompareGreaterThan(v1, v2));
            operations.Register(t1, t2, ">=", (v1, v2) => CompareGreaterThanEquals(v1, v2));
            operations.Register(t1, t2, "<", (v1, v2) => CompareLessThan(v1, v2));
            operations.Register(t1, t2, "<=", (v1, v2) => CompareLessThanEquals(v1, v2));
        }
        
        private static (VarNumber, VarNumber) CheckTypes(VarBase v1, VarBase v2)
        {
            if(!(v1 is VarNumber r1))
                throw new Exception("Unexpected Type");
            
            if(!(v2 is VarNumber r2))
                throw new Exception("Unexpected Type");

            return (r1, r2);
        }
    }
}