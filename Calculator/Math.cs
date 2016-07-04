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
            if (n % 1 == 0)
            {
                ulong result = 1;
                for (ulong i = 1; i <= n; i++)
                {
                    result *= i;
                }
                return result;
            }
            else
                return 0;
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
            for (;;)
            {
                ushort index = 0;
                if (AInfrontOfB(reversedTherm, "+", "-"))
                {
                    index = (ushort)reversedTherm.IndexOf('+');
                    result = new Therm(ReverseString(reversedTherm.Substring(index + 1))).GetResult() + new Therm(ReverseString(reversedTherm.Remove(index))).GetResult();
                }
                else if (reversedTherm.Contains('-'))
                {
                    index = (ushort)reversedTherm.IndexOf('-');
                    result = new Therm(ReverseString(reversedTherm.Substring(index + 1))).GetResult() - new Therm(ReverseString(reversedTherm.Remove(index))).GetResult();
                }
                else if (AInfrontOfB(reversedTherm, "*", "/"))
                {
                    index = (ushort)reversedTherm.IndexOf('*');
                    result = new Therm(ReverseString(reversedTherm.Substring(index + 1))).GetResult() * new Therm(ReverseString(reversedTherm.Remove(index))).GetResult();
                }
                else if (reversedTherm.Contains('/'))
                {
                    index = (ushort)reversedTherm.IndexOf('/');
                    result = new Therm(ReverseString(reversedTherm.Substring(index + 1))).GetResult() / new Therm(ReverseString(reversedTherm.Remove(index))).GetResult();
                }
                else if (AInfrontOfB(reversedTherm, "^", "√"))
                {
                    index = (ushort)reversedTherm.IndexOf('^');
                    result = System.Math.Pow(new Therm(ReverseString(reversedTherm.Substring(index + 1))).GetResult(), new Therm(ReverseString(reversedTherm.Remove(index))).GetResult());
                }
                else if (reversedTherm.Contains('√'))
                {
                    index = (ushort)reversedTherm.IndexOf('√');
                    result = System.Math.Pow(new Therm(ReverseString(reversedTherm.Substring(index + 1))).GetResult(), 1 / new Therm(ReverseString(reversedTherm.Remove(index))).GetResult());
                }
                else if (reversedTherm.Contains('!'))
                    Math.Factorial((ulong)new Therm(ReverseString(reversedTherm.Substring(reversedTherm.IndexOf('!') + 1))).GetResult());
                else
                    result = Convert.ToDouble(therm);
                break;
            }
        }

        string CalculateBrackets(string therm)
        {
            while (therm.Contains('(') || therm.Contains(')'))
            {
                ushort i = 0, openBracketIndex = 0, closedBracketIndex = 0;
                do
                {
                    if (AInfrontOfB(therm, "(", ")"))
                    {
                        ushort posA = (ushort)therm.IndexOf('(');
                        therm = therm.Remove(posA, 1).Insert(posA, "o");
                        if (i++ == 0)
                            openBracketIndex = posA;
                    }
                    else if(therm.Contains(")"))
                    {
                        ushort posZ = (ushort)therm.IndexOf(')');
                        therm = therm.Remove(posZ, 1).Insert(posZ, "c");
                        if (--i == 0)
                            closedBracketIndex = posZ;
                    }
                } while (i != 0);
                therm = therm.Replace('o', '(').Replace('c', ')');
                therm = therm.Remove(openBracketIndex, closedBracketIndex - openBracketIndex + 1).Insert(openBracketIndex, new Therm(therm.Substring(openBracketIndex + 1, closedBracketIndex - openBracketIndex - 1)).GetResult().ToString());
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

    static class Console
    {
        [DllImport("Kernel32")]
        public static extern void AllocConsole();

        [DllImport("Kernel32")]
        public static extern void FreeConsole();

        public static void Open()
        {
            AllocConsole();
        }
    }
}
