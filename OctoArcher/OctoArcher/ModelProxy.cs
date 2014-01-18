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
        private TcpClient tcp;
        private StreamReader reader;
        private StreamWriter writer;
        Thread readerThread;
        public ModelListener View { get; set; }

        /// <summary>
        /// Create a ModelProxy. The constructor will connect to the server if possible.
        /// </summary>
        /// <param name="ipAddress"> ip of the server to joint</param>
        /// <param name="port"> port on the server </param>
        public ModelProxy(String ipAddress, int port)
        {
            this.tcp = new TcpClient(ipAddress, port);
            Stream s = this.tcp.GetStream();
            this.reader = new StreamReader(s);
            this.writer = new StreamWriter(s);

            writer.AutoFlush = true;
            this.startListener();
        }

        private void startListener()
        {
            readerThread = new Thread(() =>
            {
                while (true)
                {
                    string command = reader.ReadLine();
                    Console.WriteLine("Received command {0} from server", command);
                    string[] cmd = command.Split(' ');
                    switch (cmd[0])
                    {
                        case NetProp.MOVE_PLAYER: //moved
                            Player p = new Player();
                            p.Id = int.Parse(cmd[1]);
                            p.X = float.Parse(cmd[2]);
                            p.Y = float.Parse(cmd[3]);
                            p.dX = float.Parse(cmd[4]);
                            p.dY = float.Parse(cmd[5]);
                            View.playerMoving(p);
                            break;
                        case NetProp.START_GAME: //game start
                            View.startGame();
                            break;
                        case NetProp.END_GAME: // game end
                            View.endGame();
                            break;
                        case NetProp.REMOVE_PLAYER: // player removed
                            Player pr = new Player();
                            pr.Id = int.Parse(cmd[1]);
                            View.playerRemoved(pr);
                            break;
                        case NetProp.PLAYER_CREATED:
                            Player np = new Player();
                            np.Id = int.Parse(cmd[1]);
                            np.X = float.Parse(cmd[2]);
                            np.Y = float.Parse(cmd[3]);
                            np.dX = float.Parse(cmd[4]);
                            np.dY = float.Parse(cmd[5]);
                            View.playerCreated(np);
                            break;
                    }
                }
            });
            readerThread.Start();
            
        }

        public void putPlayer(Player p, float x, float y)
        {
            sendData(NetProp.PUT_PLAYER, p.Id, x, y);
        }

        public void makeMove(Player p, float dx, float dy)
        {
            //Console.WriteLine("ModelProxy Sending command m {0} {1} {2}", p.Id, p.X, p.Y);
            //writer.WriteLine("m {0} {1} {2}", p.Id, p.X, p.Y);
            sendData(NetProp.MOVE_PLAYER, p.Id, dx, dy);
        }

        public void addPlayer(Player p)
        {
            //Console.WriteLine("ModelProxy Sending command a {0} {1} {2} {3} {4}", p.Id, p.X, p.Y, p.dX, p.dY);
            //writer.WriteLine("a {0} {1} {2} {3} {4}", p.Id, p.X, p.Y, p.dX, p.dY);
            sendData(NetProp.ADD_PLAYER, p.Id, p.X, p.Y, p.dX, p.dY);
        }

        public void removePlayer(Player p)
        {
            //Console.WriteLine("ModelProxy Sending command r {0}", p.Id);
            //writer.WriteLine("r {0}", p.Id);
            sendData(NetProp.REMOVE_PLAYER, p.Id);
        }

        private void sendData(string commandType, params object[] data)
        {
            string s = commandType + " ";

            for (int i = 0 ; i < data.Length ; i ++)
            {
                s += "{" + i + "} ";
            }
            writer.WriteLine(s, data);
            Console.WriteLine("ModelProxy Sending: " + s, data);
            
        }

        public void shutdown()
        {
            this.tcp.Close();
            this.readerThread.Abort();
            
        }
    }
}
