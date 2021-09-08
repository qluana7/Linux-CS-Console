using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CsConsole.Command
{
    public partial class CommandExecutor
    {
        public MethodInfo[] Methods { get; set; }

        public void RunCommand(string str)
        {
            var sp = str.Split(' ');

            var cmd = sp[0][1..];
            var arg = sp[1..];

            var m = Methods.FirstOrDefault(l => l.Name.ToLower() == cmd);

            // Console.WriteLine(string.Join('\n', Methods.Select(l => l.Name)));

            if (m == null)
            {
                NotFound();
                return;
            }

            m.Invoke(null, new object[] { arg });

            void NotFound()
                => Console.WriteLine($"Cannot found command - [{cmd}]. Use [`help] to get imformation.");
        }
    }

    public class Commands
    {
        private static void Config(string[] args)
        {
            
        }

        private static void Help(string[] _)
        {

        }

        private static void Clear(string[] _)
            => Console.Clear();

        private static void Exit(string[] _)
            => Environment.Exit(0);
    }
}