using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

[assembly: log4net.Config.XmlConfigurator(ConfigFile = "log4net.config", Watch = true)]

namespace RankHelper
{
    static class Program
    {
        private static System.Threading.Mutex mutex;

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            mutex = new System.Threading.Mutex(true, "RankHelper");

            if (mutex.WaitOne(0, false))
            {
                LogHelper.InitLog4Net();
                if (args != null && args.Length > 0) //打印出参数
                {
                    Appinfo.bWork = true;
                }
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
