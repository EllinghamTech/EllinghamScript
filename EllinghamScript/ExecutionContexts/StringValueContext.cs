using EllinghamScript.Internal;
using EllinghamScript.Variables;

namespace EllinghamScript.ExecutionContexts
{
    public class StringValueContext : ExecutionContext
    {
        private VarString _variable;
        public StringValueContext(ScriptRunner scriptRunner, string rawValue) : base(scriptRunner)
        {
            _variable = new VarString(rawValue, false);
        }

        public StringValueContext(ScriptRunner scriptRunner) : base(scriptRunner)
        {
            _variable = new VarString(scriptRunner.CollectString());
        }

        public override VarBase Execute()
        {
            return _variable;
        }
    }
}