using EllinghamScript.Variables;

namespace EllinghamScript.VariableOperations
{
    public static class StringOperations
    {
        /// <summary>
        /// All variables have a string representation, so it is safe to use
        /// VarBase without verifying types.  This allows use to be as generic
        /// as possible, such as 1 + "test" = "1test", etc.
        /// </summary>
        /// <param name="vs1"></param>
        /// <param name="vs2"></param>
        /// <returns></returns>
        public static VarString Add(VarBase vs1, VarBase vs2)
        {
            return new VarString {Value = string.Concat(vs1, vs2)};
        }

        public static VarBoolean CompareEquals(VarBase vs1, VarBase vs2)
        {
            return new VarBoolean(vs1.ToString().Equals(vs2.ToString()));
        }

        public static VarBoolean CompareNotEquals(VarBase vs1, VarBase vs2)
        {
            return new VarBoolean(!vs1.ToString().Equals(vs2.ToString()));
        }

        public static void Register(Operations operations)
        {
            operations.Register(typeof(VarString), typeof(VarString), "+", (v1, v2) => Add(v1, v2));
            operations.Register(typeof(VarString), typeof(VarString), "==", (v1, v2) => CompareEquals(v1, v2));
            operations.Register(typeof(VarString), typeof(VarString), "!=", (v1, v2) => CompareNotEquals(v1, v2));
        }
    }
}