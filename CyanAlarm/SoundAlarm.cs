using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CyanAlarm
{
    public class SoundAlarm
    {
        public static void Sound_Alarm()
        {
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(Properties.Resources.alarm);
            player.Play();

            System.Timers.Timer aTimer = new System.Timers.Timer(2000);
            aTimer.Elapsed += (o, e) => { aTimer.Dispose(); if (!Principal.uploadOnStorage) player.Stop(); };
            aTimer.AutoReset = true;
            aTimer.Enabled = true;

            if (Principal.uploadOnStorage) System.Diagnostics.Process.Start(@"E:\DOCUMENTI\Workspace Visual Studio\CyanAlarm\CyanAlarm\Music\toMainAudioDevice.vbs");
        }
    }
}
