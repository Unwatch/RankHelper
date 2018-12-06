using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using log4net;


namespace RankHelper
{
    public partial class SettingForm : Form
    {
        private static ILog Logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        tagTask currentTask;
        public event EventHandler RefreshStatisticsEvent; //使用默认的事件处理委托

        public SettingForm()
        {
            InitializeComponent();
            Init();
            InitData();
        }

        void Init()
        {
            foreach (eEngines item in Enum.GetValues(typeof(eEngines)))
            {
                comboBox_Engines.Items.Add(define.GetEnumName(item));
            }
            comboBox_Engines.SelectedIndex = 0;
            comboBox_Engines.DropDownStyle = ComboBoxStyle.DropDownList;

            foreach (eSearchType item in Enum.GetValues(typeof(eSearchType)))
            {
                comboBox_searchType.Items.Add(define.GetEnumName(item));
            }
            comboBox_searchType.SelectedIndex = 0;
            comboBox_searchType.DropDownStyle = ComboBoxStyle.DropDownList;

            foreach (eWebBrowser item in Enum.GetValues(typeof(eWebBrowser)))
            {
                comboBox_webBrowser.Items.Add(define.GetEnumName(item));
            }
            comboBox_webBrowser.SelectedIndex = 0;
            comboBox_webBrowser.DropDownStyle = ComboBoxStyle.DropDownList;

            foreach (ePageAccessType item in Enum.GetValues(typeof(ePageAccessType)))
            {
                comboBox_PageAccessType.Items.Add(define.GetEnumName(item));
            }
            comboBox_PageAccessType.SelectedIndex = 0;
            comboBox_PageAccessType.DropDownStyle = ComboBoxStyle.DropDownList;

            toolTip_title.IsBalloon = true;
            toolTip_title.SetToolTip(label_Title, "网站标题和链接至少输入一个");
            toolTip_title.SetToolTip(label_siteUrl, "网站标题和链接至少输入一个");

            toolTip_time.IsBalloon = true;
            toolTip_time.SetToolTip(label_EngineTime, "输入1~59，单位秒");
            toolTip_time.SetToolTip(label_siteTime, "输入1~59，单位秒");
            toolTip_time.SetToolTip(textBox_pageTime, "输入1~59，单位秒");

            toolTip_pageCount.IsBalloon = true;
            toolTip_pageCount.SetToolTip(label_CountPage, "搜索的页码数量限制,输入1~99，默认50");

            toolTip_siteCount.IsBalloon = true;
            toolTip_siteCount.SetToolTip(label_CountLimit, "输入1~9999，默认30");

            toolTip_Engines.IsBalloon = true;
            toolTip_Engines.SetToolTip(label_Engines, "神马搜索只支持浏览器移动版，选择非移动版默认设为该浏览器移动版");
            toolTip_Engines.SetToolTip(label_webBrowser, "神马搜索只支持浏览器移动版，选择非移动版默认设为该浏览器移动版");

            foreach (eTempleTime item in Enum.GetValues(typeof(eTempleTime)))
            {
                comboBox_templeTime.Items.Add(define.GetEnumName(item));
            }
            comboBox_templeTime.SelectedIndex = 0;
            comboBox_templeTime.DropDownStyle = ComboBoxStyle.DropDownList;

            textBox_CountLimit.Enabled = false;

        }

        void InitData()
        {
            tagTask newTask = new tagTask();
            SetTask(newTask);
        }

