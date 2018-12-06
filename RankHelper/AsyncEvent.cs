using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Remoting.Messaging;

namespace AsyncCall
{
    public class AsyncEvent
    {
        /// <summary>  
        /// 声明委托  
        /// </summary>  
        /// <typeparam name="T"></typeparam>  
        /// <returns></returns>  
        public delegate T MyAsyncDelegate<T>();

        public delegate T MyAsyncDelegate2<T>(int a, int b);

        /// <summary>  
        /// 回调函数得到异步线程的返回结果  
        /// </summary>  
        /// <param name="iasync"></param>  
        public static T CallBack<T>(IAsyncResult iasync)
        {
            AsyncResult async = (AsyncResult)iasync;
            MyAsyncDelegate<T> del = (MyAsyncDelegate<T>)async.AsyncDelegate;
            return (T)del.EndInvoke(iasync);
        }
    }
}