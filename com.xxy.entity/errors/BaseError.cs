using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.xxy.entity.errors
{
    public class BaseError:Exception
    {
        private String message;
        private String code;
        public BaseError(String code, String message)
        {
            this.code = code;
            this.message = message;
        }
    }
}
