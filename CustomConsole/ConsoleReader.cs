using System;
using System.Collections.Generic;
using System.Linq;

namespace CsConsole.CustomConsole
{
    public class ConsoleReader
    {
        private static string _buffer = string.Empty;
        private static string Buffer
        {
            get => _buffer;
            set
            {
                var ind = _buffer.Length - Index;
                _buffer = value;
                Console.Write("\r" + " ".Multiple(Console.BufferWidth - 1));
                Console.Write($"\rC# > {_buffer}");
                Index = _buffer.Length - ind;
            }
        }
        
        private static int _index = 0;
        private static int Index
        {
            get => _index;
            set
            {
                _index = value;
                Console.CursorLeft = "C# > ".Length + _index;
            }
        }

        public static string ReadLine()
        {
            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.LeftArrow:
                        if (Index != 0)
                            Index--;
                        break;
                    case ConsoleKey.RightArrow:
                        if (Index != Buffer.Length)
                            Index++;
                        break;
                    case ConsoleKey.UpArrow:
                        break;
                    case ConsoleKey.DownArrow:
                        break;
                    case ConsoleKey.Escape:
                        Buffer = string.Empty;
                        Index = 0;
                        break;
                    case ConsoleKey.Enter:
                        Console.WriteLine();
                        Buffer = string.Empty;
                        Index = 0;
                        break;
                    case ConsoleKey.Backspace:
                        Buffer.Remove(Index - 1);
                        break;
                    default:
                        Buffer.Insert(Index, key.KeyChar.ToString());
                        break;
                }
            } while (key.Key != ConsoleKey.Enter);

            return Buffer;
        }
    }
}