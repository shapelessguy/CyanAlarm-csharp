using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using AForge.Video;
using AForge.Video.DirectShow;
using Accord.Video.FFMPEG;

using System.Runtime.InteropServices;
using System.IO;
using System.Globalization;

namespace CyanAlarm
{
    public partial class Principal : Form
    {
        public static int againActive = 30*1000;  // ms after which the alarm will be on again
        public static int default_framerateLevel = 5;
        public static int default_resolutionLevel = 3;
        public static int min_activity = 10;   // mins of inactivity after which the program tries to shut off the webcams
        public static string pathRec = @"E:\CyanAlarmRecords";
        public static string pathIP = @"C:\Users\Eva";
        public static int maxStreamWidth = 1400;
        public static int minStreamWidth = 100;
        public static int maxStreamFPS = 30;
        public static int minStreamFPS = 5;

        public static Principal main;
        public static Bitmap ServerPicture = null;
        public static Size streamSize_max;
        public static Size streamSize;
        public static int framerateLevel;
        public static int resolutionLevel;
        public static int delay;
        public static bool client = false;
        public static bool debug = false;
        public static Timer saveTimer = new Timer();


        public static List<string> messages = new List<string>();
        public static List<string> messages_fromClient = new List<string>();
        Bitmap null_image = new Bitmap(20,20);
        Timer update;
        Timer saveEnergy;
        COM com;
        System.Threading.Thread serverThread;
        System.Threading.Thread clientThread;
        public static Timer timerIP;
        string filter = "";
        static public bool webIsRunning = false;
        static public bool track1_touching = false;
        static public bool track2_touching = false;
        static public bool track3_touching = false;
        static public bool track4_touching = false;
        public Principal()
        {
            main = this;
            InitializeComponent();
            Settings.Interpret(Properties.Settings.Default.settings);
            FormClosing += (o, e) => { Properties.Settings.Default.settings = Settings.getSettings(); Properties.Settings.Default.Save(); };
            framerateLevel = default_framerateLevel;
            resolutionLevel = default_resolutionLevel;
            streamSize_max = new Size(maxStreamWidth, maxStreamWidth / 16 * 9 + (maxStreamWidth / 16 * 9) % 2);
            StreamSize(resolutionLevel);
            StreamFrameRate(framerateLevel);
            server_framerate.Value = framerateLevel;
            server_resolution.Value = resolutionLevel;
            //streamSize = new Size(streamSize_max.Width/9*10, streamSize_max.Height/ 9 * 10);
            saveTimer.Tick += saveTimerTick;
            using (Graphics g = Graphics.FromImage(null_image)) { g.Clear(Color.Black); }
            serverPic.SizeMode = PictureBoxSizeMode.StretchImage;
            clientPic.SizeMode = PictureBoxSizeMode.StretchImage;
            client_resolution.MouseDown += (o, e) => { track1_touching = true; }; client_resolution.MouseUp += (o, e) => { track1_touching = false; };
            server_resolution.MouseDown += (o, e) => { track2_touching = true; }; server_resolution.MouseUp += (o, e) => { track2_touching = false; };
            client_framerate.MouseDown += (o, e) => { track3_touching = true; }; client_framerate.MouseUp += (o, e) => { track3_touching = false; };
            server_framerate.MouseDown += (o, e) => { track4_touching = true; }; server_framerate.MouseUp += (o, e) => { track4_touching = false; };
            client_resolution.ValueChanged += (o, e) => {
                //if (trackBar1.Value != trackBar2.Value) trackBar2.Value = trackBar1.Value;
                ClientSide.messagesToSend.Add("Resolution: " + client_resolution.Value);
                StreamSize(client_resolution.Value);
            };
            server_resolution.ValueChanged += (o, e) => {// if (trackBar1.Value != trackBar2.Value) trackBar1.Value = trackBar2.Value;
                ServerSide.messagesToSend.Add("Resolution: " + server_resolution.Value);
                StreamSize(server_resolution.Value); };
            client_framerate.ValueChanged += (o, e) => {
               // if (trackBar4.Value != trackBar3.Value) trackBar4.Value = trackBar3.Value;
                ClientSide.messagesToSend.Add("Framerate: "+client_framerate.Value);
                StreamFrameRate(client_framerate.Value);
            };
            server_framerate.ValueChanged += (o, e) => {
                ServerSide.messagesToSend.Add("Framerate: " + server_framerate.Value);
                // if (trackBar4.Value != trackBar3.Value) trackBar3.Value = trackBar4.Value;
                StreamFrameRate(server_framerate.Value);
            };
            for (int i = 0; i < WebcamPanel.Controls.OfType<Webcam>().Count(); i++) WebcamPanel.Controls.OfType<Webcam>().ToArray()[i].initialIndex = i;
            if (!client)
            {
                foreach (Control ctrl in Controls) ctrl.KeyDown += Press;
                foreach (Control ctrl in WebcamPanel.Controls) ctrl.KeyDown += Press;
                foreach (Webcam webcam in WebcamPanel.Controls.OfType<Webcam>()) foreach (Control ctrl in webcam.Controls) ctrl.KeyDown += Press;
                KeyDown += Press;
                Urgent(false);
                textBox1.Text = Properties.Settings.Default.minAutoRec.ToString();
                serverThread = new System.Threading.Thread(ServerSide.thread);
                serverThread.Start();
                com = new COM();

                saveEnergy = new Timer() { Enabled = true, Interval = 1000 };
                saveEnergy.Tick += (o, e) =>
                {
                    urgentTick++;
                    if (urgentTick > 60 * min_activity) { urgentTick = 0; Urgent(false); }
                    //framerateLevel = default_framerateLevel;
                    //resolutionLevel = default_resolutionLevel;
                };
                WindowState = FormWindowState.Minimized;
            }

            if(client || debug)
            {
                timerIP = new Timer() { Enabled = true, Interval = 1000 };
                timerIP.Tick += (o, e) =>
                {
                    if (ticks > 600) { FirebaseClass.DownloadIP(); ticks = 0; }
                    else ticks++;
                };
                clientThread = new System.Threading.Thread(ClientSide.thread);
                clientThread.Start();
            }
            else { client_btn.Hide(); }

            FormClosing += (o, e) => { PrelimClose(); };

            if (client)
            {
                ClientPanel.BringToFront();
                client_btn.Hide();
                server_btn.Hide();
                web_btn.Hide();
            }
        }
        static public int ticks = 600;
        private static void BackoffIP()
        {
            ticks -= 10;
            if (ticks < 0) ticks = 0;
        }

