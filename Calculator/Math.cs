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
            for (ulong i = 1; i <= n; i++, result *= i) ;
            return result;
        }
    }

    class Therm
    {
        string therm;
        double result;

        public Therm(string therm)
        {
            int index = 0;
            if((index = therm.IndexOf("E")) != -1)
                therm = strTools.Replace(therm, index, "*10^");
            this.therm = strTools.ReplaceAll(therm, "%", "/100", "π", System.Math.PI.ToString(), "e", System.Math.E.ToString());
            Calculate();
        }

        public double GetResult()
        {
            return result;
        }

        bool AInfrontOfB(string s, string a, string b)
        {
            return s.Contains(a) && (s.IndexOf(a) < s.IndexOf(b) || !s.Contains(b));
        }

        void Calculate()
        {
            string reversedTherm = strTools.Reverse(CalculateFunctions(therm));
            int index = 0;
            result =
                (index = reversedTherm.IndexOf('+')) != -1 && AInfrontOfB(reversedTherm, "+", "-") ? Math.Calculate(strTools.Reverse(reversedTherm.Substring(index + 1))) + Math.Calculate(strTools.Reverse(reversedTherm.Remove(index))) :
                (index = reversedTherm.IndexOf('-')) != -1 ? Math.Calculate(strTools.Reverse(reversedTherm.Substring(index + 1))) - Math.Calculate(strTools.Reverse(reversedTherm.Remove(index))) :
                (index = reversedTherm.IndexOf('*')) != -1 && AInfrontOfB(reversedTherm, "*", "/") ? Math.Calculate(strTools.Reverse(reversedTherm.Substring(index + 1))) * Math.Calculate(strTools.Reverse(reversedTherm.Remove(index))) :
                (index = reversedTherm.IndexOf('/')) != -1 ? Math.Calculate(strTools.Reverse(reversedTherm.Substring(index + 1))) / Math.Calculate(strTools.Reverse(reversedTherm.Remove(index))) :
                (index = reversedTherm.IndexOf('^')) != -1 && AInfrontOfB(reversedTherm, "^", "√") ? System.Math.Pow(Math.Calculate(strTools.Reverse(reversedTherm.Substring(index + 1))), Math.Calculate(strTools.Reverse(reversedTherm.Remove(index)))) :
                (index = reversedTherm.IndexOf('√')) != -1 ? System.Math.Pow(Math.Calculate(strTools.Reverse(reversedTherm.Remove(index))), 1 / Math.Calculate(strTools.Reverse(reversedTherm.Substring(index + 1)))) :
                reversedTherm.Contains('!') ? Math.Factorial((ulong)Math.Calculate(strTools.Reverse(reversedTherm.Substring(reversedTherm.IndexOf('!') + 1)))):
                therm == "" ? 0 :
                Convert.ToDouble(strTools.Reverse(reversedTherm));
        }

        string CalculateFunctions(string therm)
        {
            while (therm.Contains('('))
            {
                int openBracketIndex = therm.IndexOf('('), closedBracketIndex = 0;
                therm = strTools.Replace(therm, openBracketIndex, "F");
                for(int i = 1; i != 0;)
                    if (AInfrontOfB(therm, "(", ")"))
                    {
                        therm = strTools.Replace(therm, therm.IndexOf('('), "[");
                        i++;
                    }
                    else
                    {
                        therm = strTools.Replace(therm, closedBracketIndex = therm.IndexOf(')'), "]");
                        i--;
                    }
                double bracketResult = Math.Calculate(strTools.Section(therm = strTools.ReplaceAll(therm, "[", "(", "]", ")"), openBracketIndex + 1, closedBracketIndex - 1));
                therm =
                    therm.Contains("sinF") ? strTools.Replace(therm, openBracketIndex - 3, closedBracketIndex, System.Math.Sin(bracketResult).ToString()) :
                    therm.Contains("cosF") ? strTools.Replace(therm, openBracketIndex - 3, closedBracketIndex, System.Math.Cos(bracketResult).ToString()) :
                    therm.Contains("tanF") ? strTools.Replace(therm, openBracketIndex - 3, closedBracketIndex, System.Math.Tan(bracketResult).ToString()) :
                    therm.Contains("logF") ? strTools.Replace(therm, openBracketIndex - 3, closedBracketIndex, System.Math.Log10(bracketResult).ToString()) :
                    therm.Contains("lnF") ? strTools.Replace(therm, openBracketIndex - 2, closedBracketIndex, System.Math.Log(bracketResult).ToString()) :
                    strTools.Replace(therm, openBracketIndex, closedBracketIndex, bracketResult.ToString());
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
                s = s.Replace(items[i], items[i + 1]);
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