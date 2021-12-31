
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CyanAlarm
{
    public class COM
    {
        public static int timeDisconnectionMax = 5; // seconds in which program does not 
                                                    // receive any messages from arduino
        public static int nFailMax = 15;

        public bool closeFlag = false;
        public static int portNum;
        public SerialPort sp;
        public List<string> buffer = new List<string>();
        public Dictionary<string,Pin> pins = new Dictionary<string,Pin>();
        public int num_pins = 3;
        public bool alarm = false;
        public Pin alarmsBy;
        public int lastDetectionTime = 0;
        Timer lastCheck;
        bool checkEnabled = false;
        public COM()
        {
            lastCheck = new Timer() { Enabled = true, Interval = 1000 };
            lastCheck.Tick += (o, e) => {
                if (!checkEnabled) return;
                lastDetectionTime += 1;
                if (lastDetectionTime > timeDisconnectionMax)
                { 
                    alarm = true; 
                    Console.WriteLine("No response");
                    lastDetectionTime = 0;
                    checkEnabled = false;
                }
            };

            InitializeSerialPort();

        }
        private void InitializeSerialPort()
        {
            System.Threading.Thread initializePortThread = new System.Threading.Thread(InitializeSerialPortT);
            initializePortThread.Start();
        }
        private void InitializeSerialPortT()
        {
            lastDetectionTime = 0;
            checkEnabled = false;
            if (sp != null)
            {
                sp.DataReceived -= new SerialDataReceivedEventHandler(sp_DataReceived);
                pins.Clear();
                sp.Close();
                System.Threading.Thread.Sleep(10000);
                sp.Dispose();
                System.Threading.Thread.Sleep(10000);
            }

            try
            {
                Console.WriteLine("Inizializing Port");
                int portNum = 0;
                for (portNum = 0; portNum < 10; portNum++)
                {
                    Console.WriteLine("| Trying port " + portNum);
                    try
                    {
                        sp = new SerialPort("COM" + portNum, 9600, Parity.None, 8, StopBits.One);
                        sp.Open();
                        lastDetectionTime = 0;
                        sp.DataReceived += new SerialDataReceivedEventHandler(sp_DataReceived);
                        data = 0;
                        System.Threading.Thread.Sleep(2000);
                        if (data == 0)
                        {
                            Console.WriteLine("| Port " + portNum + " is not arduino serial port"); continue;
                        }
                        Console.WriteLine("| Connected to the port " + portNum + "!");
                        break;
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("| Connection to the port " + portNum + " failed");
                        System.Threading.Thread.Sleep(200);
                    }
                }
                if (portNum == 10)
                {
                    MessageBox.Show("Arduino not found!");
                    closeThread();
                }

                if (sp.IsOpen) Console.WriteLine("| Port " + portNum + " is opened");

                checkEnabled = true;

                return;
            }
            catch (Exception)
            {
                System.Threading.Thread.Sleep(1000);
                closeThread();
            }

        }
        private void closeThread()
        {
            try { Close(); }
            catch (Exception) { }
        }
        public void Close()
        {
            Console.WriteLine("Closing");
            try { sp.DataReceived -= new SerialDataReceivedEventHandler(sp_DataReceived); } catch (Exception) { }
            closeFlag = true;
            //try{ sp.Close(); } catch (Exception) { }
            //try{ sp.Dispose(); } catch (Exception) { }
            //alarm = false;
        }
        public string Read ()
        {
            return sp.ReadExisting();
        }
        List<string> messagesToArduino = new List<string>();
        int fail = 0;
        int data = 0;
        string lastPin = "Pin_";


        private void sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            data++;
            lastDetectionTime = 0;
            if (fail > nFailMax) { alarm = true; Console.WriteLine("More than 10 faults"); }
            try
            {
                string message = "";
                
                message = sp.ReadExisting().Replace(".", ",");
                if (message.Contains("Pin") && message.Length < 15)
                {
                    string newName = message.Substring(0, 4);
                    if (!pins.Keys.Contains(newName))
                    {
                        pins[newName] = new Pin(newName);
                        foreach (string name in pins.Keys)
                        { if (string.Compare(lastPin, name) < 0 ) lastPin = name; }
                        Console.WriteLine("New pin!!!!  ->  " + newName);
                    }
                }
                if (alarm) return;
                if (message.Contains("_")) return;

                buffer.Add(message);
                if (buffer.Count == 0) return;
                string[] splitted = buffer[buffer.Count - 1].Split(new string[] { ": " }, StringSplitOptions.RemoveEmptyEntries);
                if (splitted.Length != 2) { fail++; return; }
                string key = splitted[0];
                string value_str = splitted[1];
                float value = (float)Convert.ToDouble(value_str);

                Pin pin = null;
                foreach (var k in pins.Keys) if (key.Contains(k)) pin = pins[k];
                if (pin == null) { fail++; return; }
                if (pin.Compute(value)) { alarm = true; alarmsBy = pin; }

                if (pin.messageToArduino != "") { messagesToArduino.Add(pin.messageToArduino); pin.messageToArduino = ""; }
                if(pin.name == lastPin)
                {
                    string output = "";
                    for(int i=0; i<pins.Count; i++) if(messagesToArduino.Count>=i+1) output += messagesToArduino[i];
                    messagesToArduino.Clear();
                    sp.WriteLine(output);
                    //Console.WriteLine("Message sent to arduino: "+output);
                }
            }
            catch (Exception) { Console.WriteLine("fail!"); fail++; try { if(buffer.Count>0) Console.Write("fail: " +buffer[buffer.Count - 1]); return; } catch (Exception) { }; }
            fail = 0;
        }
        public void InitializePins()
        {
            Console.WriteLine("_____Reset Alarm____");
            alarmsBy = null;
            lastDetectionTime = 0;
            alarm = false;
            fail = 0;
            InitializeSerialPort();
            //foreach (var k in pins.Keys) { pins[k].Initialize(); }
        }
    }

    public class Pin
    {
        public string messageToArduino = "";
        public string name = "";
        public List<float> firstdetection_list = new List<float>();
        public List<float> detections = new List<float>();
        public float firstDetection = 0;
        public float mean = 0;
        public float devStd = 0;
        public int alert = 0;
        public bool alarm = false;
        public DateTime previous_time;
        public Pin(string name)  { previous_time = DateTime.Now; this.name = name; }
        public void AddElement(float value)
        {
            if (firstdetection_list.Count < 10) {
                firstdetection_list.Add(value); detections.Add(value);
                if (firstdetection_list.Count == 9) {
                    firstDetection = Median(firstdetection_list.ToArray());
                    mean = firstDetection;
                    devStd = DevStd(firstdetection_list.ToArray());
                }
            }
            else detections.Add(value);
        }
        public void Initialize()
        {
            alarm = false;
            firstdetection_list.Clear();
            detections.Clear();
            firstDetection = 0;
            mean = 0;
            devStd = 0;
            alert = 0;
            previous_time = DateTime.Now;
        }
        public bool Compute(float value)
        {
            if (!Settings.Allow(name)) { messageToArduino = "F"; if (firstdetection_list.Count > 0) Initialize(); return alarm; }
            else messageToArduino = "N";

            AddElement(value);
            if (firstdetection_list.Count < 10) return false;

            if (detections.Count > 20)
            {
                float weightNewDetection = 0.01f;
                mean = ((1- weightNewDetection) * mean + weightNewDetection * detections[0]);
                devStd = DevStd(detections.ToArray());
                detections.RemoveAt(0);
                //if (name == "Pin1") { Console.WriteLine(mean + "   " + detections[detections.Count-1] + "   " + devStd); }
            }

            float last = detections[detections.Count - 1];
            float prev = detections[detections.Count - 2];
            if (last - prev > 4*devStd || last==0 || Math.Abs(last-mean)>1) alert++;
            else alert--;
            if (alert > 10) alarm = true;
            else if (alert < 0) alert = 0;
            if (previous_time.Subtract(DateTime.Now).Seconds > 3) alarm = true;
            return alarm;
        }
        float DevStd(float[] xs)
        {
            float sum = 0;
            foreach(float n in xs) sum += (float)Math.Pow((n - mean), 2);
            return Math.Max((float)Math.Sqrt(sum / xs.Length), 0.4f);
        }

        float Median(float[] xs)
        {
            Array.Sort(xs);
            return xs[xs.Length / 2];
        }
    }
}
