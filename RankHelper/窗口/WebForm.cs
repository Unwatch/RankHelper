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
using mshtml;
using Microsoft.Win32;

namespace RankHelper
{
    public partial class WebForm : Form
    {
        private static ILog Logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public tagTask currentTask;
        public event EventHandler ShowTaskEvent;
        public event EventHandler EndTaskEvent;
        public event EventHandler StopTaskEvent;

        private WebBase webBase;

        public WebForm()
        {
            InitializeComponent();
            InitializeBrowser();

            this.webBrowser_new.DocumentCompleted += WebBrowser_new_DocumentCompleted;
            this.webBrowser_new.NewWindow += WebBrowser_new_NewWindow;
        }

        public void ShowTask(AppEventArgs arg)
        {
            //ShowTaskEvent(this, new AppEventArgs() { message_string = str });
            ShowTaskEvent(this, arg);
        }

        public void EndTask(AppEventArgs arg)
        {
            EndTaskEvent(this, arg);
        }

        private void WebBrowser_new_NewWindow(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            webBrowser_new.Navigate(webBrowser_new.StatusText);
        }

        public void InitializeBrowser()
        {

        }

        void InitWebBrowser()
        {
            //设置webBrowser 
            webBrowser_new.ScriptErrorsSuppressed = true; //禁用错误脚本提示 
            //webBrowser_new.IsWebBrowserContextMenuEnabled = false; //禁用右键菜单 
            //webBrowser_new.WebBrowserShortcutsEnabled = false; //禁用快捷键 
            //webBrowser_new.AllowWebBrowserDrop = false;//禁止拖拽
            //webBrowser_new.ScrollBarsEnabled = true;//禁止滚动条     
        }

        ~WebForm()
        {
            //需要关闭浏览器负载程序时操作
            try
            {

            }
            catch { }
        }

        private void OnApplicationExit(object sender, EventArgs e)
        {

        }

        private void WebBrowser_new_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            ////将所有的链接的目标，指向本窗体 
            //foreach (HtmlElement archor in this.webBrowser_new.Document.Links)
            //{
            //    archor.SetAttribute("target", "_self");
            //}
            ////将所有的FORM的提交目标，指向本窗体 
            //foreach (HtmlElement form in this.webBrowser_new.Document.Forms)
            //{
            //    form.SetAttribute("target", "_self");
            //}

            if (currentTask == null)
            {
                return;
            }

            if (webBrowser_new.ReadyState != WebBrowserReadyState.Complete)// || e.Url.ToString() != webBrowser_new.Url.ToString())
            {
                return;
            }

            if (currentTask == null)
            {
                return;
            }

            switch (currentTask.webState)
            {
                case EWebbrowserState.Start://step1
                    {
                        webBase.webBrowser_DocumentCompleted_Search(sender, e);
                    }
                    break;
                case EWebbrowserState.Search://step2
                    {
                        webBase.webBrowser_DocumentCompleted_SearchSite(sender, e);
                    }
                    break;
                case EWebbrowserState.SearchSite://step3
                    {
                        webBase.webBrowser_DocumentCompleted_SearchSite(sender, e);
                    }
                    break;
                case EWebbrowserState.AccessSite://step4
                    {
                        webBase.webBrowser_DocumentCompleted_AccessSite(sender, e);
                    }
                    break;
                case EWebbrowserState.AccessPage://step5
                    {
                        webBase.webBrowser_DocumentCompleted_AccessPage(sender, e);
                    }
                    break;
                default:
                    break;
            }
        }

        private void WebBrowser_LoadError(object sender, CefSharp.LoadErrorEventArgs e)
        {
            //重置任务
            if (e.Frame.IsMain)
            {
                if (currentTask != null && (currentTask.webState == EWebbrowserState.Start || currentTask.webState == EWebbrowserState.Search
                    || currentTask.webState == EWebbrowserState.SearchSite || currentTask.webState == EWebbrowserState.AccessSite 
                    || currentTask.webState == EWebbrowserState.AccessPage))
                {

                    e.Browser.StopLoad();
                    ShowTaskEvent(this, new AppEventArgs() { message_string = string.Format("加载网页发生错误{0}网址{1},重新执行,任务{2}", e.ErrorText, e.FailedUrl, currentTask.nID) });
                    StartSearch(this, new AppEventArgs() { message_task = currentTask });
                }
            }
            //throw new NotImplementedException();
        }

