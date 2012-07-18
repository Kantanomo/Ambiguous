using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Ambiguous
{
    static class Program
    {
        public static bool Devmode = false;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        //static void Main()
        //{
        //    Application.EnableVisualStyles();
        //    Application.SetCompatibleTextRenderingDefault(false);
        //    Application.Run(new Form1());
        //}
        static void Main(string[] Args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Form1 form = new Form1();
            if (Args.Length > 0)
            {
                if (Args[0] == "-Dev")
                    Devmode = true;
            }
            Application.Run(new Form1());
        }
    }
}
