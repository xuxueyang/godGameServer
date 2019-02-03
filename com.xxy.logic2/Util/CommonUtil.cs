using System;
using System.Collections.Generic;
using System.Text;

namespace com.xxy.entity.Util
{
    public class CommonUtil
    {
        /// <summary>
        /// 玩家创建的用UUID，怪物、地图技能、卡牌，这些全部由我自己操作的，就舍得为自动增长
        /// </summary>
        /// <returns></returns>
        public  static string getUUID()
        {
            return System.Guid.NewGuid().ToString("N");
        }

        private CommonUtil()
        {

        }
    }
}
