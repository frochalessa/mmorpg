using Bindings;
using System;
using System.Net.Sockets;

namespace Server
{
    class Client
    {
        public static Client[] instance = new Client[Constants.MAX_PLAYERS];

        public int index;
        public string ip;
        public Socket socket;
        public bool closing = true;
        public byte[] buffer = new byte[1024];

        public void Start()
        {
            socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), socket);
            closing = false;
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            Socket socket = (Socket)ar.AsyncState;

            try
            {
                int received = socket.EndReceive(ar);
                if (received <= 0)
                {
                    CloseClient(index);
                }
                else
                {
                    byte[] databuffer = new byte[received];
                    Array.Copy(buffer, databuffer, received);
                    ServerHandleData.instance.HandleData(index, databuffer);
                    socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), socket);
                }
            }
            catch
            {
                CloseClient(index);
            }
        }

        private void CloseClient(int index)
        {
            closing = true;
            Console.WriteLine(">> Connection from {0} has been terminated.", ip);
            socket.Close();
            socket = null;
        }
    }
}
