using System;
using Microsoft.CodeAnalysis.CSharp;

namespace CsConsole
{
    public static class Utils
    {
        public static string GetVersion(LanguageVersion ver)
            => LanguageVersionFacts.ToDisplayString(LanguageVersionFacts.MapSpecifiedToEffectiveVersion(ver));
    }
}