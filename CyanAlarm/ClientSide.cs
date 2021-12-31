using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace CyanAlarm
{
    public static class ClientSide
    {
        public static bool toClose = false;
        public static Bitmap image = null;
        public static bool newImage = false;
        public static List<string> messagesToSend = new List<string>();
        public static System.Threading.Thread listening;
        public static Socket server;
        public static void thread()
        {
            System.Threading.Thread.Sleep(2000);
            while (!toClose)
            {
                System.Threading.Thread.Sleep(1000);
                messagesToSend.Add("StartMedia");
                listening = new System.Threading.Thread(Listen);
                listening.Start();
                mainClient();
                if (Principal.ticks < 590) Principal.ticks = 590;
            }
        }

        private static void Listen()
        {
            byte[] data = new byte[200];
            while (!toClose)
            {
                if (server != null)
                {
                    try
                    {
                        int bytes = server.Receive(data, 0, data.Length, SocketFlags.None);
                        //Console.WriteLine("Received something.. ");
                        if (bytes > 0)
                        {
                            string message = Encoding.ASCII.GetString(data, 0, data.Length);
                            Principal.messages_fromClient.Add(message);
                        }
                    }
                    catch (System.Net.Sockets.SocketException) { }
                    catch (Exception) { }
                }
                System.Threading.Thread.Sleep(50);
            }
        }

        static byte[] data;
        static void mainClient()
        {
            //IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("151.53.99.12"), 10000);
            IPEndPoint ipep;
            try
            {
                Console.WriteLine(ServerSide.SERVER_IP);
                ipep = new IPEndPoint(IPAddress.Parse(ServerSide.SERVER_IP), ServerSide.SERVER_PORT);
            }
            catch (Exception) { Console.WriteLine("cycle"); return; }
            EndPoint senderRemote = (EndPoint)ipep;
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            data = new byte[280887];

            while (!toClose)
            {
                try
                {
                    server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    IAsyncResult result = server.BeginConnect(ipep, null, null);
                    bool success = result.AsyncWaitHandle.WaitOne(2000, true);
                    if (server.Connected)
                    {
                        Console.WriteLine("Connected");
                        messagesToSend.Add("Save: -001");
                        server.EndConnect(result);
                        break;
                    }
                    else
                    {
                        server.Close();
                        Console.WriteLine("Failed to connect server.");
                    }
                }
                catch (SocketException e)
                {
                    Console.WriteLine("Unable to connect to server.");
                    return;
                }
            }

            int bytes = 0;
            System.Threading.Thread.Sleep(200);
            while (!toClose)
            {
                try { SendMessages(); } catch (Exception) { break; }
                try
                {
                    if (newImage) continue;
                    bytes = server.ReceiveFrom(data, 0, data.Length, SocketFlags.None, ref senderRemote);
                    Console.WriteLine(bytes);
                    if (bytes < 30) continue;
                }
                catch (Exception) { return; }

                System.Threading.Thread getImage = new System.Threading.Thread(GetImage);
                getImage.Start();

                System.Threading.Thread.Sleep(10);
            }

            
            Console.WriteLine("Disconnecting from server...");
            try
            {
                server.Shutdown(SocketShutdown.Both);
                server.Close();
                Console.ReadLine();
            }
            catch (Exception) { }
            
        }

        private static void GetImage()
        {
            MemoryStream ms = new MemoryStream(data);
            //Preview(data);

            try
            {
                image = (Bitmap)Image.FromStream(ms);
                newImage = true;
            }
            catch (ArgumentException e) { }
        }

        private static void SendMessages()
        {
            if (server != null)
            {
                if (messagesToSend.Count > 0)
                {
                    for (int i = 0; i < messagesToSend.Count; i++)
                    {
                        if (i >= messagesToSend.Count) break;
                        byte[] bmpBytes = Encoding.ASCII.GetBytes(messagesToSend[i]);
                        server.Send(bmpBytes, 0, bmpBytes.Length, SocketFlags.None);
                        Console.WriteLine("Message (" + bmpBytes.Length + " bytes) sent from client: " + messagesToSend[i]);
                        messagesToSend.RemoveAt(0);
                        System.Threading.Thread.Sleep(30);
                        i--;
                    }
                }
            }
        }

        private static void Preview(byte[] data)
        {
            //return;
            Console.WriteLine("Client Side  -  n bytes: " + data.Length);
            for (int i = 0; i < 10; i++) Console.Write(data[i]);
            Console.Write("....");
            long fin = data.Length;
            if (data != null) for (long i = fin - 10; i < fin; i++) Console.Write(data[i]);
            Console.WriteLine();
        }

    }
}
