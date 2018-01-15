using System;
using System.Net;
using System.Net.Sockets;
using Bindings;

namespace Server
{
    class ServerTcp
    {
        public static ServerTcp instance = new ServerTcp();

        private static Socket _server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private static byte[] _buffer = new byte[1024];

        public void Start()
        {
            _server.Bind(new IPEndPoint(IPAddress.Any, 5555));
            _server.Listen(10);
            _server.BeginAccept(new AsyncCallback(AcceptCallback), null);
        }

        private void AcceptCallback(IAsyncResult ar)
        {
            Socket socket = _server.EndAccept(ar);
            _server.BeginAccept(new AsyncCallback(AcceptCallback), null);

            for (int i = 0; i < Constants.MAX_PLAYERS; i++)
            {
                if (Client.instance[i].socket == null)
                {
                    Client.instance[i].socket = socket;
                    Client.instance[i].index = i;
                    Client.instance[i].ip = socket.RemoteEndPoint.ToString();
                    Client.instance[i].Start();
                    Console.WriteLine(">> Connection from '{0} received.", Client.instance[i].ip);
                    return;
                }
            }
        }
    }
}
