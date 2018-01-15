using Bindings;
using System.Threading;

namespace Server
{
    class GameLogic
    {
        Timer savingPlayer;

        public void ServerLoop()
        {
            savingPlayer = new Timer(SavePlayers, null, 0, 300000);
        }

        private void SavePlayers(object o)
        {
            for (int i = 1; i < Constants.MAX_PLAYERS; i++)
            {
                //if (Globals.general.IsPlaying(i))
                //{
                //    Globals.database.SavePlayer(i);
                //}
            }
        }
    }
}
