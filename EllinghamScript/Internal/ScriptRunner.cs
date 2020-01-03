using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EllinghamScript.ExecutionContexts;
using EllinghamScript.Functions;
using EllinghamScript.Variables;

namespace EllinghamScript.Internal
{
    public class ScriptRunner
    {
        public Script Script { get; set; }
        public int Pointer { get; private set; } = 0;
        
        public char PrevPrevChar => (Pointer > 1) ? Script.CharArr[Pointer - 2] : Constants.NullOperator;
        public char PrevChar => (Pointer > 0) ? Script.CharArr[Pointer - 1] : Constants.NullOperator;
        public char CurChar => Script.CharArr[Pointer];
        public char NextChar => (Script.Length > Pointer + 1) ? Script.CharArr[Pointer + 1] : Constants.NullOperator;
        public char NextNextChar => (Script.Length > Pointer + 2) ? Script.CharArr[Pointer + 2] : Constants.NullOperator;
        
        public Operations Operations = new Operations();
        public Dictionary<string, VarBase> Variables = new Dictionary<string, VarBase>();
        public Dictionary<string, FunctionBase> Functions = new Dictionary<string, FunctionBase>();

        public ScriptRunner(Script script)
        {
            Script = script;
            
            if(Script.Length == 0)
                throw new Exception();
            
            VariableOperations.NumberOperations.Register(Operations);
            VariableOperations.StringOperations.Register(Operations);
            VariableOperations.BooleanOperations.Register(Operations);
        }

        /// <summary>
        /// Gets all the expressions that make up the entire script
        /// </summary>
        /// <param name="resetPointer"></param>
        /// <param name="preventRecurseOn"></param>
        /// <returns></returns>
        public IEnumerable<ExecutionContext> GetAllExecutionContexts(bool resetPointer = true, char[] preventRecurseOn = null)
        {
            if (resetPointer) Pointer = 0;

            List<ExecutionContext> executionContexts = new List<ExecutionContext>();
            
            while (true)
            {
                ExecutionContext context = CollectContext(preventRecurseOn);
                
                if(context == null)
                    return executionContexts;
                
                executionContexts.Add(context);
            }
        }

        public VarBase Execute()
        {
            IEnumerable<ExecutionContext> execution = GetAllExecutionContexts(true);

            VarBase result = null;
            
            foreach (ExecutionContext context in execution)
            {
                result = context.Execute();
            }

            return result;
        }

        /// <summary>
        /// Collects a symbol, a token that does not start with a number and contains
        /// only alphanumeric characters.
        /// </summary>
        /// <returns></returns>
        public string CollectSymbol()
        {
            StringBuilder symbol = new StringBuilder();

            while (Pointer < Script.Length)
            {
                if (CurChar == ' ')
                {
                    if (symbol.Length == 0)
                    {
                        Pointer++;
                        continue;
                    }
                    
                    break;
                }

                if (char.IsLetterOrDigit(CurChar))
                {
                    symbol.Append(CurChar);
                    Pointer++;
                }
                else
                    break;
            }

            return symbol.ToString();
        }
        
        /// <summary>
        /// Trys to collect a symol, if the returning symbol is not in the
        /// arguments products then return the symbol but do not change the
        /// script pointer.
        /// </summary>
        /// <returns></returns>
        public string TryCollectSymbol(IEnumerable<string> symbols)
        {
            int pointer = Pointer;
            string symbol = CollectSymbol();

            if (!symbols.Contains(symbol))
                Pointer = pointer;

            return symbol;
        }

        /// <summary>
        /// Collects a numeric, a token that starts with a number and can contain
        /// a decimal point.  Parsed as a double.
        /// </summary>
        /// <returns></returns>
        public double CollectNumeric()
        {
            StringBuilder symbol = new StringBuilder();

            while (Pointer < Script.Length)
            {
                if (CurChar == ' ')
                {
                    if (symbol.Length == 0)
                    {
                        Pointer++;
                        continue;
                    }
                    
                    break;
                }

                if (char.IsDigit(CurChar) || CurChar == '.')
                {
                    symbol.Append(CurChar);
                    Pointer++;
                }
                else
                    break;
            }

            return double.Parse(symbol.ToString());
        }

