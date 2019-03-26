using System;
using System.Collections.Generic;
using com.xxy.logic.Base;
using godGameServer.dao.model;
using godGameServer.logic.BattleRoom.module;
using Protocol.CommandProtocol;
using Protocol.DTO.BattleRoomDTO;

//using 

namespace com.xxy.entity.model.BattleRoom
{
    /// <summary>
    /// 两个玩家之间的对战房间
    /// </summary>
    public class RoomTwoPlayer: MetaRoom
    {
        private TwoPlayerRoomRole winner = null;
        private TwoPlayerRoomRole loser = null;
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

        /// <summary>
        /// 初始化房間
        /// </summary>
        public override void _start_room()
        {
            __brocast_role_skills_cards();//广播技能和卡牌
            //随机选取一个角色作为主角色，现在就默认第一个是主角色吧
            //Random rd = new Random();
            //int i = rd.Next();
            //int first = i % npcs.Count;
            // 因为预处理在这边解决了
            battleTimeType = BattleTimeType.START;
            __selectStartRole(new List<RoomRole> { playerOne,playerTwo}, new List<int>() { 0 });
        }

        public override void _timerLogic()
        {
            bool over = _solve_battle_game_over();
            if (over)
                return;
            Console.WriteLine("处理：" + this.id + " 的逻辑中...");
            var currenPlayer = this.playerOne.IsMyTime ? this.playerOne : this.playerTwo;

            var result = new RoomDTO();
            var success = roomDTOs.TryDequeue(out result);
            if (success)
            {
                //校验是不是这个用户使用的，不是的话，丢弃
                switch (result.protocol)
                {
                    case BattleRoomProtocol.OVER_GAME_S:
                        if(playerOne.id == result.roomRoleId)
                        {
                            winner = playerTwo;
                            loser = playerOne;
                        }else if(playerTwo.id == result.roomRoleId)
                        {
                            winner = playerOne;
                            loser = playerTwo;
                        }
                        if (winner != null && loser != null)
                        {
                            _game_over();
                            return;
                        }
                        break;
                    default:
                        if (result.roomRoleId == currenPlayer.id)
                            __solve_message_dto(result);
                        break;
                }
            }
            //需要执行所有角色的预处理状态(每帧，而不是每回合）
            currenPlayer._solve_battle_logic(new List<RoomRole> { playerOne,playerTwo});


            //校验是不是所有当前角色的属于回合over状态，是的话，进入下一个回合
            bool next = currenPlayer.battleTimeType == BattleTimeType.OVER;
            if (next)
            {
                this.battleTimeType = BattleTimeType.OVER;
            }
            switch (this.battleTimeType)
            {
                case BattleTimeType.START:
                    break;
                case BattleTimeType.PRE:
                    break;
                case BattleTimeType.BATTLE:

                    break;
                case BattleTimeType.NEXT:
                    break;
                case BattleTimeType.OVER:
                    //TODO 将进入另一个回合
                    _nextTime();
                    break;
                default:
                    // 默认是全员等待OVER的状态
                    break;
            }
        }

        protected override void _nextTime()
        {
            int index = 1;
            if (this.playerOne.IsMyTime)
            {
                this.playerOne.IsMyTime = false;
                index = 2;
            }
            __selectStartRole(new List<RoomRole> { playerOne, playerTwo }, new List<int> { index });
        }

        protected override void _game_over()
        {
            if(this.battleTimeType != BattleTimeType._GAME_OVER)
            {
                Console.WriteLine("回合结束：[" + this.id + "]");
                this.battleTimeType = BattleTimeType._GAME_OVER;
            }
        }

        /// <summary>
        /// 判断有没有回合结束
        /// </summary>
        /// <returns></returns>
        protected override bool _solve_battle_game_over()
        {
            if (playerOne.isDead || playerTwo.isDead)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}