using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AsyncCall;
using log4net;
using System.Timers;

namespace RankHelper
{
    public partial class StatisticsForm : Form
    {
        private static ILog Logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        AsyncEvent.MyAsyncDelegate<int> del;
        public event EventHandler StartTaskEvent; //使用默认的事件处理委托
        public event EventHandler StopTaskEvent;

        public event EventHandler ChangeTaskEvent;
        public event EventHandler ShowTaskEvent;

        private System.Timers.Timer taskTimer;
        private System.Timers.Timer taskIntervalTimer;
        private System.Timers.Timer showTaskTimer;
        private System.Timers.Timer resetTaskTimer;
        private System.Timers.Timer CheckIntervalTimer;
        private DateTime lastDate;
        public bool bWork { get; private set; }
        bool bExecuteTask = false;
        tagTask LastTask = new tagTask();
        //数据库文件路径
        private string dbPath;

        public StatisticsForm()
        {
            InitializeComponent();
            Init();
            InitDB();
            InitTimer();
            InitTask();
        }

        void Init()
        {
            this.listView_statistics.GridLines = true; //显示表格线
            this.listView_statistics.View = View.Details;//显示表格细节
            //this.listView_statistics.LabelEdit = true; //是否可编辑,ListView只可编辑第一列。
            this.listView_statistics.Scrollable = true;//有滚动条
            this.listView_statistics.HeaderStyle = ColumnHeaderStyle.Nonclickable;//对表头进行设置
            this.listView_statistics.FullRowSelect = true;//是否可以选择行
            //this.listView_statistics.CheckBoxes = true;
            //添加表头
            //this.listView_statistics.Columns.Add("", 0);
            this.listView_statistics.Columns.Add("选择", 60);//0
            this.listView_statistics.Columns.Add("编号", 60);//1
            this.listView_statistics.Columns.Add("关键字", 110);//2
            this.listView_statistics.Columns.Add("网站标题", 110);//3
            this.listView_statistics.Columns.Add("网站链接", 110);//4
            this.listView_statistics.Columns.Add("搜索引擎", 110);//5
            this.listView_statistics.Columns.Add("停留时间", 110);//6
            this.listView_statistics.Columns.Add("站内外", 110);//7
            this.listView_statistics.Columns.Add("搜索页码", 110);//8
            this.listView_statistics.Columns.Add("浏览器", 110);//9

            this.listView_statistics.Columns.Add("今天有效流量", 110);//10
            this.listView_statistics.Columns.Add("今天无效流量", 110);//11
            this.listView_statistics.Columns.Add("总流量", 110);//12
            this.listView_statistics.Columns.Add("今天执行次数", 110);//13
            this.listView_statistics.Columns.Add("日限制", 110);//14
            this.listView_statistics.Columns.Add("停留时间", 110);//15
            this.listView_statistics.Columns.Add("上次访问时间", 110);//16

            this.listView_statistics.Columns.Add("今天内页有效流量", 110);//17
            this.listView_statistics.Columns.Add("今天内页无效流量", 130);//18
            this.listView_statistics.Columns.Add("内页总流量", 110);//19
            this.listView_statistics.Columns.Add("内页停留时间", 110);//20
            this.listView_statistics.Columns.Add("上次访问时间", 110);//21
            this.listView_statistics.Columns.Add("发布时间", 110);//22
            //添加各项
            //ListViewItem[] p = new ListViewItem[2];
            //p[0] = new ListViewItem(new string[] { "", "aaaa", "bbbb" });

            //this.listView_statistics.Items.AddRange(p);
            this.listView_statistics.Scrollable = true;

        }

        void InitDB()
        {
            //数据库文件路径就在运行目录下
            dbPath = $"{Environment.CurrentDirectory}\\task.db";
        }

