using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.ComponentModel;
using SQLite;
using SQLitePCL;

namespace RankHelper
{
    public enum eEngines
    {
        [Description("百度")]
        Baidu,
        [Description("360")]
        Qihu,
        [Description("搜狗")]
        Sogou,
        [Description("神马搜索")]
        Sm
    }

    public enum eSearchType
    {
        [Description("站内")]
        OnSite,
        [Description("站外")]
        OffSite
    };

    public enum eWebBrowser
    {
        [Description("IE")]
        IE,
        [Description("IE(移动版)")]
        IE_mobile,
        [Description("Chrome")]
        Chrome,
        [Description("Chrome(移动版)")]
        Chrome_mobile,
        [Description("360")]
        Qihu,
        [Description("360(移动版)")]
        Qihu_mobile,
        [Description("搜狗")]
        Sogou,
        [Description("搜狗(移动版)")]
        Sogou_mobile,
        [Description("QQ")]
        QQ,
        [Description("QQ(移动版)")]
        QQ_mobile,
        [Description("遨游")]
        maxthon,
        [Description("遨游(移动版)")]
        maxthon_mobile,
        [Description("世界之窗")]
        theworld,
        [Description("世界之窗(移动版)")]
        theworld_mobile,
        [Description("随机")]
        Rand
    }

    public enum ePageAccessType
    {
        [Description("不访问")]
        None,
        [Description("随机访问")]
        Rand,
        [Description("指定访问")]
        Appoint
    };

    public enum eTempleTime
    {
        [Description("智能分配")]
        Rand,
        [Description("模板0")]
        Temple0,
        [Description("模板1")]
        Temple1,
        [Description("模板2")]
        Temple2
    };

    public class tagTempleTime
    {
        [PrimaryKey]
        public int nID { get; set; }
        public bool bCheck00 { get; set; }
        public int nCount00 { get; set; }
        public int nCount00_Excute { get; set; }
        public bool bCheck01 { get; set; }
        public int nCount01 { get; set; }
        public int nCount01_Excute { get; set; }
        public bool bCheck02 { get; set; }
        public int nCount02 { get; set; }
        public int nCount02_Excute { get; set; }
        public bool bCheck03 { get; set; }
        public int nCount03 { get; set; }
        public int nCount03_Excute { get; set; }
        public bool bCheck04 { get; set; }
        public int nCount04 { get; set; }
        public int nCount04_Excute { get; set; }
        public bool bCheck05 { get; set; }
        public int nCount05 { get; set; }
        public int nCount05_Excute { get; set; }
        public bool bCheck06 { get; set; }
        public int nCount06 { get; set; }
        public int nCount06_Excute { get; set; }
        public bool bCheck07 { get; set; }
        public int nCount07 { get; set; }
        public int nCount07_Excute { get; set; }
        public bool bCheck08 { get; set; }
        public int nCount08 { get; set; }
        public int nCount08_Excute { get; set; }
        public bool bCheck09 { get; set; }
        public int nCount09 { get; set; }
        public int nCount09_Excute { get; set; }
        public bool bCheck10 { get; set; }
        public int nCount10 { get; set; }
        public int nCount10_Excute { get; set; }
        public bool bCheck11 { get; set; }
        public int nCount11 { get; set; }
        public int nCount11_Excute { get; set; }
        public bool bCheck12 { get; set; }
        public int nCount12 { get; set; }
        public int nCount12_Excute { get; set; }
        public bool bCheck13 { get; set; }
        public int nCount13 { get; set; }
        public int nCount13_Excute { get; set; }
        public bool bCheck14 { get; set; }
        public int nCount14 { get; set; }
        public int nCount14_Excute { get; set; }
        public bool bCheck15 { get; set; }
        public int nCount15 { get; set; }
        public int nCount15_Excute { get; set; }
        public bool bCheck16 { get; set; }
        public int nCount16 { get; set; }
        public int nCount16_Excute { get; set; }
        public bool bCheck17 { get; set; }
        public int nCount17 { get; set; }
        public int nCount17_Excute { get; set; }
        public bool bCheck18 { get; set; }
        public int nCount18 { get; set; }
        public int nCount18_Excute { get; set; }
        public bool bCheck19 { get; set; }
        public int nCount19 { get; set; }
        public int nCount19_Excute { get; set; }
        public bool bCheck20 { get; set; }
        public int nCount20 { get; set; }
        public int nCount20_Excute { get; set; }
        public bool bCheck21 { get; set; }
        public int nCount21 { get; set; }
        public int nCount21_Excute { get; set; }
        public bool bCheck22 { get; set; }
        public int nCount22 { get; set; }
        public int nCount22_Excute { get; set; }
        public bool bCheck23 { get; set; }
        public int nCount23 { get; set; }
        public int nCount23_Excute { get; set; }

