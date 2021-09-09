using System;
using System.Collections.Generic;
using System.Linq;

namespace CsConsole
{
    public static class Extension
    {
        public static string Multiple(this string s, int count)
        {
            string r = string.Empty;
            for (int i = 0; i < count; i++)
            {
                r += s;
            }
            return r;
        }

        public static IEnumerable<T> Resize<T>(this IEnumerable<T> vs, int size, T defaultValue = default(T))
        {
            var c = vs.Count();

            if (c < size)
                return vs.Take(size);
            else if (c == size)
                return vs;
            else
            {
                for (int i = 0; i < c - size; i++)
                    vs = vs.Append(defaultValue);

                return vs;
            }
        }
    }
}