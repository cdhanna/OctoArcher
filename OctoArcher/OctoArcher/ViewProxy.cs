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

            writer.AutoFlush = true;
        }

        private Model model;
        public Model Model { get { return model; } set { model = value; startReaderThread(); } }

        private void startReaderThread()
        {
            Thread readerThread = new Thread(() =>
            {
                try
                {
                    while (true)
                    {
                        string command = reader.ReadLine();
                        Console.WriteLine("ViewProxy Received command {0} from client", command);

                        string[] cmd = command.Split(' ');
                        Player p;
                        switch (cmd[0])
                        {
                            case NetProp.MOVE_PLAYER:
                                p = new Player();
                                p.Id = int.Parse(cmd[1]);
                                model.makeMove(p, float.Parse(cmd[2]), float.Parse(cmd[3]));
                                break;
                            case NetProp.ADD_PLAYER:
                                p = new Player();
                                p.Id = int.Parse(cmd[1]);
                                p.X = float.Parse(cmd[2]);
                                p.Y = float.Parse(cmd[3]);
                                p.dX = float.Parse(cmd[4]);
                                p.dY = float.Parse(cmd[5]);
                                //model.createHumanPlayer(pa);
                                model.makeMove(p, p.dX, p.dY);
                                break;
                            case NetProp.PUT_PLAYER:
                                p = new Player();
                                p.Id = int.Parse(cmd[1]);
                                p.X = float.Parse(cmd[2]);
                                p.Y = float.Parse(cmd[3]);
                                model.putPlayer(p, p.X, p.Y);
                                break;
                        }
                    }
                }
                catch (Exception e)
                {
                    // Ignore 
                }
            });
            readerThread.Start();
        }

        public void playerMoving(Player p)
        {
            //Console.WriteLine("Sending command m {0} {1} {2} {3} {4}", p.Id, p.X, p.Y, p.dX, p.dY);
            //writer.WriteLine("m {0} {1} {2} {3} {4}", p.Id, p.X, p.Y, p.dX, p.dY);
            sendData(NetProp.MOVE_PLAYER, p.Id, p.X, p.Y, p.dX, p.dY);
        }

        public void startGame()
        {
            //Console.WriteLine("Sending command s");
            //writer.WriteLine("s");
            sendData(NetProp.START_GAME);
        }

        public void endGame()
        {
            //Console.WriteLine("Sending command e");
            //writer.WriteLine("e");
            sendData(NetProp.END_GAME);
        }


        public void playerRemoved(Player p)
        {
            //Console.WriteLine("Sending command r");
            //writer.WriteLine("r {0}", p.Id);
            sendData(NetProp.REMOVE_PLAYER, p.Id);
        }

        public void playerCreated(Player p)
        {
            sendData(NetProp.PLAYER_CREATED, p.Id, p.X, p.Y, p.dX, p.dY);
        }

        private void sendData(string commandType, params object[] data)
        {
            string s = commandType + " ";
            for (int i = 0; i < data.Length; i++)
            {
                s += "{" + i + "} ";
            }
            writer.WriteLine(s, data);
            Console.WriteLine("ViewProxy Sending: " + s, data);

        }


  
    }
}
