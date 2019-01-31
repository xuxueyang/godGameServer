using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Protocol.CommandProtocol
{

    public class BattleRoomProtocol
    {
        //public const int CREATE_ERROR_S = -1;//创建房间失败

        public const int CREATE_ONE_C = 0;//客户端申请创建单人战斗房间
        public const int CREATE_ONE_S = 1;//服务端回应申请单人战斗房间

        public const int LEFT_C = 2;//客户端申请解除战斗房间
        public const int LEFT_S = 3;//服务器解除战斗房间

        public const int OVER_TIME_C = 4; //解除当前回合
        public const int OVER_TIME_S = 5;

        public const int USE_CARD_C = 6; // 使用卡牌
        public const int USE_CARD_S = 7;

        public const int USE_SKILL_C = 8;//使用技能
        public const int USE_SKILL_S = 9;


        public const int NPC_USE_CARD = 210;//npc使用卡牌
        public const int NPC_USE_SKILL = 211;//npc使用技能

        public const int START_TIME_S = 301;//战斗开始
    }
}
