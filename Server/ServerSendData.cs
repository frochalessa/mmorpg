﻿using Bindings;

namespace Server
{
    class ServerSendData
    {
        public static ServerSendData instance = new ServerSendData();

        public void SendDataTo(int index, byte[] data)
        {
            byte[] sizeinfo = new byte[4];
            sizeinfo[0] = (byte)data.Length;
            sizeinfo[1] = (byte)(data.Length >> 8);
            sizeinfo[2] = (byte)(data.Length >> 16);
            sizeinfo[3] = (byte)(data.Length >> 24);

            Client.instance[index].socket.Send(sizeinfo);
            Client.instance[index].socket.Send(data);
        }

        public void SendAlertMsg(int index, string msg)
        {
            PacketBuffer buffer = new PacketBuffer();
            buffer.WriteInteger((int)ServerPackets.SAlertMsg);
            buffer.WriteString(msg);
            SendDataTo(index, buffer.ToArray());
            buffer.Dispose();
        }

        public void SendLoginOk(int index)
        {
            PacketBuffer buffer = new PacketBuffer();
            buffer.WriteInteger((int)ServerPackets.SLoginOk);
            buffer.WriteInteger(index);
            SendDataTo(index, buffer.ToArray());
            buffer.Dispose();
        }
    }
}
