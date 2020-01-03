namespace EllinghamScript
{
    internal static class Constants
    {
        public const char EndStatement = ';';
        public const char OperatorAssign = '=';
        public const char OperatorNot = '!';
        public const char BracketsOpen = '(';
        public const char BracketsClose = ')';
        public const char BracesOpen = '{';
        public const char BracesClose = '}';
        public const char ArgumentSeparator = ',';
        public const char DotOperator = '.';

        public const char DoubleQuotes = '"';
        public const char SingleQuotes = '\'';
        public const char BackTicks = '`';

        public const char Escape = '\\';
        public const char NullOperator = '\0';
        
        // Control Flow
        public const string If = "if";
        public const string ElseIf = "elseif";
        public const string Else = "else";
        public const string While = "while";
        public const string For = "for";
        public const string Foreach = "foreach";

        public static readonly char[] ReservedOperators =
        {
            EndStatement,
            OperatorAssign,
            OperatorNot,
            BracketsOpen,
            BracketsClose,
            BracesOpen,
            BracesClose,
            ArgumentSeparator,
            DotOperator,
            DoubleQuotes,
            SingleQuotes,
            BackTicks
        };

        public static readonly char[] NonOperators =
        {
            BracesClose,
            BracesOpen,
            BracketsClose,
            BracketsOpen,
            DoubleQuotes,
            SingleQuotes,
            EndStatement
        };

        public static readonly char[] EndOperators =
        {
            BracesClose,
            BracketsClose,
            ArgumentSeparator,
            EndStatement
        };

        public static readonly char[] StringLiterals =
        {
            SingleQuotes,
            DoubleQuotes
        };

        public static readonly string[] ControlFlowSymbols =
        {
            If,
            While,
            For,
            Foreach
        };
    }
}