using System;
using System.Collections.Generic;
using System.Text;

namespace Bindings
{
    class Types
    {
        public static PlayerStruct[] _player = new PlayerStruct[Constants.MAX_PLAYERS];

        [Serializable]
        public struct PlayerStruct
        {
            //Account
            public string username;
            public string password;

            //General
            public string name;
            public int sprite;
            public int level;
            public int exp;

            //Position
            public int map;
            public int x;
            public int y;
            public byte dir;

            //Client only
            public int xOffset;
            public int yOffset;
            public int Moving;
            public byte Steps;
        }
    }
}
