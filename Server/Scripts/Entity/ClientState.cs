using System.Net.Sockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Scripts.Entity
{
    public class ClientState
    {
        private Socket socket;
        private byte[] buffer;


        public ClientState()
        {
            buffer = new byte[1024];
        }

        public ClientState(Socket socket, byte[] buffer)
        {
            this.socket = socket;
            this.buffer = buffer;
        }

        public Socket M_Socket
        {
            get
            {
                return socket;
            }
            set
            {
                this.socket = value;
            }
        }


        public byte[] M_Buffer
        {
            get
            {
                return buffer;
            }
            set
            {
                this.buffer = value;
            }
        }
    }
}