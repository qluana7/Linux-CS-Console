using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CsConsole.Command
{
    public partial class CommandExecutor
    {
        public MethodInfo[] Methods { get; }

        public ArgumentsConverter Converter { get; }

        public void RunCommand(string str)
        {
            var sp = str.Split(' ');

            var cmd = sp[0][1..];
            var arg = sp[1..];

            var m = Methods.FirstOrDefault(l => l.Name.ToLower() == cmd);

            if (m == null)
            {
                NotFound();
                return;
            }

            var p = m.GetParameters();

            List<object> obj = new List<object>();

            (string Argument, ParameterInfo Parameter)[] zp = arg.Zip(p).ToArray();

            for (int i = 0; i < zp.Length; i++)
            {
                var g = zp[i];

                if (g.Parameter.IsDefined(typeof(ParamArrayAttribute)))
                    arg.Skip(i);

                try
                {
                    var o = Converter.Convert(g.Argument, g.Parameter.ParameterType);
                    obj.Add(o);
                }
                catch (ArgumentException)
                { }
            }

            try
            {
                m.Invoke(null, obj.Any() ? obj.ToArray() : null);
            }
            catch (TargetParameterCountException)
            {
                if (cmd == "config")
                {
                    m.Invoke(null, new object[] { "help", new string[0] });
                    return;
                }
            }

            void NotFound()
                => Console.WriteLine($"Cannot found command - [{cmd}]. Use [`help] to get imformation.");
        }
    }

    public class Commands
    {
        [Description("Setting C# console options.")]
        public static void Config(string command, params string[] args)
        {
            switch (command)
            {
                case "import":
                    Config_Import(args);
                    break;
                case "version":
                    Config_Version(args);
                    break;
                case "help":
                    Config_Help();
                    break;
                default:
                    Config_Help();
                    break;
            }

            void Config_Help()
            {
                var n = nameof(Config).ToLower();
                Console.WriteLine($"{n}: {n} ");
            }

            void Config_Import(string[] s)
            {

            }

            void Config_Version(string[] s)
            {

            }
        }

        [Description("Get C# console tools commands list.")]
        public static void Help()
        {
            Console.WriteLine("**C# Console Tools Commands List**\n");
            
            var m = typeof(Commands).GetMethods(BindingFlags.Static | BindingFlags.Public);
            var str = m.Select(l => $"{l.Name.ToLower(), -7} {l.GetCustomAttribute<DescriptionAttribute>().Description}");

            Console.WriteLine(string.Join('\n', str));
        }

        [Description("Clear the C# console.")]
        public static void Clear()
            => Console.Clear();

        [Description("Exit C# console.")]
        public static void Exit()
            => Environment.Exit(0);
    }
}