        private void StreamSize(int level)
        {
            int minStreamHeight = minStreamWidth/16*9 + (minStreamWidth / 16 * 9) % 2;
            streamSize = new Size((maxStreamWidth - minStreamWidth) * level/10 + minStreamWidth, (streamSize_max.Height - minStreamHeight) * level/10 + minStreamHeight);
            streamSize = new Size(streamSize.Width + streamSize.Width % 2, streamSize.Height + streamSize.Height % 2);
            Console.WriteLine("size: " + streamSize);
            resolutionLevel = level;
        }
        private void StreamFrameRate(int level)
        {
            framerateLevel = level;
            level = (level - 9) * (-1);
            int min_delay = (int)(1000 / (float)(maxStreamFPS));
            int max_delay = (int)(1000 / (float)(minStreamFPS));
            delay = min_delay + (max_delay - min_delay) * level/10;
            Console.WriteLine("delay: "+delay);
            //delay = (int)(1000/(float)((level+2)));
        }
        private void Press(object o, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Add)
            {
                e.SuppressKeyPress = true;
                if (saveTimer.Enabled) return;
                bool running = false;
                foreach (Webcam webcam in WebcamPanel.Controls.OfType<Webcam>()) if (webcam.isMonitoring()) running = true;
                Console.WriteLine("Command given to webcams: Run="+ !running);
                RunWebcams(!running);
            }
        }

        private void RunWebcams(bool run)
        {
            if(run) foreach (Webcam webcam in WebcamPanel.Controls.OfType<Webcam>()) webcam.Start();
            else foreach (Webcam webcam in WebcamPanel.Controls.OfType<Webcam>()) webcam.Stop();
        }

