using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Crad.Windows.Forms.Actions.Sample
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new EditorForm());
        }

        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            MessageBox.Show("Woooops! Unexpected error!" + Environment.NewLine + Environment.NewLine + e.Exception.Message);
        }
    }
}