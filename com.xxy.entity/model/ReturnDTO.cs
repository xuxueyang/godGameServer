using com.xxy.logic.Base.Errors;
using System;
using System.Collections.Generic;
using System.Text;

namespace com.xxy.entity.model
{
    [Serializable]
    /// <summary>
    /// 内部处理的消息体
    /// </summary>
    public class ReturnDTO
    {
        /// <summary>
        /// 含有的错误消息
        /// </summary>
        public List<BaseError> errors;
        /// <summary>
        /// 默认没有错误
        /// </summary>
        public bool hasError = false;
        /// <summary>
        /// 返回的消息体
        /// </summary>
        public object message;
        /// <summary>
        /// 返回的code
        /// </summary>
        public string returnCode;

        public ReturnDTO(string returnCode)
        {
            this.returnCode = returnCode;
            if (!returnCode.StartsWith("200")&&!returnCode.StartsWith("0"))
            {
                this.hasError = false;
            }
            // returnCode，分解，将后续的作为message
            String[] arr = returnCode.Split('—');
            if (arr.Length > 1)
            {
                this.message = arr[1];
            }
        }
        public ReturnDTO(string returnCode,object message)
        {
            this.message = message;
            this.returnCode = returnCode;
        }
    }
}
