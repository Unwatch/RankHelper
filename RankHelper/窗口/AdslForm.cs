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
            setting.nCount = textBox_Count.Text.Trim().Equals("")?0:int.Parse(textBox_Count.Text);
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
            if (e.KeyCode== Keys.Enter)
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
    }
}
