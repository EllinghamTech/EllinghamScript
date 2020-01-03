using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using EllinghamScript.Variables;

namespace EllinghamScript
{
    public class Operations
    {
        private readonly Dictionary<(Type, Type, string), Expression<Func<VarBase, VarBase, VarBase>>> OpsDictionary
            = new Dictionary<(Type, Type, string), Expression<Func<VarBase, VarBase, VarBase>>>();
        
        public List<string> Ops { get; private set; } = new List<string>();

        /// <summary>
        /// Register an operator (like +) between two variable types.
        ///
        /// E.g. VarNumber + VarNumber
        /// </summary>
        /// <param name="t1">First type (a VarBase type)</param>
        /// <param name="t2">Second Type (a VarBase type)</param>
        /// <param name="op">Valid operator character</param>
        /// <param name="expression">The expression to execute</param>
        /// <exception cref="Exception">Exception on invalid operator character</exception>
        public void Register(Type t1, Type t2, string op, Expression<Func<VarBase, VarBase, VarBase>> expression)
        {
            // Things like = and ! are handled elsewhere
            if(op.Length == 1 && Constants.ReservedOperators.Contains(op[0]))
                throw new Exception( $"'{op}' operator is a special case operator");

            // Remove if it exists
            if (OpsDictionary.ContainsKey((t1, t2, op)))
                OpsDictionary.Remove((t1, t2, op));
            
            OpsDictionary.Add((t1, t2, op), expression);

            if (!Ops.Contains(op))
                Ops.Add(op);
        }

        /// <summary>
        /// Registers a list of operator definitions
        /// </summary>
        /// <param name="list"></param>
        public void Register(IEnumerable<(Type, Type, string, Expression<Func<VarBase, VarBase, VarBase>>)> list)
        {
            foreach ((Type t1, Type t2, string op, Expression<Func<VarBase,VarBase,VarBase>> expression) in list)
            {
                Register(t1, t2, op, expression);
            }
        }

        /// <summary>
        /// Finds the best match for an operation, throwing an exception if no match is found.
        /// </summary>
        /// <param name="t1">Type 1</param>
        /// <param name="t2">Type 2</param>
        /// <param name="op">Operator Character</param>
        /// <returns>Expression</returns>
        /// <exception cref="Exception">Exception on no match found</exception>
        public Expression<Func<VarBase, VarBase, VarBase>> FindBest(Type t1, Type t2, string op)
        {
            // First see if exact match exists
            if (OpsDictionary.ContainsKey((t1, t2, op)))
                return OpsDictionary[(t1, t2, op)];
            
            // If not, use the strings operator if they exist (all variables must have a string representation)
            if (OpsDictionary.ContainsKey((typeof(VarString), typeof(VarString), op)))
                return OpsDictionary[(typeof(VarString), typeof(VarString), op)];
            
            // And fall back to their boolean representations
            if (OpsDictionary.ContainsKey((typeof(VarBoolean), typeof(VarBoolean), op)))
                return OpsDictionary[(typeof(VarBoolean), typeof(VarBoolean), op)];
            
            // If no string operator exists, we are out of options.  There is no match so moan.
            throw new Exception("Invalid Operation");
        }
    }
}