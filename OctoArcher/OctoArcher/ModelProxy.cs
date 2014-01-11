using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace OctoArcher
{
    class ModelProxy
    {
        private Socket socket;
        private StreamReader reader;


        public ModelListener Model { get; set; }

        public ModelProxy(Socket socket)
        {
            this.socket = socket;
            Stream s = new NetworkStream(socket);
            this.reader = new StreamReader(s);

            this.startListener();
        }

        private void startListener()
        {
            Thread readerThread = new Thread(() =>
            {
                while (true)
                {
                    string command = reader.ReadLine();
                    Console.WriteLine("Received command {0} from server", command);
                    string[] cmd = command.Split(' ');
                    switch (cmd[0])
                    {
                        case "m":
                            break;
                        case "s":
                            break;
                        case "e":
                            break;
                    }
                }
            });
        }


    }
}
