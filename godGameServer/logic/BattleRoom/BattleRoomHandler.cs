﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.xxy.NetFrame;
using com.xxy.NetFrame.auto;
using godGameServer.logic.BattleRoom.module;
using godGameServer.tool;
using Protocol.CommandProtocol;

namespace godGameServer.logic.BattleRoom
{
    class BattleRoomHandler : HandlerInterface
    {
        RoomManager roomManager = new RoomManager();

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
                case BattleRoomProtocol.CREATE_C:
                    // 创建战斗房间，需要战斗人员们的ID，从ID获取到配置的技能信息，初始化玩家的卡牌、技能、血量、MP等数据
                    // 创建怪物ID，做一个定时器，每回合调用。
                    roomManager.createRoom(new List<UserToken>() { token});
                    break;
            }
        }
    }
}
