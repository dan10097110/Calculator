using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace Calculator
{
    static class Math
    {
        //fakultät und prozent im direkten zusammenspiel funktionieren nicht


        public static double Calculate(string thermString)
        {
            thermString = StrTools.ReplaceAll(thermString, "π", System.Math.PI.ToString(), "e", System.Math.E.ToString());
            List<string> therm = ThermToArray(thermString);
            return Calc(therm);
        }

        public static double Calc(List<string> therm)
        {
            while (therm.Contains("("))
            {
                int openBracketIndex = therm.IndexOf("("), closedBracketIndex = GetClosedBracketIndexToFirstOpen(therm);
                double bracketResult = Calc(therm.GetRange(openBracketIndex + 1, closedBracketIndex - openBracketIndex - 1));
                therm = LstTools.Replace(therm, openBracketIndex, "F");
                therm =
                    therm.Contains("sinF") ? LstTools.Replace(therm, openBracketIndex - 3, closedBracketIndex, System.Math.Sin(bracketResult).ToString()) :
                    therm.Contains("cosF") ? LstTools.Replace(therm, openBracketIndex - 3, closedBracketIndex, System.Math.Cos(bracketResult).ToString()) :
                    therm.Contains("tanF") ? LstTools.Replace(therm, openBracketIndex - 3, closedBracketIndex, System.Math.Tan(bracketResult).ToString()) :
                    therm.Contains("logF") ? LstTools.Replace(therm, openBracketIndex - 3, closedBracketIndex, System.Math.Log10(bracketResult).ToString()) :
                    therm.Contains("lnF") ?  LstTools.Replace(therm, openBracketIndex - 2, closedBracketIndex, System.Math.Log(bracketResult).ToString()) :
                    LstTools.Replace(therm, openBracketIndex, closedBracketIndex, bracketResult.ToString());
            }
            return Operators(therm);
        }

        static double Operators(List<string> therm)
        {
            int index;
            List<string> reversedTherm = new List<string>(therm);
            reversedTherm.Reverse();
            return
                (index = reversedTherm.IndexOf("+")) != -1 && LstTools.AExistsAndInfrontOfB(reversedTherm, "+", "-") ? Operators(LstTools.Reverse(reversedTherm.GetRange(index + 1, reversedTherm.Count - index - 1))) + Operators(LstTools.Reverse(reversedTherm.GetRange(0, index))) :
                (index = reversedTherm.IndexOf("-")) != -1 ? Operators(LstTools.Reverse(reversedTherm.GetRange(index + 1, reversedTherm.Count - index - 1))) - Operators(LstTools.Reverse(reversedTherm.GetRange(0, index))) :
                (index = reversedTherm.IndexOf("*")) != -1 && LstTools.AExistsAndInfrontOfB(reversedTherm, "*", "/") ? Operators(LstTools.Reverse(reversedTherm.GetRange(index + 1, reversedTherm.Count - index - 1))) * Operators(LstTools.Reverse(reversedTherm.GetRange(0, index))) :
                (index = reversedTherm.IndexOf("/")) != -1 ? Operators(LstTools.Reverse(reversedTherm.GetRange(index + 1, reversedTherm.Count - index - 1))) / Operators(LstTools.Reverse(reversedTherm.GetRange(0, index))) :
                (index = reversedTherm.IndexOf("^")) != -1 && LstTools.AExistsAndInfrontOfB(reversedTherm, "^", "√") ? System.Math.Pow(Operators(LstTools.Reverse(reversedTherm.GetRange(index + 1, reversedTherm.Count - index - 1))), Operators(LstTools.Reverse(reversedTherm.GetRange(0, index)))) :
                (index = reversedTherm.IndexOf("√")) != -1 ? System.Math.Pow(Operators(LstTools.Reverse(reversedTherm.GetRange(0, index))), 1 / Operators(LstTools.Reverse(reversedTherm.GetRange(index + 1, reversedTherm.Count - index - 1)))) :
                (index = reversedTherm.IndexOf("!")) != -1 && LstTools.AExistsAndInfrontOfB(reversedTherm, "!", "%") ? Operators(LstTools.Reverse(LstTools.Replace(reversedTherm, index, index + 1, Factorial((ulong)Operators(LstTools.Reverse(reversedTherm.GetRange(index + 1, reversedTherm.Count - index - 1)))).ToString()))) :
                (index = reversedTherm.IndexOf("%")) != -1 ? Operators(LstTools.Reverse(LstTools.Replace(reversedTherm, index, index + 1, (Operators(LstTools.Reverse(reversedTherm.GetRange(index + 1, reversedTherm.Count - index - 1))) / 100).ToString()))) :
                (index = reversedTherm.IndexOf("E")) != -1 ? Operators(LstTools.Reverse(reversedTherm.GetRange(index + 1, reversedTherm.Count - index - 1))) * System.Math.Pow(10, Operators(LstTools.Reverse(reversedTherm.GetRange(0, index)))) :
                double.Parse(therm.First());
                //einheiten wie meter oder pi oder prozent können hier drangehangen werden
        }

        static List<string> ThermToArray(string therm)
        {
            List<string> thermArray = new List<string>();
            while (therm != "")
            {
                //openBracket
                while (therm.StartsWith("("))
                {
                    thermArray.Add(therm.Remove(1));
                    therm = therm.Substring(1);
                }
                
                //number
                int min = Min(
                    therm.Contains('*') ? therm.IndexOf('*') : int.MaxValue, 
                    therm.Contains('/') ? therm.IndexOf('/') : int.MaxValue, 
                    therm.Contains('^') ? therm.IndexOf('^') : int.MaxValue, 
                    therm.Contains('√') ? therm.IndexOf('√') : int.MaxValue, 
                    therm.Substring(1).IndexOf('+') != -1 ? therm.Substring(1).IndexOf('+') + 1 : int.MaxValue, 
                    therm.Substring(1).IndexOf('-') != -1 ? therm.Substring(1).IndexOf('-') + 1 : int.MaxValue, 
                    therm.Contains(')') ? therm.IndexOf(')') : int.MaxValue, 
                    therm.Contains('(') ? therm.IndexOf('(') : int.MaxValue, 
                    therm.Contains('!') ? therm.IndexOf('!') : int.MaxValue,
                    therm.Contains('%') ? therm.IndexOf('%') : int.MaxValue);
                if (min == int.MaxValue)
                {
                    thermArray.Add(therm);
                    therm = "";
                }
                else
                {
                    thermArray.Add(therm.Remove(min));
                    therm = therm.Substring(min);
                }

                //fakultät/percent
                while (therm.StartsWith("!") || therm.StartsWith("%"))
                {
                    if (therm.Length == 1)
                    {
                        thermArray.Add(therm);
                        therm = "";
                    }
                    else
                    {
                        thermArray.Add(therm.Remove(1));
                        therm = therm.Substring(1);
                    }
                }

                //openBracket
                while (therm.StartsWith(")"))
                {
                    if (therm.Length == 1)
                    {
                        thermArray.Add(therm);
                        therm = "";
                    }
                    else
                    {
                        thermArray.Add(therm.Remove(1));
                        therm = therm.Substring(1);
                    }
                }

                //fakultät/percent
                while (therm.StartsWith("!") || therm.StartsWith("%"))
                {
                    if (therm.Length == 1)
                    {
                        thermArray.Add(therm);
                        therm = "";
                    }
                    else
                    {
                        thermArray.Add(therm.Remove(1));
                        therm = therm.Substring(1);
                    }
                }

                //operator
                if (therm != "")
                {
                    thermArray.Add(therm.Remove(1));
                    therm = therm.Substring(1);
                }
            }
            return thermArray;
        }

        public static ulong Factorial(ulong n)
        {
            ulong result = 1;
            for (ulong i = 2; i <= n; result *= i++) ;
            return result;
        }

        public static int Min(params int[] d)
        {
            int min = int.MaxValue;
            for (int i = 0; i < d.Length; i++)
                min = System.Math.Min(min, d[i]);
            return min;
        }

        static int GetClosedBracketIndexToFirstOpen(List<string> therm)
        {
            int closedBracketIndex = -1, i = 0;
            do
            {
                if (LstTools.AExistsAndInfrontOfB(therm, "(", ")"))
                {
                    therm = LstTools.Replace(therm, therm.IndexOf("("), "[");
                    i++;
                }
                else
                {
                    therm = LstTools.Replace(therm, closedBracketIndex = therm.IndexOf(")"), "]");
                    i--;
                }
            } while (i != 0);
            return closedBracketIndex;
        }
    }
}