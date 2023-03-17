using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server
{
    public class Start
    {
        public static void Main(string[] args)
        {
            Server.PrintServerInfo();

            Server.Start();

            Server.CommandHandler();
        }
    }
}