        void InitTimer()
        {
            this.taskTimer = new System.Timers.Timer();
            this.taskTimer.Elapsed += new System.Timers.ElapsedEventHandler(taskTimer_Elapsed);
            this.taskTimer.AutoReset = true;
            this.taskTimer.Interval = 1000 * 60 * 5;
            this.taskTimer.Enabled = false;

            this.taskIntervalTimer = new System.Timers.Timer();
            this.taskIntervalTimer.Elapsed += new System.Timers.ElapsedEventHandler(taskIntervalTimer_Elapsed);
            this.taskIntervalTimer.AutoReset = true;
            this.taskIntervalTimer.Interval = 1000 * 60 * 1;
            this.taskIntervalTimer.Enabled = false;

            this.showTaskTimer = new System.Timers.Timer();
            this.showTaskTimer.Elapsed += new System.Timers.ElapsedEventHandler(showTaskTimer_Elapsed);
            this.showTaskTimer.AutoReset = true;
            this.showTaskTimer.Interval = 1000 * 5;
            this.showTaskTimer.Enabled = false;

            this.resetTaskTimer = new System.Timers.Timer();
            this.resetTaskTimer.Elapsed += new System.Timers.ElapsedEventHandler(resetTaskTimer_Elapsed);
            this.resetTaskTimer.AutoReset = true;
            this.resetTaskTimer.Interval = 1000 * 60;
            this.resetTaskTimer.Enabled = false;
            this.resetTaskTimer.Start();

            this.CheckIntervalTimer = new System.Timers.Timer();
            this.CheckIntervalTimer.Elapsed += CheckIntervalTimer_Elapsed;
            this.CheckIntervalTimer.AutoReset = true;
            this.CheckIntervalTimer.Interval = 1000 * 5;
            this.CheckIntervalTimer.Enabled = false;
            this.CheckIntervalTimer.SynchronizingObject = this;

            this.timer_checkbox.Interval = 1000 * 3;
            this.timer_checkbox.Enabled = false;
            this.timer_checkbox.Tick += Timer_checkbox_Tick;

            lastDate = DateTime.Now;
        }

        internal void CheckWork(object sender, EventArgs e)
        {
            checkBox_work.Checked = true;
        }

        void InitTask()
        {
            button_wait.Hide();
            bWork = false;
            del = new AsyncEvent.MyAsyncDelegate<int>(DoAsyncEvent);
        }

        void AddTask(tagTask task)
        {
            //ListViewItem[] item = new ListViewItem[1];
            //item[0] = new string[] { "", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20" };
            //this.listView_statistics.Items.AddRange(item);

            string[] arrayTask = new string[23];
            string strCheck = task.bCheck ? "启用" : "未启用";
            arrayTask = new string[] {
                strCheck,//0
                task.nID.ToString(),//1
                task.strKeyword,//2
                task.strTitle,//3
                task.strSiteUrl,//4
                define.GetEnumName(task.engine),//5
                "",//6
                define.GetEnumName(task.pageAccessType),//7
                task.nCountPage.ToString(),//8
                define.GetEnumName(task.webBrowser),//9
                task.nCountVaildToday.ToString(),//10
                task.nCountInvaildToday.ToString(),//11
                task.nCountTotal.ToString(),//12
                task.nCountExcuteToday.ToString(),//13
                task.nCountLimit.ToString(),//14
                "15",//15
                "16",//16
                task.nCountPageVaildToday.ToString(),//17
                task.nCountInvaildToday.ToString(),//18
                task.nCountPageTotal.ToString(),//19
                "20",//20
                "21",//21
                "22"//22
            };
            ListViewItem item = new ListViewItem(arrayTask);

            this.listView_statistics.Items.Add(item);

            for (int i = 0; i < this.listView_statistics.CheckedItems.Count; i++)
            {
                this.listView_statistics.CheckedItems[i].Checked = true;
                //this.listView_statistics.CheckedItems[i].Selected = false;
            }
        }

