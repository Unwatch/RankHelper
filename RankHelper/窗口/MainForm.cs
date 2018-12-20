using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using log4net;
using System.Runtime.InteropServices;

namespace RankHelper
{
    public partial class MainForm : Form
    {

        private static ILog Logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        StatisticsForm statisticsForm;
        SettingForm settingForm;
        WebForm webForm;
        AdslForm adslForm;
        //定义消息发布的事件  事件是委托的一个特殊实例  事件只能在类的内部触发执行
        //public event EventHandler SendMsgEvent; //使用默认的事件处理委托
        public event EventHandler RefreshStatisticsAllEvent; //使用默认的事件处理委托
        public event EventHandler RefreshStatisticsEvent;
        public event EventHandler StartSearchEvent;
        public event EventHandler StopTaskEvent;
        public event EventHandler EndTaskEvent;

        public event EventHandler ChangeTaskEvent;
        public event EventHandler ShowTabEvent;
        public event EventHandler CheckWorkEvent;

        public MainForm()
        {
            InitializeComponent();
            Init();
            InitTab();
            RegisterEvent();
            RefreshStatisticsAllEvent(this, new AppEventArgs() { });
            ShowIP();
            CheckWork();
        }

        public void Init()
        {
            //设置标题
            //this.Text = string.Format("{0} (版本{1})", Appinfo.strTitleName, Appinfo.strVersion);
            this.Text = string.Format("{0}", Appinfo.strTitleName);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            Appinfo.Init();
            toolStripStatusLabel_Status.Text = "未开始挂机";
        }

        public void InitMenu()
        {
            //添加菜单一
            //ToolStripMenuItem subItem;
            //subItem = AddContextMenu("用户：justin", this.menuStrip_main.Items, null);
            //AddContextMenu("充值积分", subItem.DropDownItems, new EventHandler(MenuClicked_RechargePoint));
        }

        public void InitTab()
        {

            statisticsForm = new StatisticsForm();
            statisticsForm.TopLevel = false;
            tabControlTop.TabPages[0].Controls.Add(statisticsForm);
            //statisticsForm.Parent = tabControlTop.TabPages[0];
            statisticsForm.Show();
            //statisticsForm.Parent = this;

            settingForm = new SettingForm();
            settingForm.TopLevel = false;
            tabControlTop.TabPages[1].Controls.Add(settingForm);
            settingForm.Show();

            webForm = new WebForm();

            webForm.TopLevel = false;
            webForm.Show();
            this.panel.Hide();
            this.panel.BringToFront();
            this.panel.Controls.Add(webForm);

            adslForm = new AdslForm();
            adslForm.TopLevel = false;
            tabControlTop.TabPages[2].Controls.Add(adslForm);
            adslForm.Show();

            //tabControlTop.SelectedIndexChanged += new EventHandler(SelectedIndexChanged_tabControlTop);
            tabControlTop.Dock = DockStyle.Fill;
        }

        void RegisterEvent()
        {
            RefreshStatisticsEvent += statisticsForm.RefreshStatistics;
            RefreshStatisticsAllEvent += statisticsForm.RefreshStatisticsAll;
            settingForm.RefreshStatisticsEvent += RefreshStatistics;
            ShowTabEvent += settingForm.ShowTab;

            statisticsForm.StartTaskEvent += StartSearch;
            statisticsForm.StopTaskEvent += StopSearch;
            webForm.StopTaskEvent += StopSearch;
            statisticsForm.ChangeTaskEvent += ChangeTask;
            statisticsForm.ShowTaskEvent += ShowTaskStatus;
            statisticsForm.ShowIPEvent += UpdateIP;

            ChangeTaskEvent += settingForm.ChangeTask;

            StartSearchEvent += webForm.StartSearch;
            StopTaskEvent += webForm.StopSearch;
            webForm.EndTaskEvent += EndTask;

            EndTaskEvent += statisticsForm.EndTask;

            webForm.ShowTaskEvent += ShowTaskStatus;
            CheckWorkEvent += statisticsForm.CheckWork;
        }

        /// <summary>
        /// 添加子菜单
        /// </summary>
        /// <param name="text">要显示的文字，如果为 - 则显示为分割线</param>
        /// <param name="cms">要添加到的子菜单集合</param>
        /// <param name="callback">点击时触发的事件</param>
        /// <returns>生成的子菜单，如果为分隔条则返回null</returns>
        ToolStripMenuItem AddContextMenu(string text, ToolStripItemCollection cms, EventHandler callback)
        {
            if (text == "-")
            {
                ToolStripSeparator tsp = new ToolStripSeparator();
                cms.Add(tsp);
                return null;
            }
            else if (!string.IsNullOrEmpty(text))
            {
                ToolStripMenuItem tsmi = new ToolStripMenuItem(text);
                tsmi.Tag = text + "TAG";
                if (callback != null) tsmi.Click += callback;
                cms.Add(tsmi);

                return tsmi;
            }

            return null;
        }

        void MenuClicked_RechargePoint(object sender, EventArgs e)
        {
            //((sender as ToolStripMenuItem).Tag)强制转换
            //MessageBox.Show(((sender as ToolStripMenuItem).Text));
            BrowserUtils.OpenBrowserUrl(Appinfo.strUrl_RechargePoint);
        }

