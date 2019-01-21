using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.xxy.entity.Util
{
    public class CommonUtil
    {
        public  static string getUUID()
        {
            return System.Guid.NewGuid().ToString("N");
        }

        private CommonUtil()
        {

        }
    }
}
