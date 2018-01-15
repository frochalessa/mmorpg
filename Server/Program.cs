using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        private static Thread consoleThread;

        static void Main(string[] args)
        {
            consoleThread = new Thread(new ThreadStart(ConsoleThread));
            consoleThread.Start();
            Console.WriteLine("The Wata Server - Version 1.0");
            Console.WriteLine("A server developed by Fellipe Rocha");
            Console.WriteLine("");
            General.instance.Start();

        }

        private static void ConsoleThread()
        {
            Console.ReadLine();
        }
    }
}