        private void tabControlTop_Selected(object sender, TabControlEventArgs e)
        {
            switch (tabControlTop.SelectedIndex)
            {
                case 0:
                    {
                        //if (MessageBox.Show("未保存当前任务,需要退出吗?", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning)== DialogResult.OK)
                        //{
                        //    ShowTabEvent(this, new AppEventArgs() { });
                        //}
                        //else
                        //{
                        //    tabControlTop.SelectedIndex = 1;
                        //}
                    }
                    break;
                case 1:
                    {

                    }
                    break;
                case 2:
                    {
                        //if (MessageBox.Show("未保存当前任务,需要退出吗?", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                        //{
                        //    ShowTabEvent(this, new AppEventArgs() { });
                        //}
                        //else
                        //{
                        //    tabControlTop.SelectedIndex = 1;
                        //}
                    }
                    break;
                default:
                    break;
            }
        }

        void SelectedIndexChanged_tabControlTop(object sender, EventArgs e)
        {

        }

        internal void RefreshStatistics(object sender, EventArgs e)
        {
            //RefreshStatisticsEvent(this, new AppEventArgs() { message = tmp });

            AppEventArgs arg = e as AppEventArgs;

            switch (arg.message_task.taskAcion)
            {
                case eTaskAcion.Add:
                    {
                        RefreshStatisticsEvent(this, e);
                    }
                    break;
                case eTaskAcion.Change:
                    {
                        RefreshStatisticsEvent(this, e);
                        tabControlTop.SelectedIndex = 0;
                    }
                    break;
                case eTaskAcion.CancelChange:
                    {
                        tabControlTop.SelectedIndex = 0;
                    }
                    break;
                case eTaskAcion.Delete:
                    {
                        RefreshStatisticsEvent(this, e);
                        tabControlTop.SelectedIndex = 0;
                    }
                    break;
                case eTaskAcion.Reset:
                    {
                        RefreshStatisticsEvent(this, e);
                        tabControlTop.SelectedIndex = 0;
                    }
                    break;
                default:
                    break;
            }

        }

        internal void StartSearch(object sender, EventArgs e)
        {
            AppEventArgs arg = e as AppEventArgs;
            this.panel.Show();

            SetForegroundWindow();
            SetWindowPos();

            ShowTaskStatus(string.Format("开始执行任务{0}", arg.message_task.nID));
            StartSearchEvent(this, e);
        }

        internal void StopSearch(object sender, EventArgs e)
        {
            AppEventArgs arg = e as AppEventArgs;
            //ShowTaskStatus(string.Format("空闲"));
            StopTaskEvent(sender, e);
            this.panel.Hide();

        }

        internal void EndTask(object sender, EventArgs e)
        {
            //ShowTaskStatus(string.Format("空闲"));
            EndTaskEvent(sender, e);
        }

        internal void ChangeTask(object sender, EventArgs e)
        {
            ChangeTaskEvent(this, e);
            tabControlTop.SelectedIndex = 1;
        }

        internal void ShowTaskStatus(object sender, EventArgs e)
        {
            AppEventArgs arg = e as AppEventArgs;
            ShowTaskStatus(arg.message_string);
        }

        internal void UpdateIP(object sender, EventArgs e)
        {
            ShowIP();
        }

        void ShowTaskStatus(string info)
        {
            toolStripStatusLabel_Status.Text = info;
            Logger.Info(info);
        }

        void ShowIP()
        {
            string info = string.Format("当前IP地址：{0}", NetworkUtils.GetIpAddress());
            toolStripStatusLabel_IP.Text = info;
            Logger.Info(info);
        }

        void CheckWork()
        {
            if (Appinfo.bWork == true)
            {
                CheckWorkEvent(this, new AppEventArgs() { });
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("点击确定将会退出程序！", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel)
            {
                e.Cancel = true;    //取消"关闭窗口"事件
                return;
            }
            AppUtils.CloseOtherApp("流量精灵助手监控器");
        }

        private void MainForm_Deactivate(object sender, EventArgs e)
        {
            SetForegroundWindow();
        }

        //把程序显示在最前方
        public void SetForegroundWindow()
        {
            return;
            //IntPtr Window_Handle = (IntPtr)AppUtils.FindWindow(null, Appinfo.strTitleName);//查找所有的窗体，看看想查找的句柄是否存在，Microsoft Word  句柄              //
            //if (Window_Handle != IntPtr.Zero)
            //{
            //    this.Show();
            //    this.Activate();
            //    //this.SetForegroundWindow();
            //    //SetWindowPos(Window_Handle, HWND_TOPMOST, 0, 0, 0, 0, SWP_NOSIZE | SWP_NOMOVE); 
            //    //SetWindowPos(Window_Handle, HWND_NOTOPMOST, 0, 0, 0, 0, SWP_NOSIZE | SWP_NOMOVE); 
            //    //SetForegroundWindow(Window_Handle);           
            //    ////::AttachThreadInput(dwCurID, dwForeID, FALSE);
            //    AppUtils.SetForegroundWindow(Window_Handle);
            //}
            //this.TopMost = true;
            //SetForegroundWindow();

            //this.Activate();
            //this.Focus();
            //AppUtils.SetFocus(this.Handle);
            if (statisticsForm.bWork)
            {
                AppUtils.SetWindowPos(this.Handle, -1, 0, 0, 0, 0, 1 | 2);
            }
            else
            {
                AppUtils.SetWindowPos(this.Handle, -2, 0, 0, 0, 0, 1 | 2);
            }
        }

        public void SetWindowPos()
        {
            if (statisticsForm.bWork)
            {
                this.Location = new Point(0, 0);
            }
        }

        //禁止移动
        protected override void WndProc(ref Message m)
        {
            if (statisticsForm.bWork)
            {
                if (m.Msg != 0x0112 && m.WParam != (IntPtr)0xF012)
                {
                    base.WndProc(ref m);
                }
            }
            else
                base.WndProc(ref m);
        }
    }
}
