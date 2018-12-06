using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using SQLite;
using SQLitePCL;

namespace RankHelper
{
    public class Appinfo
    {
        private static ILog Logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static string strVersion = "1.0";
        public static string strTitleName = "流量精灵助手";

        public static string strServiceVersion = "1.0";
        public static string strServiceTitleName = "流量精灵助手监控器";

        public static bool bWork = false;

        public static string strUrl_RechargePoint = "www.baidu.com";
        public static string strUserAgent;

        public static List<tagTask> listTask;
        private static string dbPath;

        public static void Init()
        {
            Appinfo.dbPath = $"{Environment.CurrentDirectory}\\Tasks.db";
            Appinfo.LoadTask();
        }

        public static void LoadTask()
        {
            Appinfo.listTask = new List<tagTask>();
            Appinfo.listTask.Clear();

            using (var db = new TaskDB(Appinfo.dbPath))
            {
                var tasks = db.Tasks.ToList();
                Appinfo.listTask = tasks;
            }
            using (var db = new TempleTimeDB(Appinfo.dbPath))
            {
                var tasks = db.TempleTimes.ToList();

                for (int i = 0; i < tasks.Count; i++)
                {
                    for (int j = 0; j < Appinfo.listTask.Count; j++)
                    {
                        if (Appinfo.listTask[j].nID == tasks[i].nID)
                        {
                            Appinfo.listTask[j].tagtempleTime = tasks[i];
                            continue;
                        }
                    }
                }
            }            
        }

        public static void AddTask(tagTask task)
        {
            //int nID = 0;
            //for (int i = Appinfo.listTask.Count-1; i >= 0; i--)
            //{
            //    nID = Appinfo.listTask[i].nID + 1;
            //    break;
            //}
            //task.nID = nID;
            Appinfo.listTask.Add(task);

            using (var db = new TaskDB(Appinfo.dbPath))
            {
                try
                {
                    int count = db.Insert(task);
                    Logger.Info($"{DateTime.Now}, 插入{count}条记录");
                }
                catch (Exception e)
                {
                    throw;
                }
            }

            Appinfo.LoadTask();
            task.tagtempleTime.nID = Appinfo.listTask[Appinfo.listTask.Count - 1].nID;

            using (var db = new TempleTimeDB(Appinfo.dbPath))
            {
                try
                {
                    int count = db.Insert(task.tagtempleTime);
                }
                catch (Exception e)
                {
                    throw;
                }
            }

            Appinfo.LoadTask();
            task = Appinfo.listTask[Appinfo.listTask.Count-1];
        }

