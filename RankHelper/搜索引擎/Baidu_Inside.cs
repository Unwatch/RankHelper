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
    public class Baidu_Inside : WebBase
    {
        public Baidu_Inside(WebForm webForm, string strSiteUrl, string strhtml) : base(webForm, strSiteUrl, strhtml)
        {
            nPos_x = 30;
            nPos_y = 70;
            webForm.webBrowser_new.Navigate("www.baidu.com");
        }

        public override void webBrowser_DocumentCompleted_Search(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (e.Url.ToString().Contains("baidu"))
            {
                webForm.currentTask.webState = EWebbrowserState.none;
                webForm.textBox_url.Text = e.Url.ToString();
                HtmlElement ele_search;
                if (webForm.currentTask.webBrowser == eWebBrowser.IE || webForm.currentTask.webBrowser == eWebBrowser.Chrome || webForm.currentTask.webBrowser == eWebBrowser.Qihu || webForm.currentTask.webBrowser == eWebBrowser.Sogou || webForm.currentTask.webBrowser == eWebBrowser.QQ || webForm.currentTask.webBrowser == eWebBrowser.maxthon || webForm.currentTask.webBrowser == eWebBrowser.theworld)
                {
                    //webForm.webBrowser_new.Document.GetElementById("kw").Focus();
                    //webForm.webBrowser_new.Document.GetElementById("kw").InnerText = webForm.currentTask.strKeyword;
                    ele_search = webForm.webBrowser_new.Document.GetElementById("kw");
                }
                else//移动端
                {
                    ele_search = webForm.webBrowser_new.Document.GetElementById("kw");
                }

                if (ele_search == null)
                {
                    webForm.currentTask.webState = EWebbrowserState.Search;
                    webForm.webBrowser_new.Refresh();
                    return;
                }

                Point point_search = GetPoint(ele_search);
                //webForm.webBrowser_new.Document.Window.ScrollTo(0, point_search.Y);
                int top = webForm.webBrowser_new.Document.GetElementsByTagName("HTML")[0].ScrollTop;//滚动条垂直位置
                KeyUtils.SetCursorPos(point_search.X + nPos_x, point_search.Y + nPos_y - top);
                //KeyUtils.SetCursorPos(point_search.X, point_search.Y);

                //ele_search.Focus();
                KeyUtils.MouseLBUTTON();
                this.taskIntervalTimer.Start();
                KeyUtils.Copy(webForm.currentTask.strKeyword);

                //if (webForm.currentTask.webBrowser == eWebBrowser.IE || webForm.currentTask.webBrowser == eWebBrowser.Chrome || webForm.currentTask.webBrowser == eWebBrowser.Qihu || webForm.currentTask.webBrowser == eWebBrowser.Sogou || webForm.currentTask.webBrowser == eWebBrowser.QQ || webForm.currentTask.webBrowser == eWebBrowser.maxthon || webForm.currentTask.webBrowser == eWebBrowser.theworld)
                //{
                //    ele_search.InnerText = webForm.currentTask.strKeyword;
                //}
                //else
                //{
                //    ele_search.InnerText = webForm.currentTask.strKeyword;
                //}

                Sleep(3000);
                HtmlElement ele_submit;

                if (webForm.currentTask.webBrowser == eWebBrowser.IE || webForm.currentTask.webBrowser == eWebBrowser.Chrome || webForm.currentTask.webBrowser == eWebBrowser.Qihu || webForm.currentTask.webBrowser == eWebBrowser.Sogou || webForm.currentTask.webBrowser == eWebBrowser.QQ || webForm.currentTask.webBrowser == eWebBrowser.maxthon || webForm.currentTask.webBrowser == eWebBrowser.theworld)
                {
                    ele_submit = webForm.webBrowser_new.Document.GetElementById("su");
                }
                else
                {
                    ele_submit = webForm.webBrowser_new.Document.GetElementById("su");
                }

                if (ele_submit == null)
                {
                    webForm.currentTask.webState = EWebbrowserState.Search;
                    webForm.webBrowser_new.Refresh();
                    return;
                }

                Point point_submit = GetPoint(ele_submit);
                webForm.webBrowser_new.Document.Window.ScrollTo(0, point_submit.Y);
                int top2 = webForm.webBrowser_new.Document.GetElementsByTagName("HTML")[0].ScrollTop;//滚动条垂直位置
                KeyUtils.SetCursorPos(point_submit.X + nPos_x, point_submit.Y + nPos_y - top2);
                //KeyUtils.SetCursorPos(point_submit.X, point_submit.Y);

                KeyUtils.MouseLBUTTON();
                this.taskIntervalTimer.Start();

                if (webForm.currentTask == null)
                {
                    return;
                }
                webForm.currentTask.webState = EWebbrowserState.Search;
                webForm.ShowTask(new AppEventArgs() { message_string = string.Format("开始搜索关键词,任务{0}", webForm.currentTask.nID) });
                //e.Browser.Reload();
            }
        }

        public override void webBrowser_DocumentCompleted_SearchSite(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (!e.Url.ToString().Contains("www.baidu.com/s?"))
            {
                return;
            }

            nItem = -1;

            HtmlElementCollection divCol = webForm.webBrowser_new.Document.GetElementsByTagName("div");
            for (int i = 0; i < divCol.Count; i++)
            {
                if (divCol[i].GetAttribute("class") != null)
                {
                    if (divCol[i].GetAttribute("class").Equals("search_tool"))
                    {
                        divCol[i].InvokeMember("click");
                        return;
                    }
                }
            }

            return;

            HtmlElementCollection eleInputCol = webForm.webBrowser_new.Document.GetElementsByTagName("input");
            for (int i = 0; i < eleInputCol.Count; i++)
            {
                if (eleInputCol[i].GetAttribute("name") == null)
                {
                    EndTask(true);
                    return;
                }

                if (eleInputCol[i].GetAttribute("name") == "si")
                {
                    eleInputCol[i].InnerText = webForm.currentTask.strOnSiteUrl;
                    Sleep(3000);

                    HtmlElementCollection eleCol = eleInputCol[i].Parent.Children;
                    for (int j = 0; j < eleCol.Count; j++)
                    {
                        if (eleCol[j].TagName =="a")
                        {
                            eleCol[j].InvokeMember("click");
                            this.taskIntervalTimer.Start();
                            return;
                        }
                    }
                }
            }

            if (e.Url.ToString().Contains("baidu"))
            {
                webForm.currentTask.webState = EWebbrowserState.none;
                webForm.textBox_url.Text = e.Url.ToString();
                webForm.ShowTask(new AppEventArgs() { message_string = string.Format("开始查找网站,当前页码{0},任务{1}", nPageIndex, webForm.currentTask.nID) });
                Sleep(3000);

                //List<tagSearchResult> listSearch = new List<tagSearchResult>();
                //搜索项目
                for (int i = 1; i <= 10; i++)//1~10项
                {
                    HtmlElement ele_search;
                    ele_search = webForm.webBrowser_new.Document.GetElementById(i.ToString());
                    if (ele_search == null)
                    {
                        break;
                    }
                    if (!webForm.currentTask.strTitle.Trim().Equals("") && !webForm.currentTask.strSiteUrl.Trim().Equals(""))
                    {
                        if (ele_search.InnerText.Contains(webForm.currentTask.strTitle) || ele_search.InnerText.Contains(webForm.currentTask.strSiteUrl))
                        {
                            HtmlElementCollection eleCol = ele_search.GetElementsByTagName("a");
                            foreach (HtmlElement element in eleCol)
                            {
                                nItem = i;
                                webForm.ShowTask(new AppEventArgs() { message_string = string.Format("查找到符合的网站,当前页码{0},任务{1}", nPageIndex, webForm.currentTask.nID) });
                                webForm.currentTask.webState = EWebbrowserState.AccessSite;
                                //element.InvokeMember("click");
                                Sleep(3000);
                                Point point_ele = GetPoint(element);
                                int nPos_y = 70;

                                webForm.webBrowser_new.Document.Window.ScrollTo(0, point_ele.Y - nPos_y);
                                int top2 = webForm.webBrowser_new.Document.GetElementsByTagName("HTML")[0].ScrollTop;//滚动条垂直位置
                                Sleep(3000);
                                KeyUtils.SetCursorPos(point_ele.X + nPos_x, point_ele.Y + nPos_y - top2);
                                Sleep(3000);
                                webForm.currentTask.webState = EWebbrowserState.AccessSite;
                                KeyUtils.MouseLBUTTON();
                                this.taskIntervalTimer.Start();

                                break;
                            }
                        }
                    }
                    else if (!webForm.currentTask.strTitle.Trim().Equals(""))
                    {
                        if (ele_search.InnerText.Contains(webForm.currentTask.strTitle))
                        {
                            HtmlElementCollection eleCol = ele_search.GetElementsByTagName("a");
                            foreach (HtmlElement element in eleCol)
                            {
                                nItem = i;
                                webForm.ShowTask(new AppEventArgs() { message_string = string.Format("查找到符合的网站,当前页码{0},任务{1}", nPageIndex, webForm.currentTask.nID) });
                                webForm.currentTask.webState = EWebbrowserState.AccessSite;
                                //element.InvokeMember("click");
                                Sleep(3000);
                                Point point_ele = GetPoint(element);
                                int nPos_y = 70;

                                webForm.webBrowser_new.Document.Window.ScrollTo(0, point_ele.Y - nPos_y);
                                int top2 = webForm.webBrowser_new.Document.GetElementsByTagName("HTML")[0].ScrollTop;//滚动条垂直位置
                                Sleep(3000);
                                KeyUtils.SetCursorPos(point_ele.X + nPos_x, point_ele.Y + nPos_y - top2);
                                Sleep(3000);
                                webForm.currentTask.webState = EWebbrowserState.AccessSite;
                                KeyUtils.MouseLBUTTON();
                                this.taskIntervalTimer.Start();

                                break;
                            }
                        }
                    }
                    else if (!webForm.currentTask.strSiteUrl.Trim().Equals(""))
                    {
                        HtmlElementCollection eleCol = ele_search.GetElementsByTagName("a");
                        foreach (HtmlElement element in eleCol)
                        {
                            nItem = i;
                            webForm.ShowTask(new AppEventArgs() { message_string = string.Format("查找到符合的网站,当前页码{0},任务{1}", nPageIndex, webForm.currentTask.nID) });
                            webForm.currentTask.webState = EWebbrowserState.AccessSite;
                            //element.InvokeMember("click");
                            Sleep(3000);
                            Point point_ele = GetPoint(element);
                            int nPos_y = 70;

                            webForm.webBrowser_new.Document.Window.ScrollTo(0, point_ele.Y - nPos_y);
                            int top2 = webForm.webBrowser_new.Document.GetElementsByTagName("HTML")[0].ScrollTop;//滚动条垂直位置
                            Sleep(3000);
                            KeyUtils.SetCursorPos(point_ele.X + nPos_x, point_ele.Y + nPos_y - top2);
                            Sleep(3000);
                            webForm.currentTask.webState = EWebbrowserState.AccessSite;
                            KeyUtils.MouseLBUTTON();
                            this.taskIntervalTimer.Start();

                            break;
                        }
                    }
                }

                //当前搜索页未找到
                if (nItem == -1)
                {
                    webForm.ShowTask(new AppEventArgs() { message_string = string.Format("没有找到符合的网站,开始查找下一页，当前页码{0},任务{1}", nPageIndex, webForm.currentTask.nID) });
                    GetNextPageurl();
                }

                //var task_item = e.Frame.EvaluateScriptAsync(js);
                ////task_item.Id = i;
                //task_item.ContinueWith(t =>
                //{
                //    if (!t.IsFaulted)
                //    {
                //        //诺亚娱乐 - 诺亚娱乐平台官方唯一网站
                //        //百度网址安全中心提醒您：该页面可能存在违法信息！
                //        //诺亚娱乐官方权威指定网站是国内知名有效合法代理登录站点,诺亚娱乐平台是彩界唯一一家拥有合法的娱乐平台注册预测走势图,行业首家拥有上下级聊天工具支持网页手机玩法。
                //        //https://www.nuoya115.com/  - 百度快照
                //        string log = string.Format("方法webBrowser_DocumentCompleted_SearchSite: 查找第{0}页第{0}项", nPageIndex, i);
                //        ShowTaskEvent(this, new AppEventArgs() { message_string = log });
                //        var response = t.Result;
                //        var EvaluateJavaScriptResult = response.Success ? (response.Result ?? "null") : response.Message;

                //        tagSearchResult searchResult = new tagSearchResult();
                //        searchResult.nItem = i;
                //        searchResult.bFinish = true;
                //        searchResult.bMatch = (response.Result==null) ? false:true;
                //        listSearch.Add(searchResult);

                //        bool bAllPageFinish = true;
                //        if (listSearch.Count==10)
                //        {
                //            foreach (var item in listSearch)
                //            {
                //                if (item.bFinish == false)
                //                {
                //                    bAllPageFinish = false;
                //                }
                //            }
                //        }


                //        if (bAllPageFinish)
                //        {
                //            foreach (var item in listSearch)
                //            {
                //                if (item.bMatch == false)
                //                {
                //                    nItem = item.nItem;
                //                    webForm.currentTask.webState = EWebbrowserState.SearchSite_Match;
                //                    e.Browser.Reload();
                //                    break;
                //                }
                //            }
                //        }
                //    }
                //});

                //return;
                //var task = e.Frame.EvaluateScriptAsync("document.getElementById('page').innerHTML");//异步
                //task.ContinueWith(t =>
                //{
                //    if (!t.IsFaulted)
                //    {
                //        var response = t.Result;
                //        var EvaluateJavaScriptResult = response.Success ? (response.Result ?? "null") : response.Message;
                //        GetBaiduPageurl((string)EvaluateJavaScriptResult);

                //        webForm.currentTask.webState = EWebbrowserState.SearchSite;
                //        webBrowser.Load(GetBaiduNextPageurl());
                //    }
                //});
            }
        }

        public override void webBrowser_DocumentCompleted_AccessSite(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (e.Url.ToString().Equals("http://www.baidu.com/") || e.Url.ToString().Equals("https://www.baidu.com/"))
            {
                return;
            }
            //if (e.Url.ToString().Contains("baidu"))
            //{
            //    return;
            //}
            webForm.textBox_url.Text = e.Url.ToString();
            webForm.currentTask.webState = EWebbrowserState.none;
            webForm.textBox_url.Text = e.Url.ToString();
            webForm.ShowTask(new AppEventArgs() { message_string = string.Format("进入网站,当前页码{0},任务{1}", 1, webForm.currentTask.nID) });

            Sleep(5000);
            switch (webForm.currentTask.pageAccessType)
            {
                case ePageAccessType.None:
                    {
                        EndTask(true);
                        return;
                    }
                    break;
                case ePageAccessType.Rand:
                    {
                        HtmlElementCollection aCol = webForm.webBrowser_new.Document.GetElementsByTagName("a");
                        if (aCol == null || aCol.Count == 0)
                        {
                            EndTask(true);
                            return;
                        }
                        Random ra = new Random(unchecked((int)DateTime.Now.Ticks));//保证产生的数字的随机性 
                        int index = ra.Next() % aCol.Count;
                        index = (index >= aCol.Count) ? aCol.Count - 1 : index;

                        aCol[index].InvokeMember("click");
                        webForm.ShowTask(new AppEventArgs() { message_string = string.Format("访问内页{0}", aCol[index].GetAttribute("href")) });
                        webForm.currentTask.webState = EWebbrowserState.AccessPage;
                        this.taskIntervalTimer.Start();
                    }
                    break;
                case ePageAccessType.Appoint:
                    {
                        HtmlElementCollection aCol = webForm.webBrowser_new.Document.GetElementsByTagName("a");
                        if (aCol == null || aCol.Count == 0)
                        {
                            EndTask(true);
                            return;
                        }
                        Random ra = new Random(unchecked((int)DateTime.Now.Ticks));//保证产生的数字的随机性 
                        int index = ra.Next() % aCol.Count;
                        index = (index >= aCol.Count) ? aCol.Count - 1 : index;

                        for (int i = 0; i < aCol.Count; i++)
                        {
                            if (aCol[i].GetAttribute("href") == null)
                            {
                                EndTask(true);
                                continue;
                            }
                            else if (aCol[i].GetAttribute("href") == webForm.currentTask.strPageUrl)
                            {
                                aCol[i].InvokeMember("click");
                                break;
                            }
                        }

                        webForm.ShowTask(new AppEventArgs() { message_string = string.Format("访问内页{0}", webForm.currentTask.strPageUrl) });
                        webForm.currentTask.webState = EWebbrowserState.AccessPage;
                        this.taskIntervalTimer.Start();
                    }
                    break;
                default:
                    break;
            }

        }

        public override void webBrowser_DocumentCompleted_AccessPage(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            webForm.textBox_url.Text = e.Url.ToString();
            webForm.currentTask.webState = EWebbrowserState.none;
            webForm.textBox_url.Text = e.Url.ToString();
            webForm.ShowTask(new AppEventArgs() { message_string = string.Format("访问内页{0}", webForm.currentTask.strPageUrl) });

            Sleep(5000);
            EndTask(true);
        }

        public override void GetNextPageurl()
        {
            HtmlElement ele_page;
            ele_page = webForm.webBrowser_new.Document.GetElementById("page");
            if (ele_page == null)
            {
                webForm.ShowTask(new AppEventArgs() { message_string = string.Format("只有一页，结束任务，当前页码{0},任务{1}", nPageIndex, webForm.currentTask.nID) });
                EndTask(false);
            }
            nPageIndex += 1;
            HtmlElementCollection eleCol = ele_page.GetElementsByTagName("a");
            foreach (HtmlElement element in eleCol)
            {
                HtmlElementCollection pageCol = element.Children;

                if (pageCol != null)
                {
                    for (int i = 0; i < pageCol.Count; i++)
                    {
                        if (pageCol[i].GetAttribute("className") == null)
                        {
                            continue;
                        }

                        if (pageCol[i].GetAttribute("className") == "pc")
                        {
                            if (pageCol[i].InnerText == nPageIndex.ToString())
                            {
                                webForm.currentTask.webState = EWebbrowserState.SearchSite;
                                Point point_ele = GetPoint(element);
                                webForm.webBrowser_new.Document.Window.ScrollTo(0, point_ele.Y);
                                int top = webForm.webBrowser_new.Document.GetElementsByTagName("HTML")[0].ScrollTop;//滚动条垂直位置
                                KeyUtils.SetCursorPos(point_ele.X + nPos_x, point_ele.Y + nPos_y - top);
                                Sleep(3000);
                                KeyUtils.MouseLBUTTON();
                                this.taskIntervalTimer.Start();

                                return;
                            }
                        }
                    }
                }
            }

            webForm.ShowTask(new AppEventArgs() { message_string = string.Format("已经是最后一页，结束任务，当前页码{0},任务{1}", nPageIndex, webForm.currentTask.nID) });
            Sleep(5000);
            EndTask(false);
        }

    }
}
