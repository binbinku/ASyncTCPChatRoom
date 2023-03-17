using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Scripts;

namespace Client
{
    public class Start
    {
        public static void Main(string[] args)
        {

            ClientRole.PrintClientInfo();
            
            ClientRole.InitUser();

            ClientRole.Init();

            ClientRole.SyncSend();
            //Console.ReadLine();
        }
    }
}