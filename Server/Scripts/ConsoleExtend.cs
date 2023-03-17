using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Scripts
{
    public static class ConsoleExtend
    {
        public static void Server(string message)
        {
            SFC(ConsoleColor.Green);
            Console.Write($"\n[Server]:");
            Resume();
            Console.Write(message);
        }

        public static void Test(string message)
        {
            SFC(ConsoleColor.Red);
            Console.Write($"\n[Test]:");
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