using System;
using System.Collections.Generic;
using System.Text;
using com.xxy.entity.Base.GameRole;
using com.xxy.entity.Util;
using com.xxy.logic.Base.Card;
using com.xxy.Protocol.DTO.BattleRoomDTO;

namespace com.xxy.entity.model.BattleRoom
{
    /// <summary>
    /// 战斗房间的类
    /// </summary>
    public class Room
    {
        /// <summary>
        /// 房间ID
        /// </summary>
        public string id;

        public List<RoomRole> roles;

        public Room()
        {
            this.id = CommonUtil.getUUID();
        }

        /// <summary>
        /// j进行房间的战斗逻辑判断
        /// </summary>
        void _solveLogic()
        {
            foreach (var item in roles)
            {
                item._solveLogic();
            }
        }

        public void useCard(BattleRoomDTO dto)
        {
            BaseCard card = null; // TODO get car by dto.useId  默认设置成伤害，直伤技能的卡牌
            var eventArgs = new UseCardEventArgs();
            eventArgs.target = null;//TODO dto.targetId;目标实体
            card.useCard(dto.sourceId,eventArgs);
            //玩家一般，使用了卡牌，设置卡牌效果，以及，。。。
        }
        public class CardEventArgs:EventArgs
        {
            public string targetId;
        }
    }
}