        void SetTask(tagTask task)
        {
            checkBox_check.Checked = task.bCheck;
            comboBox_Engines.SelectedIndex = (int)task.engine;
            comboBox_searchType.SelectedIndex = (int)task.searchType;
            textBox_keyword.Text = task.strKeyword;
            textBox_Title.Text = task.strTitle;
            textBox_siteUrl.Text = task.strSiteUrl;
            textBox_EngineTime.Text = task.nEngineTime.ToString();
            textBox_CountPage.Text = task.nCountPage.ToString();
            comboBox_webBrowser.SelectedIndex = (int)task.webBrowser;
            textBox_CountLimit.Text = task.nCountLimit.ToString();
            textBox_siteTime.Text = task.nSiteTime.ToString();
            comboBox_PageAccessType.SelectedIndex = (int)task.pageAccessType;
            textBox_pageUrl.Text = task.strPageUrl.ToString();
            textBox_pageTime.Text = task.nPageTime.ToString();

            checkBox_00.Checked = task.tagtempleTime.bCheck00;
            checkBox_01.Checked = task.tagtempleTime.bCheck01;
            checkBox_02.Checked = task.tagtempleTime.bCheck02;
            checkBox_03.Checked = task.tagtempleTime.bCheck03;
            checkBox_04.Checked = task.tagtempleTime.bCheck04;
            checkBox_05.Checked = task.tagtempleTime.bCheck05;
            checkBox_06.Checked = task.tagtempleTime.bCheck06;
            checkBox_07.Checked = task.tagtempleTime.bCheck07;
            checkBox_08.Checked = task.tagtempleTime.bCheck08;
            checkBox_09.Checked = task.tagtempleTime.bCheck09;
            checkBox_10.Checked = task.tagtempleTime.bCheck10;
            checkBox_11.Checked = task.tagtempleTime.bCheck11;
            checkBox_12.Checked = task.tagtempleTime.bCheck12;
            checkBox_13.Checked = task.tagtempleTime.bCheck13;
            checkBox_14.Checked = task.tagtempleTime.bCheck14;
            checkBox_15.Checked = task.tagtempleTime.bCheck15;
            checkBox_16.Checked = task.tagtempleTime.bCheck16;
            checkBox_17.Checked = task.tagtempleTime.bCheck17;
            checkBox_18.Checked = task.tagtempleTime.bCheck18;
            checkBox_19.Checked = task.tagtempleTime.bCheck19;
            checkBox_20.Checked = task.tagtempleTime.bCheck20;
            checkBox_21.Checked = task.tagtempleTime.bCheck21;
            checkBox_22.Checked = task.tagtempleTime.bCheck22;
            checkBox_23.Checked = task.tagtempleTime.bCheck23;

            textBox_00.Text = task.tagtempleTime.nCount00.ToString();
            textBox_01.Text = task.tagtempleTime.nCount01.ToString();
            textBox_02.Text = task.tagtempleTime.nCount02.ToString();
            textBox_03.Text = task.tagtempleTime.nCount03.ToString();
            textBox_04.Text = task.tagtempleTime.nCount04.ToString();
            textBox_05.Text = task.tagtempleTime.nCount05.ToString();
            textBox_06.Text = task.tagtempleTime.nCount06.ToString();
            textBox_07.Text = task.tagtempleTime.nCount07.ToString();
            textBox_08.Text = task.tagtempleTime.nCount08.ToString();
            textBox_09.Text = task.tagtempleTime.nCount09.ToString();
            textBox_10.Text = task.tagtempleTime.nCount10.ToString();
            textBox_11.Text = task.tagtempleTime.nCount11.ToString();
            textBox_12.Text = task.tagtempleTime.nCount12.ToString();
            textBox_13.Text = task.tagtempleTime.nCount13.ToString();
            textBox_14.Text = task.tagtempleTime.nCount14.ToString();
            textBox_15.Text = task.tagtempleTime.nCount15.ToString();
            textBox_16.Text = task.tagtempleTime.nCount16.ToString();
            textBox_17.Text = task.tagtempleTime.nCount17.ToString();
            textBox_18.Text = task.tagtempleTime.nCount18.ToString();
            textBox_19.Text = task.tagtempleTime.nCount19.ToString();
            textBox_20.Text = task.tagtempleTime.nCount20.ToString();
            textBox_21.Text = task.tagtempleTime.nCount21.ToString();
            textBox_22.Text = task.tagtempleTime.nCount22.ToString();
            textBox_23.Text = task.tagtempleTime.nCount23.ToString();

            comboBox_templeTime.SelectedIndex = (int)task.templeTime;
            comboBox_templeTime_SelectedIndexChanged(comboBox_templeTime, new EventArgs());

            switch (task.taskAcion)
            {
                case eTaskAcion.Add:
                {
                        button_batchAdd.Enabled = true;
                        button_Add.Enabled = true;
                        button_Change.Enabled = false;
                        button_Cancel.Enabled = false;
                        button_Reset.Enabled = false;
                        button_Delete.Enabled = false;
                }
                break;
                case eTaskAcion.Change:
                case eTaskAcion.Delete:
                    {
                        button_batchAdd.Enabled = false;
                        button_Add.Enabled = false;
                        button_Change.Enabled = true;
                        button_Cancel.Enabled = true;
                        button_Reset.Enabled = true;
                        button_Delete.Enabled = true;
                    }
                    break;
                default:
                    break;
            }
        }

