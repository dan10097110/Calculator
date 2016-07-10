using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    static class StrTools
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
