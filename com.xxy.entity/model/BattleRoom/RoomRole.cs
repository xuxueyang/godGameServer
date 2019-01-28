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
    public class RoomRole
    {
        public string id;
        public BaseRoleAction role;
        //TODO 卡牌资源管理
        public RoleType roleType;
        public List<BaseBuff> buffs = new List<BaseBuff>();
        public List<BaseCard> cardList = new List<BaseCard>();
        //TODO 技能资源管理
        public List<BaseSkill> skillList = new List<BaseSkill>();
        public BattleTimeType battleTimeType = BattleTimeType.OVER;
        public string roomId;
        /// <summary>
        /// 默认不是自己的回合
        /// </summary>
        public bool isMyTime = false;
        public RoomRole(string roomId,BaseRoleAction role,RoleType roleType)
        {
            this.id = CommonUtil.getUUID();
            this.role = role;
            this.roomId = roomId;
            this.roleType = roleType;
            //初始化房间角色信息
            // 信息托管
            this.cardList = role.GetBaseCards();
            this.skillList = role.GetBaseSkills();
            //判断buff效果
            foreach (var item in buffs)
            {
                if(item.buffType == BuffType.ONE_EFFECT)
                {
                    item.effect(this.role, this.role);
                }
            }
        }
        /// <summary>
        /// 处理每次判断处理的逻辑
        /// </summary>
        public void _solve_each_logic()
        {
            // 只会在开始的时候处理一次预处理逻辑
            if (this.battleTimeType == BattleTimeType.START)
            {
                Console.WriteLine("预处理" + this.id + "角色中:" + this.role.ToString());
                foreach (var item in buffs)
                {
                    if(item.buffType == BuffType.EACH_TIME_EFFECT)
                    {
                        item.effect(this.role, this.role);
                    }
                }
                // TODO 技能分为一些范围，一些技能（状态与buff，是提前设定的，有些buff是战斗中，有些是）
                // TODO 所以设定为，就在角色身上？还是进本获取？
                this.battleTimeType = BattleTimeType.PRE;
            }
        }
        /// <summary>
        /// 处理战斗结束的状态.玩家不能主动进入下一个状态，需要room控制
        /// </summary>
        public void _solve_over_logic()
        {
            if (!isMyTime)
                return;
            Console.WriteLine("回合已经结束，等待结束");
        }
        /// <summary>
        /// 定时处理战斗逻辑
        /// </summary>
        public void _solve_battle_logic(List<RoomRole> targetRoomRoles)
        {
            if(!isMyTime||this.battleTimeType==BattleTimeType.OVER)
                return;
            // 能到我的回合，说明我还没有死！
            //依次执行，回合操作，战斗结束，死亡判断
            if (roleType == RoleType.NPC)
            {
                Console.WriteLine("处理" + this.id + "角色的战斗逻辑...");
                List<BaseRoleAction> targets = new List<BaseRoleAction>();
                foreach (var item in targetRoomRoles)
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
                Console.WriteLine("等待玩家的输入");
            }
        }
    }
}
