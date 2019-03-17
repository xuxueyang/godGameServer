using System;
using System.Collections.Generic;
using System.Text;
using com.xxy.entity.Base.Buff;
using com.xxy.entity.Base.GameRole;
using com.xxy.entity.Util;
using com.xxy.logic.Base;
using com.xxy.logic.Base.Card;
using com.xxy.logic.Base.Skill;

namespace com.xxy.entity.model.BattleRoom
{
    /// <summary>
    /// 房间里人员信息的类
    /// </summary>
    public class RoomRole:MetaRoomRole
    {
        public string id;
        public BaseRoleAction role;
        public long accontId;
        //代表所处的阵营
        public int camp = 0;
        //TODO 卡牌资源管理
        public RoleType roleType;
        public List<BaseBuff> buffs = new List<BaseBuff>();
        public List<BaseCard> cardList = new List<BaseCard>();
        //TODO 技能资源管理
        public List<BaseSkill> skillList = new List<BaseSkill>();
        public BattleTimeType battleTimeType = BattleTimeType.OVER;

        //
        public bool isActive = true;
        private int timesNoAction = 0;
        public object _wantToUseCardSkill = null;

        public string roomId;
        /// <summary>
        /// 默认不是自己的回合
        /// </summary>
        private bool isMyTime = false;

        public bool IsMyTime {
            get  { return isMyTime; }
            set  {
                isMyTime = value;
                //如果我是玩家，进入我的回合
                if (isMyTime&&roleType == RoleType.PLAYER)
                {
                    //打印我能用的技能
                    foreach (var item in cardList)
                    {
                        Console.WriteLine("我拥有卡牌：" + item.Name + " 介绍:" + item.Description);
                    }
                    foreach (var item in skillList)
                    {
                        Console.WriteLine("我拥有技能：" + item.Name + " 介绍:" + item.Description + "消耗MP:"+item.needMp);
                    }
                }
            }
        }
        public RoomRole(string roomId, BaseRoleAction role, RoleType roleType)
        {
            _init(roomId, -1, role, roleType);
        }
        public RoomRole(string roomId, long accontId, BaseRoleAction role, RoleType roleType)
        {
            _init(roomId, accontId, role, roleType);
        }
        private void _init(string roomId, long accontId, BaseRoleAction role, RoleType roleType)
        {
            this.accontId = accontId;
            this.id = CommonUtil.getUUID();
            this.role = role;
            this.roomId = roomId;
            this.roleType = roleType;
            //初始化房间角色信息
            // 信息托管
            this.cardList = role.GetBaseCards();
            this.skillList = role.GetBaseSkills();
            //TODO 如果技能和卡牌为空，那么设置一个默认的技能（搏命！每次消耗10%的最大生命值，造成其同等伤害！）

            //判断buff效果
            foreach (var item in buffs)
            {
                if (item.buffType == BuffType.ONE_EFFECT)
                {
                    item.effect(this.role, this.role);
                }
            }
        }

