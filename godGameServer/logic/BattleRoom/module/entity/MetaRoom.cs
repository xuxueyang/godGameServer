using System.Collections.Concurrent;
using System.Collections.Generic;
using com.xxy.entity.Util;
using com.xxy.logic.Base;
using godGameServer.logic.BattleRoom.module;
using Protocol.DTO.BattleRoomDTO;

namespace com.xxy.entity.model.BattleRoom
{
    public abstract class MetaRoom
    {
        
        //消息队列
        protected ConcurrentQueue<RoomDTO> roomDTOs = new ConcurrentQueue<RoomDTO>();
        /// <summary>
        /// 房间ID
        /// </summary>
        public string id;
        public RoomType roomType;
        public RoomMessageManaer messageManaer;
        public BattleTimeType battleTimeType = BattleTimeType._PRE_START;
        public List<RoomRole> roles = new List<RoomRole>();

        public MetaRoom(RoomType roomType, RoomMessageManaer messageManaer)
        {
            this.id = CommonUtil.getUUID();
            this.messageManaer = messageManaer;
            this.roomType = roomType;
        }
        
        //判断是否回合结束
        protected abstract bool _solve_battle_game_over();
       
        //初始化房间
        public abstract void _start_room();
        //回合结束时候调用的方法
        protected abstract void _game_over();
        
        //处理传入的信息
        protected abstract void _solve_message_dto(RoomDTO result);

        /// <summary>
        /// 进入下个回合
        /// </summary>
        protected abstract void nextTime();

        // 每一次循环房间做的逻辑处理
        public abstract void _timerLogic();
        
        public void receiveMessage(int protocol, RoomDTO battleRoomDTO)
        {
            battleRoomDTO.protocol = protocol;
            this.roomDTOs.Enqueue(battleRoomDTO);
        }
        /// <summary>
        /// 得到当前回合的角色
        /// </summary>
        /// <returns></returns>
        protected List<RoomRole> getCurrentTime()
        {
            List<RoomRole> roles = new List<RoomRole>();
            foreach (var item in this.roles)
            {
                if (item.IsMyTime) { roles.Add(item); }
            }
            return roles;
        }
        protected List<RoomRole> getNoCurrentTime()
        {
            List<RoomRole> roles = new List<RoomRole>();
            foreach (var item in this.roles)
            {
                if (!item.IsMyTime) { roles.Add(item); }
            }
            return roles;
        }
        protected RoomRole getRoomRoleById(string id)
        {
            foreach (var item in roles)
            {
                if (item.id.Equals(id))
                {
                    return item;
                }
            }
            return null;
        }
        protected List<RoomRole> getAllPlayer()
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
    }
}