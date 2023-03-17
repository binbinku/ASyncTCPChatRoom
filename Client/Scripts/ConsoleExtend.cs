using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Scripts
{
    public static class ConsoleExtend
    {
        public static void Client(string message)
        {
            SFC(ConsoleColor.Green);
            Console.Write($"\n[Client]:");
            Resume();
            Console.Write(message);
        }

        public static void Message(string message)
        {
            SFC(ConsoleColor.White);
            Console.Write($"\n[Message]:");
            Resume();
            Console.Write(message + "\n");
        }

        public static void CallBack(string message)
        {
            SFC(ConsoleColor.Cyan);
            Console.Write($"\n[CallBack]:");
            Resume();
            Console.Write(message);
        }

        public static void Warning(string message)
        {
            SFC(ConsoleColor.Yellow);
            Console.Write($"\n[Warning]:");
            Resume();
            Console.Write(message);
        }

        public static void Update(string message)
        {
            SFC(ConsoleColor.Blue);
            Console.Write($"\n[Update]:");
            Resume();
            Console.Write(message);
        }

        public static void Error(string message)
        {
            SFC(ConsoleColor.Red);
            Console.Write($"\n[Error]:");
            Resume();
            Console.Write(message);
        }

        public static void ErrorB(string message)
        {
            SBC(ConsoleColor.Red);
            Console.Write($"\n[Error]:");
            Console.Write(message);
            Resume();
        }

        public static void Print(ConsoleColor fc, ConsoleColor bc, string message)
        {
            SFC(fc);
            SBC(bc);
            Console.WriteLine($"{message}");
            Resume();
        }

        internal static void SBC(ConsoleColor cc)
        {
            Console.BackgroundColor = cc;
        }

        internal static void SFC(ConsoleColor cc)
        {
            Console.ForegroundColor = cc;
        }

        internal static void Resume()
        {
            SFC(ConsoleColor.White);
            SBC(ConsoleColor.Black);
        }
    }

}