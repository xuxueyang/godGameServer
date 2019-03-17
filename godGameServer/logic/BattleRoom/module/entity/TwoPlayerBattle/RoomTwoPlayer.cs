using System;
using System.Collections.Generic;
using com.xxy.logic.Base;
using godGameServer.dao.model;
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
        private TwoPlayerRoomRole playerOne;
        private TwoPlayerRoomRole playerTwo;
        public RoomTwoPlayer(RoomType roomType, RoomMessageManaer messageManaer,List<RoomRole> roles) 
            : base(roomType,messageManaer,roles)
        {
            if(RoomType.TWO_PLAYER != roomType)
                throw new Exception("要求创建的房间类型不匹配");
            if(roles.Count !=2)
                throw new Exception("要求创建的人数不符合");
            this.playerOne = (TwoPlayerRoomRole)roles[0];
            this.playerTwo = (TwoPlayerRoomRole)roles[1];
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