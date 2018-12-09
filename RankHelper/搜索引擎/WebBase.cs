using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mshtml;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Drawing;
using System.Timers;

namespace RankHelper
{
    public class WebBase
    {
        public WebForm webForm;
        public string strhtml;
        public string strPageurl;

        public int nPos_x;
        public int nPos_y;
        public int nPageIndex = -1;//第几页
        public int nItem = -1;//第几项

        //每次访问网页超时定时器
        public System.Timers.Timer taskIntervalTimer;

        public WebBase(WebForm webForm, string strPageurl, string strhtml)
        {
            this.webForm = webForm;
            this.strPageurl = strPageurl;
            this.strhtml = strhtml;
            nPageIndex = 1;

            this.taskIntervalTimer = new System.Timers.Timer();
            this.taskIntervalTimer.Elapsed += new System.Timers.ElapsedEventHandler(taskIntervalTimer_Elapsed);
            this.taskIntervalTimer.AutoReset = true;
            this.taskIntervalTimer.Interval = 1000 * 60 * 2;
            this.taskIntervalTimer.Enabled = false;

        }

        public virtual void StartSearch(object sender, EventArgs e)
        {

        }

        public virtual void webBrowser_DocumentCompleted_Search(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            this.taskIntervalTimer.Stop();
        }

        public virtual void webBrowser_DocumentCompleted_SearchSite(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            this.taskIntervalTimer.Stop();
        }

        public virtual void webBrowser_DocumentCompleted_AccessSite(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            this.taskIntervalTimer.Stop();
        }

        public void EndTask(bool bSuccess)
        {
            this.taskIntervalTimer.Stop();
            webForm.ShowTask(new AppEventArgs() { message_string = string.Format("任务超时，重置") });
            webForm.webBrowser_new.Stop();
            webForm.EndTask(new AppEventArgs() { message_bool = bSuccess, message_task = webForm.currentTask });
        }

        public virtual void GetNextPageurl()
        {

        }

        /// <summary>
        /// 等待一段时间
        /// </summary>
        public void Sleep(int milliseconds)
        {
            var start = DateTime.Now;
            var stop = start.AddMilliseconds(milliseconds);
            while (DateTime.Now < stop)
            {
                Application.DoEvents();
            }
        }

        public Point GetPoint(HtmlElement el)
        {
            Point pos = new Point(el.OffsetRectangle.Left, el.OffsetRectangle.Top);
            //循环获取父级的坐标
            HtmlElement tempEl = el.OffsetParent;
            while (tempEl != null)
            {
                pos.X += tempEl.OffsetRectangle.Left;
                pos.Y += tempEl.OffsetRectangle.Top;
                tempEl = tempEl.OffsetParent;
            }
            return pos;
        }

        private void taskIntervalTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            EndTask(true);
        }
    }
}
