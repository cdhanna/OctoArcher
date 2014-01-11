using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace OctoArcher
{
    class ViewProxy : ModelListener
    {
        private Socket socket;
        
        StreamReader reader;
        StreamWriter writer;
        public ViewProxy(Socket socket)
        {
            this.socket = socket;
            Stream s = new NetworkStream(socket);
            reader = new StreamReader(s);
            writer = new StreamWriter(s);
        }

        private ViewListener model;
        public ViewListener Model { get { return model; } set { model = value; startReaderThread(); } }

        private void startReaderThread()
        {
            Thread readerThread = new Thread(() =>
            {
                while (true)
                {
                    string command = reader.ReadLine();
                    Console.WriteLine("Received command {0} from client", command);

                    string[] cmd = command.Split(' ');

                    switch (cmd)
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

        public void playerMoving(Player p)
        {
            Console.WriteLine("Sending command m {0} {1} {2} {3} {4}", p.Id, p.X, p.Y, p.dX, p.dY);
            writer.WriteLine("m {0} {1} {2} {3} {4}", p.Id, p.X, p.Y, p.dX, p.dY);
        }

        public void startGame()
        {
            Console.WriteLine("Sending command s");
            writer.WriteLine("s");
        }

        public void endGame()
        {
            Console.WriteLine("Sending command e");
            writer.WriteLine("e");
        }
    }
}
