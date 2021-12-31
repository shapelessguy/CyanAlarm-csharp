using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;

namespace CyanAlarm
{
    public partial class Webcam : UserControl
    {
        Timer draw;
        static public bool focusOnButtons = false;
        static public bool canDraw = true;
        static public bool urgent = false;
        public bool interrupted = false;
        public int initialIndex = -1;
        public int initialWidth = -1;
        public Webcam()
        {
            if (Principal.client) return;
            InitializeComponent();
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            draw = new Timer() { Enabled = true, Interval = 100 };
            draw.Tick += Draw;
        }

        FilterInfoCollection filterInfoCollection;
        public VideoCaptureDevice videoCaptureDevice;
        private void button1_Click(object sender, EventArgs e)
        {
            Start();
        }

        private void Webcam_Load(object sender, EventArgs e)
        {
            NewWebs();
            if(initialIndex!= -1 && comboBox1.Items.Count > initialIndex) comboBox1.SelectedIndex = initialIndex;
            videoCaptureDevice = new VideoCaptureDevice();
            Start();
        }

        Bitmap prev_img;
        int iterationsWithoutFrame = -30;
        private void VideoCaptureDevice_NewFrame(object sender, NewFrameEventArgs e)
        {
            //this.Invoke(new Action(() => pictureBox1.Image = null));
            //if (prev_img != null) prev_img.Dispose();
            prev_img = (Bitmap)e.Frame.Clone();
            iterationsWithoutFrame = 0;
        }

        public void Stop()
        {
            if (videoCaptureDevice!= null && videoCaptureDevice.IsRunning) { videoCaptureDevice.Stop(); pictureBox1.Image = null; }// Console.WriteLine(Name + " - Stopped"); }
            
        }
        public void Start()
        {
            if (videoCaptureDevice.IsRunning) Stop();
            if (comboBox1.SelectedIndex == -1) { Console.WriteLine("No selection"); return; }
            videoCaptureDevice = new VideoCaptureDevice(filterInfoCollection[comboBox1.SelectedIndex].MonikerString);

            videoCaptureDevice.NewFrame += VideoCaptureDevice_NewFrame;
            videoCaptureDevice.Start(); iterationsWithoutFrame = -30;
        }
        public bool isMonitoring()
        {
            return videoCaptureDevice.IsRunning;
        }
        private void UpdateWebs()
        {
            FilterInfoCollection updateInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            if (updateInfoCollection.Count != filterInfoCollection.Count) { NewWebs(); return; }
            foreach (FilterInfo filterInfo in updateInfoCollection)
            {
                if(!comboBox1.Items.Contains(filterInfo.Name)) { NewWebs(); return; }
            }
        }
        private void NewWebs()
        {
            comboBox1.Items.Clear();
            filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo filterInfo in filterInfoCollection) comboBox1.Items.Add(filterInfo.Name);
            for(int i=0; i<comboBox1.Items.Count; i++) if ((string)comboBox1.Items[i] == comboBox1.Text) comboBox1.SelectedIndex = i;
        }

        public void Urgent()
        {
            urgent = true;
            interrupted = false;
            if(!videoCaptureDevice.IsRunning) Start();
        }
        int iterations = 0;
        int iter_cleaner = 0;
        private void Draw(object sender, EventArgs e)
        {
            iterations++; iter_cleaner++;
            if (iter_cleaner > 200) { iter_cleaner = 0; GC.Collect(); }
            iterationsWithoutFrame++;
            if (iterations > 1000) iterations = 0;
            if (iterationsWithoutFrame > 30) { iterationsWithoutFrame = 0; prev_img = null; pictureBox1.Image = prev_img; Stop(); return; }
            if (iterations % 20 == 0) { UpdateWebs(); iterations = 1; }
            if (prev_img == null) return;
            if (!canDraw && !urgent) { interrupted = true; Stop(); return; }
            else if(interrupted) { interrupted = false; Start(); return; }
            float proportion = prev_img.PhysicalDimension.Width / prev_img.PhysicalDimension.Height;
            int width = pictureBox1.Width;
            int height = pictureBox1.Height;
            width = (int)((float)pictureBox1.Height * proportion);
            int offsetx = (Width - width)/2;
            if (pictureBox1.Width != width)
                if(this != null && this.Handle != null) this.Invoke(new Action(() => {
                    pictureBox1.Location = new Point(offsetx, pictureBox1.Location.Y);
                    pictureBox1.Size = new Size(width, height);
                    }));
            pictureBox1.Image = prev_img;
        }

        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) { focusOnButtons = true; e.SuppressKeyPress = true; }
        }
    }
}
