using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Vue;

namespace HugoWorld
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

            //login
            if (new frmLogin().ShowDialog() != DialogResult.OK)
                Environment.Exit(0);
            
            //manage heroes + class if admin...
            if (new frmManage().ShowDialog() != DialogResult.OK)
                Environment.Exit(0);

            Application.Run(new HugoWorld());
        }
    }
}