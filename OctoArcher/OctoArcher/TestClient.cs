using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net.Sockets;

namespace OctoArcher
{
    /// <summary>
    /// Pretty much copied code from http://www.codeproject.com/Articles/10649/An-Introduction-to-Socket-Programming-in-NET-using
    /// To create a simple test client. that is currently hardcoded to 192.168.1.11:1025
    /// </summary>
    class TestClient
    {

        private string ip;
        private int port;
        private TcpClient client;

        /// <summary>
        /// hard coded with
        //192.168.1.11
        // 1025
        // tcp
        /// </summary>
        public TestClient()
        {
            this.ip = "192.168.1.11";
            this.port = 4862;
            this.client = new TcpClient(ip, port);

        }

        /// <summary>
        /// Start listening forever at the ip and port. Print out stuff to Console until there is no more stuff to print
        /// </summary>
        public void startListening()
        {
            try
            {
                Stream s = client.GetStream();
                StreamReader sr = new StreamReader(s);
                StreamWriter sw = new StreamWriter(s);
                sw.AutoFlush = true;
                Console.WriteLine(sr.ReadLine());
                while (true)
                {
                    Console.Write("Name: ");
                    string name = Console.ReadLine();
                    sw.WriteLine(name);
                    if (name == "") break;
                    Console.WriteLine(sr.ReadLine());
                }
                s.Close();
            }
            finally
            {
                // code in finally block is guranteed 
                // to execute irrespective of 
                // whether any exception occurs or does 
                // not occur in the try block
                client.Close();
            } 

        }

    }
}
