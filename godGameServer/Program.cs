using com.xxy.NetFrame;
using com.xxy.NetFrame.auto;
using com.xxy.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace godGameServer
{
    class Program
    {
        static void Main(string[] args)
        {
            //服务器初始化
            ServerStart server = new ServerStart(20);
            //赋值长度、数据、编码和解码、消息中心赋值
            server.encode = MessageEncoding.encode;
            server.decode = MessageEncoding.decode;
            server.LE = LengthEncoding.encode;
            server.LD = LengthEncoding.decode;
            server.center = new HandlerCenter();
            //开启端口
            server.start(globalInfo.port);
            while (true)
            {

            }
        }
    }
}
