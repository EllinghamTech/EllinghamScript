using EllinghamScript.Internal;
using EllinghamScript.Variables;

namespace EllinghamScript.ExecutionContexts
{
    public class NumericValueContext : ExecutionContext
    {
        private readonly VarNumber _variable;
        public NumericValueContext(ScriptRunner scriptRunner, double value) : base(scriptRunner)
        {
            _variable = new VarNumber(value);
        }

        public override VarBase Execute()
        {
            return _variable;
        }
    }
}