        internal void StartSearch(object sender, EventArgs e)
        {
            //webBrowser.ShowDevTools();
            AppEventArgs arg = e as AppEventArgs;
            InitWebBrowser();
            currentTask = arg.message_task;
            currentTask.webState = EWebbrowserState.Start;

            if (currentTask.engine == eEngines.Sm)
            {
                if(currentTask.webBrowser == eWebBrowser.IE_mobile
                    || currentTask.webBrowser == eWebBrowser.Chrome_mobile
                    || currentTask.webBrowser == eWebBrowser.Qihu_mobile
                    || currentTask.webBrowser == eWebBrowser.Sogou_mobile
                    || currentTask.webBrowser == eWebBrowser.QQ_mobile
                    || currentTask.webBrowser == eWebBrowser.maxthon_mobile
                    || currentTask.webBrowser == eWebBrowser.theworld_mobile
                    )
                {
                    currentTask.webBrowser += 1;
                }
            }

            switch (currentTask.webBrowser)
            {
                case eWebBrowser.IE:
                    {
                        Appinfo.strUserAgent = "Mozilla / 4.0(compatible; MSIE 7.0; Windows NT 6.1; WOW64; Trident / 7.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; .NET4.0C; .NET4.0E)";
                    }
                    break;
                case eWebBrowser.IE_mobile:
                    {
                        Appinfo.strUserAgent = "Mozilla/5.0 (Linux; Android 8.0.0; SM-G9350 Build/R16NW; wv) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/70.0.3538.17 Mobile Safari/537.36 MicroMessenger/6.7.2.1340(0x260702A3) NetType/WIFI Language/zh_CN";
                    }
                    break;
                case eWebBrowser.Chrome:
                    {
                        Appinfo.strUserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/63.0.3239.132 Safari/537.36";
                    }
                    break;
                case eWebBrowser.Chrome_mobile:
                    {
                        Appinfo.strUserAgent = "Mozilla/5.0 (Linux; Android 8.0.0; MHA-AL00 Build/HUAWEIMHA-AL00) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/63.0.3239.111 HuaweiBrowser/9.0.1.319 Mobile Safari/537.36";
                    }
                    break;
                case eWebBrowser.Qihu:
                    {
                        Appinfo.strUserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/63.0.3239.132 Safari/537.36";
                    }
                    break;
                case eWebBrowser.Qihu_mobile:
                    {
                        Appinfo.strUserAgent = "Mozilla/5.0 (Linux; Android 8.0.0; MHA-AL00 Build/HUAWEIMHA-AL00) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/63.0.3239.111 HuaweiBrowser/9.0.1.319 Mobile Safari/537.36";
                    }
                    break;
                case eWebBrowser.QQ:
                    {
                        Appinfo.strUserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/63.0.3239.132 Safari/537.36";
                    }
                    break;
                case eWebBrowser.QQ_mobile:
                    {
                        Appinfo.strUserAgent = "Mozilla/5.0 (Linux; Android 8.0.0; MHA-AL00 Build/HUAWEIMHA-AL00) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/63.0.3239.111 HuaweiBrowser/9.0.1.319 Mobile Safari/537.36";
                    }
                    break;
                case eWebBrowser.Sogou:
                    {
                        Appinfo.strUserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/63.0.3239.132 Safari/537.36";
                    }
                    break;
                case eWebBrowser.Sogou_mobile:
                    {
                        Appinfo.strUserAgent = "Mozilla/5.0 (Linux; Android 8.0.0; MHA-AL00 Build/HUAWEIMHA-AL00) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/63.0.3239.111 HuaweiBrowser/9.0.1.319 Mobile Safari/537.36";
                    }
                    break;
                case eWebBrowser.maxthon:
                    {
                        Appinfo.strUserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/63.0.3239.132 Safari/537.36";
                    }
                    break;
                case eWebBrowser.maxthon_mobile:
                    {
                        Appinfo.strUserAgent = "Mozilla/5.0 (Linux; Android 8.0.0; MHA-AL00 Build/HUAWEIMHA-AL00) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/63.0.3239.111 HuaweiBrowser/9.0.1.319 Mobile Safari/537.36";
                    }
                    break;
                case eWebBrowser.theworld:
                    {
                        Appinfo.strUserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/63.0.3239.132 Safari/537.36";
                    }
                    break;
                case eWebBrowser.theworld_mobile:
                    {
                        Appinfo.strUserAgent = "Mozilla/5.0 (Linux; Android 8.0.0; MHA-AL00 Build/HUAWEIMHA-AL00) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/63.0.3239.111 HuaweiBrowser/9.0.1.319 Mobile Safari/537.36";
                    }
                    break;
            }

            switch (currentTask.engine)
            {
                case eEngines.Baidu:
                    {
                        webBase = new Baidu(this, "", "");
                    }
                    break;
                case eEngines.Qihu:
                    {
                        webBase = new Qihu(this, "", "");
                    }
                    break;
                case eEngines.Sogou:
                    {
                        webBase = new Sogou(this, "", "");
                    }
                    break;
                case eEngines.Sm:
                    {
                        webBase = new Sm(this, "", "");
                    }
                    break;
                default:
                    break;
            }
        }

        internal void StopSearch(object sender, EventArgs e)
        {
            currentTask = null;
            webBrowser_new.Stop();
        }

        private void WebForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            StopTaskEvent(this, new AppEventArgs(){});
            return;
        }

        /// <summary>
        /// 获取webbrowser中html元素的屏幕坐标
        /// </summary>
        /// <param name="webBrowser"></param>
        /// <param name="htmlElem"></param>
        /// <returns></returns>
        public Point GetHtmlElementClientPoint(HtmlElement htmlElement)
        {
            Point p = GetOffset(htmlElement);

            HTMLDocument doc = webBrowser_new.Document.DomDocument as HTMLDocument;

            int sl = int.Parse(doc.documentElement.getAttribute("ScrollLeft").ToString());
            int st = int.Parse(doc.documentElement.getAttribute("ScrollTop").ToString());
            int nPos_x = 0;
            int nPos_y = 0;

            //加上窗体的位置及控件的位置及窗体边框，50和8是窗体边框，不同的元素宽高不一样，需要可适当调整
            p.X += htmlElement.OffsetRectangle.Left + this.Left + webBrowser_new.Left + nPos_x - sl;
            p.Y += htmlElement.OffsetRectangle.Top + this.Top + webBrowser_new.Top + nPos_y - st;
            return p;
        }

        private Point GetOffset(HtmlElement el)
        {
            //get element pos
            Point pos = new Point(el.OffsetRectangle.Left, el.OffsetRectangle.Top);

            //get the parents pos
            HtmlElement tempEl = el.OffsetParent;
            while (tempEl != null)
            {
                pos.X += tempEl.OffsetRectangle.Left;
                pos.Y += tempEl.OffsetRectangle.Top;
                tempEl = tempEl.OffsetParent;
            }
            return pos;
        }
    }
}