        tagTask GetTask(tagTask task)
        {
            task.bCheck = checkBox_check.Checked;
            task.engine = (eEngines)comboBox_Engines.SelectedIndex;
            task.searchType = (eSearchType)comboBox_searchType.SelectedIndex;
            task.strKeyword = textBox_keyword.Text;
            task.strTitle = textBox_Title.Text;
            task.strSiteUrl = textBox_siteUrl.Text;
            task.nEngineTime = int.Parse(textBox_EngineTime.Text);
            task.nCountPage = int.Parse(textBox_CountPage.Text);
            task.webBrowser = (eWebBrowser)comboBox_webBrowser.SelectedIndex;
            task.nCountLimit = int.Parse(textBox_CountLimit.Text);
            task.nSiteTime = int.Parse(textBox_siteTime.Text);
            task.pageAccessType = (ePageAccessType)comboBox_PageAccessType.SelectedIndex;
            task.strPageUrl = textBox_pageUrl.Text;
            task.nPageTime = int.Parse(textBox_pageTime.Text);
            task.tagtempleTime.bCheck00 = checkBox_00.Checked;
            task.tagtempleTime.bCheck01 = checkBox_01.Checked;
            task.tagtempleTime.bCheck02 = checkBox_02.Checked;
            task.tagtempleTime.bCheck03 = checkBox_03.Checked;
            task.tagtempleTime.bCheck04 = checkBox_04.Checked;
            task.tagtempleTime.bCheck05 = checkBox_05.Checked;
            task.tagtempleTime.bCheck06 = checkBox_06.Checked;
            task.tagtempleTime.bCheck07 = checkBox_07.Checked;
            task.tagtempleTime.bCheck08 = checkBox_08.Checked;
            task.tagtempleTime.bCheck09 = checkBox_09.Checked;
            task.tagtempleTime.bCheck10 = checkBox_10.Checked;
            task.tagtempleTime.bCheck11 = checkBox_11.Checked;
            task.tagtempleTime.bCheck12 = checkBox_12.Checked;
            task.tagtempleTime.bCheck13 = checkBox_13.Checked;
            task.tagtempleTime.bCheck14 = checkBox_14.Checked;
            task.tagtempleTime.bCheck15 = checkBox_15.Checked;
            task.tagtempleTime.bCheck16 = checkBox_16.Checked;
            task.tagtempleTime.bCheck17 = checkBox_17.Checked;
            task.tagtempleTime.bCheck18 = checkBox_18.Checked;
            task.tagtempleTime.bCheck19 = checkBox_19.Checked;
            task.tagtempleTime.bCheck20 = checkBox_20.Checked;
            task.tagtempleTime.bCheck21 = checkBox_21.Checked;
            task.tagtempleTime.bCheck22 = checkBox_22.Checked;
            task.tagtempleTime.bCheck23 = checkBox_23.Checked;
            task.tagtempleTime.nCount00 = int.Parse(textBox_00.Text);
            task.tagtempleTime.nCount01 = int.Parse(textBox_01.Text);
            task.tagtempleTime.nCount02 = int.Parse(textBox_02.Text);
            task.tagtempleTime.nCount03 = int.Parse(textBox_03.Text);
            task.tagtempleTime.nCount04 = int.Parse(textBox_04.Text);
            task.tagtempleTime.nCount05 = int.Parse(textBox_05.Text);
            task.tagtempleTime.nCount06 = int.Parse(textBox_06.Text);
            task.tagtempleTime.nCount07 = int.Parse(textBox_07.Text);
            task.tagtempleTime.nCount08 = int.Parse(textBox_08.Text);
            task.tagtempleTime.nCount09 = int.Parse(textBox_09.Text);
            task.tagtempleTime.nCount10 = int.Parse(textBox_10.Text);
            task.tagtempleTime.nCount11 = int.Parse(textBox_11.Text);
            task.tagtempleTime.nCount12 = int.Parse(textBox_12.Text);
            task.tagtempleTime.nCount13 = int.Parse(textBox_13.Text);
            task.tagtempleTime.nCount14 = int.Parse(textBox_14.Text);
            task.tagtempleTime.nCount15 = int.Parse(textBox_15.Text);
            task.tagtempleTime.nCount16 = int.Parse(textBox_16.Text);
            task.tagtempleTime.nCount17 = int.Parse(textBox_17.Text);
            task.tagtempleTime.nCount18 = int.Parse(textBox_18.Text);
            task.tagtempleTime.nCount19 = int.Parse(textBox_19.Text);
            task.tagtempleTime.nCount20 = int.Parse(textBox_20.Text);
            task.tagtempleTime.nCount21 = int.Parse(textBox_21.Text);
            task.tagtempleTime.nCount22 = int.Parse(textBox_22.Text);
            task.tagtempleTime.nCount23 = int.Parse(textBox_23.Text);

            task.templeTime = (eTempleTime)comboBox_templeTime.SelectedIndex;

            return task;
        }

