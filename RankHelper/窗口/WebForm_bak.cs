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
using CefSharp;
using System.IO;
using log4net;

namespace RankHelper
{
    public partial class WebForm_bak : Form
    {
        private static ILog Logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly ChromiumWebBrowser webBrowser;
        private static string lib, browser, locales, res;

        int nPos_x = 60;
        int nPos_y = 130;

        string strhtml;
        string strPageurl;
        int nPageIndex = -1;//第几页
        int nItem = -1;//第几项

        private tagTask currentTask;
        public event EventHandler ShowTaskEvent;
        public event EventHandler EndTaskEvent;
        public event EventHandler StopTaskEvent;

        public WebForm_bak()
        {
            InitializeComponent();
            InitializeBrowser();
            var settings = new CefSettings()
            {
                //BrowserSubprocessPath = browser,
                //LocalesDirPath = locales,
                //ResourcesDirPath = res
            };
            settings.Locale = "zh-CN";
            //缓存路径
            settings.CachePath = "BrowserCache";
            settings.CefCommandLineArgs.Add("disable-application-cache", "1");
            settings.CefCommandLineArgs.Add("disable-session-storage", "1");
            //浏览器引擎的语言
            settings.AcceptLanguageList = "zh-CN,zh;q=0.9";
            //settings.LocalesDirPath = "localeDir";
            //日志文件
            settings.LogFile = "LogData";
            settings.PersistSessionCookies = false;
            settings.PersistUserPreferences = false;
            //settings.UserAgent = "Mozilla/5.0 (Linux; Android 8.0.0; SM-G9350 Build/R16NW; wv) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/70.0.3538.17 Mobile Safari/537.36 MicroMessenger/6.7.2.1340(0x260702A3) NetType/WIFI Language/zh_CN";
            settings.UserDataPath = "userData";
            Cef.Initialize(settings);

            webBrowser = new ChromiumWebBrowser()
            {
                Dock = DockStyle.Fill,
            };
            this.panel_webbrowser.Controls.Add(webBrowser);
            webBrowser.FrameLoadEnd += webBrowser_DocumentCompleted;
            webBrowser.LoadError += WebBrowser_LoadError;
            //webBrowser.RequestHandler = new RequestHandler(webBrowser.RequestHandler);
            webBrowser.FrameLoadStart += WebBrowser_FrameLoadStart;

        }

