using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace Calculator
{
    static class Math
    {
        public static double Calculate(string therm)
        {
            return new Therm(therm).GetResult();
        }

        public static ulong Factorial(ulong n)
        {
            ulong result = 1;
            for (ulong i = 1; i <= n; i++)
            {
                result *= i;
            }
            return result;
        }
    }

    class Therm
    {
        string therm;
        double result;

        public Therm(string therm)
        {
            this.therm = strTools.ReplaceAll(therm, "%", "/100", "π", System.Math.PI.ToString(), "e", System.Math.E.ToString());
            CalculateOperator();
        }

        public double GetResult()
        {
            return result;
        }

        bool AInfrontOfB(string s, string a, string b)
        {
            return s.Contains(a) && (s.IndexOf(a) < s.IndexOf(b) || !s.Contains(b));
        }

        void CalculateOperator()
        {
            string reversedTherm = strTools.Reverse(CalculateFunctions(therm));
            int index = 0;
            if ((index = reversedTherm.IndexOf('+')) != -1 && AInfrontOfB(reversedTherm, "+", "-"))
                result = new Therm(strTools.Reverse(reversedTherm.Substring(index + 1))).GetResult() + new Therm(strTools.Reverse(reversedTherm.Remove(index))).GetResult();
            else if ((index = reversedTherm.IndexOf('-')) != -1)
                result = new Therm(strTools.Reverse(reversedTherm.Substring(index + 1))).GetResult() - new Therm(strTools.Reverse(reversedTherm.Remove(index))).GetResult();
            else if ((index = reversedTherm.IndexOf('*')) != -1 && AInfrontOfB(reversedTherm, "*", "/"))
                result = new Therm(strTools.Reverse(reversedTherm.Substring(index + 1))).GetResult() * new Therm(strTools.Reverse(reversedTherm.Remove(index))).GetResult();
            else if ((index = reversedTherm.IndexOf('/')) != -1)
                result = new Therm(strTools.Reverse(reversedTherm.Substring(index + 1))).GetResult() / new Therm(strTools.Reverse(reversedTherm.Remove(index))).GetResult();
            else if ((index = reversedTherm.IndexOf('^')) != -1 && AInfrontOfB(reversedTherm, "^", "√"))
                result = System.Math.Pow(new Therm(strTools.Reverse(reversedTherm.Substring(index + 1))).GetResult(), new Therm(strTools.Reverse(reversedTherm.Remove(index))).GetResult());
            else if ((index = reversedTherm.IndexOf('√')) != -1)
                result = System.Math.Pow(new Therm(strTools.Reverse(reversedTherm.Substring(index + 1))).GetResult(), 1 / new Therm(strTools.Reverse(reversedTherm.Remove(index))).GetResult());
            else if (reversedTherm.Contains('!'))
                result = Math.Factorial((ulong)new Therm(strTools.Reverse(reversedTherm.Substring(reversedTherm.IndexOf('!') + 1))).GetResult());
            else
                result = Convert.ToDouble(strTools.Reverse(reversedTherm));
        }

        string CalculateFunctions(string therm)
        {
            while (therm.Contains('('))
            {
                ushort i = 0, openBracketIndex = 0, closedBracketIndex = 0;
                do
                {
                    if (AInfrontOfB(therm, "(", ")"))
                    {
                        ushort pos1 = (ushort)therm.IndexOf('(');
                        therm = therm.Remove(pos1, 1).Insert(pos1, "[");
                        if (i++ == 0)
                            openBracketIndex = pos1;
                    }
                    else if(therm.Contains(")"))
                    {
                        ushort pos2 = (ushort)therm.IndexOf(')');
                        therm = therm.Remove(pos2, 1).Insert(pos2, "]");
                        if (--i == 0)
                            closedBracketIndex = pos2;
                    }
                } while (i != 0);
                double bracketResult = new Therm(strTools.Section(therm = strTools.ReplaceAll(therm, "[", "(", "]", ")"), openBracketIndex + 1, closedBracketIndex - 1)).GetResult();
                therm = strTools.Replace(therm, openBracketIndex, "[");
                if (therm.Contains("sin["))
                    therm = strTools.Replace(therm, openBracketIndex - 3, closedBracketIndex, System.Math.Sin(System.Math.PI * bracketResult / 180.0).ToString());
                else if (therm.Contains("cos["))
                    therm = strTools.Replace(therm, openBracketIndex - 3, closedBracketIndex, System.Math.Cos(System.Math.PI * bracketResult / 180.0).ToString());
                else if (therm.Contains("tan["))
                    therm = strTools.Replace(therm, openBracketIndex - 3, closedBracketIndex, System.Math.Tan(System.Math.PI * bracketResult / 180.0).ToString());
                else if (therm.Contains("log["))
                    therm = strTools.Replace(therm, openBracketIndex - 3, closedBracketIndex, System.Math.Log10(bracketResult).ToString());
                else if (therm.Contains("ln["))
                    therm = strTools.Replace(therm, openBracketIndex - 2, closedBracketIndex, System.Math.Log(bracketResult).ToString());
                else
                    therm = strTools.Replace(therm, openBracketIndex, closedBracketIndex, bracketResult.ToString());
            }
            return therm;
        }
    }

    static class strTools
    {
        public static string Replace(string s, int index, string item)
        {
            return s.Remove(index, 1).Insert(index, item);
        }

        public static string Replace(string s, int startIndex, int endIndex, string item)
        {
            return s.Remove(startIndex, endIndex - startIndex + 1).Insert(startIndex, item);
        }

        public static string ReplaceAll(string s, params string[] items)
        {
            for (int i = 0; i < items.Length; i += 2)
            {
                s = s.Replace(items[i], items[i + 1]);
            }
            return s;
        }

        public static string RemoveSection(string s, int startIndex, int endIndex)
        {
            return s.Remove(startIndex, endIndex - startIndex + 1);
        }

        public static string Section(string s, int startIndex, int endIndex)
        {
            return s.Substring(startIndex, endIndex - startIndex + 1);
        }

        public static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}