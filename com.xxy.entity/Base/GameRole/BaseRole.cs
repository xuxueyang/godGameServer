using System;
using System.Collections.Generic;
using System.Text;

namespace com.xxy.entity.Base.GameRole
{
    public delegate void CallBack(object sender,EventArgs args);

    public enum RoleType
    {
        NPC,//npc的角色，电脑控制
        PLAYER//玩家
    }
    /// <summary>
    /// 基础的角色类，所以（包含怪物，都应该继承此类）
    /// 血量、蓝量、动作组件（被攻击），攻击，这些、
    /// </summary>
    public  interface  BaseRole
    {
        bool isDead();
        int GetHp();
        int GetMp();
        void SetHp(int hp);
        void SetMp(int mp);
        /// <summary>
        /// 攻击的实现
        /// </summary>
        void attack(CallBack callBack);

        /// <summary>
        /// 被攻击的实现
        /// </summary>
        void attacked(object attack,EventArgs args,CallBack callBack);

        /// <summary>
        /// 死亡的触发
        /// </summary>
        void dead(CallBack callBack);
        RoleType getRoleType();
    }
}
