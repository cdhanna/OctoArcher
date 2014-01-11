using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net.Sockets;

namespace OctoArcher
{
    class TestClient
    {



        /// <summary>
        /// hard coded with
        //192.168.1.11
        // 1025
        // tcp
        /// </summary>
        public TestClient()
        {
            string ip = "192.168.1.11";
            int port = 1025;
            TcpClient client = new TcpClient(ip, port);

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
