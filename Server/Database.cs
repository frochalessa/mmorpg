using Bindings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Database
    {
        public static Database instance = new Database();

        public void CheckPath(string folder)
        {
            Console.WriteLine(">> Checking: .../" + Constants.PATH_DATA + folder);
            if (!Directory.Exists(Constants.PATH_DATA + folder))
            {
                Directory.CreateDirectory(Constants.PATH_DATA + folder);
            }
        }

        public bool AccountExist(string username)
        {
            if (!File.Exists(Constants.PATH_DATA + Constants.PATH_ACCOUNT + username + Constants.FILE_EXTENSION))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool PasswordOk(int index, string username, string password)
        {
            Stream stream = File.Open(Constants.PATH_DATA + Constants.PATH_ACCOUNT + "/" + username + Constants.FILE_EXTENSION, FileMode.Open);
            BinaryFormatter bf = new BinaryFormatter();
            Types._player[index] = (Types.PlayerStruct)bf.Deserialize(stream);
            stream.Close();

            if (Types._player[index].password == password)
                return true;
            else
                return false;
        }

        public void AddAccount(int index, string username, string password)
        {
            ClearPlayer(index);

            Types._player[index].username = username;
            Types._player[index].password = password;
            Types._player[index].level = 1;

            SavePlayer(index);
        }

        public void ClearPlayer(int index)
        {
            Types._player[index].username = "";
            Types._player[index].password = "";
            Types._player[index].name = "";
        }

        public void SavePlayer(int index)
        {
            Stream stream = File.Open(Constants.PATH_DATA + Constants.PATH_ACCOUNT + "/" + Types._player[index].username + Constants.FILE_EXTENSION, FileMode.OpenOrCreate);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(stream, Types._player[index]);
            stream.Close();
        }

        public void LoadPlayer(int index, string username)
        {
            Stream stream = File.Open(Constants.PATH_DATA + Constants.PATH_ACCOUNT + "/" + username + Constants.FILE_EXTENSION, FileMode.Open);
            BinaryFormatter bf = new BinaryFormatter();
            Types._player[index] = (Types.PlayerStruct)bf.Deserialize(stream);
            stream.Close();
        }
    }
}
