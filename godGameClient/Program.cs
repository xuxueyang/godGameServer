using com.xxy.entity.model;
using com.xxy.NetFrame.auto;
using com.xxy.Protocol;
using com.xxy.Protocol.DTO;
using Protocol.CommandProtocol;
using Protocol.DTO.BattleRoomDTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace godGameClient
{
    class Program
    {
        //private static ThreadStart SendMsg(StreamWriter sw)
        //{
        //    string sendMsg = string.Empty;
        //    Console.WriteLine("客户端启动");
        //    while (true)
        //    {

        //        sendMsg = Console.ReadLine();
        //        switch (sendMsg)
        //        {
        //            case "login":
        //                Console.WriteLine("准备登录");
        //                var bytes = login();
        //                sw.WriteLine(bytes);
        //                sw.Flush();             //刷新流
        //                //socket.Send(bytes); //发送数据
        //                //                        Console.WriteLine("登录完毕");
        //                break;
        //            default:
        //                Console.WriteLine("未知命令");
        //                break;
        //        }
        //    }
        //}
        //private static ThreadStart Receive(StreamReader sr)
        //{
        //    Console.WriteLine("开始监听服务器");
        //    while (true)
        //    {
        //        Thread.Sleep(2000);
        //        var message = sr.ReadLine();
        //        Console.WriteLine("服务器:" + message);
        //        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(message);
        //        var model = (SocketModel)MessageEncoding.decode(bytes);
        //        switch (model.command)
        //        {
        //            case BattleRoomProtocol.OVER_GAME_S:
        //                break;
        //            case LoginProtocol.LOGIN_SRES:
        //                Console.WriteLine("登录完毕");
        //                break;
        //            default:
        //                Console.WriteLine("未知命令");
        //                break;
        //        }
        //    }
        //}
        private static byte[] result = new byte[20480];
        private static RoomDTO roomDTO = null;
        private static List<RoomInfoDTO> selfInfo = null;
        private static List<RoomInfoDTO> otherInfo = null;
        static void Main(string[] args)
        {
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress serverIP = IPAddress.Parse(globalInfo.ipAddr);
            int port = globalInfo.port;
            IPEndPoint ipEndPoint = new IPEndPoint(serverIP, port);
            socket.Connect(ipEndPoint);
            //socket.ReceiveAsync(SocketAsyncEventArgs)
            //Parse将字符串转换为IP地址类型
            //IPAddress myIP = IPAddress.Parse("127.0.0.1");
            ////构造一个TcpClient类对象,TCP客户端
            //TcpClient client = new TcpClient();
            ////与TCP服务器连接
            //client.Connect(myIP, globalInfo.port);
            //Console.WriteLine("服务器已经连接...请输入对话内容...");

            ////创建网络流,获取数据流
            //NetworkStream stream = client.GetStream();
            ////读数据流对象
            //StreamReader sr = new StreamReader(stream);
            ////写数据流对象
            //StreamWriter sw = new StreamWriter(stream);

            //Thread th2 = new Thread(SendMsg(sw)); //也可简写为new Thread(ThreadMethod);                

            //Thread th = new Thread(Receive(sr)); //也可简写为new Thread(ThreadMethod);                
            //th.IsBackground = true;
            //th2.Start(); //启动线程 
            //th.Start(); //启动线程
            //Console.WriteLine("服务器:" + sr.ReadLine());
            Thread receiveServerThread = new Thread(new ThreadStart(delegate() {
                // 接受和显示服务器信息
                while (true)
                {
                    socket.Receive(result);

                    var k = result.ToList<byte>();
                    byte[] bytes = LengthEncoding.decode(ref k);
                    SocketModel model = (SocketModel)MessageEncoding.decode(bytes);
                    switch (model.command)
                    {
                        case BattleRoomProtocol.OVER_GAME_S:
                            break;
                        case LoginProtocol.LOGIN_SRES:
                            Console.WriteLine("登录完毕");
                            break;
                        case BattleRoomProtocol.CREATE_ONE_S:
                            Console.WriteLine("房间创建成功");
                            break;
                        case BattleRoomProtocol.START_TIME_S:
                            Console.WriteLine("得到初始化信息");
                            var dto = model.getMessage<ReturnDTO>();
                            RoomDTO room = (RoomDTO)dto.message;
                            roomDTO = room;
                            Console.WriteLine("战斗房间的其他人信息:");
                            selfInfo = (List<RoomInfoDTO>)roomDTO.map[CommonFieldProtocol.battleRoomBaseSelfInfo];
                            otherInfo = (List<RoomInfoDTO>)roomDTO.map[CommonFieldProtocol.battleRoomBaseOtherInfo];

                            Console.WriteLine(otherInfo[0]);
                            Console.WriteLine("战斗房间的自己信息:");
                            Console.WriteLine(selfInfo[0]);
                            break;
                        default:
                            Console.WriteLine("未知命令");
                            break;
                    }
                    bytes = new byte[20480];
                }
            }));
            receiveServerThread.Start();

            string sendMsg = string.Empty;
            sendMsg = Console.ReadLine();

            while (sendMsg!= "" + BattleRoomProtocol.OVER_GAME_S)
            {
                switch (sendMsg)
                {
                    case "login":
                        {
                            Console.WriteLine("准备登录");
                            var bytes = login();
                            socket.Send(bytes); //发送数据
                        }
                        break;
                    case "createroom":
                        {
                            Console.WriteLine("准备创建房间");
                            var bytes = createBattleRoom();
                            socket.Send(bytes); //发送数据
                        }
                        break;
                    case "usecard":
                        {
                            Console.WriteLine("使用卡牌");
                            var bytes = usecard();
                            socket.Send(bytes); //发送数据
                        }
                        break;
                    case "useskill":
                        {
                            Console.WriteLine("使用技能");
                            var bytes = useskill();
                            socket.Send(bytes); //发送数据
                        }
                        break;
                    default:
                        Console.WriteLine("未知命令");
                      break;
                }
                sendMsg = Console.ReadLine();

           }
           // client.Close();
          //  Console.Read();
        }

        private static byte[] usecard()
        {
            SocketModel model = new SocketModel();
            model.type = TypeProtocol.TYPE_BATTLE_ROOM;
            model.area = 0;
            model.command = BattleRoomProtocol.USE_CARD_C;
            RoomDTO dto = new RoomDTO();
            dto.roomId = roomDTO.roomId;
            dto.roomRoleId = roomDTO.roomRoleId;
            dto.map.Add(CommonFieldProtocol.useCardId, selfInfo[0].cardIds[0]);
            dto.map.Add(CommonFieldProtocol.targetIds, new List<string> { otherInfo[0].roomRoleId });
            
            model.message = dto;
            var message = MessageEncoding.encode(model);
            return LengthEncoding.encode(message);
        }

        private static byte[] useskill()
        {
            SocketModel model = new SocketModel();
            model.type = TypeProtocol.TYPE_BATTLE_ROOM;
            model.area = 0;
            model.command = BattleRoomProtocol.USE_SKILL_C;
            RoomDTO dto = new RoomDTO();
            dto.roomId = roomDTO.roomId;
            dto.roomRoleId = roomDTO.roomRoleId;
            model.message = dto;
            var message = MessageEncoding.encode(model);
            return LengthEncoding.encode(message);
        }

        static byte[] createBattleRoom()
        {
            SocketModel model = new SocketModel();
            model.type = TypeProtocol.TYPE_BATTLE_ROOM;
            model.area = 0;
            model.command = BattleRoomProtocol.CREATE_ONE_C;
            model.message = null;
            var message = MessageEncoding.encode(model);
            return LengthEncoding.encode(message);
        }
        static byte[] login()
        {
            AccountInfoDTO accountInfoDTO = new AccountInfoDTO();
            accountInfoDTO.accountName = "admin";
            accountInfoDTO.password = "admin";
            SocketModel model = new SocketModel();
            model.type = TypeProtocol.TYPE_LOGIN;
            model.area = 0;
            model.command = LoginProtocol.LOGIN_CREQ;
            model.message = accountInfoDTO;
            var message = MessageEncoding.encode(model);
            return LengthEncoding.encode(message);
        }
        //byte[] bytes = System.Text.Encoding.UTF8.GetBytes(sendMsg); //将要发送的信息转化为字节数组，因为Socket发送数据时是以字节的形式发送的

    }
}
