using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.Windows.Forms;

namespace RankHelper
{
    class AppUtils
    {
        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        public static extern int FindWindow(string lpClassName, string lpWindowName);
        [DllImport("User32.dll", EntryPoint = "FindWindowEx")]
        public static extern int FindWindowEx(IntPtr lpClassName, IntPtr lpWindowName, string isnull, string anniu);
        [DllImport("user32.dll", EntryPoint = "SendMessageA")]
        public static extern int SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        [DllImport("shell32.dll")]
        public static extern int ShellExecute(IntPtr hwnd, StringBuilder lpszOp, StringBuilder lpszFile, StringBuilder lpszParams, StringBuilder lpszDir, int FsShowCmd);
        [System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint = "SetForegroundWindow")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);//设置此窗体为活动窗体
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int y, int Width, int Height, int flags);// 得到当前活动的窗口 
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern System.IntPtr GetForegroundWindow();
        [DllImport("user32.dll",CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetWindow(HandleRef hWnd, int nCmd);
        [DllImport("user32.dll")]
        public static extern IntPtr SetParent(IntPtr child, IntPtr parent);
        [DllImport("user32.dll", EntryPoint = "GetDCEx",CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetDCEx(IntPtr hWnd, IntPtr hrgnClip, int flags);
        [DllImport("user32.dll",CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool SetWindowPos(HandleRef hWnd, HandleRef hWndInsertAfter, int x, int y, int cx, int cy, int flags);
        [DllImport("user32.dll")]
        public static extern int ReleaseDC(IntPtr window, IntPtr handle);
        [System.Runtime.InteropServices.DllImport("user32", EntryPoint = "SetForegroundWindow")]
        public static extern bool SetFocus(IntPtr hWnd);//设置此窗体为活动窗体

        const int WM_CLOSE = 0x10;   //关闭
        const uint WM_DESTROY = 0x02;
        const uint WM_QUIT = 0x12;

        public static void CloseOtherApp(string appName)
        {
            IntPtr Window_Handle = (IntPtr)FindWindow(null, appName);//查找所有的窗体，看看想查找的句柄是否存在，Microsoft Word  句柄              //
            if (Window_Handle == IntPtr.Zero)   //如果没有查找到相应的句柄
            {
            }
            else    //查找到相应的句柄
            {
                SendMessage(Window_Handle, WM_CLOSE, 0, 0);   //关闭窗体
            }
        }

        public static void CloseExternalApp(string appName)
        {
            IntPtr Window_Handle = (IntPtr)FindWindow(null, appName);//查找所有的窗体，看看想查找的句柄是否存在，Microsoft Word  句柄              //
            if (Window_Handle == IntPtr.Zero)   //如果没有查找到相应的句柄
            {
            }
            else    //查找到相应的句柄
            {
                SendMessage(Window_Handle, WM_CLOSE, 0, 0);   //关闭窗体
            }
        }

        public static int OpenExternalApp(string appName)
        {
            return 0;
            AppUtils.CloseOtherApp("流量精灵助手监控器");
            int nResult = ShellExecute(IntPtr.Zero, new StringBuilder("Open"), new StringBuilder(appName), new StringBuilder(""), new StringBuilder(System.Environment.CurrentDirectory), 1);
            return nResult;
        }

        // <summary>          
        // 修改程序在注册表中的键值
        // </summary>          
        // <param name="isAuto">true:开机启动,false:不开机自启</param>        
        public static void AutoStart(bool isAuto)
        {
            try
            {
                if (isAuto == true)
                {
                    RegistryKey R_local = Registry.LocalMachine;//RegistryKey R_local = Registry.CurrentUser;
                    RegistryKey R_run = R_local.CreateSubKey(@"SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Run");
                    //R_run.SetValue("RankHelperService", Application.ExecutablePath);
                    R_run.SetValue("RankHelperService", string.Format("{0}\\RankHelperService.exe", System.Environment.CurrentDirectory));
                    R_run.Close();

                    RegistryKey R_run2 = R_local.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run");
                    //R_run.SetValue("RankHelperService", Application.ExecutablePath);
                    R_run2.SetValue("RankHelperService", string.Format("{0}\\RankHelperService.exe", System.Environment.CurrentDirectory));
                    R_run2.Close();

                    R_local.Close();
                }
                else
                {
                    RegistryKey R_local = Registry.LocalMachine;//RegistryKey R_local = Registry.CurrentUser; 
                    RegistryKey R_run = R_local.CreateSubKey(@"SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Run");
                    R_run.DeleteValue("RankHelperService", false);
                    R_run.Close();

                    RegistryKey R_run2 = R_local.CreateSubKey(@"SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Run");
                    R_run2.DeleteValue("RankHelperService", false);
                    R_run2.Close();
                    R_run2.Close();

                    R_local.Close();
                }
                //GlobalVariant.Instance.UserConfig.AutoStart = isAuto;           
            }
            catch (Exception)
            {
                //MessageBoxDlg dlg = new MessageBoxDlg();    
                //dlg.InitialData("您需要管理员权限修改", "提示", MessageBoxButtons.OK, MessageBoxDlgIcon.Error);
                //dlg.ShowDialog();                
                MessageBox.Show("您需要管理员权限修改！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