        public tagTempleTime()
        {
            nID = -1;
            bCheck00 = false;
            nCount00 = 0; nCount00_Excute = 0;
            bCheck01 = false;
            nCount01 = 0; nCount01_Excute = 0;
            bCheck02 = false;
            nCount02 = 0; nCount02_Excute = 0;
            bCheck03 = false;
            nCount03 = 0; nCount03_Excute = 0;
            bCheck04 = false;
            nCount04 = 0; nCount04_Excute = 0;
            bCheck05 = false;
            nCount05 = 0; nCount05_Excute = 0;
            bCheck06 = false;
            nCount06 = 0; nCount06_Excute = 0;
            bCheck07 = false;
            nCount07 = 0; nCount07_Excute = 0;
            bCheck08 = false;
            nCount08 = 0; nCount08_Excute = 0;
            bCheck09 = false;
            nCount09 = 0; nCount09_Excute = 0;
            bCheck10 = false;
            nCount10 = 0; nCount10_Excute = 0;
            bCheck11 = false;
            nCount11 = 0; nCount11_Excute = 0;
            bCheck12 = false;
            nCount12 = 0; nCount12_Excute = 0;
            bCheck13 = false;
            nCount13 = 0; nCount13_Excute = 0;
            bCheck14 = false;
            nCount14 = 0; nCount14_Excute = 0;
            bCheck15 = false;
            nCount15 = 0; nCount15_Excute = 0;
            bCheck16 = false;
            nCount16 = 0; nCount16_Excute = 0;
            bCheck17 = false;
            nCount17 = 0; nCount17_Excute = 0;
            bCheck18 = false;
            nCount18 = 0; nCount18_Excute = 0;
            bCheck19 = false;
            nCount19 = 0; nCount19_Excute = 0;
            bCheck20 = false;
            nCount20 = 0; nCount20_Excute = 0;
            bCheck21 = false;
            nCount21 = 0; nCount21_Excute = 0;
            bCheck22 = false;
            nCount22 = 0; nCount22_Excute = 0;
            bCheck23 = false;
            nCount23 = 0; nCount23_Excute = 0;
        }

        //public tagTempleTime(bool bCheck,int nCount)
        //{
        //    this.bCheck = bCheck;
        //    this.nCount = nCount;
        //}
    }

    public enum eTaskAcion
    {
        [Description("添加")]
        Add,
        [Description("修改")]
        Change,
        [Description("取消修改")]
        CancelChange,
        [Description("删除")]
        Delete,
        [Description("重置任务")]
        Reset
    };

    public enum EWebbrowserState
    {
        Start,//打开搜索引擎
        Search,//进行关键词搜索
        SearchSite,//搜索站点
        SearchSite_Match,//搜索站点
        AccessSite,//访问站点
        SearchPage,//搜索内页
        AccessPage,//搜索内页
        none//搜索内页
    }