        void ChangeTask(tagTask task)
        {
            for (int i = 0; i < this.listView_statistics.Items.Count; i++)
            {
                string strID = this.listView_statistics.Items[i].SubItems[1].Text; //用我们刚取到的index取被选中的某一列的值从0开始

                if (strID == task.nID.ToString())
                {
                    string strCheck = task.bCheck ? "启用" : "未启用";

                    this.listView_statistics.Items[i].SubItems[0].Text = strCheck;
                    this.listView_statistics.Items[i].SubItems[2].Text = task.strKeyword;
                    this.listView_statistics.Items[i].SubItems[3].Text = task.strTitle;
                    this.listView_statistics.Items[i].SubItems[4].Text = task.strSiteUrl;
                    this.listView_statistics.Items[i].SubItems[5].Text = define.GetEnumName(task.engine);
                    this.listView_statistics.Items[i].SubItems[7].Text = define.GetEnumName(task.pageAccessType);
                    this.listView_statistics.Items[i].SubItems[8].Text = task.nCountPage.ToString();
                    this.listView_statistics.Items[i].SubItems[9].Text = define.GetEnumName(task.webBrowser);
                    this.listView_statistics.Items[i].SubItems[13].Text = task.nCountExcuteToday.ToString();
                    this.listView_statistics.Items[i].SubItems[14].Text = task.nCountLimit.ToString();
                    break;
                }
            }
        }

        void DeleteTask(tagTask task)
        {
            bool bFind = false;
            for (int i = 0; i < this.listView_statistics.Items.Count; i++)
            {
                string strID = this.listView_statistics.Items[i].SubItems[1].Text; //用我们刚取到的index取被选中的某一列的值从0开始

                if (strID == task.nID.ToString())
                {
                    this.listView_statistics.Items[i].Remove();
                    bFind = true;
                    break;
                }
            }
        }

        void CountTask(tagTask task)
        {
            for (int i = 0; i < this.listView_statistics.Items.Count; i++)
            {
                string strID = this.listView_statistics.Items[i].SubItems[1].Text; //用我们刚取到的index取被选中的某一列的值从0开始

                if (strID == task.nID.ToString())
                {
                    this.listView_statistics.Items[i].SubItems[10].Text = task.nCountVaildToday.ToString();
                    this.listView_statistics.Items[i].SubItems[11].Text = task.nCountInvaildToday.ToString();
                    this.listView_statistics.Items[i].SubItems[12].Text = task.nCountTotal.ToString();
                    this.listView_statistics.Items[i].SubItems[13].Text = task.nCountExcuteToday.ToString();
                    this.listView_statistics.Items[i].SubItems[17].Text = task.nCountPageVaildToday.ToString();
                    this.listView_statistics.Items[i].SubItems[18].Text = task.nCountPageInvaildToday.ToString();
                    this.listView_statistics.Items[i].SubItems[19].Text = task.nCountPageTotal.ToString();
                    break;
                }
            }
        }

        internal void RefreshStatistics(object sender, EventArgs e)
        {
            AppEventArgs arg = e as AppEventArgs;

            switch (arg.message_task.taskAcion)
            {
                case eTaskAcion.Add:
                    {
                        AddTask(arg.message_task);
                        arg.message_task.taskAcion = eTaskAcion.Change;
                    }
                    break;
                case eTaskAcion.Change:
                    {
                        ChangeTask(arg.message_task);
                    }
                    break;
                case eTaskAcion.Delete:
                    {
                        DeleteTask(arg.message_task);
                    }
                    break;
                default:
                    break;
            }
        }

        internal void RefreshStatisticsAll(object sender, EventArgs e)
        {
            foreach (var task in Appinfo.listTask)
            {
                AddTask(task);
            }
        }

