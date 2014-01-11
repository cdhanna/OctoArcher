using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace OctoArcher
{
    class Server
    {
        static TcpListener listener;

        public static void Main()
        {
            listener = new TcpListener(NetProp.SERVER_IP, NetProp.PORT);
            listener.Start();

            for (int i = 0; i < 5; i++)
            {
                Thread t = new Thread(new ThreadStart(Handler));
                t.Start();
            }
        }

        public static void Handler()
        {
            while (true)
            {
                Socket socket = listener.AcceptSocket();
                Console.WriteLine("Received connection from " + socket.RemoteEndPoint);

                try
                {
                    Stream s = new NetworkStream(socket);
                    StreamReader reader = new StreamReader(s);
                    StreamWriter writer = new StreamWriter(s);

                    writer.AutoFlush = true;
                    writer.WriteLine("Hello from 192.168.1.11");

                    s.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
        }
    }
}
