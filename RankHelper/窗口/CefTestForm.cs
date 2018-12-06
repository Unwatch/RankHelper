using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp.WinForms;

namespace RankHelper
{
    public partial class CefTestForm : Form
    {
        private readonly ChromiumWebBrowser browser;

        public CefTestForm()
        {
            InitializeComponent();

            //browser = new ChromiumWebBrowser("https://ie.icoa.cn/")
            browser = new ChromiumWebBrowser("www.jianshu.com")
            {
                Dock = DockStyle.Fill,
            };
            this.Controls.Add(browser);

        }

        ~CefTestForm()
        {
            browser.Dispose();
        }
    }
}
