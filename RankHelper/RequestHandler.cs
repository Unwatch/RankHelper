using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CefSharp;
using System.IO;

namespace RankHelper
{
    public class RequestHandler : CefSharp.Handler.DefaultRequestHandler //CefSharp.Example.Handlers
    {
        public string _directory = "DownloadFile/";

        private Dictionary<UInt64, MemoryStreamResponseFilter> responseDictionary = new Dictionary<UInt64, MemoryStreamResponseFilter>();


        public IRequestHandler _requestHeandler;

        public RequestHandler(IRequestHandler rh) : base()
        {
            _requestHeandler = rh;
        }

        public override CefReturnValue OnBeforeResourceLoad(IWebBrowser browserControl, IBrowser browser, IFrame frame, IRequest request, IRequestCallback callback)
        {
            if (request.ResourceType == ResourceType.MainFrame)
            {
                var headers = request.Headers;
                headers["User-Agent"] = Appinfo.strUserAgent;
                request.Headers = headers;
            }

            return CefReturnValue.Continue;
        }
    }
}
