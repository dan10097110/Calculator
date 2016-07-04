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
            //prozent wird unsicher ersetzt
            this.therm = therm.Replace("%", "/100").Replace("π", System.Math.PI.ToString()).Replace("e", System.Math.E.ToString());
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
            string reversedTherm = ReverseString(CalculateBrackets(therm));
            int index = 0;
            if ((index = reversedTherm.IndexOf('+')) != -1 && AInfrontOfB(reversedTherm, "+", "-"))
                result = new Therm(ReverseString(reversedTherm.Substring(index + 1))).GetResult() + new Therm(ReverseString(reversedTherm.Remove(index))).GetResult();
            else if ((index = reversedTherm.IndexOf('-')) != -1)
                result = new Therm(ReverseString(reversedTherm.Substring(index + 1))).GetResult() - new Therm(ReverseString(reversedTherm.Remove(index))).GetResult();
            else if ((index = reversedTherm.IndexOf('*')) != -1 && AInfrontOfB(reversedTherm, "*", "/"))
                result = new Therm(ReverseString(reversedTherm.Substring(index + 1))).GetResult() * new Therm(ReverseString(reversedTherm.Remove(index))).GetResult();
            else if ((index = reversedTherm.IndexOf('/')) != -1)
                result = new Therm(ReverseString(reversedTherm.Substring(index + 1))).GetResult() / new Therm(ReverseString(reversedTherm.Remove(index))).GetResult();
            else if ((index = reversedTherm.IndexOf('^')) != -1 && AInfrontOfB(reversedTherm, "^", "√"))
                result = System.Math.Pow(new Therm(ReverseString(reversedTherm.Substring(index + 1))).GetResult(), new Therm(ReverseString(reversedTherm.Remove(index))).GetResult());
            else if ((index = reversedTherm.IndexOf('√')) != -1)
                result = System.Math.Pow(new Therm(ReverseString(reversedTherm.Substring(index + 1))).GetResult(), 1 / new Therm(ReverseString(reversedTherm.Remove(index))).GetResult());
            else if (reversedTherm.Contains('!'))
                result = Math.Factorial((ulong)new Therm(ReverseString(reversedTherm.Substring(reversedTherm.IndexOf('!') + 1))).GetResult());
            else
                result = Convert.ToDouble(ReverseString(reversedTherm));
        }

        string CalculateBrackets(string therm)
        {
            while (therm.Contains('('))
            {
                ushort i = 0, openBracketIndex = 0, closedBracketIndex = 0;
                do
                {
                    if (AInfrontOfB(therm, "(", ")"))
                    {
                        ushort pos1 = (ushort)therm.IndexOf('(');
                        therm = therm.Remove(pos1, 1).Insert(pos1, "p");
                        if (i++ == 0)
                            openBracketIndex = pos1;
                    }
                    else if(therm.Contains(")"))
                    {
                        ushort pos2 = (ushort)therm.IndexOf(')');
                        therm = therm.Remove(pos2, 1).Insert(pos2, "c");
                        if (--i == 0)
                            closedBracketIndex = pos2;
                    }
                } while (i != 0);
                therm = therm.Remove(openBracketIndex, 1).Insert(openBracketIndex, "(");
                if (therm.Contains("sin("))
                {
                    therm = therm.Replace('p', '(').Replace('c', ')');
                    therm = therm.Remove(openBracketIndex - 3, closedBracketIndex - openBracketIndex + 4).Insert(openBracketIndex - 3, System.Math.Sin(System.Math.PI * new Therm(therm.Substring(openBracketIndex + 1, closedBracketIndex - openBracketIndex - 1)).GetResult() / 180.0).ToString());
                }
                else if (therm.Contains("cos("))
                {
                    therm = therm.Replace('p', '(').Replace('c', ')');
                    therm = therm.Remove(openBracketIndex - 3, closedBracketIndex - openBracketIndex + 4).Insert(openBracketIndex - 3, System.Math.Cos(System.Math.PI * new Therm(therm.Substring(openBracketIndex + 1, closedBracketIndex - openBracketIndex - 1)).GetResult() / 180.0).ToString());
                }
                else if (therm.Contains("tan("))
                {
                    therm = therm.Replace('p', '(').Replace('c', ')');
                    therm = therm.Remove(openBracketIndex - 3, closedBracketIndex - openBracketIndex + 4).Insert(openBracketIndex - 3, System.Math.Tan(System.Math.PI * new Therm(therm.Substring(openBracketIndex + 1, closedBracketIndex - openBracketIndex - 1)).GetResult() / 180.0).ToString());
                }
                else if (therm.Contains("log("))
                {
                    therm = therm.Replace('p', '(').Replace('c', ')');
                    therm = therm.Remove(openBracketIndex - 3, closedBracketIndex - openBracketIndex + 4).Insert(openBracketIndex - 3, System.Math.Log10(new Therm(therm.Substring(openBracketIndex + 1, closedBracketIndex - openBracketIndex - 1)).GetResult()).ToString());
                }
                else if (therm.Contains("ln("))
                {
                    therm = therm.Replace('p', '(').Replace('c', ')');
                    therm = therm.Remove(openBracketIndex - 2, closedBracketIndex - openBracketIndex + 3).Insert(openBracketIndex - 2, System.Math.Log(new Therm(therm.Substring(openBracketIndex + 1, closedBracketIndex - openBracketIndex - 1)).GetResult()).ToString());
                }
                else
                {
                    therm = therm.Replace('p', '(').Replace('c', ')');
                    therm = therm.Remove(openBracketIndex, closedBracketIndex - openBracketIndex + 1).Insert(openBracketIndex, new Therm(therm.Substring(openBracketIndex + 1, closedBracketIndex - openBracketIndex - 1)).GetResult().ToString());
                }
            }
            return therm;
        }

        string ReverseString(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}