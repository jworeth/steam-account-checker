using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Text;

namespace steam_checker
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main());
        }

        //static void CurrentDomain_AssemblyLoad(object sender, AssemblyLoadEventArgs e)
        //{
        //    Splash.progressBar1.CreateGraphics().DrawString(e.LoadedAssembly.FullName, new Font("Arial", (float)8.25, FontStyle.Regular), Brushes.Black, new PointF(150, progressBar1.Height / 2 - 7));
        //    //_threading.Logrtb(Splash.label1, e.LoadedAssembly.FullName);
        //    //_threading.SetControlPropertyThreadSafe(label1.Text, "Text", e.LoadedAssembly.FullName.ToString());
        //}

    }
}
