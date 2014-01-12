using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace OctoArcher
{
    class Server
    {
        

        public static void Main()
        {
            Server server = new Server();
        }

        private TcpListener listener;
        private Thread mainServerThread;
        private Thread aiUpdate;


        public Server()
        {
            listener = new TcpListener(IPAddress.Parse(NetProp.SERVER_IP), NetProp.PORT);
            listener.Start();

            Model model = new Model();

            Random rand = new Random();
            for (int i = 0; i < 00; i++)
            {
                Player p = new Player();
                p.Id = model.getNextPlayerId();
                p.X = rand.Next(100, 500);
                p.Y = rand.Next(100, 500);

                p.dX = rand.Next(-1, 1);
                p.dY = rand.Next(-1, 1);

                model.addPlayer(p);
            }

            this.aiUpdate = new Thread(() =>
            {
                while (true)
                {
                    model.updateComputers();
                    Thread.Sleep(rand.Next(750, 1250));
                }
            });

            aiUpdate.Start();

            this.mainServerThread = new Thread(() =>
                {
                    while (true)
                    {
                        Socket socket = listener.AcceptSocket();
                        Console.WriteLine("SERVER: Received connection from " + socket.RemoteEndPoint);

                        ViewProxy viewProxy = new ViewProxy(socket);
                        viewProxy.Model = model;

                        //Player player = new Player();
                        //player.Id = model.getNextPlayerId();
                        model.addModelListener(viewProxy);
                    }
                });
            mainServerThread.Start();
            
        }

        public void shutdown()
        {
            this.mainServerThread.Abort();
            this.aiUpdate.Abort();
            this.listener.Stop();
        }

    }
}
