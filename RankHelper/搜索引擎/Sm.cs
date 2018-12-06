using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mshtml;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Drawing;

namespace RankHelper
{
    public class Sm : WebBase
    {
        public Sm(WebForm webForm, string strPageurl, string strhtml):base(webForm, strPageurl, strhtml)
        {
            webForm.webBrowser_new.Navigate("www.m.sm.cn");
        }
    }
}