        private void PrelimClose()
        {
            try { ServerSide.newsock.Close(); } catch (Exception) { }
            try { if(ServerSide.client!= null) ServerSide.client.Close(); } catch (Exception) { }
            ServerSide.toClose = true;
            ClientSide.toClose = true;
            if (!client) com.Close();
            if (!client) foreach (Webcam webcam in WebcamPanel.Controls.OfType<Webcam>()) webcam.Stop();
        }

        bool start = false;
        private void Form1_Load(object sender, EventArgs ea)
        {
            update = new Timer() { Enabled = true, Interval = 100 };
            update.Tick += (o, e) => { Update(); };
            if (!client)
            {
                comboBox1.Items.Add("All");
                comboBox1.Text = "All";
                for (int i = 0; i < com.num_pins; i++) comboBox1.Items.Add("Pin" + i);
            }
            SizeChanged += (o, e) => { ResizeWin(); };
            ResizeWin();
            // start = true;
        }

        int n_frames = 0;
        static int n_frames_max = 300;
        private void Update()
        {
            webIsRunning = webRunning();
            if (com.closeFlag) Close();
            if(client || debug)
            {
                DrawClientImage();
                string[] commands_fromClient = messages_fromClient.ToArray();
                messages_fromClient.Clear();
                Interpret_fromClient(commands_fromClient);
            }
            if (!client)
            {
                if (Webcam.focusOnButtons) { Webcam.focusOnButtons = false; FocusOnButtons(); }
                if (ServerSide.connected) onlineState.BackgroundImage = Properties.Resources.online; else onlineState.BackgroundImage = Properties.Resources.offline;
                Bitmap bmp = null;
                if(webIsRunning) bmp = DrawServerImage();
                if (vFWriter != null) try {
                        if (bmp != null) { vFWriter.WriteVideoFrame(bmp); n_frames++; }
                        if (n_frames >= n_frames_max) StopSavingVideo();
                    } catch (Exception) { }
                else n_frames = 0;
                string[] commands = messages.ToArray();
                messages.Clear();
                Interpret(commands);
                if (saveTimer.Enabled) { SavingVideo();  recServer.BackgroundImage = Properties.Resources.recOn; }
                else { recServer.BackgroundImage = Properties.Resources.recOff; }
                if (com.alarm) {
                    //update.Dispose(); PrelimClose();
                    Alarm(); }
                if (!start) { com.buffer.Clear(); return; }
                if (com.buffer.Count == 0) return;
                string[] lines = com.buffer.ToArray();
                com.buffer.Clear();
                foreach (string line in lines) if (line.Contains(filter) || filter == "All") listBox1.Items.Insert(0, line);
                if (listBox1.Items.Count > 100) { for (int i = listBox1.Items.Count - 1; i > 80; i--) listBox1.Items.RemoveAt(i); }
            }
        }
        static DateTime date;
        static public bool uploadOnStorage = false;
        public static void EnableSave(int seconds)
        {
            if (seconds == 0) { seconds = 5; uploadOnStorage = false; }
            else uploadOnStorage = true;
            saveTimer.Enabled = false;
            saveTimer.Interval = seconds * 1000;
            date = DateTime.Now;
            saveTimer.Enabled = true;
        }
        VideoFileWriter vFWriter;
        string actFileName = "";
        private void SavingVideo()
        {
            if (vFWriter != null) return;
            Console.WriteLine("Start saving video");
            SendRemainingTime_toClient();
            vFWriter = new VideoFileWriter();
            string date = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
            actFileName = "CamRec-" + date + ".avi";
            vFWriter.Open(Path.Combine(pathRec, actFileName), streamSize_max.Width, streamSize_max.Height, 8, VideoCodec.MPEG4);
        }
        private void SendRemainingTime_toClient()
        {
            long delta = saveTimer.Interval / 1000 - (long)DateTime.Now.Subtract(Principal.date).TotalSeconds;
            if (delta < 0) delta = 0;
            string diff = delta.ToString();
            for (int i = diff.Length; i < 4; i++) diff = "0" + diff;
            for (int i = 0; i < 5; i++) ServerSide.messagesToSend.Add("Save: " + diff);
        }
        private void StopSavingVideo()
        {
            Console.WriteLine("Stop saving video");
            n_frames = 0;
            if(uploadOnStorage) FirebaseClass.UploadOnStorage(Path.Combine(pathRec, actFileName));
            vFWriter.Close();
            vFWriter = null;
        }
        private void saveTimerTick(object sender, EventArgs e)
        {
            saveTimer.Enabled = false;
            for(int i=0; i<5; i++) ServerSide.messagesToSend.Add("Save: 0000");
            StopSavingVideo();
            Urgent(false);
        }

