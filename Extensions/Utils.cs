using System;
using System.Linq;
using System.Reflection;
using Microsoft.CodeAnalysis.CSharp;

namespace CsConsole
{
    public static class Utils
    {
        public static string GetVersion(LanguageVersion ver)
            => LanguageVersionFacts.ToDisplayString(LanguageVersionFacts.MapSpecifiedToEffectiveVersion(ver));

        public static string GetArgumentString<T>(string name, BindingFlags flags) where T : class
        {
            var m = typeof(T).GetMethod(name, flags);
            var ps = m.GetParameters();

            return string.Join(' ', ps.Select(l => $"[{l.Name}{(l.IsDefined(typeof(ArgumentsAttribute)) ? "..." : string.Empty)} :{l.ParameterType.Name.Split('.').Last()}]"));
        }
    }
}