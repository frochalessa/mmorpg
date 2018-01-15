using Bindings;

namespace Client
{
    class ClientSendData
    {
        public static ClientSendData instance = new ClientSendData();

        public void SendLogin()
        {
            PacketBuffer buffer = new PacketBuffer();
            buffer.WriteInteger((int)ClientPackets.CLogin);
            buffer.WriteString(Globals.loginUsername);
            buffer.WriteString(Globals.loginPassword);
            ClientTcp.instance.SendData(buffer.ToArray());
            buffer.Dispose();
        }

        public void SendRegister()
        {
            PacketBuffer buffer = new PacketBuffer();
            buffer.WriteInteger((int)ClientPackets.CRegister);
            buffer.WriteString(Globals.regUsername);
            buffer.WriteString(Globals.regPassword);
            ClientTcp.instance.SendData(buffer.ToArray());
            buffer.Dispose();
        }
    }
}
