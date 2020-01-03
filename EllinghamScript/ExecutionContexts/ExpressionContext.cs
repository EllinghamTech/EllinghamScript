using System;
using EllinghamScript.Internal;

namespace EllinghamScript.ExecutionContexts
{
    public class ExpressionContext : ExecutionContext
    {
        public ExpressionContext(ScriptRunner scriptRunner, bool explicitExpression = true) : base(scriptRunner)
        {
            // An expression can have many forms
            // 1 + 1
            // 1 + (1 + 1)
            // 1 + b + (method() + 1) + 1
            // We need to account for all of them using the "split and merge" technique.  Ish.
            //
            // We take the first context, then we test to see if there is an operator.  If there is
            // an operator there must be a second context, so we merge them using the OperationContext.
            // Then we see if there is another operator, if so we merge the OperationContext and next
            // context into yet another OperationContext.
            //
            // so 1+2 becomes OperationContext of Numeric 1, Numeric 2, op +
            // 1 + 2 + 3 becomes OperationContext of (OperationContext of Numeric 1, Numeric 2, op +), Numeric 3, op +
            //
            // This is why there is no order of operations in platform script.  However expressions within the expression
            // are operated on using the expression context.
            //
            // 1 + (2 + 3) becomes OperationContext of Numeric 1, ExpressionContext of (1 + 3), op +

            // So collect the first context
            ExecutionContext mergedContext = scriptRunner.CollectContext();
            
            if(mergedContext == null) throw new Exception("No context");

            while (true)
            {
                // Collect op
                string op = scriptRunner.CollectOperator();

                if (!string.IsNullOrEmpty(op))
                {
                    ExecutionContext second = scriptRunner.CollectContext(new []{explicitExpression ? Constants.BracketsClose : Constants.NullOperator});
                    mergedContext = new OperationContext(scriptRunner, mergedContext, second, op);
                }
                else
                {
                    // No more operations, lets get out of here!
                    break;
                }
            }
            
            // Our merged context becomes the ContextToExecute
            ContextToExecute = mergedContext;
            
            // Phew...
        }
    }
}