        private void checkBox_work_CheckedChanged(object sender, EventArgs e)
        {
            tagSetting setting = Appinfo.QuerySetting();
            if (setting.strUsername.Trim().Equals("")|| setting.strPwd.Trim().Equals(""))
            {
                MessageBox.Show("ADSL拨号未设置账号密码", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (!checkBox_work.Checked)
            {
                checkBox_work.Text = "开始挂机";
                AppUtils.CloseOtherApp("流量精灵助手监控器");
            }
            else
            {
                checkBox_work.Text = "停止挂机";
                if (AppUtils.OpenExternalApp("RankHelperService.exe") ==42)
                {

                }
                else if (AppUtils.OpenExternalApp("RankHelperService") == 42)
                {

                }
            }
            DoAsyncEvent();
            //IAsyncResult result = del.BeginInvoke(new AsyncCallback(CallBack), null);
        }

        /// <summary>  
        /// 回调函数得到异步线程的返回结果  
        /// </summary>  
        /// <param name="iasync"></param>  
        public void CallBack(IAsyncResult iasync)
        {
            if (AsyncEvent.CallBack<int>(iasync) > 0)
            {
                //MessageBox.Show("成功");
            }
        }
        /// <summary>  
        /// 执行方法  
        /// </summary>  
        /// <returns></returns>  
        private int DoAsyncEvent()
        {
            try
            {
                //foreach (var item in Appinfo.listTask)
                //{
                //    if (item.nID.Equals(nTaskID))
                //    {
                //        return item;
                //    }
                //}
                this.taskTimer.Stop();
                this.taskIntervalTimer.Stop();
                this.showTaskTimer.Stop();

                //停止挂机
                if (!checkBox_work.Checked)
                {
                    bWork = false;
                    bExecuteTask = false;
                    StopTaskEvent(this, new AppEventArgs() { });
                    ShowTaskEvent(this, new AppEventArgs() { message_string = "未开始挂机" });
                }
                else
                {
                    bWork = true;
                    bExecuteTask = false;

                    //删除缓存、cookie文件
                    //FileUtils.DeleteFolder(System.Environment.CurrentDirectory+"\\"+ "BrowserCache");
                    //清除缓存
                    string cachePath = Environment.GetFolderPath(Environment.SpecialFolder.InternetCache);
                    //获取缓存路径
                    //DirectoryInfo di = new DirectoryInfo(cachePath);
                    //foreach (FileInfo fi in di.GetFiles("*.*", SearchOption.AllDirectories))//遍历所有的文件夹 删除里面的文件
                    //{
                    //    try
                    //    {
                    //        fi.Delete();
                    //    }
                    //    catch
                    //    {

                    //    }
                    //}
                    //BrowserUtils.ClearCache();
                    ShowTaskEvent(this, new AppEventArgs() { message_string = "删除缓存、cookie文件......" });
                    System.Threading.Thread.Sleep(5000);
                    ShowTaskEvent(this, new AppEventArgs() { message_string = "删除完成" });
                    //更换IP
                    if (Appinfo.GetIP().Equals(NetworkUtils.GetIpAddress()))
                    {
                        //ShowTaskEvent(this, new AppEventArgs() { message_string = "更换IP" });
                        //this.taskIntervalTimer.Start();
                        //return -1;
                    }

                    bool bNewTurn = false;
                    foreach (var task in Appinfo.listTask)
                    {
                        if (LastTask != null && LastTask.nID == Appinfo.listTask[Appinfo.listTask.Count - 1].nID)
                        {
                            bNewTurn = true;
                        }
                    }

                    foreach (var task in Appinfo.listTask)
                    {
                        //每轮任务顺序执行
                        if (!task.bCheck)
                        {
                            continue;
                        }
                        if (!bNewTurn && task.nID <= LastTask.nID)
                        {
                            continue;
                        }
                        if (task.nCountExcuteToday >= task.nCountLimit)
                        {
                            continue;
                        }
                        //todo 执行时间检查
                        int nCount = 0;
                        int nCountExcute = 0;
                        switch (DateTime.Now.Hour)
                        {
                            case 0:
                                {
                                    nCount = task.tagtempleTime.nCount00;
                                    nCountExcute = task.tagtempleTime.nCount00_Excute;
                                }
                                break;
                            case 1:
                                {
                                    nCount = task.tagtempleTime.nCount01;
                                    nCountExcute = task.tagtempleTime.nCount01_Excute;
                                }
                                break;
                            case 2:
                                {
                                    nCount = task.tagtempleTime.nCount02;
                                    nCountExcute = task.tagtempleTime.nCount02_Excute;
                                }
                                break;
                            case 3:
                                {
                                    nCount = task.tagtempleTime.nCount03;
                                    nCountExcute = task.tagtempleTime.nCount03_Excute;
                                }
                                break;
                            case 4:
                                {
                                    nCount = task.tagtempleTime.nCount04;
                                    nCountExcute = task.tagtempleTime.nCount04_Excute;
                                }
                                break;
                            case 5:
                                {
                                    nCount = task.tagtempleTime.nCount05;
                                    nCountExcute = task.tagtempleTime.nCount05_Excute;
                                }
                                break;
                            case 6:
                                {
                                    nCount = task.tagtempleTime.nCount06;
                                    nCountExcute = task.tagtempleTime.nCount06_Excute;
                                }
                                break;
                            case 7:
                                {
                                    nCount = task.tagtempleTime.nCount07;
                                    nCountExcute = task.tagtempleTime.nCount07_Excute;
                                }
                                break;
                            case 8:
                                {
                                    nCount = task.tagtempleTime.nCount08;
                                    nCountExcute = task.tagtempleTime.nCount08_Excute;
                                }
                                break;
                            case 9:
                                {
                                    nCount = task.tagtempleTime.nCount09;
                                    nCountExcute = task.tagtempleTime.nCount09_Excute;
                                }
                                break;
                            case 10:
                                {
                                    nCount = task.tagtempleTime.nCount10;
                                    nCountExcute = task.tagtempleTime.nCount10_Excute;
                                }
                                break;
                            case 11:
                                {
                                    nCount = task.tagtempleTime.nCount11;
                                    nCountExcute = task.tagtempleTime.nCount11_Excute;
                                }
                                break;
                            case 12:
                                {
                                    nCount = task.tagtempleTime.nCount12;
                                    nCountExcute = task.tagtempleTime.nCount12_Excute;
                                }
                                break;
                            case 13:
                                {
                                    nCount = task.tagtempleTime.nCount13;
                                    nCountExcute = task.tagtempleTime.nCount13_Excute;
                                }
                                break;
                            case 14:
                                {
                                    nCount = task.tagtempleTime.nCount14;
                                    nCountExcute = task.tagtempleTime.nCount14_Excute;
                                }
                                break;
                            case 15:
                                {
                                    nCount = task.tagtempleTime.nCount15;
                                    nCountExcute = task.tagtempleTime.nCount15_Excute;
                                }
                                break;
                            case 16:
                                {
                                    nCount = task.tagtempleTime.nCount16;
                                    nCountExcute = task.tagtempleTime.nCount16_Excute;
                                }
                                break;
                            case 17:
                                {
                                    nCount = task.tagtempleTime.nCount17;
                                    nCountExcute = task.tagtempleTime.nCount17_Excute;
                                }
                                break;
                            case 18:
                                {
                                    nCount = task.tagtempleTime.nCount18;
                                    nCountExcute = task.tagtempleTime.nCount18_Excute;
                                }
                                break;
                            case 19:
                                {
                                    nCount = task.tagtempleTime.nCount19;
                                    nCountExcute = task.tagtempleTime.nCount19_Excute;
                                }
                                break;
                            case 20:
                                {
                                    nCount = task.tagtempleTime.nCount20;
                                    nCountExcute = task.tagtempleTime.nCount20_Excute;
                                }
                                break;
                            case 21:
                                {
                                    nCount = task.tagtempleTime.nCount21;
                                    nCountExcute = task.tagtempleTime.nCount21_Excute;
                                }
                                break;
                            case 22:
                                {
                                    nCount = task.tagtempleTime.nCount22;
                                    nCountExcute = task.tagtempleTime.nCount22_Excute;
                                }
                                break;
                            case 23:
                                {
                                    nCount = task.tagtempleTime.nCount23;
                                    nCountExcute = task.tagtempleTime.nCount23_Excute;
                                }
                                break;
                            default: break;
                        }

                        if (nCountExcute >= nCount)
                        {
                            continue;
                        }
                        LastTask.nID = task.nID;
                        bExecuteTask = true;
                        StartTaskEvent(this, new AppEventArgs() { message_task = task });
                        this.taskTimer.Start();

                        return 1;
                    }

                    this.taskIntervalTimer.Start();
                    this.showTaskTimer.Start();
                    ShowTaskEvent(this, new AppEventArgs() { message_string = "未找到可以执行的任务,1分钟后开始查找下一个任务" });
                }
                return 1;
            }
            catch (Exception e)
            {
                Logger.Info(string.Format("方法DoAsyncEvent: "), e);

                return -1;
            }
        }

        private void listView_statistics_DoubleClick(object sender, EventArgs e)
        {
            ListView listView = sender as ListView;

            if (listView.SelectedItems.Count > 0) //判断listview有被选中项
            {
                int nIndex = listView.SelectedItems[0].Index; //取当前选中项的index,SelectedItems[0]这必须为0
                string strID = listView.Items[nIndex].SubItems[1].Text; //用我们刚取到的index取被选中的某一列的值从0开始

                tagTask task = Appinfo.QueryTask(int.Parse(strID));
                if (task != null)
                {
                    task.taskAcion = eTaskAcion.Change;
                    ChangeTaskEvent(this, new AppEventArgs() { message_task = task });
                }
                else
                {
                    MessageBox.Show("打开任务失败", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("选择一项任务", "警告", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        //taskTimer定时器执行的任务 
        private void taskTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            this.taskTimer.Stop();
            if (bWork)
            {
                //停止当前任务
                if (bExecuteTask)
                {
                    bExecuteTask = false;
                    StopTaskEvent(this, new AppEventArgs() { });
                    ShowTaskEvent(this, new AppEventArgs() { message_string = "限定时间内未完成当前任务，开始执行下一个任务" });
                    IAsyncResult result = del.BeginInvoke(new AsyncCallback(CallBack), null);
                }
                else
                {
                    //ChangeTaskEvent(this, new AppEventArgs() { message_string = "开始执行下一个任务" });
                }
            }
        }

        private void taskIntervalTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            this.taskIntervalTimer.Stop();
            if (bWork)
            {
                ShowTaskEvent(this, new AppEventArgs() { message_string = "开始查找下一个任务" });
                IAsyncResult result = del.BeginInvoke(new AsyncCallback(CallBack), null);
            }
        }

        private void showTaskTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            this.showTaskTimer.Stop();
            ShowTaskEvent(this, new AppEventArgs() { message_string = "开始挂机，当前状态空闲" });
        }

        private void resetTaskTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (DateTime.Now.Hour == 0 && DateTime.Now.Minute >= 0 && DateTime.Now.Minute <= 5)
            {
                if (lastDate.ToString("yyyy-MM-dd").Equals(DateTime.Now.ToString("yyyy-MM-dd")))
                {
                    return;
                }
                else
                {
                    resetTaskTimer.Stop();
                    lastDate = DateTime.Now;
                    for (int i = 0; i < Appinfo.listTask.Count; i++)
                    {
                        Appinfo.listTask[i].nCountExcuteToday = 0;
                        Appinfo.listTask[i].tagtempleTime.nCount00_Excute = 0;
                        Appinfo.listTask[i].tagtempleTime.nCount01_Excute = 0;
                        Appinfo.listTask[i].tagtempleTime.nCount02_Excute = 0;
                        Appinfo.listTask[i].tagtempleTime.nCount03_Excute = 0;
                        Appinfo.listTask[i].tagtempleTime.nCount04_Excute = 0;
                        Appinfo.listTask[i].tagtempleTime.nCount05_Excute = 0;
                        Appinfo.listTask[i].tagtempleTime.nCount06_Excute = 0;
                        Appinfo.listTask[i].tagtempleTime.nCount07_Excute = 0;
                        Appinfo.listTask[i].tagtempleTime.nCount08_Excute = 0;
                        Appinfo.listTask[i].tagtempleTime.nCount09_Excute = 0;
                        Appinfo.listTask[i].tagtempleTime.nCount10_Excute = 0;
                        Appinfo.listTask[i].tagtempleTime.nCount11_Excute = 0;
                        Appinfo.listTask[i].tagtempleTime.nCount12_Excute = 0;
                        Appinfo.listTask[i].tagtempleTime.nCount13_Excute = 0;
                        Appinfo.listTask[i].tagtempleTime.nCount14_Excute = 0;
                        Appinfo.listTask[i].tagtempleTime.nCount15_Excute = 0;
                        Appinfo.listTask[i].tagtempleTime.nCount16_Excute = 0;
                        Appinfo.listTask[i].tagtempleTime.nCount17_Excute = 0;
                        Appinfo.listTask[i].tagtempleTime.nCount18_Excute = 0;
                        Appinfo.listTask[i].tagtempleTime.nCount19_Excute = 0;
                        Appinfo.listTask[i].tagtempleTime.nCount20_Excute = 0;
                        Appinfo.listTask[i].tagtempleTime.nCount21_Excute = 0;
                        Appinfo.listTask[i].tagtempleTime.nCount22_Excute = 0;
                        Appinfo.listTask[i].tagtempleTime.nCount23_Excute = 0;
                    }

                    ShowTaskEvent(this, new AppEventArgs() { message_string = "0点开始执行当天任务" });
                    resetTaskTimer.Start();
                }
            }
        }

        internal void EndTask(object sender, EventArgs e)
        {
            AppEventArgs arg = e as AppEventArgs;

            for (int i = 0; i < Appinfo.listTask.Count; i++)
            {
                if (arg.message_task.nID == Appinfo.listTask[i].nID)
                {
                    switch (arg.message_task.webState)
                    {
                        case EWebbrowserState.Start:
                            {

                            }
                            break;
                        case EWebbrowserState.Search:
                            {

                            }
                            break;
                        case EWebbrowserState.SearchSite:
                            {
                                Appinfo.listTask[i].nCountExcuteToday += 1;
                                AddExcuteCount(Appinfo.listTask[i]);
                                if (arg.message_bool)
                                {
                                    Appinfo.listTask[i].nCountVaildToday += 1;
                                }
                                else
                                {
                                    Appinfo.listTask[i].nCountInvaildToday += 1;
                                    ShowTaskEvent(this, new AppEventArgs() { message_string = string.Format("未在搜索引擎找到符合的标题或链接，结束该任务，任务{0}", Appinfo.listTask[i].nID) });
                                }
                                Appinfo.listTask[i].nCountTotal += 1;
                                Appinfo.UpdateIP(NetworkUtils.GetIpAddress());
                                bExecuteTask = false;
                            }
                            break;
                        case EWebbrowserState.SearchPage:
                            {
                                Appinfo.listTask[i].nCountExcuteToday += 1;
                                Appinfo.listTask[i].nCountVaildToday += 1;
                                AddExcuteCount(Appinfo.listTask[i]);
                                Appinfo.listTask[i].nCountTotal += 1;
                                Appinfo.UpdateIP(NetworkUtils.GetIpAddress());
                                bExecuteTask = false;
                            }
                            break;
                        case EWebbrowserState.AccessPage:
                            {
                                Appinfo.listTask[i].nCountExcuteToday += 1;
                                Appinfo.listTask[i].nCountVaildToday += 1;
                                Appinfo.listTask[i].nCountTotal += 1;
                                if (arg.message_task.pageAccessType == ePageAccessType.None)
                                {

                                }
                                else
                                {
                                    if (arg.message_bool)
                                    {
                                        Appinfo.listTask[i].nCountPageVaildToday += 1;
                                        ShowTaskEvent(this, new AppEventArgs() { message_string = string.Format("访问内页成功，结束该任务，任务{0}", Appinfo.listTask[i].nID) });
                                        Appinfo.listTask[i].nCountPageTotal += 1;
                                    }
                                    else
                                    {
                                        Appinfo.listTask[i].nCountPageInvaildToday += 1;
                                        Appinfo.listTask[i].nCountPageTotal += 1;
                                        ShowTaskEvent(this, new AppEventArgs() { message_string = string.Format("未找到符合的内页，结束该任务，任务{0}", Appinfo.listTask[i].nID) });
                                    }
                                }
                                bExecuteTask = false;
                            }
                            break;
                        default:
                            break;
                    }

                    Appinfo.CountTask(Appinfo.listTask[i]);
                    //执行完成一个任务关闭程序
                    System.Environment.Exit(0);
                    break;
                }
            }
        }

        internal void AddExcuteCount(tagTask task)
        {
            switch (DateTime.Now.Hour)
            {
                case 0:
                    {
                        task.tagtempleTime.nCount00+=1;
                    }
                    break;
                case 1:
                    {
                        task.tagtempleTime.nCount01 += 1;
                    }
                    break;
                case 2:
                    {
                        task.tagtempleTime.nCount02 += 1;
                    }
                    break;
                case 3:
                    {
                        task.tagtempleTime.nCount03 += 1;
                    }
                    break;
                case 4:
                    {
                        task.tagtempleTime.nCount04 += 1;
                    }
                    break;
                case 5:
                    {
                        task.tagtempleTime.nCount05 += 1;
                    }
                    break;
                case 6:
                    {
                        task.tagtempleTime.nCount06 += 1;
                    }
                    break;
                case 7:
                    {
                        task.tagtempleTime.nCount07 += 1;
                    }
                    break;
                case 8:
                    {
                        task.tagtempleTime.nCount08 += 1;
                    }
                    break;
                case 9:
                    {
                        task.tagtempleTime.nCount09 += 1;
                    }
                    break;
                case 10:
                    {
                        task.tagtempleTime.nCount10 += 1;
                    }
                    break;
                case 11:
                    {
                        task.tagtempleTime.nCount11 += 1;
                    }
                    break;
                case 12:
                    {
                        task.tagtempleTime.nCount12 += 1;
                    }
                    break;
                case 13:
                    {
                        task.tagtempleTime.nCount13 += 1;
                    }
                    break;
                case 14:
                    {
                        task.tagtempleTime.nCount14 += 1;
                    }
                    break;
                case 15:
                    {
                        task.tagtempleTime.nCount15 += 1;
                    }
                    break;
                case 16:
                    {
                        task.tagtempleTime.nCount16 += 1;
                    }
                    break;
                case 17:
                    {
                        task.tagtempleTime.nCount17 += 1;
                    }
                    break;
                case 18:
                    {
                        task.tagtempleTime.nCount18 += 1;
                    }
                    break;
                case 19:
                    {
                        task.tagtempleTime.nCount19 += 1;
                    }
                    break;
                case 20:
                    {
                        task.tagtempleTime.nCount20 += 1;
                    }
                    break;
                case 21:
                    {
                        task.tagtempleTime.nCount21 += 1;
                    }
                    break;
                case 22:
                    {
                        task.tagtempleTime.nCount22 += 1;
                    }
                    break;
                case 23:
                    {
                        task.tagtempleTime.nCount23 += 1;
                    }
                    break;
                default: break;
            }
        }

        private void checkBox_work_MouseDown(object sender, MouseEventArgs e)
        {
            button_wait.Show();
            checkBox_work.Hide();
            this.CheckIntervalTimer.Start();
        }

        private void CheckIntervalTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            this.CheckIntervalTimer.Stop();
            checkBox_work.Checked = !checkBox_work.Checked;
            checkBox_work.Show();
            button_wait.Hide();
        }

        private void Timer_checkbox_Tick(object sender, EventArgs e)
        {

        }

    }
}
