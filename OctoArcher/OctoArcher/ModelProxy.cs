using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace OctoArcher
{
    class ModelProxy : ViewListener
    {
        private Socket socket;
        private StreamReader reader;
        private StreamWriter writer;

        public ModelListener View { get; set; }

        public ModelProxy(Socket socket)
        {
            this.socket = socket;
            Stream s = new NetworkStream(socket);
            this.reader = new StreamReader(s);
            this.writer = new StreamWriter(s);
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
                        case "m": //moved
                            Player p = new Player(int.Parse(cmd[1]));
                            p.X = int.Parse(cmd[2]);
                            p.Y = int.Parse(cmd[3]);
                            p.dX = int.Parse(cmd[4]);
                            p.dY = int.Parse(cmd[5]);
                            View.playerMoving(p);
                            break;
                        case "s": //game start
                            View.startGame();
                            break;
                        case "e": // game end
                            View.endGame();
                            break;
                        case "r": // player removed
                            Player pr = new Player(int.Parse(cmd[1]));
                            View.playerRemoved(pr);
                            break;
                    }
                }
            });
        }



        public void makeMove(Player p, float dx, float dy)
        {
            Console.WriteLine("ModelProxy Sending command m {0} {1} {2}", p.Id, p.X, p.Y);
            writer.WriteLine("m {0} {1} {2}", p.Id, p.X, p.Y);
        }

        public void addPlayer(Player p)
        {
            Console.WriteLine("ModelProxy Sending command a {0} {1} {2} {3} {4}", p.Id, p.X, p.Y, p.dX, p.dY);
            writer.WriteLine("a {0} {1} {2} {3} {4}", p.Id, p.X, p.Y, p.dX, p.dY);
        }

        public void removePlayer(Player p)
        {
            Console.WriteLine("ModelProxy Sending command r {0}", p.Id);
            writer.WriteLine("r {0}", p.Id);
        }
    }
}