        public static bool ChangeTask(tagTask task)
        {
            for (int i = 0; i < Appinfo.listTask.Count; i++)
            {
                if (Appinfo.listTask[i].nID == task.nID)
                {
                    Appinfo.listTask[i].engine = task.engine;
                    Appinfo.listTask[i].searchType = task.searchType;
                    Appinfo.listTask[i].strKeyword = task.strKeyword;
                    Appinfo.listTask[i].strTitle = task.strTitle;
                    Appinfo.listTask[i].strSiteUrl = task.strSiteUrl;
                    Appinfo.listTask[i].nEngineTime = task.nEngineTime;
                    Appinfo.listTask[i].nCountPage = task.nCountPage;
                    Appinfo.listTask[i].webBrowser = task.webBrowser;
                    Appinfo.listTask[i].nCountLimit = task.nCountLimit;
                    Appinfo.listTask[i].nSiteTime = task.nSiteTime;
                    Appinfo.listTask[i].pageAccessType = task.pageAccessType;
                    Appinfo.listTask[i].strPageUrl = task.strPageUrl;
                    Appinfo.listTask[i].nPageTime = task.nPageTime;

                    Appinfo.listTask[i].tagtempleTime.bCheck00 = task.tagtempleTime.bCheck00;
                    Appinfo.listTask[i].tagtempleTime.bCheck01 = task.tagtempleTime.bCheck01;
                    Appinfo.listTask[i].tagtempleTime.bCheck02 = task.tagtempleTime.bCheck02;
                    Appinfo.listTask[i].tagtempleTime.bCheck03 = task.tagtempleTime.bCheck03;
                    Appinfo.listTask[i].tagtempleTime.bCheck04 = task.tagtempleTime.bCheck04;
                    Appinfo.listTask[i].tagtempleTime.bCheck05 = task.tagtempleTime.bCheck05;
                    Appinfo.listTask[i].tagtempleTime.bCheck06 = task.tagtempleTime.bCheck06;
                    Appinfo.listTask[i].tagtempleTime.bCheck07 = task.tagtempleTime.bCheck07;
                    Appinfo.listTask[i].tagtempleTime.bCheck08 = task.tagtempleTime.bCheck08;
                    Appinfo.listTask[i].tagtempleTime.bCheck09 = task.tagtempleTime.bCheck09;
                    Appinfo.listTask[i].tagtempleTime.bCheck10 = task.tagtempleTime.bCheck10;
                    Appinfo.listTask[i].tagtempleTime.bCheck11 = task.tagtempleTime.bCheck11;
                    Appinfo.listTask[i].tagtempleTime.bCheck12 = task.tagtempleTime.bCheck12;
                    Appinfo.listTask[i].tagtempleTime.bCheck13 = task.tagtempleTime.bCheck13;
                    Appinfo.listTask[i].tagtempleTime.bCheck14 = task.tagtempleTime.bCheck14;
                    Appinfo.listTask[i].tagtempleTime.bCheck15 = task.tagtempleTime.bCheck15;
                    Appinfo.listTask[i].tagtempleTime.bCheck16 = task.tagtempleTime.bCheck16;
                    Appinfo.listTask[i].tagtempleTime.bCheck17 = task.tagtempleTime.bCheck17;
                    Appinfo.listTask[i].tagtempleTime.bCheck18 = task.tagtempleTime.bCheck18;
                    Appinfo.listTask[i].tagtempleTime.bCheck19 = task.tagtempleTime.bCheck19;
                    Appinfo.listTask[i].tagtempleTime.bCheck20 = task.tagtempleTime.bCheck20;
                    Appinfo.listTask[i].tagtempleTime.bCheck21 = task.tagtempleTime.bCheck21;
                    Appinfo.listTask[i].tagtempleTime.bCheck22 = task.tagtempleTime.bCheck22;
                    Appinfo.listTask[i].tagtempleTime.bCheck23 = task.tagtempleTime.bCheck23;

                    Appinfo.listTask[i].tagtempleTime.nCount00 = task.tagtempleTime.nCount00;
                    Appinfo.listTask[i].tagtempleTime.nCount01 = task.tagtempleTime.nCount01;
                    Appinfo.listTask[i].tagtempleTime.nCount02 = task.tagtempleTime.nCount02;
                    Appinfo.listTask[i].tagtempleTime.nCount03 = task.tagtempleTime.nCount03;
                    Appinfo.listTask[i].tagtempleTime.nCount04 = task.tagtempleTime.nCount04;
                    Appinfo.listTask[i].tagtempleTime.nCount05 = task.tagtempleTime.nCount05;
                    Appinfo.listTask[i].tagtempleTime.nCount06 = task.tagtempleTime.nCount06;
                    Appinfo.listTask[i].tagtempleTime.nCount07 = task.tagtempleTime.nCount07;
                    Appinfo.listTask[i].tagtempleTime.nCount08 = task.tagtempleTime.nCount08;
                    Appinfo.listTask[i].tagtempleTime.nCount09 = task.tagtempleTime.nCount09;
                    Appinfo.listTask[i].tagtempleTime.nCount10 = task.tagtempleTime.nCount10;
                    Appinfo.listTask[i].tagtempleTime.nCount11 = task.tagtempleTime.nCount11;
                    Appinfo.listTask[i].tagtempleTime.nCount12 = task.tagtempleTime.nCount12;
                    Appinfo.listTask[i].tagtempleTime.nCount13 = task.tagtempleTime.nCount13;
                    Appinfo.listTask[i].tagtempleTime.nCount14 = task.tagtempleTime.nCount14;
                    Appinfo.listTask[i].tagtempleTime.nCount15 = task.tagtempleTime.nCount15;
                    Appinfo.listTask[i].tagtempleTime.nCount16 = task.tagtempleTime.nCount16;
                    Appinfo.listTask[i].tagtempleTime.nCount17 = task.tagtempleTime.nCount17;
                    Appinfo.listTask[i].tagtempleTime.nCount18 = task.tagtempleTime.nCount18;
                    Appinfo.listTask[i].tagtempleTime.nCount19 = task.tagtempleTime.nCount19;
                    Appinfo.listTask[i].tagtempleTime.nCount20 = task.tagtempleTime.nCount20;
                    Appinfo.listTask[i].tagtempleTime.nCount21 = task.tagtempleTime.nCount21;
                    Appinfo.listTask[i].tagtempleTime.nCount22 = task.tagtempleTime.nCount22;
                    Appinfo.listTask[i].tagtempleTime.nCount23 = task.tagtempleTime.nCount23;

                    Appinfo.listTask[i].templeTime = task.templeTime;

                    using (var db = new TaskDB(Appinfo.dbPath))
                    {
                        try
                        {
                            int count = db.Update(Appinfo.listTask[i]);
                            Logger.Info($"{DateTime.Now}, 修改1条记录,{Appinfo.listTask[i].nID}");
                        }
                        catch (Exception e)
                        {
                            throw;
                        }
                    }

                    using (var db = new TempleTimeDB(Appinfo.dbPath))
                    {
                        try
                        {
                            int count = db.Update(Appinfo.listTask[i].tagtempleTime);
                        }
                        catch (Exception e)
                        {
                            throw;
                        }
                    }

                    return true;
                }
            }
            return false;
        }