        private void Interpret(string[] messages)
        {
            foreach(string message in messages)
            {
                //Console.Beep();
                // Console.WriteLine();
                Console.WriteLine("Message from Client incoming\n"+message);
                if (message.Contains("StartMedia"))
                {
                    Urgent(true);
                    Console.WriteLine("Message (" + message.Length + " bytes) received: StartMedia");
                }
                //if (message.Contains("Urgent: "))
                {
                //    bool val = false;
                //    string value = message.Split(new string[] { ": " }, StringSplitOptions.RemoveEmptyEntries)[1];
                //    if (value.Contains("true")) val = true;
                //    Urgent(Convert.ToBoolean(val));
                }
                if (message.Contains("StopMedia"))
                {
                    if (saveTimer.Enabled) return;
                    Urgent(false);
                    Console.WriteLine("Message (" + message.Length + " bytes) received: StopMedia");
                }
                if (message.Contains("__Ping__"))
                {
                    //Console.WriteLine("Ping sent and ping received");
                    //ServerSide.ping = true;
                }
                if (message.Contains("__PingCalling__"))
                {
                    ServerSide.callFeedback = true;
                }
                else if (message.Contains("WantToSeeCam0"))
                {
                    cam1_Click(cam1, null);
                    serverCam = 0;
                }
                else if (message.Contains("WantToSeeCam1"))
                {
                    cam1_Click(cam2, null);
                    serverCam = 1;
                }
                else if (message.Contains("WantToSeeCam2"))
                {
                    cam1_Click(cam3, null);
                    serverCam = 2;
                }
                else if (message.Contains("WantToSeeCam3"))
                {
                    cam1_Click(cam4, null);
                    serverCam = 3;
                }
                else if (message.Contains("WantToSeeCam-1"))
                {
                    cam_all_Click(cam_all, null);
                    serverCam = -1;
                }
                else if (message.Contains("Framerate: "))
                {
                    try
                    {
                        string value = message.Split(new string[] { ": " }, StringSplitOptions.RemoveEmptyEntries)[1];
                        value = value.Substring(0, 1);
                        Console.WriteLine("Received framerate: " + value);
                        if (!track4_touching) server_framerate.Value = Convert.ToInt32(value);
                    }
                    catch (Exception) { }
                }
                else if (message.Contains("Resolution: "))
                {
                    try
                    {
                        string value = message.Split(new string[] { ": " }, StringSplitOptions.RemoveEmptyEntries)[1];
                        value = value.Substring(0, 1);
                        Console.WriteLine("Received resolution: " + value);
                        if(!track2_touching) server_resolution.Value = Convert.ToInt32(value);
                    }
                    catch (Exception) { }
                }
                else if (message.Contains("Settings:"))
                {
                    try
                    {
                        string[] values = message.Split(new string[] { "Settings:" }, StringSplitOptions.RemoveEmptyEntries);
                        string value = values[values.Length - 1];
                        Settings.Interpret(value);
                    }
                    catch (Exception) { }
                }
                else if (message.Contains("Remaining"))
                {
                    SendRemainingTime_toClient();
                }
                else if (message.Contains("Save: "))
                {
                    try
                    {
                        string value = message.Split(new string[] { ": " }, StringSplitOptions.RemoveEmptyEntries)[1];
                        value = value.Substring(0, 4);
                        Console.WriteLine("Received save command: " + value);
                        if (value != "-001") EnableSave(Convert.ToInt32(value));
                        SendRemainingTime_toClient();
                    }
                    catch (Exception) { }
                }
                else
                {
                    for (int i = 0; i < 2; i++)
                    {
                        //System.Threading.Thread.Sleep(400);
                        //Console.Beep();
                    }
                    //Console.WriteLine("Unknown message ("+ message.Length +" bytes) received: " + message);
                }
            }
        }
        static public int recording = 0;
        private void Interpret_fromClient(string[] messages)
        {
            foreach (string message in messages)
            {
                if (message.Contains("Resolution: "))
                {
                    try
                    {
                        string value = message.Split(new string[] { ": " }, StringSplitOptions.RemoveEmptyEntries)[1];
                        value = value.Substring(0, 1);
                        if (!track1_touching) client_resolution.Value = Convert.ToInt32(value);
                    }
                    catch (Exception) { }
                }
                else if (message.Contains("Framerate: "))
                {
                    try
                    {
                        string value = message.Split(new string[] { ": " }, StringSplitOptions.RemoveEmptyEntries)[1];
                        value = value.Substring(0, 1);
                        if (!track3_touching) client_framerate.Value = Convert.ToInt32(value);
                    }
                    catch (Exception) { }
                }
                else if (message.Contains("Save: "))
                {
                    try
                    {
                        string value = message.Split(new string[] { ": " }, StringSplitOptions.RemoveEmptyEntries)[1];
                        value = value.Substring(0, 4);
                        recording = Convert.ToInt32(value);
                        Console.WriteLine("Received save feedback: " + value);
                        if (recording > 0) recClient.BackgroundImage = Properties.Resources.recOn;
                        else recClient.BackgroundImage = Properties.Resources.recOn;
                    }
                    catch (Exception) { }
                }
            }
        }
        private void ResizeWin()
        {
            //foreach (Webcam webcam in panel1.Controls.OfType<Webcam>()) webcam.Stop();
            Size size1 = new Size(WebcamPanel.Width / 2, WebcamPanel.Height / 2);
            webcam1.Size = size1;
            webcam2.Size = size1;
            webcam3.Size = size1;
            webcam4.Size = size1;
            webcam1.Location = new Point(0, 0);
            webcam2.Location = new Point(webcam1.Width, 0);
            webcam3.Location = new Point(0, webcam1.Height);
            webcam4.Location = new Point(size1.Width, webcam2.Height);
            listBox1.Height = comboBox1.Location.Y - listBox1.Location.Y - 10;
            if (client)
            {
                ClientPanel.Location = new Point(0, 0);
                ClientPanel.Size = new Size(Width, Height - 25);
            }
            if (client || debug)
            {
                if (WindowState == FormWindowState.Minimized) ClientSide.messagesToSend.Add("StopMedia");
                else ClientSide.messagesToSend.Add("StartMedia");
            }
            //Timer wait = new Timer() { Enabled = true, Interval = 100 };
            //wait.Tick += (o, e) => { wait.Dispose(); foreach (Webcam webcam in panel1.Controls.OfType<Webcam>()) webcam.Start(); };

        }

