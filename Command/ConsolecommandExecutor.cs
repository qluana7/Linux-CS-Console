using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CsConsole.Configuration;

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

            (string Argument, ParameterInfo Parameter)[] zp = arg.Zip(p.Resize(arg.Count())).ToArray();

            for (int i = 0; i < zp.Length; i++)
            {
                var g = zp[i];

                if (g.Parameter.IsDefined(typeof(ArgumentsAttribute)))
                {
                    obj.Add(string.Join(' ', arg.Skip(i)));
                    break;
                }

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
                    m.Invoke(null, new object[] { arg[0], string.Empty });
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
        public static void Config(string command, [Arguments] string arg)
        {
            var args = arg.Split(' ');

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
                Console.WriteLine($"{n}: {n} {Utils.GetArgumentString<Commands>("Config", BindingFlags.Public | BindingFlags.Static)}");
            }

            void Config_Import(string[] s)
            {
                if (s.Length == 1 && !new string[] { "help", "list" }.Contains(s[0]))
                {
                    Import_Help();
                    return;
                }

                switch (s[0])
                {
                    case "list":
                        Import_List();
                        break;
                    case "add":
                        Import_Add(s[1..]);
                        break;
                    case "remove":
                        if (int.TryParse(s[1], out int i) && i > 0)
                            Import_Remove(i);
                        else
                            Console.WriteLine("Put valid number.");
                        break;
                    case "set":
                        Import_Set(s[1..]);
                        break;
                    case "help":
                        Import_Help();
                        break;
                    default:
                        Import_Help();
                        break;
                }

                void Import_List()
                {
                    var imp = Program.Manager.Configure.Configure.Imports.Distinct().ToArray();
                    Console.WriteLine(string.Concat(imp.Select(l => $"{Array.IndexOf<string>(imp, l) + 1}. {l}\n")));

                    Console.WriteLine("Total counts : " + imp.Length);
                }

                void Import_Add(string[] imports)
                {
                    var con = Program.Manager.Configure.Configure;
                    var imp = con.Imports as List<string>;

                    imp.AddRange(imports);

                    Program.Manager.Configure = new CommandConfigure()
                    {
                        Configure = new CommandExecutorConfigure()
                        {
                            Imports = imp,
                            Version = con.Version
                        }
                    };
                }

                void Import_Remove(int ind)
                {
                    ind -= 1;
                    var con = Program.Manager.Configure.Configure;
                    var imp = con.Imports as List<string>;

                    if (ind >= imp.Count)
                    {
                        Console.WriteLine("Out of index.");
                        return;
                    }

                    imp.RemoveAt(ind);

                    Program.Manager.Configure = new CommandConfigure()
                    {
                        Configure = new CommandExecutorConfigure()
                        {
                            Imports = imp,
                            Version = con.Version
                        }
                    };
                }

                void Import_Set(string[] imports)
                {
                    var con = Program.Manager.Configure.Configure;

                    Program.Manager.Configure = new CommandConfigure()
                    {
                        Configure = new CommandExecutorConfigure()
                        {
                            Imports = imports,
                            Version = con.Version
                        }
                    };
                }

                void Import_Help()
                {
                    Console.WriteLine("Usage: config import [command] (args..)\n\n" +
                                      "Manage pre-imports list. pre-imports are include when using in console to compile and to run.\n\n" +
                                      "Commands : \n" +
                                      "  list                   - Show current pre-imports list\n" +
                                     $"  add [imports..]        - Add pre-import string to list.\n" +
                                     $"  remove [index:+number] - Remove pre-import string to list with index.\n" +
                                      "  set [imports..]        - Set new pre-import list.\n");
                }
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