    public class tagTask
    {
        [PrimaryKey, AutoIncrement]
        public int nID { get; set; }
        public eTaskAcion taskAcion { get; set; }
        public EWebbrowserState webState { get; set; }
        public bool bCheck { get; set; }
        public string strKeyword { get; set; }
        public string strTitle { get; set; }
        public string strSiteUrl { get; set; }
        public eEngines engine { get; set; }
        public eWebBrowser webBrowser { get; set; }
        public int nEngineTime { get; set; }
        public eSearchType searchType { get; set; }
        public int nCountPage { get; set; }
        public int nCountVaildToday { get; set; }
        public int nCountInvaildToday { get; set; }
        public int nCountTotal { get; set; }
        public int nCountExcuteToday { get; set; }
        public int nCountLimit { get; set; }
        public int nSiteTime { get; set; }
        public DateTime tViewSiteLastTime { get; set; }

        public ePageAccessType pageAccessType { get; set; }
        public string strPageUrl { get; set; }
        public int nCountPageVaildToday { get; set; }
        public int nCountPageInvaildToday { get; set; }
        public int nCountPageTotal { get; set; }
        public int nPageTime { get; set; }
        public DateTime tViewPageLastTime { get; set; }
        public DateTime tCreateTime { get; set; }
        public tagTempleTime tagtempleTime;// { get; set; }
        public eTempleTime templeTime { get; set; }

        public tagTask()
        {
            nID = -1;
            taskAcion = eTaskAcion.Add;
            webState = EWebbrowserState.none;
            bCheck = true;
            strKeyword = "";
            strTitle = "";
            strSiteUrl = "";
            engine = eEngines.Baidu;
            webBrowser = eWebBrowser.IE;
            nEngineTime = 3;
            searchType = eSearchType.OffSite;
            nCountPage = 50;
            nCountVaildToday = 0;
            nCountInvaildToday = 0;
            nCountTotal = 0;
            nCountExcuteToday = 0;
            nCountLimit = 30;
            nSiteTime = 3;
            tViewSiteLastTime = new DateTime();
            pageAccessType = ePageAccessType.None;
            strPageUrl = "";
            nCountPageVaildToday = 0;
            nCountPageInvaildToday = 0;
            nCountPageTotal = 0;
            nPageTime = 3;
            tViewPageLastTime = new DateTime();
            tCreateTime = new DateTime();
            tagtempleTime = new tagTempleTime();
            tagtempleTime.nID = nID;
            templeTime = eTempleTime.Rand;
        }
    };

    public class define
    {
        public static string GetEnumName(Enum en)
        {
            Type temType = en.GetType();
            MemberInfo[] memberInfos = temType.GetMember(en.ToString());
            if (memberInfos != null && memberInfos.Length > 0)
            {
                object[] objs = memberInfos[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (objs != null && objs.Length > 0)
                {
                    return ((DescriptionAttribute)objs[0]).Description;
                }
            }
            return en.ToString();
        }
    }

    public class AppEventArgs : EventArgs
    {
        public string message_string;
        public bool message_bool;
        public tagTask message_task;

    }

    public enum eConnectionType
    {
        [Description("宽带连接")]
        Broadband,
        [Description("模板0")]
        Temple0,
        [Description("模板1")]
        Temple1,
        [Description("模板2")]
        Temple2
    };

    public enum eConnectionInvert
    {
        [Description("30秒")]
        s30,
        [Description("60秒")]
        s60,
        [Description("90秒")]
        s90
    };

    public class tagSetting
    {
        [PrimaryKey]
        public int nID { get; set; }
        public bool bCheck { get; set; }
        public string strUsername { get; set; }
        public string strPwd { get; set; }
        public eConnectionType connectionType { get; set; }
        public eConnectionInvert connectionInvert { get; set; }
        public int nCount { get; set; }
        public string IP { get; set; }

        public tagSetting()
        {
            nID = 0;
            bCheck = true;
            strUsername = "";
            strPwd = "";
            connectionType = eConnectionType.Broadband;
            connectionInvert = eConnectionInvert.s60;
            nCount = 9999;
            IP = "";
        }
    };

    public class tagSearchResult
    {
        public int nItem;
        public bool bFinish;
        public bool bMatch;

        public tagSearchResult()
        {
            nItem = 0;
            bFinish = true;
            bMatch = true;
        }
    };
}
