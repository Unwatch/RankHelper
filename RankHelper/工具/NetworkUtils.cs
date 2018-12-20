using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using DotRas;
using System.Collections.ObjectModel;
using System.Net;

namespace RankHelper
{
    class NetworkUtils
    {
        /// <summary>
        /// 获取本地IP地址信息
        /// </summary>
        //public static string GetIpAddress()
        //{
        //    string hostName = Dns.GetHostName();   //获取本机名
        //    IPHostEntry localhost = Dns.GetHostByName(hostName);    //方法已过期，可以获取IPv4的地址
        //                                                            //IPHostEntry localhost = Dns.GetHostEntry(hostName);   //获取IPv6地址
        //    IPAddress localaddr = localhost.AddressList[0];

        //    return localaddr.ToString();
        //}
        public static string GetIpAddress()
        {
            foreach (RasConnection connection in RasConnection.GetActiveConnections())
            {
                RasIPInfo ipAddresses = (RasIPInfo)connection.GetProjectionInfo(RasProjectionType.IP);
                if (ipAddresses != null)
                {
                    return ipAddresses.IPAddress.ToString();
                }
            }

            return "";
        }
    }
}