        private void button2_Click(object sender, EventArgs e)
        {
            start = true;
            FocusOnButtons();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            start = false;
            FocusOnButtons();
        }

        protected override void WndProc(ref Message m)
        {
            try
            {
                base.WndProc(ref m);
                if (m.Msg == 0x0112)
                {
                    if (m.WParam == new IntPtr(0xF030) || m.WParam == new IntPtr(0xF032))
                    {
                        ResizeWin();
                    }
                }
                if (m.Msg == 0x0112)
                {
                    if (m.WParam == new IntPtr(0xF122))
                    {
                        ResizeWin();
                    }
                    if (m.WParam == new IntPtr(0xF120)) if (WindowState != FormWindowState.Maximized) ResizeWin();
                }
                if (WindowState == FormWindowState.Minimized) Webcam.canDraw = false; else Webcam.canDraw = true;
            }
            catch (Exception) { Close(); }
        }

        bool alarm_allow = true;
        private void Alarm()
        {
            if (!alarm_allow) return;
            Console.WriteLine("----------------------ALARM----------------------");
            try
            {
                if(com.alarmsBy != null)
                {
                    Console.WriteLine("---------Alarm by {0}  ---->   ", com.alarmsBy.name);
                    foreach (float value in com.alarmsBy.detections) 
                        Console.WriteLine("                        ---->   " + value);
                }
            }
            catch (Exception) { }

            Urgent(true);
            EnableSave(Convert.ToInt32(Properties.Settings.Default.minAutoRec*60));
            System.Threading.Thread SoundAlarm_thread = new System.Threading.Thread(new System.Threading.ThreadStart(SoundAlarm.Sound_Alarm));
            SoundAlarm_thread.Start();
            if (Settings.allAlarms)
            {
                ServerSide.actDate = DateTime.Now.ToString("dd MMMM hh:mm tt", new CultureInfo("en-EN"));
                ServerSide.callFeedback = false;
            }
            SendRemainingTime_toClient();
            alarm_allow = false;
            //com.Close();
            Timer timerResume = new Timer() { Enabled = true, Interval = againActive };
            timerResume.Tick += (o, e) => { com.InitializePins(); alarm_allow = true; timerResume.Dispose(); };
            return;
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern bool SetCursorPos(int x, int y);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        public const int MOUSEEVENTF_LEFTDOWN = 0x02;
        public const int MOUSEEVENTF_LEFTUP = 0x04;
        [DllImport("user32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd);
        private delegate bool EnumWindowsProc(IntPtr hWnd, int lParam);
        [DllImport("USER32.DLL")]
        private static extern bool EnumWindows(EnumWindowsProc enumFunc, int lParam);

        [DllImport("USER32.DLL")]
        private static extern IntPtr GetShellWindow();
        [DllImport("User32.dll", SetLastError = false, CharSet = CharSet.Auto)]
        static extern long GetClassName(IntPtr hwnd, StringBuilder lpClassName, long nMaxCount);

        [DllImport("USER32.DLL")]
        private static extern bool IsWindowVisible(IntPtr hWnd);
        [DllImport("USER32.DLL")]
        private static extern int GetWindowTextLength(IntPtr hWnd);
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetWindowRect(IntPtr hWnd, ref Rectangle rect);
        [DllImport("USER32.DLL")]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);
        [DllImport("user32.dll", SetLastError = true)]
        static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        private const int SW_MAXIMIZE = 3;
        private const int SW_MINIMIZE = 6;
        private const int SW_SHOWNORMAL = 1;
        const uint SWP_NOSIZE = 0x0001;
        const uint SWP_NOZORDER = 0x0004;
        const uint SWP_SHOWWINDOW = 0x0040;
        const uint SWP_NOMOVE = 0x0002;
        [DllImportAttribute("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        public IntPtr FindWindow(string[] partialname, string class_name)
        {
            IDictionary<IntPtr, string> OpenWindows = GetOpenWindows();
            IntPtr handle = IntPtr.Zero;
            foreach (KeyValuePair<IntPtr, string> window in OpenWindows)
            {
                bool containsall = true;
                foreach (string stringa in partialname) if (!window.Value.Contains(stringa)) containsall = false;
                if (containsall)
                {
                    StringBuilder lpString = new StringBuilder("none123");

                    if (class_name != "") GetClassName(window.Key, lpString, 100);
                    if (lpString.ToString() == class_name || class_name == "")
                    {
                        return window.Key;
                    }
                }
            }
            return IntPtr.Zero;
        }

        public static IDictionary<IntPtr, string> GetOpenWindows()
        {
            IntPtr shellWindow = GetShellWindow();
            Dictionary< IntPtr, string> windows = new Dictionary<IntPtr, string>();

            EnumWindows(delegate (IntPtr hWnd, int lParam)
            {
                if (hWnd == shellWindow) return true;
                if (!IsWindowVisible(hWnd)) return true;

                int length = GetWindowTextLength(hWnd);
                if (length == 0)
                {
                    Rectangle lpRect = new Rectangle();
                    GetWindowRect(hWnd, ref lpRect);
                    return true;
                }

                StringBuilder builder = new StringBuilder(length);
                GetWindowText(hWnd, builder, length + 1);

                windows[hWnd] = builder.ToString();
                return true;

            }, 0);

            return windows;
        }

        void FocusOnButtons()
        {
            if (start) button3.Focus();
            else button2.Focus();
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            filter = comboBox1.Text;
            FocusOnButtons();
        }

        private void ChangeTab(Button act_button)
        {
            foreach (Button button in Controls.OfType<Button>())
            {
                button.FlatStyle = FlatStyle.Standard;
                button.BackColor = Color.WhiteSmoke;
                button.ForeColor = Color.Black;
                button.FlatAppearance.BorderSize = 1;
            }
            act_button.FlatStyle = FlatStyle.Flat;
            act_button.BackColor = Color.FromArgb(16,16,16);
            act_button.ForeColor = Color.White;
            act_button.FlatAppearance.BorderSize = 0;
            FocusOnButtons();
        }
        private void ChangeServerCam(Button act_button)
        {
            foreach (Button button in ServerPanel.Controls.OfType<Button>())
            {
                button.FlatStyle = FlatStyle.Standard;
                button.BackColor = Color.AliceBlue;
            }
            act_button.FlatStyle = FlatStyle.Flat;
            act_button.BackColor = Color.LightSkyBlue;
            FocusOnButtons();
        }
        private void ChangeClientCam(Button act_button)
        {
            ClientSide.messagesToSend.Add("WantToSeeCam" + clientCam);
            foreach (Button button in ClientPanel.Controls.OfType<Button>())
            {
                button.FlatStyle = FlatStyle.Standard;
                button.BackColor = Color.AliceBlue;
            }
            act_button.FlatStyle = FlatStyle.Flat;
            act_button.BackColor = Color.LightSkyBlue;
            FocusOnButtons();
        }

        public int serverCam = -1;
        private void button1_Click(object sender, EventArgs e)
        {
            ChangeTab((Button)sender);
            WebcamPanel.BringToFront();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ChangeTab((Button)sender);
            ServerPanel.BringToFront();
        }
        private void client_btn_Click(object sender, EventArgs e)
        {
            ChangeTab((Button)sender);
            ClientPanel.BringToFront();
        }

        private void cam1_Click(object sender, EventArgs e)
        {
            ChangeServerCam((Button)sender);
            serverCam = 0;
        }

        private void cam2_Click(object sender, EventArgs e)
        {
            ChangeServerCam((Button)sender);
            serverCam = 1;
        }

        private void cam3_Click(object sender, EventArgs e)
        {
            ChangeServerCam((Button)sender);
            serverCam = 2;
        }

        private void cam4_Click(object sender, EventArgs e)
        {
            ChangeServerCam((Button)sender);
            serverCam = 3;
        }

        private void cam_all_Click(object sender, EventArgs e)
        {
            ChangeServerCam((Button)sender);
            serverCam = -1;
        }

        static public int urgentTick = 0;
        public void Urgent(bool urg)
        {
            if (urg)
            {
                urgentTick = 0;
                //if(WindowState == FormWindowState.Minimized) WindowState = FormWindowState.Normal;
                foreach (Webcam webcam in WebcamPanel.Controls.OfType<Webcam>()) webcam.Urgent();
            }
            else {
                //WindowState = FormWindowState.Minimized;
                foreach (Webcam webcam in WebcamPanel.Controls.OfType<Webcam>()) webcam.Stop();
            }
        }
        public bool webRunning()
        {
            bool running = false;
            foreach (Webcam webcam in WebcamPanel.Controls.OfType<Webcam>()) if (webcam.videoCaptureDevice.IsRunning) running = true;
            return running;
        }

        int iter_cleaning = 0;
        public static bool resourceBusy = false;
        private Bitmap DrawServerImage()
        {
            Bitmap image = null;
            Bitmap image_out = null;
            try
            {
                if (serverCam == -1)
                {
                    List<Bitmap> images = new List<Bitmap>();
                    foreach (Webcam webcam in WebcamPanel.Controls.OfType<Webcam>())
                        if (webcam.pictureBox1.Image != null) images.Add((Bitmap)webcam.pictureBox1.Image.Clone());
                        else images.Add(null);
                    image = CombineBitmap(images);
                }
                else
                {
                    image = (Bitmap)WebcamPanel.Controls.OfType<Webcam>().ToArray()[serverCam].pictureBox1.Image;
                }
                resourceBusy = true;
                if (image == null) { image = (Bitmap)null_image.Clone(); image_out = (Bitmap)null_image.Clone(); ServerPicture = (Bitmap)image.Clone(); }
                else
                {
                    image_out = (Bitmap)image.Clone();
                    image = new Bitmap(image, new Size(Math.Min(image.Width, streamSize.Width), Math.Min(image.Height, streamSize.Height)));
                    ServerPicture = (Bitmap)image.Clone();
                    iter_cleaning++;
                    if (iter_cleaning > 5) { iter_cleaning = 0; GC.Collect(); }
                }
                resourceBusy = false;
                //image = (Bitmap)ServerPicture.Clone();
                serverPic.Image = image;

                image_out = new Bitmap(image_out, new Size(streamSize_max.Width, streamSize_max.Height));
                return image_out;
            }
            catch (Exception) { resourceBusy = false; return null;  }
        }
        private void DrawClientImage()
        {
            if (ClientSide.image == null) { clientPic.Image = null; return; }
            try
            {
                Bitmap image = (Bitmap)ClientSide.image.Clone();
                ClientSide.newImage = false;
                clientPic.Image = image;
            }
            catch (Exception) { }
        }
        public static Bitmap CombineBitmap(List<Bitmap> images)
        {
            Bitmap finalImage = null;

            try
            {
                int width = streamSize_max.Width;
                int height = streamSize_max.Height;

                //create a bitmap to hold the combined image
                finalImage = new System.Drawing.Bitmap(width, height);

                //get a graphics object from the image so we can draw on it
                using (Graphics g = Graphics.FromImage(finalImage))
                {
                    //set background color
                    g.Clear(Color.Black);

                    //go through each image and draw it on the final image
                    int offsetx = 0;
                    int offsety = 0;

                    foreach (Bitmap image in images)
                    {
                        if(image != null) g.DrawImage(image, new Rectangle(offsetx, offsety, width/2, height/2));
                        if (offsetx >= width / 2) { offsetx = 0; offsety += height / 2; }
                        else offsetx += width / 2;
                    }
                }

                return finalImage;
            }
            catch (Exception ex)
            {
                if (finalImage != null)
                    finalImage.Dispose();

                throw ex;
            }
            finally
            {
                //clean up memory
                foreach (Bitmap image in images)
                {
                    if(image != null) image.Dispose();
                }
            }
        }

        public int clientCam = -1;
        private void clientCam1_Click(object sender, EventArgs e)
        {
            clientCam = 0;
            ChangeClientCam((Button)sender);
        }

        private void clientCam2_Click(object sender, EventArgs e)
        {
            clientCam = 1;
            ChangeClientCam((Button)sender);
        }

        private void clientCam3_Click(object sender, EventArgs e)
        {
            clientCam = 2;
            ChangeClientCam((Button)sender);
        }

        private void clientCam4_Click(object sender, EventArgs e)
        {
            clientCam = 3;
            ChangeClientCam((Button)sender);
        }

        private void clientCam_all_Click(object sender, EventArgs e)
        {
            clientCam = -1;
            ChangeClientCam((Button)sender);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            ClientSide.messagesToSend.Add("Urgent: true");
        }

        private void recClient_Click(object sender, EventArgs e)
        {
            ClientSide.messagesToSend.Add("Save: 0300");
        }

        private void recServer_Click(object sender, EventArgs e)
        {
            EnableSave(30);
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            bool converted = false;
            int value = 0;
            try
            {
                value = Convert.ToInt32(textBox1.Text);
                converted = true;
            }
            catch (Exception) { }
            if (!converted) textBox1.Text = Properties.Settings.Default.minAutoRec.ToString();
            else
            {
                Properties.Settings.Default.minAutoRec = value;
                if (value != 0) { Properties.Settings.Default.lastReal = value.ToString(); Settings.forReal = true; }
                else { Settings.forReal = false; }
                Settings.onSettingsChanged();
            }
            Properties.Settings.Default.Save();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) { FocusOnButtons(); e.SuppressKeyPress = true; }
        }

        static Settings settings;
        private void button5_Click(object sender, EventArgs e)
        {
            if(settings != null && Settings.active)
            {
                settings.BringToFront();
            }
            else
            {
                settings = new Settings();
                settings.Show();
            }
        }
        
    }
}
