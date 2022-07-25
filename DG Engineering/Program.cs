using System;
using System.Windows.Forms;

namespace DG_Engineering
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.ThreadException += Application_ThreadException;
            Application.Run(new LoginWindow());
        }

//        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
//        {
//            MessageBox.Show(@"Error:" + @"

//" + e.Exception.Message,@"Attention",MessageBoxButtons.OK,MessageBoxIcon.Information);
//        }
    }
}
