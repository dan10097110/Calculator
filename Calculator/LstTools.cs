using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    static class LstTools
    {
        public static bool AExistsAndInfrontOfB(List<string> s, string a, string b)
        {
            return s.Contains(a) && (s.IndexOf(a) < s.IndexOf(b) || !s.Contains(b));
        }

        public static List<string> Replace(List<string> ls, int index, string item)
        {
            List<string> list = new List<string>(ls);
            list.RemoveAt(index);
            list.Insert(index, item);
            return list;
        }

        public static List<string> Replace(List<string> list, int startIndex, int endIndex, string item)
        {
            List<string> newList = new List<string>(list);
            newList.RemoveRange(startIndex, endIndex - startIndex + 1);
            newList.Insert(startIndex, item);
            return newList;
        }

        public static List<string> Reverse(List<string> list)
        {
            List<string> newList = new List<string>(list);
            newList.Reverse();
            return newList;
        }
    }
}
