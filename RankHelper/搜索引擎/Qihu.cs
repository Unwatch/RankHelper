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
    public class Qihu : WebBase
    {
        public Qihu(WebForm webForm, string strPageurl, string strhtml):base(webForm, strPageurl, strhtml)
        {
            nPos_x = 30;
            nPos_y = 80;
            webForm.webBrowser_new.Navigate("www.so.com");
        }

        public override void webBrowser_DocumentCompleted_Search(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (e.Url.ToString().Contains("so"))
            {
                webForm.currentTask.webState = EWebbrowserState.none;
                webForm.textBox_url.Text = e.Url.ToString();
                HtmlElement ele_search;
                if (webForm.currentTask.webBrowser == eWebBrowser.IE || webForm.currentTask.webBrowser == eWebBrowser.Chrome || webForm.currentTask.webBrowser == eWebBrowser.Qihu || webForm.currentTask.webBrowser == eWebBrowser.Sogou || webForm.currentTask.webBrowser == eWebBrowser.QQ || webForm.currentTask.webBrowser == eWebBrowser.maxthon || webForm.currentTask.webBrowser == eWebBrowser.theworld)
                {
                    ele_search = webForm.webBrowser_new.Document.GetElementById("input");
                }
                else//移动端
                {
                    ele_search = webForm.webBrowser_new.Document.GetElementById("input");
                }

                if (ele_search == null)
                {
                    webForm.currentTask.webState = EWebbrowserState.Search;
                    webForm.webBrowser_new.Refresh();
                    return;
                }

                Point point_search = GetPoint(ele_search);
                webForm.webBrowser_new.Document.Window.ScrollTo(0, point_search.Y);
                int top = webForm.webBrowser_new.Document.GetElementsByTagName("HTML")[0].ScrollTop;//滚动条垂直位置
                KeyUtils.SetCursorPos(point_search.X + nPos_x, point_search.Y + nPos_y - top);

                KeyUtils.MouseLBUTTON();
                this.taskIntervalTimer.Start();
                KeyUtils.Copy(webForm.currentTask.strKeyword);

                Sleep(3000);
                HtmlElement ele_submit;

                if (webForm.currentTask.webBrowser == eWebBrowser.IE || webForm.currentTask.webBrowser == eWebBrowser.Chrome || webForm.currentTask.webBrowser == eWebBrowser.Qihu || webForm.currentTask.webBrowser == eWebBrowser.Sogou || webForm.currentTask.webBrowser == eWebBrowser.QQ || webForm.currentTask.webBrowser == eWebBrowser.maxthon || webForm.currentTask.webBrowser == eWebBrowser.theworld)
                {
                    ele_submit = webForm.webBrowser_new.Document.GetElementById("search-button");
                }
                else
                {
                    ele_submit = webForm.webBrowser_new.Document.GetElementById("search-button");
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

                KeyUtils.MouseLBUTTON();
                this.taskIntervalTimer.Start();

                if (webForm.currentTask == null)
                {
                    return;
                }
                webForm.currentTask.webState = EWebbrowserState.Search;
                webForm.ShowTask(new AppEventArgs() { message_string = string.Format("开始搜索关键词,任务{0}", webForm.currentTask.nID) });
            }
        }

        public override void webBrowser_DocumentCompleted_SearchSite(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            //if (e.Url.ToString().Equals("http://www.so.com/") || e.Url.ToString().Equals("https://www.so.com/"))
            if (!e.Url.ToString().Contains("www.so.com/s?ie=utf-8&fr=none&src=360sou_newhome&q=")&&
                !e.Url.ToString().Contains("www.so.com/s?q="))
            {
                return;
            }

            nItem = 0;

            if (e.Url.ToString().Contains("so"))
            {
                webForm.currentTask.webState = EWebbrowserState.none;
                webForm.textBox_url.Text = e.Url.ToString();
                webForm.ShowTask(new AppEventArgs() { message_string = string.Format("开始查找网站,当前页码{0},任务{1}", nPageIndex, webForm.currentTask.nID) });
                Sleep(3000);

                //List<tagSearchResult> listSearch = new List<tagSearchResult>();
                //搜索项目
                //HtmlElement ele_search;
                HtmlElementCollection eleCol = webForm.webBrowser_new.Document.GetElementsByTagName("li");
                if (eleCol != null)
                {
                    for (int i = 0; i < eleCol.Count; i++)//1~10项
                    {
                        if (eleCol[i].GetAttribute("className") ==null)
                        {
                            continue;
                        }

                        if (eleCol[i].GetAttribute("className") == "res-list")
                        {
                            nItem++;
                            if (!webForm.currentTask.strTitle.Trim().Equals("") && !webForm.currentTask.strPageUrl.Trim().Equals(""))
                            {
                                if (eleCol[i].InnerText.Contains(webForm.currentTask.strTitle) || eleCol[i].InnerText.Contains(webForm.currentTask.strPageUrl))
                                {
                                    webForm.ShowTask(new AppEventArgs() { message_string = string.Format("查找到符合的网站,当前页码{0},任务{1}", nPageIndex, webForm.currentTask.nID) });
                                    webForm.currentTask.webState = EWebbrowserState.AccessSite;
                                    webForm.webBrowser_new.Document.Window.ScrollTo(0, 10000);
                                    Sleep(3000);
                                    Point point_ele = GetPoint(eleCol[i]);
                                    int nPos_y = 60;

                                    webForm.webBrowser_new.Document.Window.ScrollTo(0, point_ele.Y - nPos_y);
                                    int top2 = webForm.webBrowser_new.Document.GetElementsByTagName("HTML")[0].ScrollTop;//滚动条垂直位置
                                    //KeyUtils.SetCursorPos(point_ele.X + nPos_x - 900, point_ele.Y + nPos_y - top2);
                                    KeyUtils.SetCursorPos(point_ele.X + nPos_x, point_ele.Y + nPos_y - top2);
                                    Sleep(3000);
                                    webForm.currentTask.webState = EWebbrowserState.AccessSite;
                                    KeyUtils.MouseLBUTTON();
                                    this.taskIntervalTimer.Start();

                                    return;
                                }
                            }
                            else if (!webForm.currentTask.strTitle.Trim().Equals(""))
                            {
                                if (eleCol[i].InnerText.Contains(webForm.currentTask.strTitle))
                                {
                                    webForm.ShowTask(new AppEventArgs() { message_string = string.Format("查找到符合的网站,当前页码{0},任务{1}", nPageIndex, webForm.currentTask.nID) });
                                    webForm.currentTask.webState = EWebbrowserState.AccessSite;
                                    webForm.webBrowser_new.Document.Window.ScrollTo(0, 10000);
                                    Sleep(3000);
                                    Point point_ele = GetPoint(eleCol[i]);
                                    int nPos_y = 60;

                                    webForm.webBrowser_new.Document.Window.ScrollTo(0, point_ele.Y - nPos_y);
                                    int top2 = webForm.webBrowser_new.Document.GetElementsByTagName("HTML")[0].ScrollTop;//滚动条垂直位置
                                    //KeyUtils.SetCursorPos(point_ele.X + nPos_x - 900, point_ele.Y + nPos_y - top2);
                                    KeyUtils.SetCursorPos(point_ele.X + nPos_x, point_ele.Y + nPos_y - top2);
                                    Sleep(3000);
                                    webForm.currentTask.webState = EWebbrowserState.AccessSite;
                                    KeyUtils.MouseLBUTTON();
                                    this.taskIntervalTimer.Start();

                                    return;
                                }
                            }
                            else if (!webForm.currentTask.strPageUrl.Trim().Equals(""))
                            {
                                webForm.ShowTask(new AppEventArgs() { message_string = string.Format("查找到符合的网站,当前页码{0},任务{1}", nPageIndex, webForm.currentTask.nID) });
                                webForm.currentTask.webState = EWebbrowserState.AccessSite;
                                webForm.webBrowser_new.Document.Window.ScrollTo(0, 10000);
                                Sleep(3000);
                                Point point_ele = GetPoint(eleCol[i]);
                                int nPos_y = 60;

                                webForm.webBrowser_new.Document.Window.ScrollTo(0, point_ele.Y - nPos_y);
                                int top2 = webForm.webBrowser_new.Document.GetElementsByTagName("HTML")[0].ScrollTop;//滚动条垂直位置
                                                                                                                     //KeyUtils.SetCursorPos(point_ele.X + nPos_x - 900, point_ele.Y + nPos_y - top2);
                                KeyUtils.SetCursorPos(point_ele.X + nPos_x, point_ele.Y + nPos_y - top2);
                                Sleep(3000);
                                webForm.currentTask.webState = EWebbrowserState.AccessSite;
                                KeyUtils.MouseLBUTTON();
                                this.taskIntervalTimer.Start();

                                return;
                            }
                        }
                    }

                    //当前搜索页未找到
                    //if (nItem == -1)
                    {
                        webForm.ShowTask(new AppEventArgs() { message_string = string.Format("没有找到符合的网站,开始查找下一页，当前页码{0},任务{1}", nPageIndex, webForm.currentTask.nID) });
                        GetNextPageurl();
                    }
                }
            }
        }

        public override void webBrowser_DocumentCompleted_AccessSite(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (e.Url.ToString().Equals("http://www.so.com/") || e.Url.ToString().Equals("https://www.so.com/"))
            {
                return;
            }
            //if (e.Url.ToString().Contains("so"))
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
                    }
                    break;
                case ePageAccessType.Rand:
                    {
                        EndTask(true);
                    }
                    break;
                case ePageAccessType.Appoint:
                    {
                        EndTask(true);
                    }
                    break;
                default:
                    break;
            }

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
                if (element.InnerText == nPageIndex.ToString())
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

            webForm.ShowTask(new AppEventArgs() { message_string = string.Format("已经是最后一页，结束任务，当前页码{0},任务{1}", nPageIndex, webForm.currentTask.nID) });
            Sleep(5000);
            EndTask(false);
        }

    }
}
