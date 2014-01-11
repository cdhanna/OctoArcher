﻿using System;
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


        public ModelListener View { get; set; }

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
                            Player p = new Player(int.Parse(cmd[1]));
                            p.X = int.Parse(cmd[2]);
                            p.Y = int.Parse(cmd[3]);
                            p.dX = int.Parse(cmd[4]);
                            p.dY = int.Parse(cmd[5]);
                            View.playerMoving(p);
                            break;
                        case "s":
                            View.startGame();
                            break;
                        case "e":
                            View.endGame();
                            break;
                    }
                }
            });
        }



        public void makeMove(Player p, float dx, float dy)
        {
            throw new NotImplementedException();
        }

        public void addPlayer(Player p)
        {
            throw new NotImplementedException();
        }

        public void removePlayer(Player p)
        {
            throw new NotImplementedException();
        }
    }
}
