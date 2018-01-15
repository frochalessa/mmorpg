using System;
using System.Collections.Generic;
using Bindings;

namespace Server
{
    class ServerHandleData
    {
        public static ServerHandleData instance = new ServerHandleData();

        private delegate void Packets(int index, byte[] data);
        private static Dictionary<int, Packets> _packets;

        public void Start()
        {
            Console.WriteLine(">> Initialize Server Packets...");
            _packets = new Dictionary<int, Packets>
            {
                //add here your server packets
                { (int)ClientPackets.CLogin, HandleLogin },
                { (int)ClientPackets.CRegister, HandleRegister },
            };
        }

        public void HandleData(int index, byte[] data)
        {
            PacketBuffer buffer = new PacketBuffer();
            buffer.WriteBytes(data);
            int packet = buffer.ReadInteger();
            buffer.Dispose();
            if (_packets.TryGetValue(packet, out Packets _packet))
            {
                _packet.Invoke(index, data);
            }
        }

        private void HandleLogin(int index, byte[] data)
        {
            PacketBuffer buffer = new PacketBuffer();
            buffer.WriteBytes(data);
            buffer.ReadInteger();
            string username = buffer.ReadString();
            string password = buffer.ReadString();
            buffer.Dispose();

            if (!Database.instance.AccountExist(username))
            {
                Database.instance.AddAccount(index, username, password);
                Console.WriteLine("Username does not exist.");
                return;
            }
            if (!Database.instance.PasswordOk(index, username, password))
            {
                Console.WriteLine("Password is wrong.");
                return;
            }
            Console.WriteLine("Player logged in succesfully.");
            Database.instance.LoadPlayer(index, username);
        }

        private void HandleRegister(int index, byte[] data)
        {
            PacketBuffer buffer = new PacketBuffer();
            buffer.WriteBytes(data);
            buffer.ReadInteger();
            string username = buffer.ReadString();
            string password = buffer.ReadString();

            if (!Database.instance.AccountExist(username))
            {
                Database.instance.AddAccount(index, username, password);
            }
            else
            {
                Console.WriteLine("Username already exist.");
            }
        }
    }
}
