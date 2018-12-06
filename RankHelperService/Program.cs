using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RankHelperService
{
    static class Program
    {
        private static System.Threading.Mutex mutex;
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            mutex = new System.Threading.Mutex(true, "RankHelperService");
            if (mutex.WaitOne(0, false))
            {
                Application.Run(new MainForm());
            }
            else
            {
                //MessageBox.Show("程序已经在运行！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.Exit();
            }
        }
    }
}
