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
using System.Net.NetworkInformation;
using DotRas;
using System.Collections.ObjectModel;
using System.Net;

namespace RankHelper
{
    public partial class AdslForm : Form
    {
        private static ILog Logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public AdslForm()
        {
            InitializeComponent();
            Init();
            InitData();
        }

        void Init()
        {
            foreach (eConnectionType item in Enum.GetValues(typeof(eConnectionType)))
            {
                comboBox_Connect.Items.Add(define.GetEnumName(item));
            }
            comboBox_Connect.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox_Connect.Enabled = false;

            foreach (eConnectionInvert item in Enum.GetValues(typeof(eConnectionInvert)))
            {
                comboBox_Invert.Items.Add(define.GetEnumName(item));
            }
            comboBox_Invert.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        void InitData()
        {
            tagSetting setting = Appinfo.QuerySetting();
            if (setting == null)
            {
                setting = new tagSetting();
            }
            SetSetting(setting);
        }

        void SetSetting(tagSetting setting)
        {
            checkBox_AutoStart.Checked = setting.bCheck;
            textBox_Username.Text = setting.strUsername;
            textBox_Pwd.Text = setting.strPwd;
            comboBox_Connect.SelectedIndex = (int)setting.connectionType;
            comboBox_Invert.SelectedIndex = (int)setting.connectionInvert;
            textBox_Count.Text = setting.nCount.ToString();
        }

        void GetSetting(tagSetting setting)
        {
            setting.bCheck = checkBox_AutoStart.Checked;
            setting.strUsername = textBox_Username.Text;
            setting.strPwd = textBox_Pwd.Text;
            setting.connectionType = (eConnectionType)comboBox_Connect.SelectedIndex;
            setting.connectionInvert = (eConnectionInvert)comboBox_Invert.SelectedIndex;
            setting.nCount = textBox_Count.Text.Trim().Equals("") ? 0 : int.Parse(textBox_Count.Text);
        }

        private void checkBox_AutoStart_CheckedChanged(object sender, EventArgs e)
        {
            AppUtils.AutoStart(checkBox_AutoStart.Checked);
            tagSetting setting = Appinfo.QuerySetting();
            GetSetting(setting);
            Appinfo.UpdateSetting(setting);
        }

        private void textBox_Count_Leave(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text.Trim().Equals(""))
            {
                textBox.Text = "9999";
            }
            int nTime = int.Parse(textBox.Text);
            if (nTime < 1)
            {
                textBox.Text = "1";
            }
            else if (nTime > 9999)
            {
                textBox.Text = "9999";
            }

            tagSetting setting = Appinfo.QuerySetting();
            GetSetting(setting);
            Appinfo.UpdateSetting(setting);

        }

        private void textBox_Username_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                checkBox_AutoStart.Select();
            }
        }

        private void textBox_Username_Leave(object sender, EventArgs e)
        {
            tagSetting setting = Appinfo.QuerySetting();
            GetSetting(setting);
            Appinfo.UpdateSetting(setting);
        }

        /// <summary>
        /// 创建或更新一个PPPOE连接(指定PPPOE名称)
        /// </summary>
        public void CreateOrUpdatePPPOE(string updatePPPOEname)
        {
            RasDialer dialer = new RasDialer();
            RasPhoneBook allUsersPhoneBook = new RasPhoneBook();
            string path = RasPhoneBook.GetPhoneBookPath(RasPhoneBookType.AllUsers);
            allUsersPhoneBook.Open(path);
            // 如果已经该名称的PPPOE已经存在，则更新这个PPPOE服务器地址
            if (allUsersPhoneBook.Entries.Contains(updatePPPOEname))
            {
                allUsersPhoneBook.Entries[updatePPPOEname].PhoneNumber = " ";
                // 不管当前PPPOE是否连接，服务器地址的更新总能成功，如果正在连接，则需要PPPOE重启后才能起作用
                allUsersPhoneBook.Entries[updatePPPOEname].Update();
            }
            // 创建一个新PPPOE
            else
            {
                string adds = string.Empty;
                ReadOnlyCollection<RasDevice> readOnlyCollection = RasDevice.GetDevices();
                //                foreach (var col in readOnlyCollection)
                //                {
                //                    adds += col.Name + ":" + col.DeviceType.ToString() + "|||";
                //                }
                //                _log.Info("Devices are : " + adds);
                // Find the device that will be used to dial the connection.
                RasDevice device = RasDevice.GetDevices().Where(o => o.DeviceType == RasDeviceType.PPPoE).First();
                RasEntry entry = RasEntry.CreateBroadbandEntry(updatePPPOEname, device);    //建立宽带连接Entry
                entry.PhoneNumber = " ";
                allUsersPhoneBook.Entries.Add(entry);
            }
        }

        /// <summary>
        /// 断开 宽带连接
        /// </summary>
        public void Disconnect()
        {
            ReadOnlyCollection<RasConnection> conList = RasConnection.GetActiveConnections();
            foreach (RasConnection con in conList)
            {
                con.HangUp();
            }
        }

        /// <summary>
        /// 宽带连接，成功返回true,失败返回 false
        /// </summary>
        /// <param name="PPPOEname">宽带连接名称</param>
        /// <param name="username">宽带账号</param>
        /// <param name="password">宽带密码</param>
        /// <returns></returns>
        public bool Connect(string PPPOEname, string username, string password, ref string msg)
        {
            try
            {
                CreateOrUpdatePPPOE(PPPOEname);
                using (RasDialer dialer = new RasDialer())
                {
                    dialer.EntryName = PPPOEname;
                    dialer.AllowUseStoredCredentials = true;
                    dialer.Timeout = 1000;
                    dialer.PhoneBookPath = RasPhoneBook.GetPhoneBookPath(RasPhoneBookType.AllUsers);
                    dialer.Credentials = new NetworkCredential(username, password);
                    dialer.Dial();
                    return true;
                }
            }
            catch (RasException re)
            {
                msg = re.ErrorCode + " " + re.Message;
                MessageBox.Show(msg);
                return false;
            }
        }

        private void button_connect_Click(object sender, EventArgs e)
        {
            string msg = null;
            tagSetting setting = Appinfo.QuerySetting();
            bool isok = Connect(define.GetEnumName(setting.connectionType), setting.strUsername.ToString(), setting.strPwd.ToString(), ref msg);
        }

        private void button_disconnect_Click(object sender, EventArgs e)
        {
            Disconnect();
        }

        private void button_reconnect_Click(object sender, EventArgs e)
        {
            Disconnect();
            string msg = null;
            tagSetting setting = Appinfo.QuerySetting();
            bool isok = Connect(define.GetEnumName(setting.connectionType), setting.strUsername.ToString(), setting.strPwd.ToString(), ref msg);
        }

        private void button_getIP_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            foreach (RasConnection connection in RasConnection.GetActiveConnections())
            {
                RasIPInfo ipAddresses = (RasIPInfo)connection.GetProjectionInfo(RasProjectionType.IP);
                if (ipAddresses != null)
                {
                    sb.AppendFormat("ClientIP:{0}\r\n", ipAddresses.IPAddress.ToString());
                    sb.AppendFormat("ServerIP:{0}\r\n", ipAddresses.ServerIPAddress.ToString());
                }
                sb.AppendLine();
            }
            MessageBox.Show(sb.ToString());
        }
    }
}
