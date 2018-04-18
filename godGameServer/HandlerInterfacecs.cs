using com.xxy.NetFrame;
using com.xxy.NetFrame.auto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace godGameServer
{
    public interface HandlerInterface
    {
        /// <summary>
        /// 客户端连接
        /// </summary>
        /// <param name="token">连接的客户端对象</param>
        void ClientConnect(UserToken token);
        /// <summary>
        /// 客户端消息
        /// </summary>
        /// <param name="">发送客户端对象</param>
        /// <param name="message">消息内容</param>
        void MessageReceive(UserToken token, SocketModel message);
        /// <summary>
        /// 客户端断开连接
        /// </summary>
        /// <param name="token">要断开的客户端对象</param>
        /// <param name="error">断开的错误信息</param>
        void ClientClose(UserToken token, string error);
    }
}