        public static bool CountTask(tagTask task)
        {
            for (int i = 0; i < Appinfo.listTask.Count; i++)
            {
                if (Appinfo.listTask[i].nID == task.nID)
                {
                    Appinfo.listTask[i].nCountVaildToday = task.nCountVaildToday;
                    Appinfo.listTask[i].nCountInvaildToday = task.nCountInvaildToday;
                    Appinfo.listTask[i].nCountTotal = task.nCountTotal;
                    Appinfo.listTask[i].nCountExcuteToday = task.nCountExcuteToday;
                    Appinfo.listTask[i].nCountPageVaildToday = task.nCountPageVaildToday;
                    Appinfo.listTask[i].nCountPageInvaildToday = task.nCountPageInvaildToday;
                    Appinfo.listTask[i].nCountPageTotal = task.nCountPageTotal;

                    using (var db = new TaskDB(Appinfo.dbPath))
                    {
                        try
                        {
                            int count = db.Update(Appinfo.listTask[i]);
                            Logger.Info($"{DateTime.Now}, 统计1条记录,{Appinfo.listTask[i].nID}");
                        }
                        catch (Exception e)
                        {
                            throw;
                        }
                    }
                    return true;
                }
            }
            return false;
        }

        public static bool DeleteTask(tagTask task)
        {
            for (int i = 0; i < Appinfo.listTask.Count; i++)
            {
                if (Appinfo.listTask[i].nID == task.nID)
                {
                    Appinfo.listTask.Remove(Appinfo.listTask[i]);
                    using (var db = new TaskDB(Appinfo.dbPath))
                    {
                        try
                        {
                            int count = db.Delete(task);
                            Logger.Info($"{DateTime.Now}, 删除{count}条记录,{task.nID}");
                        }
                        catch (Exception e)
                        {
                            throw;
                        }
                    }
                    using (var db = new TempleTimeDB(Appinfo.dbPath))
                    {
                        try
                        {
                            int count = db.Delete(task.tagtempleTime);
                        }
                        catch (Exception e)
                        {
                            throw;
                        }
                    }
                    return true;
                }
            }

            return false;
        }

        public static tagTask QueryTask(int nTaskID)
        {
            foreach (var item in Appinfo.listTask)
            {
                if (item.nID.Equals(nTaskID))
                {
                    return item;
                }
            }
            return null;
        }

        public static tagSetting QuerySetting()
        {

            using (var db = new SettingDB(Appinfo.dbPath))
            {
                try
                {
                    tagSetting setting = db.Query<tagSetting>("SELECT * FROM tagSetting;").FirstOrDefault();
                    if(setting==null)
                    {
                        setting = new tagSetting();
                        db.Insert(setting);
                    }
                    return setting;
                }
                catch (Exception e)
                {
                    throw;
                }
            }
            return null;
        }

        public static bool UpdateSetting(tagSetting setting)
        {
            using (var db = new SettingDB(Appinfo.dbPath))
            {
                try
                {
                    string sql = string.Format("UPDATE tagSetting SET bCheck ='{0}',strUsername = '{1}', strPwd = '{2}',connectionType = '{3}',connectionInvert = '{4}',nCount = '{5}',IP = '{6}' WHERE nID = '{7}'",
                        TypeUtils.BoolToInt(setting.bCheck),
                        setting.strUsername,
                        setting.strPwd,
                        setting.connectionType,
                        setting.connectionInvert,
                        setting.nCount,
                        setting.IP,
                        setting.nID);
                    int count = db.Execute(sql);
                    Logger.Info($"{DateTime.Now}, 更新设置");
                }
                catch (Exception e)
                {
                    throw;
                }
            }
            return true;
        }

        public static bool UpdateIP(string strIP)
        {
            using (var db = new SettingDB(Appinfo.dbPath))
            {
                try
                {
                    string sql = string.Format("UPDATE tagSetting SET IP = '{0}' WHERE nID = '0'", strIP);
                    int count = db.Execute(sql);
                    if(count == 0)
                    {
                        count = db.Insert(new tagSetting());
                        count = db.Execute(sql);
                    }
                    Logger.Info($"{DateTime.Now}, 更新IP,{strIP}");
                }
                catch (Exception e)
                {
                    throw;
                }
            }
            return true;
        }

        public static string GetIP()
        {
            using (var db = new SettingDB(Appinfo.dbPath))
            {
                try
                {
                    var info = db.Setting.FirstOrDefault();
                    if (info!=null)
                    {
                        return info.IP;
                    }
                }
                catch (Exception e)
                {
                    throw;
                }
            }
            return "";
        }
    }
}
