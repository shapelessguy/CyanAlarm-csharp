using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CyanAlarm
{
    static class Program
    {
        /// <summary>
        /// Punto di ingresso principale dell'applicazione.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (!CheckOtherApplications()) return;
            string allowedDir = @"E:\DOCUMENTI\Workspace Visual Studio\CyanAlarm\CyanAlarm\bin\Debug";
            if (Directory.GetCurrentDirectory() != allowedDir) System.Threading.Thread.Sleep(120*1000);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Principal());
        }
        private static bool CheckOtherApplications()
        {
            int j = 0;
            foreach (Process clsProcess in Process.GetProcesses()) { if (clsProcess.ProcessName == Application.ProductName) j++; }
            if (j > 1) return false; else return true;
        }
    }
}
