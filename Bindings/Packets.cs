namespace Bindings
{
    //get send from server to client
    //client has to listen to serverpackets
    public enum ServerPackets
    {
        SAlertMsg = 1,
    }
    //get send from client to server
    //server has to listen to clientpackets
    public enum ClientPackets
    {
        CRegister = 1,
        CLogin,
    }
}
