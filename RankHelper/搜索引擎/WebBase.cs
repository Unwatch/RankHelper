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
    public class WebBase
    {
        public WebForm webForm;
        public string strhtml;
        public string strPageurl;

        public int nPos_x;
        public int nPos_y;
        public int nPageIndex = -1;//第几页
        public int nItem = -1;//第几项

        public WebBase(WebForm webForm, string strPageurl, string strhtml)
        {
            this.webForm = webForm;
            this.strPageurl = strPageurl;
            this.strhtml = strhtml;
            nPageIndex = 1;

        }

        public virtual void StartSearch(object sender, EventArgs e)
        {

        }

        public virtual void webBrowser_DocumentCompleted_Search(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        public virtual void webBrowser_DocumentCompleted_SearchSite(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        public virtual void webBrowser_DocumentCompleted_AccessSite(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        public void EndTask(bool bSuccess)
        {
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
    }
}
