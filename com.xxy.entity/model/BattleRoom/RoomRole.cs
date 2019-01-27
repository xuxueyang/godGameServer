using System;
using System.Collections.Generic;
using System.Text;
using com.xxy.entity.Base.GameRole;
using com.xxy.logic.Base.Card;
using com.xxy.logic.Base.Skill;

namespace com.xxy.entity.model.BattleRoom
{
    /// <summary>
    /// 房间里人员信息的类
    /// </summary>
    public class RoomRole
    {
        public BaseRole role;
        //TODO 卡牌资源管理
        public List<BaseCard> cardList;
        //TODO 技能资源管理
        public List<BaseSkill> skillList;
        
        public string roomId;
        /// <summary>
        /// 默认不是自己的回合
        /// </summary>
        public bool isMyTime = false;
        public RoomRole(string roomId,BaseRole role)
        {
            this.role = role;
            this.roomId = roomId;
            //初始化房间角色信息
            
        }

        /// <summary>
        /// 定时处理战斗逻辑
        /// </summary>
        public void _solveLogic()
        {
            if(!isMyTime)
                return;
            // 能到我的回合，说明我还没有死！
            //依次执行，回合操作，战斗结束，死亡判断
            if (role.getRoleType() == RoleType.NPC)
            {
                //做一些定时任务，每次执行指令应该需要时间，或者说？立刻返回
                // NPC应该只能调用技能。
                //TODO 判断如果血量超过阶段，触发方法
                
                //TODO 从技能中随机挑选一个使用
                
                //发送回合结束的信息
                this.isMyTime = false;
                //TODO 发送NPC回合结束的触发
            }
            else if(role.getRoleType() == RoleType.PLAYER)
            {
                //等待玩家输入指令
            }
        }
    }
}
