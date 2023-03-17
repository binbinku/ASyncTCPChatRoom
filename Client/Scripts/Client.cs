using System.Text;
using System.Net;
using System.Net.Sockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Scripts.Entity;

namespace Client.Scripts
{
    public class ClientRole
    {
        private static Socket clientSocket;

        private static string clientVersion = "V0.01";

        private static string author = "#BK";

        private static string serverIP = "127.0.0.1";

        private static int serverPort = 5418;

        private static IPEndPoint serverIPEndPoint;

        private static byte[] recData;

        public static void Init()
        {

            ConsoleExtend.Client("启动初始化程序");

            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            serverIPEndPoint = new IPEndPoint(IPAddress.Parse(serverIP), serverPort);

            //1.同步连接
            //SyncConnect(iPEndPoint);
            //2.异步连接
            ASyncConnect(serverIPEndPoint);

        }

        public static void SyncConnect(IPEndPoint iPEndPoint)
        {
            ConsoleExtend.Client($"正在连接服务器:{serverIP}:{serverPort}");

            clientSocket.Connect(iPEndPoint);

            while (true)
            {
                byte[] data = new byte[1024];

                clientSocket.Receive(data);

                string str = Encoding.UTF8.GetString(data);

                ConsoleExtend.Message(str);
            }


            ConsoleExtend.Client($"已连接到服务器");
        }

        private static void ASyncConnect(IPEndPoint iPEndPoint)
        {
            ConsoleExtend.Client("开始异步连接服务器");
            clientSocket.BeginConnect(iPEndPoint, ASyncConnectCallBack, clientSocket);
        }

        private static void ASyncConnectCallBack(IAsyncResult ar)
        {
            ConsoleExtend.CallBack("异步连接回调");

            Socket cs = null;

            try
            {
                cs = ar.AsyncState as Socket;

                if (cs != null)
                {
                    cs.EndConnect(ar);
                    ConsoleExtend.Client("已与服务器成功建立连接");
                    ASyncReceive();
                    SyncSend();
                }
            }
            catch (SocketException ex)
            {
                ConsoleExtend.Error("连接服务器超时");
                Thread.Sleep(5000);
                ConsoleExtend.Warning("尝试重新连接");
                cs.BeginConnect(serverIPEndPoint, ASyncConnectCallBack, cs);
            }

        }

        public static void ASyncReceive()
        {
            recData = new byte[1024];

            ConsoleExtend.Client("开始异步接收消息");
            clientSocket.BeginReceive(recData, 0, 1024, 0, ASyncReceiveCallBack, clientSocket);

        }

        public static void ASyncReceiveCallBack(IAsyncResult ar)
        {
            ConsoleExtend.CallBack("接收回调");
            int recLength = -1;
            try
            {
                recLength = clientSocket.EndReceive(ar);

                string recStr = Encoding.UTF8.GetString(recData);

                ConsoleExtend.Message(recStr);

                clientSocket.BeginReceive(recData, 0, 1024, 0, ASyncReceiveCallBack, clientSocket);

            }
            catch (SocketException ex)
            {
                ConsoleExtend.Error("异步接收异常" + ex.ToString());
            }


        }

        public static void SyncSend()
        {

            Console.WriteLine();
            string sendStr = "";

            while ((sendStr = Console.ReadLine()) != "quit")
            {
                byte[] sendData = Encoding.UTF8.GetBytes($"[{User.Instance.Name}]:" + sendStr);

                clientSocket.Send(sendData);
            }

        }

        public static void InitUser()
        {
            User user = new User();

            ConsoleExtend.Warning("输入昵称:");

            string userName = Console.ReadLine();

            user.Name = userName;
        }

        public static void PrintClientInfo()
        {

            string asciiText =
@" ____  _  __   ____ _     ___ _____ _   _ _____ 
| __ )| |/ /  / ___| |   |_ _| ____| \ | |_   _|
|  _ \| ' /  | |   | |    | ||  _| |  \| | | |  
| |_) | . \  | |___| |___ | || |___| |\  | | |  
|____/|_|\_\  \____|_____|___|_____|_| \_| |_|  
";
            Console.Title = $"Client{clientVersion}";

            ConsoleExtend.Print(ConsoleColor.Green, ConsoleColor.Black, asciiText);

            ConsoleExtend.Print(ConsoleColor.Cyan, ConsoleColor.Black, $"作者:{author}");

            ConsoleExtend.Warning("异步TCP聊天室[客户端]");

            ConsoleExtend.Update($"当前版本:{clientVersion}");

        }
    }
}