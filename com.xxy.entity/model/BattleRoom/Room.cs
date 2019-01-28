using System;
using System.Collections.Generic;
using System.Text;
using com.xxy.entity.Base.GameRole;
using com.xxy.entity.Util;
using com.xxy.logic.Base;
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
        public RoomType roomType;
        private BattleTimeType battleTimeType = BattleTimeType.START;
        public List<RoomRole> roles = new List<RoomRole>();

        public Room(RoomType roomType)
        {
            this.id = CommonUtil.getUUID();
            this.roomType = roomType;
        }

        /// <summary>
        /// 进行房间的战斗逻辑判断
        /// </summary>
        public void _timerLogic()
        {
            //需要执行所有角色的预处理状态
            Console.WriteLine("处理：" + this.id + " 的逻辑中...");
            foreach (var item in roles)
            {
                item._solve_each_logic();            
            }
            //校验是不是所有当前角色的属于回合over状态，是的话，进入下一个回合
            bool next = true;
            foreach (var item in this._currentRoles)
            {
                item._solve_battle_logic(this.getNoCurrentTime());
                if (item.battleTimeType != BattleTimeType.OVER)
                {
                    next = false;
                }
            }
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
                    nextTime();
                    break;
                default:
                    // 默认是全员等待OVER的状态
                    break;
            }
        }
        private void nextTime()
        {
            //TODO 进入下一个回合，将排除当前的角色进入下一个阶段
            // 先将全员设置为不可用，从剩下的设置为回合开始
            Console.WriteLine("好啦。回合结束，让我们看看对面怎么操作的吧！~\n\n");

            var nextPlayers = getNoCurrentTime();
            foreach (var item in this._currentRoles)
            {
                item.isMyTime = false;
            }
            int max = nextPlayers.Count-1;
            var ints = new List<int>();
            while (max >= 0)
            {
                ints.Add(max--);
            }
            selectStartRole(nextPlayers, ints);
        }
        private List<RoomRole> _currentRoles;
        /// <summary>
        /// 得到当前回合的角色
        /// </summary>
        /// <returns></returns>
        private List<RoomRole> getCurrentTime()
        {
            List<RoomRole> roles = new List<RoomRole>();
            foreach (var item in this.roles)
            {
                if (item.isMyTime) { roles.Add(item); }
            }
            return roles;
        }
        private List<RoomRole> getNoCurrentTime()
        {
            List<RoomRole> roles = new List<RoomRole>();
            foreach (var item in this.roles)
            {
                if (!item.isMyTime) { roles.Add(item); }
            }
            return roles;
        }
        public List<RoomRole> getAllPlayer()
        {
            List<RoomRole> roles = new List<RoomRole>();
            foreach (var item in this.roles)
            {
                if (item.roleType == logic.Base.RoleType.PLAYER) { roles.Add(item); }
            }
            return roles;
        }
        public List<RoomRole> getAllNPC()
        {
            List<RoomRole> roles = new List<RoomRole>();
            foreach (var item in this.roles)
            {
                if (item.roleType == logic.Base.RoleType.NPC) { roles.Add(item); }
            }
            return roles;
        }

        public class CardEventArgs:EventArgs
        {
            public string targetId;
        }
        public void selectStartRole(List<RoomRole> roles, List<int> index)
        {
            foreach (var first in index)
            {
                roles[first].isMyTime = true;
            }
            // 同时将这些状态设置为Battle
            var players = getCurrentTime();
            foreach (var item in players)
            {
                item.battleTimeType = BattleTimeType.START;
            }
            this.battleTimeType = BattleTimeType.START;
            this._currentRoles = players;
        }
    }
}
