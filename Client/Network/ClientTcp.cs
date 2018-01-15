using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class ClientTcp
    {
        public static ClientTcp instance = new ClientTcp();

        private static Socket _client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private static byte[] _asyncBuff = new byte[1024];

        public void Connect()
        {
            _client.BeginConnect("127.0.0.1", 5555, new AsyncCallback(ConnectCallback), _client);
        }

        private void ConnectCallback(IAsyncResult ar)
        {
            _client.EndConnect(ar);
            while (true)
            {
                OnReceive();
            }
        }

        private void OnReceive()
        {
            byte[] _sizeinfo = new byte[4];
            byte[] _receivedbuffer = new byte[1024];
            int totalread = 0, currentread = 0;

            try
            {
                currentread = totalread = _client.Receive(_sizeinfo);
                if (totalread <= 0)
                {
                    Console.WriteLine("You are not connected to the server.");
                }
                else
                {
                    while (totalread < _sizeinfo.Length && currentread > 0)
                    {
                        currentread = _client.Receive(_sizeinfo, totalread, _sizeinfo.Length - totalread, SocketFlags.None);
                        totalread += currentread;
                    }

                    int messagesize = 0;
                    messagesize |= _sizeinfo[0];
                    messagesize |= (_sizeinfo[1] << 8);
                    messagesize |= (_sizeinfo[2] << 16);
                    messagesize |= (_sizeinfo[3] << 24);

                    byte[] data = new byte[messagesize];
                    totalread = 0;
                    currentread = totalread = _client.Receive(data, totalread, data.Length - totalread, SocketFlags.None);
                    while (totalread < messagesize && currentread > 0)
                    {
                        currentread = _client.Receive(data, totalread, data.Length - totalread, SocketFlags.None);
                        totalread += currentread;
                    }

                    ClientHandleData.instance.HandleData(data);
                }
            }
            catch
            {
                Console.WriteLine("You are not connected to the server.");
            }
        }

        public void SendData(byte[] data)
        {
            _client.Send(data);
            //only debug test
            Debug.WriteLine("Sending a packet to the server!");

        }
    }
}
