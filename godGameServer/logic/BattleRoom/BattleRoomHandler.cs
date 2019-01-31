using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.xxy.NetFrame;
using com.xxy.NetFrame.auto;
using com.xxy.Protocol.DTO.BattleRoomDTO;
using godGameServer.logic.BattleRoom.module;
using godGameServer.tool;
using Protocol.CommandProtocol;
using Protocol.DTO.BattleRoomDTO;

namespace godGameServer.logic.BattleRoom
{
    class BattleRoomHandler : HandlerInterface
    {
        RoomManager roomManager = new RoomManager();

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
                    roomManager.createOneRoom(new List<UserToken>() { token });
                    break;
                default:
                    {
                        //将消息转发到处理房间。
                        var room = roomManager.GetRoomById(message.getMessage<RoomDTO>().roomId);
                        // TODO room.useCard(message.getMessage<BattleRoomDTO>());
                        room.receiveMessage(BattleRoomProtocol.USE_CARD_C, message.getMessage<RoomDTO>());
                    }
                    break;
            }
        }
    }
}
