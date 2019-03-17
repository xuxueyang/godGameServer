using System;
using System.Collections.Generic;
using com.xxy.logic.Base;
using godGameServer.logic.BattleRoom.module;
using Protocol.DTO.BattleRoomDTO;

//using 

namespace com.xxy.entity.model.BattleRoom
{
    /// <summary>
    /// 两个玩家之间的对战房间
    /// </summary>
    public class RoomTwoPlayer: MetaRoom
    {
        public RoomTwoPlayer(RoomType roomType, RoomMessageManaer messageManaer,List<RoomRole> roles) 
            : base(roomType,messageManaer,roles)
        {
            if(RoomType.TWO_PLAYER != roomType)
                throw new Exception("要求创建的房间类型不匹配");
        }

        protected override bool _solve_battle_game_over()
        {
            throw new NotImplementedException();
        }

        public override void _start_room()
        {
            throw new NotImplementedException();
        }

        protected override void _game_over()
        {
            throw new NotImplementedException();
        }

        protected override void _solve_message_dto(RoomDTO result)
        {
            throw new NotImplementedException();
        }

        protected override void nextTime()
        {
            throw new NotImplementedException();
        }

        public override void _timerLogic()
        {
            throw new NotImplementedException();
        }
    }
}