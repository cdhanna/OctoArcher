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
        static TcpListener listener;

        public static void Main()
        {
            listener = new TcpListener(IPAddress.Parse(NetProp.SERVER_IP), NetProp.PORT);
            listener.Start();

            ViewListener model;

            while (true)
            {
                Socket socket = listener.AcceptSocket();
                Console.WriteLine("Received connection from " + socket.RemoteEndPoint);

                ViewProxy viewProxy = new ViewProxy(socket);
                viewProxy.Model = model;
            }
            
        }
    }
}
