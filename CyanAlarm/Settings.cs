using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyanAlarm
{
    public partial class Settings : Form
    {
        public static bool active = false;
        public static bool allAlarms = true;
        public static bool Alarm1 = true;
        public static bool Alarm2 = true;
        public static bool Alarm3 = true;
        public static bool forReal = true;

        public static string getSettings()
        {
            string output = "Settings:";
            output += toString(allAlarms);
            output += toString(Alarm1);
            output += toString(Alarm2);
            output += toString(Alarm3);
            output += toString(forReal);
            return output;
        }
        public static void Interpret(string message)
        {
            try
            {
                string[] splitted = message.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < splitted.Length; i++)
                {
                    if (i == 0) allAlarms = fromString(splitted[i]);
                    if (i == 1) Alarm1 = fromString(splitted[i]);
                    if (i == 2) Alarm2 = fromString(splitted[i]);
                    if (i == 3) Alarm3 = fromString(splitted[i]);
                    if (i == 4) forReal = fromString(splitted[i]);
                }
                if (forReal)
                {
                    Principal.main.textBox1.Text = Properties.Settings.Default.lastReal;
                    Properties.Settings.Default.minAutoRec = Convert.ToInt32(Properties.Settings.Default.lastReal);
                }
                else
                {
                    Principal.main.textBox1.Text = "0";
                    Properties.Settings.Default.minAutoRec = 0;
                }
                Console.WriteLine("Received -> " + getSettings());
            }
            catch (Exception) { }
        }
        private static bool fromString(string value) { if (value == "0") return false; else return true; }
        private static string toString(bool value)
        {
            if (!value) return "0:";
            else return "1:";
        }
        public static bool Allow(string name)
        {
            if (name.Contains("Pin0")) { return Alarm1; }
            else if (name.Contains("Pin1")) { return Alarm2; }
            else if (name.Contains("Pin2")) { return Alarm3; }
            else return true;
        }
        public Settings()
        {
            active = true;
            InitializeComponent();
            FormClosing += (o, e) => { active = false; };
            checkBox4.Checked = allAlarms;
            checkBox1.Checked = Alarm1;
            checkBox2.Checked = Alarm2;
            checkBox3.Checked = Alarm3;
        }

        private void CheckboxChanged()
        {
            onSettingsChanged();
        }

        public static void onSettingsChanged()
        {
            for (int i = 0; i < 5; i++) ServerSide.messagesToSend.Add(Settings.getSettings());
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Alarm1 = checkBox1.Checked;
            if (Alarm1) { allAlarms = true; checkBox4.Checked = true; }
            CheckboxChanged();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            Alarm2 = checkBox2.Checked;
            if (Alarm2) { allAlarms = true; checkBox4.Checked = true; }
            CheckboxChanged();
        }
        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            Alarm3 = checkBox3.Checked;
            if (Alarm3) { allAlarms = true; checkBox4.Checked = true; }
            CheckboxChanged();
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            allAlarms = checkBox4.Checked;
            if (!allAlarms) { checkBox1.Checked = false; checkBox2.Checked = false; checkBox3.Checked = false; }
            CheckboxChanged();
        }


    }
}
