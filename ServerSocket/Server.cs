﻿using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ServerSocket
{
    public class Server
    {
        public static void Start()
        {
            // Get Host IP Address that is used to establish a connection
            // In this case, we get one IP address of localhost that is IP : 127.0.0.1
            // If a host has multiple addresses, you will get a list of addresses
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = host.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);

            try
            {
                // Create a Socket that will use Tcp protocol
                Socket listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                // A Socket must be associated with an endpoint using the Bind method
                listener.Bind(localEndPoint);
                // Specify how many requests a Socket can listen before it gives Server busy response.
                // We will listen 10 requests at a time
                listener.Listen(1);

                while (true)
                {
                    Console.WriteLine("Waiting for a connection...");
                    Socket handler = listener.Accept();

                    // Incoming data from the client.
                    byte[] bytes = new byte[1024];

                    int bytesRec = handler.Receive(bytes);
                    var data = Encoding.ASCII.GetString(bytes, 0, bytesRec);
                    Console.WriteLine("Text received : {0}", data);

                    byte[] msg = Encoding.ASCII.GetBytes("Server Response...");
                    handler.Send(msg);

                    if (data.IndexOf("<EOF>") > -1)
                    {
                        handler.Shutdown(SocketShutdown.Receive);
                        handler.Close();
                        Console.WriteLine("Server shutting down...");
                        break;
                    }

                    handler.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            Console.WriteLine("\n Press any key to continue...");
            Console.ReadKey();
        }
    }
}
