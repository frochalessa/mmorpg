using Bindings;
using System;

namespace Server
{
    class General
    {
        public static General instance = new General();

        public void Start()
        {
            Database.instance.CheckPath(Constants.PATH_ACCOUNT);
            PrepareServer();
            ServerHandleData.instance.Start();
            ServerTcp.instance.Start();
            Console.WriteLine(">> Server has succesfully started and is ready to accept {0} connections.", Constants.MAX_PLAYERS);

        }
        private void PrepareServer()
        {
            // Create new intances of the clients so people can connect
            for (int i = 0; i < Constants.MAX_PLAYERS; i++)
            {
                Client.instance[i] = new Client();
            }
            // Create new intances of the Players so players can connect
            for (int i = 0; i < Constants.MAX_PLAYERS; i++)
            {
                Types._player[i] = new Types.PlayerStruct();
            }
        }
    }

}
