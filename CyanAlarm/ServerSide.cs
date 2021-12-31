using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace CyanAlarm
{
    public static class ServerSide
    {
        public static int SERVER_PORT = 10000;
        public static int intervalPing = 4000;  // period of ping-pong
        public static int sending_timeout = 4000;  // ms of unresponsive client after which the server interrupts the connection

        public static bool toClose = false;
        public static Socket client;
        public static Socket newsock;
        public static EndPoint senderRemote;
        public static List<string> messagesToSend = new List<string>();
        public static bool connected = false;
        public static System.Timers.Timer pingTimer;
        public static bool callFeedback = true;
        public static string actDate = "";
        public static string SERVER_IP = "";


        public static bool ping = true;
        public static void thread()
        {
            pingTimer = new System.Timers.Timer();
            pingTimer.Elapsed += Ping;
            pingTimer.Interval = intervalPing;

            System.Threading.Thread listening = new System.Threading.Thread(Listen);
            listening.Start();
            SendStream();
        }

        public static void Connected(bool connection)
        {
            connected = connection;
            System.Threading.Thread beepThread = new System.Threading.Thread(Beep);
            beepThread.Start();
        }
        public static bool on=false;
        private static void Beep()
        {
            System.Threading.Thread.Sleep(500);
            int n = 0;
            if (connected) n = 3;
            else n = 1;
            for (int i = 0; i < n; i++)
            {
                Console.Beep();
                System.Threading.Thread.Sleep(400);
            }
        }

        private static void Listen()
        {
            byte[] data = new byte[200];
            while (!toClose)
            {
                if (client != null)
                {
                    try
                    {
                        int bytes = client.Receive(data, SocketFlags.None);
                        //Console.WriteLine("Received something.. ");
                        if (bytes > 0 && bytes < 50)
                        {
                            string message = Encoding.ASCII.GetString(data, 0, data.Length);
                            message = message.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries)[0];
                            //Console.WriteLine("|-------------------------");
                            //Console.WriteLine("|" + message);
                            //Console.WriteLine("|-------------------------");
                            Principal.messages.Add(message);
                        }
                    }
                    //catch (SocketException) { }//reset = true; }
                    catch (Exception) { System.Threading.Thread.Sleep(1000); }
                }
                System.Threading.Thread.Sleep(50);
            }
        }
        
        private static void newClientSocket()
        {
            IPEndPoint ipep = new IPEndPoint(IPAddress.Any, SERVER_PORT);
            senderRemote = (EndPoint)ipep;

            newsock = new Socket(AddressFamily.InterNetwork,
                            SocketType.Stream, ProtocolType.Tcp);

            newsock.Bind(ipep);
            newsock.Listen(0);
            Console.WriteLine("Waiting for a client... time: {0}", DateTime.Now.ToString());

            try { client = newsock.Accept(); } catch (Exception) { Console.WriteLine("Can't accept client"); return; }
            try { newsock.Close(); } catch (Exception) { Console.WriteLine("Can't close welcoming socket"); return; }

            byte[] bmpBytes = Encoding.ASCII.GetBytes("__Ping__");
            client.Send(bmpBytes, 0, bmpBytes.Length, SocketFlags.None);
            System.Threading.Thread.Sleep(50);

            Connected(true);
            IPEndPoint newclient = (IPEndPoint)client.RemoteEndPoint;
            Console.WriteLine("Connected with {0} at port {1}.. time: {2}", newclient.Address, newclient.Port, DateTime.Now.ToString());
            
            client.SendTimeout = sending_timeout;

            for(int i=0; i<3; i++)
            {
                messagesToSend.Add("Framerate: " + Principal.framerateLevel);
                messagesToSend.Add("Resolution: " + Principal.resolutionLevel);
            }
            //for(int i=0; i<5; i++) messagesToSend.Add(Settings.getSettings());
        }

        private static bool reset = false;
        private static void SendStream()
        {
            while (!toClose)
            {
                newClientSocket();
                pingTimer.Enabled = true;
                while (!toClose)
                {
                    if (reset) { reset = false; break; }
                    if(!callFeedback) messagesToSend.Add("Calling!~_~" + actDate+"~_~");
                    try { SendMessages(); } catch (Exception) { Console.WriteLine("Exception first phase"); break; }        // first phase of sending messages
                    System.Threading.Thread.Sleep(Principal.delay/3);


                    if (Principal.ServerPicture == null || !Principal.webIsRunning) continue;
                    MemoryStream ms = new MemoryStream();
                    try
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            if (!Principal.resourceBusy) break;
                            else System.Threading.Thread.Sleep(10);
                        }
                        Principal.ServerPicture.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                        byte[] bmpBytes = ms.ToArray();
                        //Preview(bmpBytes);
                        ms.Close();
                        client.Send(bmpBytes, 0, bmpBytes.Length, SocketFlags.None);    // second phase of sending images
                    }
                    catch (Exception) {
                        try { ms.Close(); } catch (Exception) { };
                        Console.WriteLine("Exception second phase");
                        System.Threading.Thread.Sleep(Principal.delay);
                        break; }
                    System.Threading.Thread.Sleep(Principal.delay / 3);

                    try
                    {
                        byte[] bmpBytes = Encoding.ASCII.GetBytes("Separator!");
                        client.Send(bmpBytes, 0, bmpBytes.Length, SocketFlags.None);        // third phase of sending separator
                        Principal.ServerPicture = null;
                    }
                    catch (Exception) { Console.WriteLine("Exception third phase"); }
                    System.Threading.Thread.Sleep(Principal.delay / 3);
                }

                pingTimer.Enabled = false;
                Connected(false);
                try
                {
                    if(client != null) client.Close();
                    newsock.Close();
                }
                catch (Exception) { Console.WriteLine("Exception in closing all the sockets"); }
                client = null;
                Principal.messages.Add("StopMedia");
            }
        }

        static int fail = 0;
        private static void Ping(object sender, EventArgs e)
        {
            System.Threading.Thread pingThread = new System.Threading.Thread(thread);
            pingThread.Start();
            void thread()
            {
                //ping = false;
                messagesToSend.Add("__Ping__");
                //System.Threading.Thread.Sleep(intervalPing);
                //if (!ping) fail++; else fail = 0;
                //if (fail > 5) { reset = true; fail = 0; }
            }
        }

        static string lastMessage = "";
        static bool lastpoints = false;
        private static void SendMessages()
        {
            if(client!= null)
            {
                if (messagesToSend.Count > 0)
                {
                    System.Threading.Thread.Sleep(30);
                    for (int i = 0; i < messagesToSend.Count; i++)
                    {
                        //if (messagesToSend[i] != "Separator!") continue;
                        if (i >= messagesToSend.Count) break;
                        byte[] bmpBytes = Encoding.ASCII.GetBytes(messagesToSend[i]);
                        client.Send(bmpBytes, 0, bmpBytes.Length, SocketFlags.None);
                        string message = "Message (" + bmpBytes.Length + " bytes) sent: " + messagesToSend[i];
                        try
                        {
                            if (messagesToSend[i] != "Separator!")
                            {
                                if(messagesToSend[i] != "__Ping__") Console.WriteLine(message);
                                //else Console.WriteLine(message);

                                //if (message != lastMessage) { Console.WriteLine(message); lastMessage = message; lastpoints = false; }
                                //else if (!lastpoints) { Console.WriteLine("..."); lastpoints = true; }
                                //else { Console.Write("."); }
                            }
                        }
                        catch (Exception) { }
                        messagesToSend.RemoveAt(0);
                        System.Threading.Thread.Sleep(30);
                        i--;
                    }
                }
            }
        }
        private static void Preview(byte[] data)
        {
            Console.WriteLine("Server Side  -  n bytes: " + data.Length);
            for (int i = 0; i < 10; i++) Console.Write(data[i]);
            Console.Write("....");
            long fin = data.Length;
            if(data != null) for (long i = fin - 10; i < fin; i++) Console.Write(data[i]);
            Console.WriteLine();
        }


        private static int SendVarData(Socket s, byte[] data)
        {
            try
            {
                int total = 0;
                int size = data.Length;
                int dataleft = size;
                int sent;

                byte[] datasize = new byte[4];
                datasize = BitConverter.GetBytes(size);
                sent = s.Send(datasize);

                while (total < size)
                {
                    sent = s.Send(data, total, dataleft, SocketFlags.None);
                    total += sent;
                    dataleft -= sent;
                }
                return total;
            }
            catch (Exception) { return 0; }
        }

        private static byte[] ReceiveVarData(Socket s)
        {
            int total = 0;
            int recv;
            byte[] datasize = new byte[4];

            try { recv = s.Receive(datasize, 0, 4, 0); } catch (Exception) { return datasize; }
            int size = BitConverter.ToInt32(datasize, 0);
            int dataleft = size;
            byte[] data = new byte[size];


            while (total < size)
            {
                recv = s.Receive(data, total, dataleft, 0);
                if (recv == 0)
                {
                    break;
                }
                total += recv;
                dataleft -= recv;
            }
            return data;
        }


    }
}
