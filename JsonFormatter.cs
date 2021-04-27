using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web.Script.Serialization;
using Microsoft.Win32;
using System.Linq;


namespace JsonFormatter
{
    /// <summary> Class used to format string output of JSON serializer (System.Web.Script.Serialization)
    /// </summary>
    static class JsonFormatter
    {
        /// <summary> Single level indent depth </summary>
        public static int IndentLength = 4;
        /// <summary> Indent symbol (whitespace by default) </summary>
        public static char IndentSymbol = ' ';
        
        /// <summary> Method used for formatting JSON from single-line text to multiline text, easily readable by human. </summary>
        /// <param name="input"> Raw single-line JSON string; string </param>
        /// <returns> Formatted, human-readable JSON string </returns>
        public static string FormatJson(string input)
        {
            char[] entering = { '{', '[', '(' };
            char[] exiting = { '}', ']', ')' };
            bool inside = false;
            int level = 0;
            int letter = 0;
            string indent = "";
            var output = new StringBuilder();
            foreach (char i in input)
            {
                if (letter == 0 && i.Equals('{'))
                {
                    output.Append(i);
                    output.Append("\n");
                    level++;
                    indent = GetIndent(level);
                    output.Append(indent);
                }
                else if (inside && i.Equals('"'))
                {
                    output.Append(i);
                    inside = false;
                    //continue;
                }
                else if (inside)
                {
                    output.Append(i);
                    //continue;
                }
                else if (!inside && i.Equals('"'))
                {
                    output.Append(i);
                    inside = true;
                    //continue;
                }
                else if (entering.Contains(i))
                {
                    output.Append("\n");
                    output.Append(indent);
                    output.Append(i);
                    output.Append("\n");
                    level++;
                    indent = GetIndent(level);
                    output.Append(indent);
                    //continue;
                }
                else if (exiting.Contains(i))
                {
                    level--;
                    indent = GetIndent(level);
                    output.Append("\n");
                    output.Append(indent);
                    output.Append(i);
                    //continue;
                }
                else if (i.Equals(','))
                {
                    output.Append(i);
                    output.Append("\n");
                    output.Append(indent);
                    //continue;
                }
                else if (!inside)
                {
                    output.Append(i);
                }
                letter++;
            }
            return output.ToString();
        }
        
        /// <summary> Method used for formatting JSON from single-line text to multiline text, easily readable by human. </summary>
        /// <param name="input"> Raw single-line JSON string; string </param>
        /// <param name="symbol"> Indent symbol; char </param>
        /// <param name="length"> Single level indent depth </param>
        /// <returns> Formatted, human-readable JSON string </returns>
        public static string FormatJson(string input, char symbol, int length)
        {
            IndentSymbol = symbol;
            IndentLength = length;
            return FormatJson(input);
        }
        
        /// <summary> Method used for formatting JSON from single-line text to multiline text, easily readable by human. </summary>
        /// <param name="input"> Raw single-line JSON string; string </param>
        /// <param name="length"> Single level indent depth </param>
        /// <returns> Formatted, human-readable JSON string </returns>
        public static string FormatJson(string input, int length)
        {
            IndentLength = length;
            return FormatJson(input);
        }
        
        /// <summary> Method used for formatting JSON from single-line text to multiline text, easily readable by human. </summary>
        /// <param name="input"> Raw single-line JSON string; string </param>
        /// <param name="symbol"> Indent symbol; char </param>
        /// <param name="length"> Single level indent depth </param>
        /// <returns> Formatted, human-readable JSON string </returns>
        public static string FormatJson(string input, char symbol)
        {
            IndentSymbol = symbol;
            return FormatJson(input);
        }
        
        // set current indent string
        private static string GetIndent(int level)
        {
            string indent = "";
            while (indent.Length < IndentLength*level)
            {
                indent += IndentSymbol;
            }
            return indent;
        }
    }
}