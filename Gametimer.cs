using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Threading;

namespace WPF_Tests
{
    public static class Gametimer
    {
        static DispatcherTimer timer;
        static DateTime startTime;
        static Label labelAusgabe;
        static bool isRunning = false; 

        public static void Init(Label label = null)
        {
            labelAusgabe = label;

            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(3000000);
            timer.Tick += new EventHandler(Tick);
        }

        public static bool IsRunning()
        {
            return isRunning;
        }

        private static void Tick(object sender, EventArgs e)
        {
            TimeSpan spielzeit = DateTime.Now - startTime;
            labelAusgabe.Content = Math.Floor(spielzeit.TotalSeconds);
        }

        public static void Start()
        {
            startTime = DateTime.Now;
            timer.Start();
            isRunning = true;
        }

        public static void Stop()
        {
            timer.Stop();
            isRunning = false;
        }
    }
}
