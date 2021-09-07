using System;
using System.Collections.Generic;
using System.Linq;

namespace CsConsole
{
    public partial class CommandExecutor
    {
        public void RunCommand(string str)
        {
            var sp = str.Split(' ');

            var cmd = sp[0][1..];
            var arg = sp[1..];

            Action<string[]> act = cmd switch
            {
                "help" => Help,
                "clear" => Clear,
                "exit" => Exit,
                _ => NotFound
            };

            act(arg);

            void NotFound(string[] _)
                => Console.WriteLine($"Cannot found command - [{cmd}]. Use [`help] to get imformation.");
        }

        public void Help(string[] _)
        {

        }

        public void Clear(string[] _)
            => Console.Clear();

        public void Exit(string[] _)
            => Environment.Exit(0);
    }
}