using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using SQLitePCL;

namespace RankHelper
{
    class TaskDB: SQLiteConnection
    {
        //定义属性，便于外部访问数据表
        public TableQuery<tagTask> Tasks { get { return this.Table<tagTask>(); } }

        public TaskDB(string dbPath) : base(dbPath)
        {
            //创建数据表
            CreateTable<tagTask>();
        }
    }

    class TempleTimeDB : SQLiteConnection
    {
        //定义属性，便于外部访问数据表
        public TableQuery<tagTempleTime> TempleTimes { get { return this.Table<tagTempleTime>(); } }

        public TempleTimeDB(string dbPath) : base(dbPath)
        {
            //创建数据表
            CreateTable<tagTempleTime>();
        }
    }

    class SettingDB : SQLiteConnection
    {
        //定义属性，便于外部访问数据表
        public TableQuery<tagSetting> Setting { get { return this.Table<tagSetting>(); } }

        public SettingDB(string dbPath) : base(dbPath)
        {
            //创建数据表
            CreateTable<tagSetting>();
        }
    }
}
