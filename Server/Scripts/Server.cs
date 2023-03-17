using System.Text;
using System.Net;
using System.Net.Sockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server.Scripts;
using Server.Scripts.Entity;

namespace Server
{
    public static class Server
    {
        private static Socket serverSocket;

        private static int serverPort = 5418;

        private static string serverVersion = "V0.01";

        private static string author = "#BK";

        private static Dictionary<Socket, ClientState> clientDic;

        public static void Start()
        {

            ConsoleExtend.Server("启动初始化程序");

            clientDic = new Dictionary<Socket, ClientState>();

            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Any, serverPort);

            serverSocket.Bind(iPEndPoint);

            ConsoleExtend.Server("初始化成功");

            Listen();

        }



        public static void Listen()
        {
            ConsoleExtend.Server($"开始监听端口:[{serverPort}]");

            serverSocket.Listen(0);

            //1.同步接收
            ASyncAccept();
        }


        public static void SyncAccept()
        {
            ConsoleExtend.Server("开始同步接收");

            while (true)
            {
                ConsoleExtend.Server("开始接受连接");

                if (serverSocket.Accept() is Socket clientAccess)
                {
                    ConsoleExtend.Server($"{clientAccess.RemoteEndPoint.ToString()} 已连接");

                    byte[] data = Encoding.UTF8.GetBytes("你好");

                    clientAccess.Send(data);

                }

            }
        }

        //异步接入
        public static void ASyncAccept()
        {
            ConsoleExtend.Server("开始异步接入");

            serverSocket.BeginAccept(ASyncAcceptCallBack, serverSocket);
        }

        //异步接入回调
        public static void ASyncAcceptCallBack(IAsyncResult ar)
        {
            try
            {
                ClientState cs = new ClientState();

                Socket client = serverSocket.EndAccept(ar);

                cs.M_Socket = client;

                clientDic.Add(client, cs);

                ConsoleExtend.Server($"客户端接入:[{client.RemoteEndPoint.ToString()}]");

                ASyncReceive(cs);

                ASyncAccept();

            }
            catch (Exception ex)
            {
                ConsoleExtend.Error("异步接入异常:" + ex.ToString());
            }

        }

        //异步接收消息
        public static void ASyncReceive(ClientState clientState)
        {
            clientState.M_Socket.BeginReceive(clientState.M_Buffer, 0, 1024, 0, ASyncReceiveCallBack, clientState.M_Socket);
            ConsoleExtend.Server($"[{clientState.M_Socket.RemoteEndPoint.ToString()}]已启动接收");
        }

        //接收消息异步回调
        public static void ASyncReceiveCallBack(IAsyncResult ar)
        {
            Socket cs = null;

            try
            {
                cs = ar.AsyncState as Socket;

                int recLength = cs.EndReceive(ar);

                ConsoleExtend.Server(cs.RemoteEndPoint.ToString() + $"收到消息:[{recLength}]");

                DistributeMessage(cs, recLength);

                ASyncReceive(clientDic[cs]);

            }
            catch (Exception ex)
            {
                ConsoleExtend.Error("接收消息异常" + ex.ToString());
            }

        }

        //消息分发
        public static void DistributeMessage(Socket client, int messageLength)
        {
            ConsoleExtend.Server($"[{client.RemoteEndPoint.ToString()}]开始消息分发");

            ClientState cs = clientDic[client];

            string recStr = Encoding.UTF8.GetString(cs.M_Buffer, 0, messageLength);

            ConsoleExtend.Test($"[{client.RemoteEndPoint.ToString()}]:" + recStr);

            foreach (var clientState in clientDic)
            {
                if(!clientState.Key.Connected)
                {
                    clientDic.Remove(clientState.Key);
                    continue;
                }

                if (clientState.Key == client)
                    continue;

                clientState.Key.Send(cs.M_Buffer);
                ConsoleExtend.Server($"[{clientState.Key.RemoteEndPoint.ToString()}]:已发送[{messageLength}]");
            }

        }


        public static void PrintServerInfo()
        {

            string asciiText =
@" ____  _  __  ____  _____ ______     _______ ____  
| __ )| |/ / / ___|| ____|  _ \ \   / / ____|  _ \ 
|  _ \| ' /  \___ \|  _| | |_) \ \ / /|  _| | |_) |
| |_) | . \   ___) | |___|  _ < \ V / | |___|  _ < 
|____/|_|\_\ |____/|_____|_| \_\ \_/  |_____|_| \_\
";
            Console.Title = $"Server{serverVersion}";

            ConsoleExtend.Print(ConsoleColor.Green, ConsoleColor.Black, asciiText);

            ConsoleExtend.Print(ConsoleColor.Cyan, ConsoleColor.Black, $"作者:{author}");

            ConsoleExtend.Warning("异步TCP聊天室[服务端]");

            ConsoleExtend.Update($"当前版本:{serverVersion}");

        }

        public static void CommandHandler()
        {
            string command = "";

            Boolean cicle = true;

            while (cicle)
            {
                command = Console.ReadLine();

                switch (command)
                {
                    case "quit":
                        ConsoleExtend.Warning("确认关闭服务器?[Y/N]");
                        string str = Console.ReadLine();
                        if (str == "Y" || str == "y")
                        {
                            cicle = false;
                        }
                        break;
                }
            }
        }

    }
}