        public void InitializeBrowser()
        {
            lib = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"resources\cefsharp_x86\libcef.dll");
            browser = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"resources\cefsharp_x86\CefSharp.BrowserSubprocess.exe");
            locales = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"resources\cefsharp_x86\locales\");
            res = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"resources\cefsharp_x86\");
            var libraryLoader = new CefLibraryHandle(lib);
            var isValid = !libraryLoader.IsInvalid;
            if (isValid)
            {

            }
        }

        void InitWebBrowser()
        {
            //设置webBrowser 
            //webBrowser.ScriptErrorsSuppressed = true; //禁用错误脚本提示 
            //webBrowser.IsWebBrowserContextMenuEnabled = false; //禁用右键菜单 
            //webBrowser.WebBrowserShortcutsEnabled = false; //禁用快捷键 
            //webBrowser.AllowWebBrowserDrop = false;//禁止拖拽
            //webBrowser.ScrollBarsEnabled = true;//禁止滚动条     
        }

        ~WebForm_bak()
        {
            //需要关闭浏览器负载程序时操作
            try
            {
                webBrowser.CloseDevTools();
                webBrowser.GetBrowser().CloseBrowser(true);
            }
            catch { }

            try
            {
                if (webBrowser != null)
                {
                    webBrowser.Dispose();
                    Cef.Shutdown();
                }
            }
            catch { }

            if (CefSharpSettings.ShutdownOnExit)
            {
                Application.ApplicationExit += OnApplicationExit;
            }
        }

        private void OnApplicationExit(object sender, EventArgs e)
        {
            if (CefSharpSettings.ShutdownOnExit)
            {
                Cef.Shutdown();
            }
        }

        private void WebBrowser_FrameLoadStart(object sender, CefSharp.FrameLoadStartEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void webBrowser_DocumentCompleted(object sender, CefSharp.FrameLoadEndEventArgs e)
        {
            if (currentTask == null)
            {
                return;
            }

            //var list = webBrowser.GetBrowser().GetFrameNames();
            //if (webBrowser.ReadyState != WebBrowserReadyState.Complete || e.Url.ToString() != webBrowser.Url.ToString())
            //if (!e.Task.WebView.IsReady)
            //{
            //    return;
            //}
            //if (e.Url.ToString().Contains("google"))
            //{
            //    return;
            //}

            if (currentTask == null)
            {
                return;
            }
            switch (currentTask.webState)
            {
                case EWebbrowserState.Start://step1
                    {
                        webBrowser_DocumentCompleted_Search(sender, e);
                    }
                    break;
                case EWebbrowserState.Search://step2
                    {
                        //MessageBox.Show("请输入指定访问链接", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        if (e.Url.ToString().Equals("http://www.baidu.com/") || e.Url.ToString().Equals("https://www.baidu.com/"))
                        {
                            return;
                        }
                        bool bLoad = false;

                        if (e.Frame.IsMain)
                        {
                            //strhtml = webBrowser.GetBrowser().MainFrame.GetSourceAsync().Result;

                            //webBrowser.GetBrowser().MainFrame.ViewSource();
                            //webBrowser.GetBrowser().MainFrame.GetSourceAsync().ContinueWith(taskHtml =>
                            //{
                            //    if (!bLoad)
                            //    {
                            //        strhtml = taskHtml.Result;
                            //    }
                            //    bLoad = true;
                            //});

                            webBrowser_DocumentCompleted_SearchSite(sender, e);
                        }
                    }
                    break;
                case EWebbrowserState.AccessSite://step3
                    {
                        //MessageBox.Show("请输入指定访问链接", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        if (e.Url.ToString().Equals("http://www.baidu.com/") || e.Url.ToString().Equals("https://www.baidu.com/"))
                        {
                            return;
                        }
                        bool bLoad = false;

                        if (e.Frame.IsMain)
                        {
                            webBrowser.GetBrowser().MainFrame.ViewSource();
                            webBrowser.GetBrowser().MainFrame.GetSourceAsync().ContinueWith(taskHtml =>
                            {
                                if (!bLoad)
                                {
                                    strhtml = taskHtml.Result;
                                }
                                bLoad = true;
                            });

                            webBrowser_DocumentCompleted_SearchSite(sender, e);
                        }
                    }
                    break;
                case EWebbrowserState.SearchSite://step3
                    {
                        //webBrowser_DocumentCompleted_SearchPage(sender, e);
                    }
                    break;
                case EWebbrowserState.SearchPage://step3
                    {
                        //webBrowser_DocumentCompleted_SearchPage(sender, e);
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
                    || currentTask.webState == EWebbrowserState.SearchSite || currentTask.webState == EWebbrowserState.SearchPage))
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
            //清除缓存
            var cookieManager = CefSharp.Cef.GetGlobalCookieManager();
            cookieManager.DeleteCookies();
            ShowTaskEvent(this, new AppEventArgs() { message_string = string.Format("开始清除缓存,任务{0}", currentTask.nID) });

            currentTask.webState = EWebbrowserState.Start;
            strPageurl = "";
            nPageIndex = 0;

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
                        webBrowser.Load("www.baidu.com");
                    }
                    break;
                case eEngines.Qihu:
                    {
                        webBrowser.Load("www.so.com");
                    }
                    break;
                case eEngines.Sogou:
                    {
                        webBrowser.Load("www.sogou.com");
                    }
                    break;
                case eEngines.Sm:
                    {
                        webBrowser.Load("www.m.sm.cn");
                    }
                    break;
                default:
                    break;
            }
        }

        internal void EndTask(bool bSuccess)
        {
            webBrowser.GetBrowser().StopLoad();
            EndTaskEvent(this, new AppEventArgs(){ message_bool = bSuccess,message_task = currentTask});
        }

        internal void StopSearch(object sender, EventArgs e)
        {
            currentTask = null;
            webBrowser.GetBrowser().StopLoad();
        }   
    
        private void webBrowser_DocumentCompleted_Search(object sender, CefSharp.FrameLoadEndEventArgs e)
        {
            switch (currentTask.engine)
            {
                case eEngines.Baidu:
                    {
                        if (e.Url.ToString().Contains("baidu"))
                        {
                            currentTask.webState = EWebbrowserState.none;
                            e.Browser.StopLoad();
                            //textBox_url.Text = e.Url.ToString();
                            //e.Frame.ExecuteJavaScriptAsync("alert('MainFrame finished loading');");
                            //输入框
                            /*
                            string ele_search = (currentTask.webBrowser == eWebBrowser.IE || currentTask.webBrowser == eWebBrowser.Chrome || currentTask.webBrowser == eWebBrowser.Qihu || currentTask.webBrowser == eWebBrowser.Sogou || currentTask.webBrowser == eWebBrowser.QQ || currentTask.webBrowser == eWebBrowser.maxthon || currentTask.webBrowser == eWebBrowser.theworld)
                                ? String.Format("document.getElementById('kw').value= '{0}'", currentTask.strKeyword)//PC版
                                : String.Format("document.getElementById('index-kw').value= '{0}'", currentTask.strKeyword);//手机版
                            e.Frame.ExecuteJavaScriptAsync(ele_search);
                            */
                            string ele_search = (currentTask.webBrowser == eWebBrowser.IE || currentTask.webBrowser == eWebBrowser.Chrome || currentTask.webBrowser == eWebBrowser.Qihu || currentTask.webBrowser == eWebBrowser.Sogou || currentTask.webBrowser == eWebBrowser.QQ || currentTask.webBrowser == eWebBrowser.maxthon || currentTask.webBrowser == eWebBrowser.theworld)
                                ? String.Format("document.getElementById('kw').getBoundingClientRect()")//PC版
                                : String.Format("document.getElementById('index-kw').getBoundingClientRect()");//手机版                            

                            var task1 = e.Frame.EvaluateScriptAsync(ele_search).ContinueWith(x =>
                            {
                                JavascriptResponse response = x.Result;
                                dynamic dictionary_object = response.Result;
                                int pos_x=0, pos_y = 0;
                                foreach (var item in (IDictionary<string, object>)dictionary_object)
                                {
                                    if (item.Key == "x")
                                    {
                                        pos_x = Convert.ToInt32(item.Value);
                                    }
                                    if (item.Key == "y")
                                    {
                                        pos_y = Convert.ToInt32(item.Value);
                                    }
                                }
                                KeyUtils.SetCursorPos(pos_x+nPos_x, pos_y+nPos_y);
                                KeyUtils.MouseLBUTTON();
                                KeyUtils.Copy(currentTask.strKeyword);
                                KeyUtils.SetCursorPos(750, 110);
                                KeyUtils.MouseLBUTTON();
                                if (currentTask == null)
                                {
                                    return;
                                }
                                currentTask.webState = EWebbrowserState.Search;
                                ShowTaskEvent(this, new AppEventArgs() { message_string = string.Format("开始搜索关键词,任务{0}", currentTask.nID) });
                                System.Threading.Thread.Sleep(5000);
                                e.Browser.Reload();
                            });                                                                                

                            //string ele_submit = (currentTask.webBrowser == eWebBrowser.IE || currentTask.webBrowser == eWebBrowser.Chrome || currentTask.webBrowser == eWebBrowser.Qihu || currentTask.webBrowser == eWebBrowser.Sogou || currentTask.webBrowser == eWebBrowser.QQ || currentTask.webBrowser == eWebBrowser.maxthon || currentTask.webBrowser == eWebBrowser.theworld)
                            //    ? String.Format("document.getElementById('su').click()")//PC版
                            //    : String.Format("document.getElementById('index-bn').click()");//手机版
                            //e.Frame.ExecuteJavaScriptAsync(ele_submit);

                            //e.Frame.ExecuteJavaScriptAsync(ele_submit);

                            //var task = e.Frame.EvaluateScriptAsync(ele_submit).ContinueWith(x =>
                            //{
                            //    var response = x.Result;

                            //    if (response.Success)
                            //    {
                            //        e.Browser.StopLoad();

                            //        //System.Threading.Thread.Sleep(2000);
                            //        e.Browser.Reload();
                            //    }

                            //    if (response.Success && response.Result != null)
                            //    {
                            //        //File.AppendAllText("result.txt", response.Result.ToString());
                            //    }
                            //});
                        }
                    }
                    break;
                case eEngines.Qihu:
                    {

                    }
                    break;
                case eEngines.Sogou:
                    {
                        if (e.Url.ToString().Contains("www.sogou.com"))
                        {
                            currentTask.webState = EWebbrowserState.none;
                            e.Browser.StopLoad();

                            KeyUtils.SetCursorPos(300,340);
                            //System.Threading.Thread.Sleep(1000);
                            KeyUtils.MouseLBUTTON();

                            string ele_search = (currentTask.webBrowser == eWebBrowser.IE || currentTask.webBrowser == eWebBrowser.Chrome || currentTask.webBrowser == eWebBrowser.Qihu || currentTask.webBrowser == eWebBrowser.Sogou || currentTask.webBrowser == eWebBrowser.QQ || currentTask.webBrowser == eWebBrowser.maxthon || currentTask.webBrowser == eWebBrowser.theworld)
                                ? String.Format("document.getElementById('query').getBoundingClientRect()")//PC版
                                : String.Format("document.getElementById('query').getBoundingClientRect()");//手机版

                            var task1 = e.Frame.EvaluateScriptAsync(ele_search).ContinueWith(x =>
                            {
                                JavascriptResponse response = x.Result;
                                dynamic dictionary_object = response.Result;
                                int pos_x = 0, pos_y = 0;
                                foreach (var item in (IDictionary<string, object>)dictionary_object)
                                {
                                    if (item.Key == "x")
                                    {
                                        pos_x = Convert.ToInt32(item.Value);
                                    }
                                    if (item.Key == "y")
                                    {
                                        pos_y = Convert.ToInt32(item.Value);
                                    }
                                }
                            });
                        }
                    }
                    break;
                //    case eEngines.Sm:
                //        {
                //            if (e.Url.ToString().Contains("www.m.sm.cn"))
                //            {
                //                textBox_url.Text = e.Url.ToString();
                //                HtmlDocument doc = webBrowser.Document;
                //                HtmlElement ele_search = doc.GetElementById("kw");
                //                if (ele_search == null)
                //                {
                //                    return;
                //                }

                //                ele_search.InnerText = currentTask.strKeyword;
                //                HtmlElement ele_submit = doc.GetElementById("su");

                //                if (ele_submit == null)
                //                {
                //                    return;
                //                }
                //                ele_submit.InvokeMember("click");
                //            }
                //        }
                //        break;
                default:
                    break;
            }
        }

        private void webBrowser_DocumentCompleted_SearchSite(object sender, CefSharp.FrameLoadEndEventArgs e)
        {
            switch (currentTask.engine)
            {
                case eEngines.Baidu:
                    {
                        if (e.Url.ToString().Contains("baidu"))
                        {
                            currentTask.webState = EWebbrowserState.none;
                            ShowTaskEvent(this, new AppEventArgs() { message_string = string.Format("开始查找网站,当前页码{0},任务{1}", 1,currentTask.nID)});

                            List<tagSearchResult> listSearch = new List<tagSearchResult>();
                            //搜索项目
                            for (int i = 1; i <= 10; i++)//1~10项
                            {
                                string js="";
                                if (!currentTask.strTitle.Trim().Equals("")&&!currentTask.strPageUrl.Trim().Equals(""))
                                {
                                    js = String.Format("document.getElementById('{0}').innerText.match('{1}')||document.getElementById('{0}').innerText.match({'2'})", i, currentTask.strTitle, currentTask.strPageUrl);
                                }
                                else if (!currentTask.strTitle.Trim().Equals(""))
                                {
                                    js = String.Format("document.getElementById('{0}').innerText.match('{1}')", i, currentTask.strTitle);
                                }
                                else if (!currentTask.strPageUrl.Trim().Equals(""))
                                {
                                    js = String.Format("document.getElementById('{0}').innerText.match('{1}')", i, currentTask.strPageUrl);
                                }
                                var task_item = e.Frame.EvaluateScriptAsync(js);
                                //task_item.Id = i;
                                task_item.ContinueWith(t =>
                                {
                                    if (!t.IsFaulted)
                                    {
                                        //诺亚娱乐 - 诺亚娱乐平台官方唯一网站
                                        //百度网址安全中心提醒您：该页面可能存在违法信息！
                                        //诺亚娱乐官方权威指定网站是国内知名有效合法代理登录站点,诺亚娱乐平台是彩界唯一一家拥有合法的娱乐平台注册预测走势图,行业首家拥有上下级聊天工具支持网页手机玩法。
                                        //https://www.nuoya115.com/  - 百度快照
                                        string log = string.Format("方法webBrowser_DocumentCompleted_SearchSite: 查找第{0}页第{0}项", nPageIndex, i);
                                        ShowTaskEvent(this, new AppEventArgs() { message_string = log });
                                        var response = t.Result;
                                        var EvaluateJavaScriptResult = response.Success ? (response.Result ?? "null") : response.Message;

                                        tagSearchResult searchResult = new tagSearchResult();
                                        searchResult.nItem = i;
                                        searchResult.bFinish = true;
                                        searchResult.bMatch = (response.Result==null) ? false:true;
                                        listSearch.Add(searchResult);

                                        bool bAllPageFinish = true;
                                        if (listSearch.Count==10)
                                        {
                                            foreach (var item in listSearch)
                                            {
                                                if (item.bFinish == false)
                                                {
                                                    bAllPageFinish = false;
                                                }
                                            }
                                        }


                                        if (bAllPageFinish)
                                        {
                                            foreach (var item in listSearch)
                                            {
                                                if (item.bMatch == false)
                                                {
                                                    nItem = item.nItem;
                                                    currentTask.webState = EWebbrowserState.SearchSite_Match;
                                                    e.Browser.Reload();
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                });
                            }

                            return;
                            var task = e.Frame.EvaluateScriptAsync("document.getElementById('page').innerHTML");//异步
                            task.ContinueWith(t =>
                            {
                                if (!t.IsFaulted)
                                {
                                    var response = t.Result;
                                    var EvaluateJavaScriptResult = response.Success ? (response.Result ?? "null") : response.Message;
                                    GetBaiduPageurl((string)EvaluateJavaScriptResult);

                                    currentTask.webState = EWebbrowserState.SearchSite;
                                    webBrowser.Load(GetBaiduNextPageurl());
                                }
                            });
                        }
                    }
                    break;
                //    case eEngines.Qihu:
                //        {
                //            if (e.Url.ToString().Contains("www.so.com"))
                //            {
                //                textBox_url.Text = e.Url.ToString();
                //                HtmlDocument doc = webBrowser.Document;
                //                HtmlElement ele_search = doc.GetElementById("kw");
                //                if (ele_search == null)
                //                {
                //                    return;
                //                }

                //                ele_search.InnerText = currentTask.strKeyword;
                //                HtmlElement ele_submit = doc.GetElementById("su");

                //                if (ele_submit == null)
                //                {
                //                    return;
                //                }
                //                ele_submit.InvokeMember("click");
                //            }
                //        }
                //        break;
                //    case eEngines.Sogou:
                //        {
                //            if (e.Url.ToString().Contains("www.sogou.com"))
                //            {
                //                textBox_url.Text = e.Url.ToString();
                //                HtmlDocument doc = webBrowser.Document;
                //                HtmlElement ele_search = doc.GetElementById("kw");
                //                if (ele_search == null)
                //                {
                //                    return;
                //                }

                //                ele_search.InnerText = currentTask.strKeyword;
                //                HtmlElement ele_submit = doc.GetElementById("su");

                //                if (ele_submit == null)
                //                {
                //                    return;
                //                }
                //                ele_submit.InvokeMember("click");
                //            }
                //        }
                //        break;
                //    case eEngines.Sm:
                //        {
                //            if (e.Url.ToString().Contains("www.m.sm.cn"))
                //            {
                //                textBox_url.Text = e.Url.ToString();
                //                HtmlDocument doc = webBrowser.Document;
                //                HtmlElement ele_search = doc.GetElementById("kw");
                //                if (ele_search == null)
                //                {
                //                    return;
                //                }

                //                ele_search.InnerText = currentTask.strKeyword;
                //                HtmlElement ele_submit = doc.GetElementById("su");

                //                if (ele_submit == null)
                //                {
                //                    return;
                //                }
                //                ele_submit.InvokeMember("click");
                //            }
                //        }
                //        break;
                default:
                    break;
            }
        }

        void GetBaiduPageurl(string pageHtml)
        {
            string[] sArray = pageHtml.Split(new string[] { "<a href="}, StringSplitOptions.RemoveEmptyEntries);
            foreach (string item in sArray)
            {
                string tmp = string.Format("><span class=\"fk fkd\">");
                if (item.Contains(tmp))
                {
                    int index = item.IndexOf(tmp);
                    strPageurl = item.Substring(0, index).Replace("\"","");
                    break;
                }
            }
        }

        string GetBaiduNextPageurl()
        {
            string[] sArray = strPageurl.Split(new string[] { "&amp;pn=", "&amp;oq=" }, StringSplitOptions.RemoveEmptyEntries);

            string tmp = "";
            int index = 0;
            foreach (string item in sArray)
            {
                if(index == 0)
                {
                    tmp += "https://www.baidu.com";
                    tmp += item;
                    tmp += "&amp;pn=";

                }
                else if (index == 1)
                {
                    if(nPageIndex==0)
                    {
                        tmp += item;
                    }
                    else
                    {
                        int nPage = int.Parse(item);
                        string page = (nPage + 10).ToString();
                        tmp += page;
                    }
                    nPageIndex++;

                }
                else if(index == 2)
                {
                    tmp += "&amp;oq=";
                    tmp += item;

                }
                index++;
            }

            //
            if (tmp.Trim()=="")
            {
                EndTask(false);
            }

            return tmp.Replace("amp;","");
        }

        private void webBrowser_DocumentCompleted_SearchPage(object sender, CefSharp.FrameLoadEndEventArgs e)
        {
 
        }

        private void WebForm_bak_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            StopTaskEvent(this, new AppEventArgs(){});
            return;
        }
    }
}