        /// <summary>
        /// Collects the operator, empty string if there is no
        /// operator.
        /// </summary>
        /// <returns></returns>
        public string CollectOperator()
        {
            StringBuilder op = new StringBuilder();

            while (Pointer < Script.Length)
            {
                if (CurChar == ' ')
                {
                    if (op.Length == 0)
                    {
                        Pointer++;
                        continue;
                    }
                    
                    break;
                }
                
                if (!char.IsLetterOrDigit(CurChar))
                {
                    if (Constants.NonOperators.Contains(CurChar))
                    {
                        if (Constants.EndOperators.Contains(CurChar))
                            Pointer++;
                        
                        break;
                    }

                    op.Append(CurChar);
                    Pointer++;
                }
                else
                    break;
            }

            return op.ToString();
        }

        /// <summary>
        /// Collects the operator but only moves the pointer forward if
        /// the operator is the same as provided in the array argument.
        /// </summary>
        /// <param name="operators"></param>
        /// <returns></returns>
        public string TryCollectOperators(string[] operators = null)
        {
            int pointer = Pointer;

            string op = CollectOperator();
            
            if(operators == null || !operators.Contains(op))
                Pointer = pointer;

            return op;
        }

        /// <summary>
        /// Collects a context with the ability to be stateless, aka
        /// does not itself cause any alterations to the variables
        /// of the script.
        /// </summary>
        /// <param name="preventRecurseOn">Prevent recurse on ending specific character</param>
        /// <returns></returns>
        public ExecutionContext CollectContext(char[] preventRecurseOn = null)
        {
            if (Pointer >= Script.Length) return null;
            
            if (CurChar == ' ')
            {
                Pointer++;
                return CollectContext(preventRecurseOn);
            }
            
            // Any symbol that starts with a letter is a function, control flow or variable
            if (char.IsLetter(CurChar))
            {
                string symbol = CollectSymbol();
                return new SymbolContext(this, symbol);
            }
            
            // Any symbol that starts with a number is a numeric value
            if (char.IsDigit(CurChar))
            {
                double numeric = CollectNumeric();
                return new NumericValueContext(this, numeric);
            }

            // Starts an expression (the Function and Method context handles the Argument version)
            if (CurChar == Constants.BracketsOpen)
            {
                Pointer++; // Start from inside the brackets
                return new ExpressionContext(this);
            }

            // Starts a block
            if (CurChar == Constants.BracesOpen)
            {
                Pointer++; // Start from inside the braces
                return new BlockContext(this);
            }

            // Starts a string
            if (Constants.StringLiterals.Contains(CurChar))
            {
                return new StringValueContext(this);
            }
            
            // Starts a string
            if (Constants.BackTicks == CurChar)
            {
                throw new NotImplementedException("Not yet implemented...  Sorry...");
            }
            
            // Endings....
            if(Constants.EndOperators.Contains(CurChar))
            {
                Pointer++;
                
                if (preventRecurseOn == null || !preventRecurseOn.Contains(PrevChar))
                {
                    // ReSharper disable once TailRecursiveCall
                    return CollectContext(preventRecurseOn);
                }
            }

            return null;
        }

        /// <summary>
        /// Collects function arguments in the form
        /// (1, 2, 3, 4) etc where each argument is itself
        /// a context.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ExecutionContext> CollectArguments()
        {
            List<ExecutionContext> executionContexts = new List<ExecutionContext>();

            if (CurChar == '(' || CurChar == ' ')
                Pointer++;

            while (true)
            {
                ExecutionContext context = CollectContext(new []{Constants.BracketsClose});

                if (context == null)
                    return executionContexts;
                
                executionContexts.Add(context);

                if (CurChar == ')')
                    return executionContexts;
            }
        }

        /// <summary>
        /// Collects a double or single quoted string
        /// </summary>
        /// <returns></returns>
        public string CollectString()
        {
            char stringType = (CurChar == Constants.DoubleQuotes ? Constants.DoubleQuotes : Constants.SingleQuotes);
            Pointer++;
            
            StringBuilder stringBuilder = new StringBuilder();
            char prevChar = Constants.NullOperator;

            while (true)
            {
                if (CurChar == Constants.Escape)
                {
                    if (prevChar == Constants.Escape)
                    {
                        stringBuilder.Append(Constants.Escape);
                        prevChar = Constants.NullOperator;
                        Pointer++;
                        continue;
                    }

                    Pointer++;
                    prevChar = CurChar;
                    continue;
                }

                if (CurChar == stringType && prevChar != Constants.Escape)
                {
                    Pointer++;
                    break; // End of string
                }

                stringBuilder.Append(CurChar);
                prevChar = CurChar;
                Pointer++;
            }

            return stringBuilder.ToString();
        }
    }
}