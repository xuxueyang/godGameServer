using System;
using System.Collections.Generic;
using System.Text;

namespace com.xxy.logic.Base.Errors
{
    public enum Code:int
    {
        NotCardTemplate=3001,//没有这样的卡牌
    }
    public class BaseError:Exception
    {
        private string message;
        private Code code;

        public BaseError(Code notCardTemplate, string message)
        {
            this.code = notCardTemplate;
            this.message = message;
        }
    }
}
