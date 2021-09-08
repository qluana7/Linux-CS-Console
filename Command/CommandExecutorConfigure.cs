using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp;
using Newtonsoft.Json;

namespace CsConsole.Command
{
    public class CommandExecutorConfigure
    {
        public IEnumerable<string> Imports { get; set; } = new List<string>();

        public LanguageVersion Version { get; set; } = LanguageVersion.Latest;
    }
}