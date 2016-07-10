using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace Calculator
{
    static class Math
    {
        /*
            x^-y -> buggy

            -> x^0-y
            -> 1-y

            -> wrong result


        */

            /*
             ++
             --
             +-
             -+



            *-
            * 
            * 
            * 
            * /-
             
             
             */

        public static double Calculate(string therm)
        {
            string b = therm;
            List<string> thermList = new List<string>();
            while(therm != "")
            {
                while (therm.StartsWith("("))
                {
                    thermList.Add(therm.Remove(1));
                    therm = therm.Substring(1);
                }

                int min = System.Math.Min(therm.Contains('*') ? therm.IndexOf('*') : int.MaxValue, System.Math.Min(therm.Contains('/') ? therm.IndexOf('/') : int.MaxValue, System.Math.Min(therm.Contains('^') ? therm.IndexOf('^') : int.MaxValue, System.Math.Min(therm.Contains('√') ? therm.IndexOf('√') : int.MaxValue, System.Math.Min(therm.Contains('+') ? therm.Substring(1).IndexOf('+') + 1 : int.MaxValue, System.Math.Min(therm.Contains('-') ? therm.Substring(1).IndexOf('-') + 1 : int.MaxValue, therm.Contains(')') ? therm.IndexOf(')') : int.MaxValue))))));
                if (min == int.MaxValue)
                {
                    thermList.Add(therm);
                    break;
                }
                else
                {
                    thermList.Add(therm.Remove(min));
                    therm = therm.Substring(min);
                }

                while (therm.StartsWith(")"))
                {
                    if (therm.Length == 1)
                    {
                        thermList.Add(therm);
                        therm = "";
                    }
                    else
                    {

                        thermList.Add(therm.Remove(1));
                        therm = therm.Substring(1);
                    }
                }
                
                if(therm != "")
                {
                    thermList.Add(therm.Remove(1));
                    therm = therm.Substring(1);
                }
            }
            therm = b;
            therm = strTools.ReplaceAll(therm, "π", System.Math.PI.ToString(), "e", System.Math.E.ToString());
            while (therm.Contains('('))
            {
                int openBracketIndex = therm.IndexOf('('), closedBracketIndex = GetClosedBracketIndexToFirstOpen(therm);
                double bracketResult = Calculate(strTools.Section(therm, openBracketIndex + 1, closedBracketIndex - 1));
                therm = strTools.Replace(therm, openBracketIndex, "F");
                therm =
                    therm.Contains("sinF") ? strTools.Replace(therm, openBracketIndex - 3, closedBracketIndex, System.Math.Sin(bracketResult).ToString()) :
                    therm.Contains("cosF") ? strTools.Replace(therm, openBracketIndex - 3, closedBracketIndex, System.Math.Cos(bracketResult).ToString()) :
                    therm.Contains("tanF") ? strTools.Replace(therm, openBracketIndex - 3, closedBracketIndex, System.Math.Tan(bracketResult).ToString()) :
                    therm.Contains("logF") ? strTools.Replace(therm, openBracketIndex - 3, closedBracketIndex, System.Math.Log10(bracketResult).ToString()) :
                    therm.Contains("lnF") ? strTools.Replace(therm, openBracketIndex - 2, closedBracketIndex, System.Math.Log(bracketResult).ToString()) :
                    strTools.Replace(therm, openBracketIndex, closedBracketIndex, bracketResult.ToString());
            }
            return Operators(therm);
        }

        static double Operators(string therm)
        {
            int index;
            string reversedTherm = strTools.Reverse(therm);
            double result =
                (index = reversedTherm.IndexOf('+')) != -1 && strTools.AExistsAndInfrontOfB(reversedTherm, "+", "-") ? Operators(strTools.Reverse(reversedTherm.Substring(index + 1))) + Operators(strTools.Reverse(reversedTherm.Remove(index))) :
                (index = reversedTherm.IndexOf('-')) != -1 ? Operators(strTools.Reverse(reversedTherm.Substring(index + 1))) - Operators(strTools.Reverse(reversedTherm.Remove(index))) :
                (index = reversedTherm.IndexOf('*')) != -1 && strTools.AExistsAndInfrontOfB(reversedTherm, "*", "/") ? Operators(strTools.Reverse(reversedTherm.Substring(index + 1))) * Operators(strTools.Reverse(reversedTherm.Remove(index))) :
                (index = reversedTherm.IndexOf('/')) != -1 ? Operators(strTools.Reverse(reversedTherm.Substring(index + 1))) / Operators(strTools.Reverse(reversedTherm.Remove(index))) :
                (index = reversedTherm.IndexOf('^')) != -1 && strTools.AExistsAndInfrontOfB(reversedTherm, "^", "√") ? System.Math.Pow(Operators(strTools.Reverse(reversedTherm.Substring(index + 1))), Operators(strTools.Reverse(reversedTherm.Remove(index)))) :
                (index = reversedTherm.IndexOf('√')) != -1 ? System.Math.Pow(Operators(strTools.Reverse(reversedTherm.Remove(index))), 1 / Operators(strTools.Reverse(reversedTherm.Substring(index + 1)))) :
                (index = reversedTherm.IndexOf('!')) != -1 ? Factorial((ulong)Operators(strTools.Reverse(reversedTherm.Substring(index + 1)))) :
                (index = reversedTherm.IndexOf('%')) != -1 ? Operators(strTools.Reverse(reversedTherm.Substring(index + 1))) / 100 :
                (index = reversedTherm.IndexOf('E')) != -1 ? Operators(strTools.Reverse(reversedTherm.Substring(index + 1))) * System.Math.Pow(10, Operators(strTools.Reverse(reversedTherm.Remove(index)))) :
                //therm == "" ? 0 :
                //einheiten wie meter oder pi oder prozent können hier drangehangen werden
                Convert.ToDouble(strTools.Reverse(reversedTherm));
            return result;
        }

        public static ulong Factorial(ulong n)
        {
            ulong result = 1;
            for (ulong i = 1; i <= n; i++, result *= i) ;
            return result;
        }

        static int GetClosedBracketIndexToFirstOpen(string therm)
        {
            int closedBracketIndex = -1, i = 0;
            do
            {
                if (strTools.AExistsAndInfrontOfB(therm, "(", ")"))
                {
                    therm = strTools.Replace(therm, therm.IndexOf('('), "[");
                    i++;
                }
                else
                {
                    therm = strTools.Replace(therm, closedBracketIndex = therm.IndexOf(')'), "]");
                    i--;
                }
            } while (i != 0);
            return closedBracketIndex;
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

        public static bool AExistsAndInfrontOfB(string s, string a, string b)
        {
            return s.Contains(a) && (s.IndexOf(a) < s.IndexOf(b) || !s.Contains(b));
        }
    }
}