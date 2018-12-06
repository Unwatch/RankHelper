using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace RankHelper
{
    public class LogHelper
    {
        public static void InitLog4Net()
        {
            //log4net.Config.XmlConfigurator.Configure();

            var configFile = new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log4net.config"));
            log4net.Config.XmlConfigurator.ConfigureAndWatch(configFile);
        }

    }
}