        private void comboBox_PageAccessType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            switch((ePageAccessType)comboBox.SelectedIndex)
            {
                case ePageAccessType.None:
                case ePageAccessType.Rand:
                    {
                        textBox_pageUrl.Enabled = false;
                        textBox_pageTime.Enabled = false;
                    }
                    break;
                case ePageAccessType.Appoint:
                    {
                        textBox_pageUrl.Enabled = true;
                        textBox_pageTime.Enabled = true;
                    }
                    break;
                default:
                    break;
            }
        }

        //添加任务
        private void button_Add_Click(object sender, EventArgs e)
        {
            currentTask = new tagTask();
            GetTask(currentTask);
            if(!CheckVaild(currentTask))
            {
                return;
            }
            currentTask.taskAcion = eTaskAcion.Add;
            Appinfo.AddTask(currentTask);
            //MainForm mainForm = (MainForm)this.Parent;
            RefreshStatisticsEvent(this, new AppEventArgs() { message_task = currentTask });
            string strInfo;
            strInfo = string.Format("添加一个新任务完毕,编号{0}", currentTask.nID);
            Logger.Info(strInfo);
            MessageBox.Show(strInfo, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            InitData();
        }

        private bool CheckVaild(tagTask task)
        {
            if(task.strKeyword.Trim().Equals(""))
            {
                MessageBox.Show("请输入关键词", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox_keyword.Focus();
                return false;
            }

            if (task.strTitle.Trim().Equals("") && task.strSiteUrl.Trim().Equals(""))
            {
                MessageBox.Show("请输入网站标题或链接", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox_Title.Focus();
                return false;
            }

            if (task.nCountPage < 0 || task.nCountPage > 999)
            {
                MessageBox.Show("请输入正确的搜索页码0~999", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox_CountPage.Focus();
                return false;
            }

            if (task.nCountLimit < 0 || task.nCountLimit > 999)
            {
                MessageBox.Show("请输入正确的每天点击量0~999", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox_CountLimit.Focus();
                return false;
            }

            if(task.pageAccessType == ePageAccessType.Appoint)
            {
                if (task.strPageUrl.Trim().Equals(""))
                {
                    MessageBox.Show("请输入指定访问链接", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox_pageUrl.Focus();
                    return false;
                }
            }

            return true;
        }

        private void comboBox_Engines_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            switch ((eEngines)comboBox.SelectedIndex)
            {
                case eEngines.Baidu:
                    {
                        comboBox_searchType.Enabled = true;
                    }
                    break;
                case eEngines.Qihu:
                case eEngines.Sogou:
                case eEngines.Sm:
                    {
                        comboBox_searchType.Enabled = false;
                        comboBox_searchType.SelectedIndex = 0;
                    }
                    break;
                default:
                    break;
            }
        }

        private void textBox_EngineTime_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if(textBox.Text.Trim().Equals(""))
            {
                return;
            }
            int nTime = int.Parse(textBox.Text);
            if(nTime<1)
            {
                textBox.Text = "1";
            }
            else if (nTime > 59)
            {
                textBox.Text = "59";
            }
        }

        private void textBox_EngineTime_Leave(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text.Trim().Equals(""))
            {
                textBox.Text = "3";
            }
        }

        private void textBox_EngineTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 0x20) e.KeyChar = (char)0;  //禁止空格键
            if ((e.KeyChar == 0x2D) && (((TextBox)sender).Text.Length == 0)) return;   //处理负数
            if (e.KeyChar > 0x20)
            {
                try
                {
                    double.Parse(((TextBox)sender).Text + e.KeyChar.ToString());
                }
                catch
                {
                    e.KeyChar = (char)0;   //处理非法字符
                }
            }
        }

        private void textBox_CountPage_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text.Trim().Equals(""))
            {
                return;
            }
            int nTime = int.Parse(textBox.Text);
            if (nTime < 1)
            {
                textBox.Text = "1";
            }
            else if (nTime > 99)
            {
                textBox.Text = "99";
            }
        }

        private void textBox_CountPage_Leave(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text.Trim().Equals(""))
            {
                textBox.Text = "50";
            }
        }

        private void textBox_CountLimit_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text.Trim().Equals(""))
            {
                return;
            }
            int nTime = int.Parse(textBox.Text);
            if (nTime < 0)
            {
                textBox.Text = "0";
            }
            else if (nTime > 9999)
            {
                textBox.Text = "9999";
            }
        }

        private void textBox_CountLimit_Leave(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text.Trim().Equals(""))
            {
                textBox.Text = "0";
            }
        }

        internal void ChangeTask(object sender, EventArgs e)
        {
            AppEventArgs arg = e as AppEventArgs;
            currentTask = arg.message_task;
            SetTask(currentTask);
        }

        private void button_Change_Click(object sender, EventArgs e)
        {
            GetTask(currentTask);
            if (!CheckVaild(currentTask))
            {
                return;
            }

            currentTask.taskAcion = eTaskAcion.Change;
 
            if (!Appinfo.ChangeTask(currentTask))
            {
                MessageBox.Show("未找到符合的任务", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            RefreshStatisticsEvent(this, new AppEventArgs() { message_task = currentTask });
            string strInfo;
            strInfo = string.Format("修改任务完毕,编号{0}", currentTask.nID);
            MessageBox.Show(strInfo, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            InitData();
        }

        private void button_Delete_Click(object sender, EventArgs e)
        {
            currentTask.taskAcion = eTaskAcion.Delete;

            if (!Appinfo.DeleteTask(currentTask))
            {
                MessageBox.Show("未找到符合的任务", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            RefreshStatisticsEvent(this, new AppEventArgs() { message_task = currentTask });
            string strInfo;
            strInfo = string.Format("删除任务完毕,编号{0}", currentTask.nID);
            MessageBox.Show(strInfo, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            InitData();
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            currentTask.taskAcion = eTaskAcion.CancelChange;

            RefreshStatisticsEvent(this, new AppEventArgs() { message_task = currentTask });
            InitData();
        }

        private void button_Reset_Click(object sender, EventArgs e)
        {
            currentTask.nCountExcuteToday = 0;
            currentTask.tagtempleTime.nCount00_Excute = 0;
            currentTask.tagtempleTime.nCount01_Excute = 0;
            currentTask.tagtempleTime.nCount02_Excute = 0;
            currentTask.tagtempleTime.nCount03_Excute = 0;
            currentTask.tagtempleTime.nCount04_Excute = 0;
            currentTask.tagtempleTime.nCount05_Excute = 0;
            currentTask.tagtempleTime.nCount06_Excute = 0;
            currentTask.tagtempleTime.nCount07_Excute = 0;
            currentTask.tagtempleTime.nCount08_Excute = 0;
            currentTask.tagtempleTime.nCount09_Excute = 0;
            currentTask.tagtempleTime.nCount10_Excute = 0;
            currentTask.tagtempleTime.nCount11_Excute = 0;
            currentTask.tagtempleTime.nCount12_Excute = 0;
            currentTask.tagtempleTime.nCount13_Excute = 0;
            currentTask.tagtempleTime.nCount14_Excute = 0;
            currentTask.tagtempleTime.nCount15_Excute = 0;
            currentTask.tagtempleTime.nCount16_Excute = 0;
            currentTask.tagtempleTime.nCount17_Excute = 0;
            currentTask.tagtempleTime.nCount18_Excute = 0;
            currentTask.tagtempleTime.nCount19_Excute = 0;
            currentTask.tagtempleTime.nCount20_Excute = 0;
            currentTask.tagtempleTime.nCount21_Excute = 0;
            currentTask.tagtempleTime.nCount22_Excute = 0;
            currentTask.tagtempleTime.nCount23_Excute = 0;

            currentTask.taskAcion = eTaskAcion.CancelChange;

            RefreshStatisticsEvent(this, new AppEventArgs() { message_task = currentTask });
            InitData();
        }

        internal void ShowTab(object sender, EventArgs e)
        {
            InitData();
        }

        private void textBox_00_Leave(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text.Trim().Equals(""))
            {
                textBox.Text = "0";
            }
            RefreshTextBox_CountLimit();
        }

        private void textBox_00_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text.Trim().Equals(""))
            {
                return;
            }
            int nTime = int.Parse(textBox.Text);
            if (nTime < 0 )
            {
                textBox.Text = "0";
            }
            else if (nTime > 99)
            {
                textBox.Text = "99";
            }

            RefreshTextBox_CountLimit();
        }

        private void comboBox_templeTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool[] arrayCheck = new bool[] { };
            int[] arrayCount = new int[] { };
            ComboBox comboBox = (ComboBox)sender;
            switch ((eTempleTime)comboBox.SelectedIndex)
            {
                case eTempleTime.Rand:
                    {
                        Random rd = new Random();
                        int i = rd.Next()%10;

                        arrayCheck = new bool[24]{ Convert.ToBoolean(rd.Next()%2),Convert.ToBoolean(rd.Next()%2),Convert.ToBoolean(rd.Next()%2),Convert.ToBoolean(rd.Next()%2),Convert.ToBoolean(rd.Next()%2),Convert.ToBoolean(rd.Next()%2),Convert.ToBoolean(rd.Next()%2),Convert.ToBoolean(rd.Next()%2),Convert.ToBoolean(rd.Next()%2),Convert.ToBoolean(rd.Next()%2),
                                                    Convert.ToBoolean(rd.Next()%2),Convert.ToBoolean(rd.Next()%2),Convert.ToBoolean(rd.Next()%2),Convert.ToBoolean(rd.Next()%2),Convert.ToBoolean(rd.Next()%2),Convert.ToBoolean(rd.Next()%2),Convert.ToBoolean(rd.Next()%2),Convert.ToBoolean(rd.Next()%2),Convert.ToBoolean(rd.Next()%2),Convert.ToBoolean(rd.Next()%2),
                                                    Convert.ToBoolean(rd.Next()%2),Convert.ToBoolean(rd.Next()%2),Convert.ToBoolean(rd.Next()%2),Convert.ToBoolean(rd.Next()%2)};

                        arrayCount = new int[24]{ rd.Next()%10,rd.Next()%10,rd.Next()%10,rd.Next()%10,rd.Next()%10,rd.Next()%10,rd.Next()%10,rd.Next()%10,rd.Next()%10,rd.Next()%10,
                                                    rd.Next()%10,rd.Next()%10,rd.Next()%10,rd.Next()%10,rd.Next()%10,rd.Next()%10,rd.Next()%10,rd.Next()%10,rd.Next()%10,rd.Next()%10,
                                                    rd.Next()%10,rd.Next()%10,rd.Next()%10,rd.Next()%10};
                    }
                    break;
                case eTempleTime.Temple0:
                    {
                        arrayCheck = new bool[24]{ false, false, false, false, false, false, false, false, false, false,
                                                    false,false,false,false,false,false,false,false,false,false,
                                                    false,false,false,false};
                        arrayCount = new int[24]{ 0,0,0,0,0,0,0,0,0,0,
                                                    0,0,0,0,0,0,0,0,0,0,
                                                    0,0,0,0};
                    }
                    break;
                case eTempleTime.Temple1:
                    {
                        arrayCheck = new bool[24]{ true, true, true, true, true, true, true, true, true, true,
                                                    true,true,true,true,true,true,true,true,true,true,
                                                    true,true,true,true};
                        arrayCount = new int[24]{ 1,1,1,1,1,1,1,1,1,1,
                                                    1,1,1,1,1,1,1,1,1,1,
                                                    1,1,1,1};
                    }
                    break;
                case eTempleTime.Temple2:
                    {
                        arrayCheck = new bool[24]{ true, true, true, true, true, true, true, true, true, true,
                                                    true,true,true,true,true,true,true,true,true,true,
                                                    true,true,true,true};
                        arrayCount = new int[24]{ 2,2,2,2,2,2,2,2,2,2,
                                                    2,2,2,2,2,2,2,2,2,2,
                                                    2,2,2,2};
                    }
                    break;
                default:
                    break;
            }

            checkBox_00.Checked = arrayCheck[0];
            checkBox_01.Checked = arrayCheck[1];
            checkBox_02.Checked = arrayCheck[2];
            checkBox_03.Checked = arrayCheck[3];
            checkBox_04.Checked = arrayCheck[4];
            checkBox_05.Checked = arrayCheck[5];
            checkBox_06.Checked = arrayCheck[6];
            checkBox_07.Checked = arrayCheck[7];
            checkBox_08.Checked = arrayCheck[8];
            checkBox_09.Checked = arrayCheck[9];
            checkBox_10.Checked = arrayCheck[10];
            checkBox_11.Checked = arrayCheck[11];
            checkBox_12.Checked = arrayCheck[12];
            checkBox_13.Checked = arrayCheck[13];
            checkBox_14.Checked = arrayCheck[14];
            checkBox_15.Checked = arrayCheck[15];
            checkBox_16.Checked = arrayCheck[16];
            checkBox_17.Checked = arrayCheck[17];
            checkBox_18.Checked = arrayCheck[18];
            checkBox_19.Checked = arrayCheck[19];
            checkBox_20.Checked = arrayCheck[20];
            checkBox_21.Checked = arrayCheck[21];
            checkBox_22.Checked = arrayCheck[22];
            checkBox_23.Checked = arrayCheck[23];

            textBox_00.Text = arrayCount[0].ToString();
            textBox_01.Text = arrayCount[1].ToString();
            textBox_02.Text = arrayCount[2].ToString();
            textBox_03.Text = arrayCount[3].ToString();
            textBox_04.Text = arrayCount[4].ToString();
            textBox_05.Text = arrayCount[5].ToString();
            textBox_06.Text = arrayCount[6].ToString();
            textBox_07.Text = arrayCount[7].ToString();
            textBox_08.Text = arrayCount[8].ToString();
            textBox_09.Text = arrayCount[9].ToString();
            textBox_10.Text = arrayCount[10].ToString();
            textBox_11.Text = arrayCount[11].ToString();
            textBox_12.Text = arrayCount[12].ToString();
            textBox_13.Text = arrayCount[13].ToString();
            textBox_14.Text = arrayCount[14].ToString();
            textBox_15.Text = arrayCount[15].ToString();
            textBox_16.Text = arrayCount[16].ToString();
            textBox_17.Text = arrayCount[17].ToString();
            textBox_18.Text = arrayCount[18].ToString();
            textBox_19.Text = arrayCount[19].ToString();
            textBox_20.Text = arrayCount[20].ToString();
            textBox_21.Text = arrayCount[21].ToString();
            textBox_22.Text = arrayCount[22].ToString();
            textBox_23.Text = arrayCount[23].ToString();

            RefreshTextBox_CountLimit();
        }

        private void RefreshTextBox_CountLimit()
        {
            int nCountLimit = 0;
            nCountLimit += checkBox_00.Checked ? int.Parse((textBox_00.Text == "")?"0": textBox_00.Text) : 0;
            nCountLimit += checkBox_01.Checked ? int.Parse((textBox_01.Text == "")?"0": textBox_01.Text) : 0;
            nCountLimit += checkBox_02.Checked ? int.Parse((textBox_02.Text == "")?"0": textBox_02.Text) : 0;
            nCountLimit += checkBox_03.Checked ? int.Parse((textBox_03.Text == "")?"0": textBox_03.Text) : 0;
            nCountLimit += checkBox_04.Checked ? int.Parse((textBox_04.Text == "")?"0": textBox_04.Text) : 0;
            nCountLimit += checkBox_05.Checked ? int.Parse((textBox_05.Text == "")?"0": textBox_05.Text) : 0;
            nCountLimit += checkBox_06.Checked ? int.Parse((textBox_06.Text == "")?"0": textBox_06.Text) : 0;
            nCountLimit += checkBox_07.Checked ? int.Parse((textBox_07.Text == "")?"0": textBox_07.Text) : 0;
            nCountLimit += checkBox_08.Checked ? int.Parse((textBox_08.Text == "")?"0": textBox_08.Text) : 0;
            nCountLimit += checkBox_09.Checked ? int.Parse((textBox_09.Text == "")?"0": textBox_09.Text) : 0;
            nCountLimit += checkBox_10.Checked ? int.Parse((textBox_10.Text == "")?"0": textBox_10.Text) : 0;
            nCountLimit += checkBox_11.Checked ? int.Parse((textBox_11.Text == "")?"0": textBox_11.Text) : 0;
            nCountLimit += checkBox_12.Checked ? int.Parse((textBox_12.Text == "")?"0": textBox_12.Text) : 0;
            nCountLimit += checkBox_13.Checked ? int.Parse((textBox_13.Text == "")?"0": textBox_13.Text) : 0;
            nCountLimit += checkBox_14.Checked ? int.Parse((textBox_14.Text == "")?"0": textBox_14.Text) : 0;
            nCountLimit += checkBox_15.Checked ? int.Parse((textBox_15.Text == "")?"0": textBox_15.Text) : 0;
            nCountLimit += checkBox_16.Checked ? int.Parse((textBox_16.Text == "")?"0": textBox_16.Text) : 0;
            nCountLimit += checkBox_17.Checked ? int.Parse((textBox_17.Text == "")?"0": textBox_17.Text) : 0;
            nCountLimit += checkBox_18.Checked ? int.Parse((textBox_18.Text == "")?"0": textBox_18.Text) : 0;
            nCountLimit += checkBox_19.Checked ? int.Parse((textBox_19.Text == "")?"0": textBox_19.Text) : 0;
            nCountLimit += checkBox_20.Checked ? int.Parse((textBox_20.Text == "")?"0": textBox_20.Text) : 0;
            nCountLimit += checkBox_21.Checked ? int.Parse((textBox_21.Text == "")?"0": textBox_21.Text) : 0;
            nCountLimit += checkBox_22.Checked ? int.Parse((textBox_22.Text == "")?"0": textBox_22.Text) : 0;
            nCountLimit += checkBox_23.Checked ? int.Parse((textBox_23.Text == "")?"0": textBox_23.Text) : 0;

            textBox_CountLimit.Text = nCountLimit.ToString();
        }

        private void checkBox_00_CheckedChanged(object sender, EventArgs e)
        {
            RefreshTextBox_CountLimit();
        }
    }
}
