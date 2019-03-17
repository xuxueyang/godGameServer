using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.xxy.NetFrame;
using com.xxy.NetFrame.auto;
using com.xxy.Protocol.DTO.BattleRoomDTO;
using godGameServer.biz;
using godGameServer.dao.model;
using godGameServer.logic.BattleRoom.module;
using godGameServer.tool;
using Protocol.CommandProtocol;
using Protocol.DTO.BattleRoomDTO;

namespace godGameServer.logic.BattleRoom
{
    class BattleRoomHandler : HandlerInterface
    {
        RoomManager roomManager = new RoomManager();
        RoomMessageManaer messageManaer = new RoomMessageManaer();
        
        private ConcurrentQueue<RoleModel> twoBattleRoomRoleModels = new ConcurrentQueue<RoleModel>();
        //TODO 做个循环，按照时间调用，战斗房间的处理方法
        public BattleRoomHandler()
        {

        }

        void HandlerInterface.ClientClose(UserToken token, string error)
        {
            //断开连接的处理
            //ExecutorPool.Instance.execute(
            //    delegate ()
            //    {
            //        roleBiz.offline(token);
            //    }
            //);
        }

        void HandlerInterface.ClientConnect(UserToken token)
        {
            //重连接的逻辑处理

        }

        void HandlerInterface.MessageReceive(UserToken token, SocketModel message)
        {
            switch (message.command)
            {
                case BattleRoomProtocol.CREATE_ONE_C:
                    // 创建战斗房间，需要战斗人员们的ID，从ID获取到配置的技能信息，初始化玩家的卡牌、技能、血量、MP等数据
                    // 创建怪物ID，做一个定时器，每回合调用。
                    Console.WriteLine("接收到一个创建房间的请求");
                    roomManager.createOneRoom(new List<UserToken>() { token });
                    break;
                //case BattleRoomProtocol.TEXT_MESSAGE:

                //    break;
                case BattleRoomProtocol.CREATE_TWO_C:
                    Console.WriteLine("有人想创建一个多人房间");
                    addTwoBattleRoomRoleModels(token);
                    break;
                default:
                    {
                        //将消息转发到处理房间。
                        var room = roomManager.GetRoomById(message.getMessage<RoomDTO>().roomId);
                        // TODO room.useCard(message.getMessage<BattleRoomDTO>());
                        room.receiveMessage(message.command, message.getMessage<RoomDTO>());
                    }
                    break;
            }
        }

        private void addTwoBattleRoomRoleModels(UserToken userToken)
        {
            RoleModel roleModel = messageManaer.getUserModel(userToken);
            if (roleModel != null)
            {
                twoBattleRoomRoleModels.Enqueue(roleModel);
                messageManaer.write(roleModel.accountId,BattleRoomProtocol.WAIT_TWO_ROOM_CREATE_S);
                //如果队列中数目大于1
                if (twoBattleRoomRoleModels.Count >= 2)
                {
                    //锁定
                    RoleModel one = null;
                    twoBattleRoomRoleModels.TryDequeue(out one);
                    if (one != null)
                    {
                        RoleModel two;
                        twoBattleRoomRoleModels.TryDequeue(out two);
                        if (two != null)
                        {
                            //创建房间，发送数据
                            roomManager.createTwoRoom(one,two);
                        }
                        else
                        {
                            twoBattleRoomRoleModels.Enqueue(one);
                        }
                    }
                }
            }
        }
    }
}
