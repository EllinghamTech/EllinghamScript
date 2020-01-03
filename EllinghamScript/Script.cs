using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EllinghamScript
{
    public class Script
    {
        public char[] CharArr { get; set; }
        public int Length { get; private set; }
        public string OriginalScript { get; set; }
        public Dictionary<int, int> CharLines { get; set; }
        
        public Script(string script)
        {
            OriginalScript = script;
            (CharArr, CharLines) = Clean(script);
            Length = CharArr.Length;
        }
        
        private static (char[], Dictionary<int, int>) Clean(string source)
        {
            StringBuilder parsedScript = new StringBuilder();
            Dictionary<int, int> charLines = new Dictionary<int, int>();

            bool inQuotes = false;
            bool inSingleQuotes = false;
            bool inComment = false;
            bool inBackTicks = false;
            bool lineComment = false;
            char previous = '\0'; // Null Char

            int parentheses = 0;
            int blocks = 0;
            int lineNumber = 0;
            int lastScriptLength = 0;

            for (int i = 0; i < source.Length; i++)
            {
                char ch = source[i];
                char next = i + 1 < source.Length ? source[i + 1] : '\0';

                if (ch == '\n')
                {
                    if (parsedScript.Length > lastScriptLength)
                    {
                        charLines[parsedScript.Length - 1] = lineNumber;
                        lastScriptLength = parsedScript.Length;
                    }

                    lineNumber++;
                }

                if (inComment && (lineComment && ch != '\n' || !lineComment && ch != '*'))
                    continue;

                switch (ch)
                {
                    case '/':
                        if (inComment || next == '/' || next == '*')
                        {
                            inComment = true;
                            lineComment = lineComment || next == '/';
                            continue;
                        }

                        break;
                    case '*': // Second character of /*
                        if (inComment && next == '/')
                        {
                            i++; // skip next character
                            inComment = false;
                            continue;
                        }

                        break;
                    
                    case Constants.DoubleQuotes:
                        if (!inComment)
                            if (previous != '\\')
                                inQuotes = !inQuotes;
                        break;
                    case Constants.SingleQuotes: // Single Quotes
                        if (!inComment)
                            if (previous != '\\')
                                inSingleQuotes = !inSingleQuotes;
                        break;
                    case Constants.BackTicks:
                        if (!inComment)
                            if (previous != '\\')
                                inBackTicks = !inBackTicks;
                        break;
                    
                    case ' ':
                        if (inQuotes || inSingleQuotes)
                            parsedScript.Append(ch);
                        else if (previous != ' ')
                            parsedScript.Append(ch);
                        continue;
                    
                    case '\t':
                    case '\r': // Only valid as part of a string
                        if (inQuotes || inSingleQuotes) parsedScript.Append(ch);
                        continue;
                    
                    case '\n': // Only valid as part of a string
                        if (inQuotes || inSingleQuotes)
                            parsedScript.Append(ch);
                        else if (lineComment)
                            inComment = lineComment = false;
                        continue;
                    
                    case Constants.BracketsOpen:
                        if (!inQuotes && !inSingleQuotes)
                            parentheses++;
                        break;
                    case Constants.BracketsClose:
                        if (!inQuotes && !inSingleQuotes)
                            parentheses--;
                        break;
                    
                    case Constants.BracesOpen:
                        if (!inQuotes)
                            blocks++;
                        break;
                    case Constants.BracesClose:
                        if (!inQuotes)
                            blocks--;
                        break;
                    case ';':
                        break;
                }

                if (!inComment)
                    parsedScript.Append(ch);

                previous = ch;
            }
            
            if (inComment && !lineComment)
                throw new Exception("Unexpected end (block comment not closed)");
            
            if (inQuotes)
                throw new Exception("Unexpected end (double quotes not ended)");
            
            if (inSingleQuotes)
                throw new Exception("Unexpected end (single quotes not ended)");
            
            if (inBackTicks)
                throw new Exception("Unexpected end (back ticks not ended)");

            if (blocks != 0)
                throw new Exception("Unexpected end (code block not ended)");

            if (parentheses != 0)
                throw new Exception("Unexpected end (parentheses/brackets not closed)");

            return (parsedScript.ToString().ToCharArray(), charLines);
        }
        
        public int GetOriginalLineNumber(int pos)
        {
            if (CharLines == null || CharLines.Count == 0)
                throw new ArgumentException();

            List<int> lineStart = CharLines.Keys.ToList();
            int lower = 0;
            int index = lower;

            if (pos <= lineStart[lower])
                return CharLines[lineStart[lower]];

            int upper = lineStart.Count - 1;
            if (pos >= lineStart[upper])
                return CharLines[lineStart[upper]];

            while (lower <= upper)
            {
                index = (lower + upper) / 2;
                int guessPos = lineStart[index];
                if (pos == guessPos)
                    break;

                if (pos < guessPos)
                {
                    if (index == 0 || pos > lineStart[index - 1])
                        break;

                    upper = index - 1;
                }
                else
                {
                    lower = index + 1;
                }
            }

            return CharLines[lineStart[index]];
        }
    }
}