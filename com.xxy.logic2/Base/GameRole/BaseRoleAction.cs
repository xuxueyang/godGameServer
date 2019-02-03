using com.xxy.logic.Base.Card;
using com.xxy.logic.Base.Skill;
using System;
using System.Collections.Generic;
using System.Text;

namespace com.xxy.entity.Base.GameRole
{

    /// <summary>
    /// 基础的战斗角色类，所以（包含怪物，都应该继承此类）
    /// 血量、蓝量、动作组件（被攻击），攻击，这些、
    /// </summary>
    public  interface  BaseRoleAction
    {
        bool isDead();
        int GetHp();
        int GetMaxHp();
        int GetMaxMp();
        int GetMp();
        void SetHp(int hp);
        void SetMp(int mp);
        void GetRoomId();
        /// <summary>
        /// 攻击的实现
        /// </summary>
        void attack(CallBack callBack);
        void UseCard();
        void UseSkill();
        List<BaseSkill> GetBaseSkills();
        List<BaseCard> GetBaseCards();
        /// <summary>
        /// 被攻击的实现
        /// </summary>
        void attacked(object attack,EventArgs args,CallBack callBack);

        /// <summary>
        /// 死亡的触发
        /// </summary>
        void dead(CallBack callBack);
        // RoleType getRoleType();
    }
}
