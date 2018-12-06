using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RankHelper
{
    class TypeUtils
    {
        public static int BoolToInt(object obj)
        {
            if (Convert.ToBoolean(obj) == true)
                return 1;
            else
                return 0;
        }

        // 整型转换为布尔类型
        public static bool IntToBool(object obj)
        {
            if (Convert.ToInt32(obj) == 1)
                return true;
            else
                return false;
        }
    }
}
