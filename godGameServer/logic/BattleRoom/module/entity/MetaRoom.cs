using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using com.xxy.entity.Base.GameRole;
using com.xxy.entity.Util;
using com.xxy.logic.Base;
using com.xxy.logic.Base.Card;
using com.xxy.Protocol;
using godGameServer.logic.BattleRoom.module;
using Protocol.CommandProtocol;
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
        protected RoomMessageManaer messageManaer;
        public BattleTimeType battleTimeType = BattleTimeType._PRE_START;
        protected List<RoomRole> roles = new List<RoomRole>();
        //当前回合角色
        protected List<RoomRole> __currentRoles;

        public MetaRoom(RoomType roomType, RoomMessageManaer messageManaer,List<RoomRole> roles)
        {
            this.id = CommonUtil.getUUID();
            this.messageManaer = messageManaer;
            this.roomType = roomType;
            this.roles = roles;
        }

        /// <summary>
        /// 判断是否回合结束
        /// </summary>
        /// <returns></returns>
        protected abstract bool _solve_battle_game_over();

        /// <summary>
        /// 初始化房间
        /// </summary>
        public abstract void _start_room();

        /// <summary>
        /// //回合结束时候调用的方法
        /// </summary>
        protected abstract void _game_over();
        
//        //处理传入的信息
//        protected abstract void _solve_message_dto(RoomDTO result);

        /// <summary>
        /// 进入下个回合
        /// </summary>
        protected abstract void _nextTime();


        /// <summary>
        /// // 每一次循环房间做的逻辑处理
        /// </summary>
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
                if (item.roleType == RoleType.PLAYER) { roles.Add(item); }
            }
            return roles;
        }
        public List<RoomRole> getAllNPC()
        {
            List<RoomRole> roles = new List<RoomRole>();
            foreach (var item in this.roles)
            {
                if (item.roleType == RoleType.NPC) { roles.Add(item); }
            }
            return roles;
        }
        protected void _write(long accontId, int commend, ReturnDTO returnDTO)
        {
            messageManaer.write(accontId, commend, returnDTO);
        }

        protected void __brocast_role_skills_cards()
        {
            //通知所有的玩家，回合开始,以及那些人回合开始
            RoomDTO dto = new RoomDTO();
            List<string> ids = new List<string>();
            //TODO 设置回合开始的人
            dto.roomId = id;
            dto.map.Add(CommonFieldProtocol.ids, ids);
            //TODO 发送对应玩家的对应信息和自己的信息
            //自己的：自己的ROOM_ID和ROOM_ROLE_ID、初始化的技能ID、卡牌ID、这些用于链接信息的
            //别人的：ROOM_ID，角色模型ID，角色模型图片ID，没了。（不应该包含技能吧？）
            List<RoomInfoDTO> list = new List<RoomInfoDTO>();
            //boss采用固定编码，roomId都采用统一制作
            foreach (var item in roles)
            {
                RoomInfoDTO infoDTO = new RoomInfoDTO();
                infoDTO.roomId = id;
                infoDTO.roomRoleId = item.id;
                infoDTO.cardIds = new List<int>();
                infoDTO.skillIds = new List<int>();
                infoDTO.cards = new List<BaseCardDTO>();
                infoDTO.skills = new List<BaseSkillDTO>();
                foreach (var cardId in item.cardList)
                {
                    infoDTO.cardIds.Add(cardId.Id);
                    BaseCardDTO baseCardDTO = new BaseCardDTO();
                    baseCardDTO.id = cardId.Id;
                    baseCardDTO.name = cardId.Name;
                    baseCardDTO.description = cardId.Description;
                    infoDTO.cards.Add(baseCardDTO);
                }
                foreach (var skillId in item.skillList)
                {
                    infoDTO.skillIds.Add(skillId.Id);
                    BaseSkillDTO baseSkillDTO = new BaseSkillDTO();
                    baseSkillDTO.id = skillId.Id;
                    baseSkillDTO.name = skillId.Name;
                    baseSkillDTO.description = skillId.Description;
                    infoDTO.skills.Add(baseSkillDTO);
                }
                list.Add(infoDTO);
            }
            foreach (var item in getAllPlayer())
            {
                dto.roomRoleId = item.id;
                List<RoomInfoDTO> selfInfoDTOs = new List<RoomInfoDTO>();
                List<RoomInfoDTO> otherInfoDTOs = new List<RoomInfoDTO>();
                foreach (var infoDTO in list)
                {
                    if (infoDTO.roomRoleId == item.id)
                    {
                        selfInfoDTOs.Add(infoDTO);
                    }
                    else
                    {
                        otherInfoDTOs.Add(infoDTO);
                    }
                }
                dto.map.Add(CommonFieldProtocol.battleRoomBaseSelfInfo, selfInfoDTOs);
                dto.map.Add(CommonFieldProtocol.battleRoomBaseOtherInfo, otherInfoDTOs);
                _write(item.accontId, BattleRoomProtocol.START_TIME_S, new ReturnDTO(RETURN_CODE.SUCCESS, dto));
            }
        }

        protected void __selectStartRole(List<RoomRole> roles, List<int> index)
        {
            if (battleTimeType == BattleTimeType._PRE_START)
            {
                throw new Exception("房间未初始化！");
            }
            foreach (var first in index)
            {
                roles[first].IsMyTime = true;
                Console.WriteLine("" + roles[first].id + " 的回合开始啦");
            }
            // 同时将这些状态设置为Battle
            var players = getCurrentTime();
            foreach (var item in players)
            {
                item.battleTimeType = BattleTimeType.START;
            }
            this.battleTimeType = BattleTimeType.START;
            this.__currentRoles = players;
            //处理一下换回合的逻辑
            foreach (var item in players)
            {
                item._solve_change_time_logic();
            }
            //输出当前所有角色的状态
            List<string> ids = new List<string>();
            foreach (var item in players)
            {
                ids.Add(item.id);
                Console.WriteLine("角色:" + item.id + "的血量目前是:" + item.role.GetHp() + " " + "MP目前是:" + item.role.GetMp());
            }
            // 通知 房间里所有的角色，切换到哪些人的回合了（！逻辑是在服务器处理，客户端仅仅做显示，非逻辑判断和接受传输操作API）
            RoomDTO dto = new RoomDTO();
            dto.map.Add(CommonFieldProtocol.ids, ids);
            foreach (var item in getAllPlayer())
            {
                _write(item.accontId, BattleRoomProtocol.OVER_TIME_S, new ReturnDTO(RETURN_CODE.SUCCESS, dto));
            }
        }

        protected void __solve_message_dto(RoomDTO result)
        {
            //收到信息的处理
            //设置room的_wantToUseCardSkill，作为消费
            switch (result.protocol)
            {
                case BattleRoomProtocol.USE_CARD_C:
                    //玩家使用来技能，针对那个对象等
                    {
                        Console.WriteLine("玩家房间角色：" + result.roomRoleId + "使用了卡牌");
                        try
                        {
                            UseCardEventArgs args = new UseCardEventArgs();
                            args.UseCardId = (int)result.map[CommonFieldProtocol.useCardId];
                            var targetIds = (List<string>)result.map[CommonFieldProtocol.targetIds];
                            args.targets = getRolesByIds(targetIds);
                            args.UseRoleId = result.roomRoleId;
                            var role = getRoomRoleById(result.roomRoleId);
                            role._wantToUseCardSkill = args;
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }
                    break;
                case BattleRoomProtocol.USE_SKILL_C:
                    //玩家使用来技能，针对某个对象
                    {
                        Console.WriteLine("玩家房间角色：" + result.roomRoleId + "使用了技能");
                        try
                        {
                            UseSkillEventArgs args = new UseSkillEventArgs();
                            args.UseSkillId = (int)result.map[CommonFieldProtocol.useSkillId];
                            var targetIds = (List<string>)result.map[CommonFieldProtocol.targetIds];
                            args.targets = getRolesByIds(targetIds);
                            args.UseRoleId = result.roomRoleId;
                            var role = getRoomRoleById(result.roomRoleId);
                            role._wantToUseCardSkill = args;
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }
                    break;
                case BattleRoomProtocol.OVER_TIME_C:
                    ///TODO 结束战斗回合
                    {
                        if (result.map.ContainsKey(CommonFieldProtocol.id))
                        {
                            var id = result.map[CommonFieldProtocol.id].ToString();
                            var role = getRoomRoleById(id);
                            if (role != null)
                            {
                                Console.WriteLine("" + id + "的角色结束了自己的回合！");
                                role.IsMyTime = false;
                            }
                        }
                    }
                    break;
            }
        }
        protected List<BaseRoleAction> getRolesByIds(List<string> targetIds)
        {
            List<BaseRoleAction> list = new List<BaseRoleAction>();
            foreach (var targetItem in targetIds)
            {
                foreach (var roleItem in this.roles)
                {
                    if (targetItem.Equals(roleItem.id))
                    {
                        list.Add(roleItem.role);
                    }
                }
            }
            return list;
        }
    }
}