        /// <summary>
        /// 处理每帧判断处理的逻辑
        /// </summary>
        public void _solve_each_logic()
        {

        }
        public void _solve_change_time_logic()
        {
            // 只会在开始的时候处理一次预处理逻辑
            if (this.battleTimeType == BattleTimeType.START)
            {
                Console.WriteLine("预处理" + this.id + "角色中:" + this.role.ToString());
                foreach (var item in buffs)
                {
                    if (item.buffType == BuffType.EACH_TIME_EFFECT)
                    {
                        item.effect(this.role, this.role);
                    }
                }
                // TODO 技能分为一些范围，一些技能（状态与buff，是提前设定的，有些buff是战斗中，有些是）
                // TODO 所以设定为，就在角色身上？还是进本获取？
                this._wantToUseCardSkill = null; //(抛弃队列)
                this.battleTimeType = BattleTimeType.PRE;
            }
        }
        /// <summary>
        /// 处理战斗结束的状态.玩家不能主动进入下一个状态，需要room控制
        /// </summary>
        public void _solve_over_logic()
        {
            if (!IsMyTime)
                return;
            Console.WriteLine("回合已经结束，等待结束");
        }
        /// <summary>
        /// 定时处理战斗逻辑
        /// </summary>
        public void _solve_battle_logic(List<RoomRole> targetNoCurrentTimeRoomRoles)
        {
            if(!IsMyTime||this.battleTimeType==BattleTimeType.OVER)
                return;
            // 能到我的回合，说明我还没有死！
            //依次执行，回合操作，战斗结束，死亡判断
            if (roleType == RoleType.NPC)
            {
                Console.WriteLine("处理NPC:" + this.id + "角色的战斗逻辑...");
                List<BaseRoleAction> targets = new List<BaseRoleAction>();
                //TODO需要删选，不能aoe呀
                foreach (var item in targetNoCurrentTimeRoomRoles)
                {
                    targets.Add(item.role);
                }
                //做一些定时任务，每次执行指令应该需要时间，或者说？立刻返回
                // NPC应该只能调用技能。
                //TODO 判断如果血量超过阶段，触发方法
                Console.WriteLine("" + this.id + "开始随机使用技能。。。");
                //TODO 从技能中随机挑选一个使用以及随机判定卡牌使用不使用
                if (this.cardList != null && this.cardList.Count > 0)
                {
                    Random rd = new Random();
                    int cardIndex = rd.Next()%this.cardList.Count;
                    var args = new UseCardEventArgs();
                    args.targets = targets;//TODO 设置使用对象！
                    this.cardList[cardIndex].useCard(this.role, args);
                }
                if (this.skillList != null && this.skillList.Count > 0)
                {
                    Random rd = new Random();
                    int skillIndex = rd.Next() % this.skillList.Count;
                    var args = new UseSkillEventArgs();
                    args.targets = targets;//TODO 设置使用对象！
                    this.skillList[skillIndex].useSkill(this.role, args);
                }
                //发送回合结束的信息
                //this.isMyTime = false;
                this.battleTimeType = BattleTimeType.OVER;
                Console.WriteLine("" + this.id + "随机使用技能结束。。。");
                //TODO 发送NPC回合结束的触发
            }
            else if(roleType == RoleType.PLAYER)
            {
                //等待玩家输入指令
                Console.WriteLine("等待"+ this.id +"玩家的输入");
                // var str = Console.ReadLine();
                // 说明输入了使用技能或卡牌
                if (_wantToUseCardSkill != null)
                {
                    timesNoAction = 0;
                    lock (_wantToUseCardSkill)
                    {
                        if (_wantToUseCardSkill is UseCardEventArgs)
                        {
                            var args = (UseCardEventArgs)_wantToUseCardSkill;
                            BaseCard useCard = null;
                            if (this.cardList != null && this.cardList.Count > 0)
                            {
                                foreach (var item in this.cardList)
                                {

                                    if (item.Id == args.UseCardId)
                                    {
                                        useCard = item;
                                        break;
                                    }
                                }
                                if (useCard != null)
                                {
                                    UseCardEventArgs newargs = new UseCardEventArgs();
                                    newargs.targets = args.targets;
                                    newargs.UseCardId = args.UseCardId;
                                    useCard.useCard(this.role, newargs);
                                }
                                    
                            }
                        }
                        else if (_wantToUseCardSkill is UseSkillEventArgs)
                        {
                            var args = (UseSkillEventArgs)_wantToUseCardSkill;
                            BaseSkill baseSkill = null;
                            if (this.skillList != null && this.skillList.Count > 0)
                            {
                                foreach (var item in this.skillList)
                                {

                                    if (item.Id == args.UseSkillId)
                                    {
                                        baseSkill = item;
                                        break;
                                    }
                                }
                                if (baseSkill != null)
                                {
                                    UseSkillEventArgs newargs = new UseSkillEventArgs();
                                    newargs.targets = args.targets;
                                    newargs.UseSkillId = args.UseSkillId;
                                    baseSkill.useSkill(this.role, newargs);
                                }                                  
                            }
                        }
                        //找不到的话，丢弃
                        this._wantToUseCardSkill = null;
                    }

                }
                else
                {
                    if(timesNoAction<=1000)
                        timesNoAction++;
                }
                if(timesNoAction >= 10)
                {
                    this.isActive = false;
                }
                else
                {
                    this.isActive = true;
                }
              
            }
        }
    }
}
