using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.xxy.NetFrame;
using com.xxy.NetFrame.auto;

namespace godGameServer.logic.JJCMatch
{
    public class JJCMatchHandler : HandlerInterface
    {
        public void ClientClose(UserToken token, string error)
        {
           //关闭匹配的

        }

        public void ClientConnect(UserToken token)
        {
            
        }

        public void MessageReceive(UserToken token, SocketModel message)
        {
            //根据协议，处理不同匹配消息。
        }
    }
}
