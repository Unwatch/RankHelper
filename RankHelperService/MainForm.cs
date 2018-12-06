using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace RankHelperService
{
    public partial class MainForm : Form
    {
        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        private static extern int FindWindow(string lpClassName, string lpWindowName);
        [DllImport("User32.dll", EntryPoint = "FindWindowEx")]
        private static extern int FindWindowEx(IntPtr lpClassName, IntPtr lpWindowName, string isnull, string anniu);
        [DllImport("shell32.dll")]
        public static extern int ShellExecute(IntPtr hwnd, StringBuilder lpszOp, StringBuilder lpszFile, StringBuilder lpszParams, StringBuilder lpszDir, int FsShowCmd);

        private System.Timers.Timer monitorTimer;

        public MainForm()
        {
            InitializeComponent();
            InitNotifyIcon();
            Init();
            InitTimer();
        }

        public void Init()
        {
            //设置标题
            this.Text = string.Format("{0}", Appinfo.strServiceTitleName, Appinfo.strServiceVersion);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.Hide();
            this.ShowInTaskbar = false;
        }

        public void InitNotifyIcon()
        {
            //设置标题
            notifyIcon.Text = string.Format("{0}监控程序，开始挂机将会自动运行,完成一个任务或者{0}停止运行将会自动重启,停止挂机自动停止监控", Appinfo.strTitleName);
            notifyIcon.BalloonTipTitle = string.Format("{0}", Appinfo.strServiceTitleName);
            notifyIcon.BalloonTipText = string.Format("开始挂机将会自动运行,完成一个任务或者{0}停止运行将会自动重启,停止挂机自动停止监控", Appinfo.strTitleName); 
        }

        void InitTimer()
        {
            this.monitorTimer = new System.Timers.Timer();
            this.monitorTimer.Elapsed += MonitorTimer_Elapsed;
            this.monitorTimer.AutoReset = true;
            this.monitorTimer.Interval = 1000 * 10 * 1;//1000 * 60 * 1;
            this.monitorTimer.Enabled = true;
        }

        private void MonitorTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            IntPtr Window_Handle = (IntPtr)FindWindow(null, Appinfo.strTitleName);//查找所有的窗体，看看想查找的句柄是否存在，Microsoft Word  句柄              //
            if (Window_Handle == IntPtr.Zero)   //如果没有查找到相应的句柄
            {
                if(OpenExternalApp("RankHelper.exe") == 42)
                {
                    notifyIcon.BalloonTipText = string.Format("开始运行{0}", Appinfo.strTitleName);
                    notifyIcon.ShowBalloonTip(30000);
                }
                else if (OpenExternalApp("RankHelper") == 42)
                {
                    notifyIcon.BalloonTipText = string.Format("开始运行{0}", Appinfo.strTitleName);
                    notifyIcon.ShowBalloonTip(30000);
                }
            }
        }

        public int OpenExternalApp(string appName)
        {
            string fileName = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
            int index = fileName.LastIndexOf("\\");
            string filePath = fileName.Substring(0, index);
            int nResult = ShellExecute(IntPtr.Zero, new StringBuilder("Open"), new StringBuilder(appName), new StringBuilder("Work"), new StringBuilder(filePath), 1);
            notifyIcon.BalloonTipText = string.Format("启动{0}状态{1}", Appinfo.strTitleName, nResult);
            notifyIcon.ShowBalloonTip(30000);
            return nResult;
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)    //最小化到系统托盘            
            {
                if (!notifyIcon.BalloonTipText.Equals(""))
                {
                    notifyIcon.Visible = true;    //显示托盘图标
                    notifyIcon.ShowBalloonTip(30000);
                }
                this.Hide();    //隐藏窗口            
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //注意判断关闭事件Reason来源于窗体按钮，否则用菜单退出时无法退出!            
            //if (e.CloseReason == CloseReason.UserClosing)
            //{
            //    e.Cancel = true;    //取消"关闭窗口"事件
            //    this.WindowState = FormWindowState.Minimized;    //使关闭时窗口向右下角缩小的效果
            //    notifyIcon.Visible = true;
            //    this.Hide();
            //    return;
            //}
            notifyIcon.Visible = false;
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            notifyIcon.Visible = false;

        }
    }
}
