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
            return new Therm(therm).GetErgebnis();
        }
    }

    class Therm
    {
        string therm;
        Therm first, second;
        byte rechenart = 0;
        List<string> klammerContent = new List<string>();
        double ergebnis;

        public Therm(string therm)
        {
            try
            {
                //prozent wird unsicher ersetzt
                therm = therm.Replace("%", "/100").Replace("π", System.Math.PI.ToString()).Replace("e", System.Math.E.ToString());
                this.therm = therm;
                SplitTherm();
                Calculate();
            }
            catch
            {
                Console.Open();
                System.Console.WriteLine("unkown error");
            }
        }

        int PositionOfCharInFrontOfIndex(char c, int pos)
        {
            return 1;
        }

        ulong Fakultät(ulong n)
        {
            ulong result = 1;
            for (ulong i = 1; i <= n; i++)
            {
                result *= i;
            }
            return result;
        }

        public double GetErgebnis()
        {
            return ergebnis;
        }

        void Calculate()
        {
            switch (rechenart)
            {
                case 1: ergebnis = first.GetErgebnis() + second.GetErgebnis(); break;
                case 2: ergebnis = first.GetErgebnis() - second.GetErgebnis(); break;
                case 3: ergebnis = first.GetErgebnis() * second.GetErgebnis(); break;
                case 4: ergebnis = first.GetErgebnis() / second.GetErgebnis(); break;
                case 5: ergebnis = System.Math.Pow(first.GetErgebnis(), second.GetErgebnis()); break;
                case 6: ergebnis = System.Math.Pow(second.GetErgebnis(), 1 / first.GetErgebnis()); break;
                default: ergebnis = Convert.ToDouble(therm); break;
            }
        }

        bool AInfrontOfB(string s, string a, string b)
        {
            return s.Contains(a) && (s.IndexOf(a) < s.IndexOf(b) || !s.Contains(b));
        }

        string FakultätBerechnen(string stringtherm)
        {
            try
            {
                while (stringtherm.Contains("!"))
                {
                    int i = stringtherm.IndexOf("!");
                    string s = stringtherm.Remove(i);
                    s = ReverseString(s);
                    List<int> pos = new List<int>();
                    if (s.Contains("+"))
                        pos.Add(s.IndexOf("+"));
                    else if (s.Contains("-"))
                        pos.Add(s.IndexOf("-"));
                    else if (s.Contains("*"))
                        pos.Add(s.IndexOf("*"));
                    else if (s.Contains("/"))
                        pos.Add(s.IndexOf("/"));
                    string smallTherm = "";
                    if (pos.Count == 0)
                    {
                        smallTherm = ReverseString(s);
                        ulong fakultät = Fakultät((ulong)(new Therm(smallTherm).GetErgebnis()));
                        stringtherm = stringtherm.Remove(0, i + 1);
                        stringtherm = stringtherm.Insert(0, fakultät.ToString());
                    }
                    else
                    {
                        int smallest = int.MaxValue;
                        foreach (int p in pos)
                            smallest = System.Math.Min(smallest, p);
                        smallTherm = ReverseString(s.Remove(smallest));
                        ulong fakultät = Fakultät((ulong)(new Therm(smallTherm).GetErgebnis()));
                        stringtherm = stringtherm.Remove(smallest + 1, i - smallest);
                        stringtherm = stringtherm.Insert(smallest + 1, fakultät.ToString());
                    }
                }
                return stringtherm;
            }
            catch
            {
                Console.Open();
                System.Console.WriteLine("error in fakultät RAUSRECHNUNG");
            }
        }

        void SplitTherm()
        {
            string rTherm = ReverseString(ReplaceKlammern(therm));
            rTherm = ReverseString(FakultätBerechnen(ReverseString(rTherm)));
            therm = ReverseString(ReplaceKlammern(InsertContent(rTherm)));
            for (;;)
            {
                if (rTherm != "c")
                {
                    byte pos = 0;
                    if (AInfrontOfB(rTherm, "+", "-"))
                    {
                        rechenart = 1;
                        pos = (byte)rTherm.IndexOf('+');
                    }
                    else if (rTherm.Contains('-'))
                    {
                        rechenart = 2;
                        pos = (byte)rTherm.IndexOf('-');
                    }
                    else if (AInfrontOfB(rTherm, "*", "/"))
                    {
                        rechenart = 3;
                        pos = (byte)rTherm.IndexOf('*');
                    }
                    else if (rTherm.Contains('/'))
                    {
                        rechenart = 4;
                        pos = (byte)rTherm.IndexOf('/');
                    }
                    else if (AInfrontOfB(rTherm, "^", "√"))
                    {
                        rechenart = 5;
                        pos = (byte)rTherm.IndexOf('^');
                    }
                    else if(rTherm.Contains('√'))
                    {
                        rechenart = 6;
                        pos = (byte)rTherm.IndexOf('√');
                    }
                    if(rechenart != 0)
                    {
                        first = new Therm(InsertContent(ReverseString(rTherm.Substring(pos + 1))));
                        second = new Therm(InsertContent(ReverseString(rTherm.Remove(pos))));
                    }
                    break;
                }
                else
                {
                    rTherm = InsertContent(rTherm);
                    rTherm = rTherm.Remove(0, 1);
                    rTherm = rTherm.Remove(rTherm.Length - 1, 1);
                    therm = ReverseString(ReplaceKlammern(rTherm));
                }
            }
        }

        string ReplaceKlammern(string s)
        {
            while (s.Contains('(') || s.Contains(')'))
            {
                byte i = 0, posKlammerAuf = 0, posKlammerZu = 0;
                do
                {
                    if (AInfrontOfB(s, "(", ")"))
                    {
                        byte posA = (byte)s.IndexOf('(');
                        s = s.Remove(posA, 1);
                        s = s.Insert(posA, "a");
                        if (i == 0) posKlammerAuf = posA;
                        i++;
                    }
                    else
                    {
                        byte posZ = (byte)s.IndexOf(')');
                        s = s.Remove(posZ, 1);
                        s = s.Insert(posZ, "z");
                        i--;
                        if (i == 0) posKlammerZu = posZ;
                    }
                } while (i != 0);
                s = s.Replace('a', '(').Replace('z', ')');
                klammerContent.Add(s.Substring(posKlammerAuf, posKlammerZu - posKlammerAuf + 1));
                s = s.Remove(posKlammerAuf, posKlammerZu - posKlammerAuf + 1);
                s = s.Insert(posKlammerAuf, "c");
            }
            return s;
        }

        string InsertContent(string therm)
        {
            while (therm.Contains('c'))
            {
                byte pos = (byte)therm.IndexOf('c');
                therm = therm.Remove(pos, 1);
                therm = therm.Insert(pos, klammerContent.First());
                klammerContent.Remove(klammerContent.First());
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
