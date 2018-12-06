using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace RankHelper
{
    public class KeyUtils
    {
        [DllImport("user32.dll", EntryPoint = "keybd_event")]
        public static extern void keybd_event(
            byte bVk,
            byte bScan,
            int dwFlags,  //这里是整数类型  0 为按下，2为释放  
            int dwExtraInfo  //这里是整数类型 一般情况下设成为 0  
        );

        [DllImport("User32.dll")]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        [DllImport("User32.dll")]
        public extern static void SetCursorPos(int x, int y);

        [DllImport("user32.dll")]
        internal static extern bool OpenClipboard(IntPtr hWndNewOwner);

        [DllImport("user32.dll")]
        internal static extern bool CloseClipboard();

        [DllImport("user32.dll")]
        internal static extern bool SetClipboardData(uint uFormat, IntPtr data);

        //复制黏贴剪贴板
        //http://www.aiuxian.com/article/p-9717.html

        //鼠标左键
        public static void MouseLBUTTON()
        {
            mouse_event(2, 0, 0, 0, 0);
            mouse_event(4, 0, 0, 0, 0);
        }

        public static void Copy(string str)
        {
            Clipboard.SetDataObject(str, true);
            //OpenClipboard(IntPtr.Zero);
            //var ptr = Marshal.StringToHGlobalUni(str);
            //SetClipboardData(13, ptr);
            //CloseClipboard();
            //Marshal.FreeHGlobal(ptr);
            Paste();
        }

        public static void Paste()
        {
            keybd_event(17, 0, 0, 0);//ctrl按下
            keybd_event(86, 0, 0, 0);//V按下
            keybd_event(17, 0, 2, 0);//ctrl释放
            keybd_event(86, 0, 2, 0);//V释放
        }

        //鼠标左键
        public static void Keybd_Enter()
        {
            keybd_event(13, 0, 0, 0);
            keybd_event(13, 0, 2, 0);
        }
    }
}
