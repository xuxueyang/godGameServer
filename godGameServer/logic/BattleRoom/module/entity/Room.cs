﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using com.xxy.entity.Base.GameRole;
using com.xxy.entity.Util;
using com.xxy.logic.Base;
using com.xxy.logic.Base.Card;
using com.xxy.Protocol;
using com.xxy.Protocol.DTO.BattleRoomDTO;
using godGameServer.logic.BattleRoom.module;
using Protocol.CommandProtocol;
using Protocol.DTO.BattleRoomDTO;

namespace com.xxy.entity.model.BattleRoom
{
    /// <summary>
    /// 战斗房间的类
    /// </summary>
    public class Room
    {
        //消息队列
        public ConcurrentQueue<RoomDTO> roomDTOs = new ConcurrentQueue<RoomDTO>();
        /// <summary>
        /// 房间ID
        /// </summary>
        public string id;
        public RoomType roomType;
        private RoomMessageManaer messageManaer;
        public BattleTimeType battleTimeType = BattleTimeType._PRE_START;
        public List<RoomRole> roles = new List<RoomRole>();

        public Room(RoomType roomType, RoomMessageManaer messageManaer)
        {
            this.id = CommonUtil.getUUID();
            this.messageManaer = messageManaer;
            this.roomType = roomType;
        }

        /// <summary>
        /// 进行房间的战斗逻辑判断
        /// </summary>
        public void _timerLogic()
        {
            //TODO 在每阵中，判断消息队列。有的话，要做处理！
            //玩家没有回合结束的权利，只能发送自己想结束回合的请求，能不能结束等，一切，均为服务器决定
            //客户端一定是属于触发式的。
            //在客户端采用：发送请求（生成一个Key做唯一标识，但是相同的，比如回合结束时的key，却是和玩家id合并作为唯一)，等待一个请求回应,将回应的callback加入队列中，有回应就调用。
            //有成功与失败的回调（应该）
            //http socket应该有重发功能--不需要自己再实现吧

            //TODO 每一帧，仅处理一个请求(请求，影响状态，但是不改变逻辑）
            //处理看有没有特殊的逻辑
            //TODO 如果第一个逻辑不是自己需要的，那么移除。（一个消息仅仅仅仅可以呗消耗一次）
            //_checkMessageSpecialLogic();
            //如果是玩家使用什么技能这些，那就假如roomrole的处理队列中，然后委托他们自己处理！
            // 客户端传来什么key，返回也是这个key
            // kafka的源码看一下。应该就是这种生产者消费者的模型
            var result = new RoomDTO();
            var success = roomDTOs.TryDequeue(out result);
            if (success)
            {
                _solve_message_dto(result);
            }
            //需要执行所有角色的预处理状态(每帧，而不是每回合）
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
            //判断人员死亡问题：
            bool over = _solve_battle_game_over(roomType);
            if (over)
            {
                //TODO 结束游戏
                _game_over();
                return;
            }
            if (next)
            {
                this.battleTimeType = BattleTimeType.OVER;
            }
            // 回合变化以及角色使用了技能，这些特殊点，需要发送网络请求！
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

        private void _game_over()
        {
            Console.WriteLine("房间:"+ this.id +" 游戏结束");
            //TODO 
            //会在下一帧移除房间
            //结算，掉落、装备、经验等等
            this.battleTimeType = BattleTimeType._GAME_OVER;
        }

        private bool _solve_battle_game_over(RoomType roomType)
        {
            switch (roomType)
            {
                case RoomType.DEMO_NPC:
                    //有一个怪物死了，那么游戏结束

                    return false;
                case RoomType.ONE_NPC_ROOM:
                    //玩家或者NPC死了，那么结束//均按照阵营
                    //还要判断玩家是不是不在线状态
                    return false;
                default:
                    return false;
            }
        }

        private void _solve_message_dto(RoomDTO result)
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
                            var role = getRoomRoleById(result.roomRoleId);
                            role._wantToUseCardSkill = args;
                        }
                        catch(Exception e)
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
        private List<BaseRoleAction> getRolesByIds(List<string> targetIds)
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

        public void receiveMessage(int protocol, RoomDTO battleRoomDTO)
        {
            battleRoomDTO.protocol = protocol;
            this.roomDTOs.Enqueue(battleRoomDTO);
        }

        private void nextTime()
        {
            //TODO 进入下一个回合，将排除当前的角色进入下一个阶段
            // 先将全员设置为不可用，从剩下的设置为回合开始
            var nextPlayers = getNoCurrentTime();
            foreach (var item in this._currentRoles)
            {
                item.IsMyTime = false;
                Console.WriteLine(""+item.id+" 的回合结束啦");

            }
            Console.WriteLine("好啦。回合结束，让我们看看对面怎么操作的吧！~\n\n");
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
                if (item.IsMyTime) { roles.Add(item); }
            }
            return roles;
        }
        private List<RoomRole> getNoCurrentTime()
        {
            List<RoomRole> roles = new List<RoomRole>();
            foreach (var item in this.roles)
            {
                if (!item.IsMyTime) { roles.Add(item); }
            }
            return roles;
        }
        public RoomRole getRoomRoleById(string id)
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
            if(battleTimeType == BattleTimeType._PRE_START)
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
            this._currentRoles = players;
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
            dto.map.Add(CommonFieldProtocol.ids,ids);
            foreach (var item in getAllPlayer())
            {
                _write(item.accontId, BattleRoomProtocol.OVER_TIME_S, new ReturnDTO(RETURN_CODE.SUCCESS, dto));
            }
        }

        public void _write(long accontId, int commend, ReturnDTO returnDTO)
        {
            messageManaer.write(accontId, commend, returnDTO);
        }
    }
}