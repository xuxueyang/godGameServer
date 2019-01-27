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

namespace godGameServer.logic.BattleRoom
{
    class BattleRoomHandler : HandlerInterface
    {
        RoomManager roomManager = new RoomManager();

        //TODO 做个循环，按照时间调用，战斗房间的处理方法
        public BattleRoomHandler()
        {
            ScheduleUtil.Instance().schedule(_solveBattleLogic, 1000);
        }

        /// <summary>
        /// 定时处理战斗逻辑（主要是针对单人NPC房间）
        /// </summary>
        void _solveBattleLogic()
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
            //收到信息的处理
            switch (message.command)
            {
                case BattleRoomProtocol.CREATE_ONE_C:
                    // 创建战斗房间，需要战斗人员们的ID，从ID获取到配置的技能信息，初始化玩家的卡牌、技能、血量、MP等数据
                    // 创建怪物ID，做一个定时器，每回合调用。
                    roomManager.createOneRoom(new List<UserToken>() { token});
                    break;
                case BattleRoomProtocol.USE_CARD_C:
                    //玩家使用来技能，针对那个对象等
                    var room = roomManager.GetRoomById(message.getMessage<BattleRoomDTO>().roomId);
                    room.useCard(message.getMessage<BattleRoomDTO>());
                    break;
                case BattleRoomProtocol.USE_SKILL_C:
                    //玩家使用来技能，针对某个对象
                    break;
                case BattleRoomProtocol.OVER_TIME_C:
                    ///TODO 结束战斗回合
                    
                    break;
            }
        }
    }
}
