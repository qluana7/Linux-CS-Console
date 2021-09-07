using System;

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
    }
}