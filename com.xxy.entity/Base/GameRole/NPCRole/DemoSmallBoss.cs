using System;
using System.Collections.Generic;
using com.xxy.logic.Base;
using com.xxy.logic.Base.Card;
using com.xxy.logic.Base.Skill;

namespace com.xxy.entity.Base.GameRole.NPCRole
{
    /// <summary>
    /// 一个小的怪物的角色
    /// </summary>
    public class DemoSmallBoss: BaseRoleData,BaseRoleAction
    {
        public DemoSmallBoss()
        {
            this.hp = 100;
            this.mp = 200;
        }
        public bool _isDead = false;
        public bool isDead()
        {
            return this._isDead;
        }

        public int GetHp()
        {
            return hp;
        }

        public int GetMp()
        {
            return mp;
        }

        public void SetHp(int hp)
        {
            this.hp = hp;
            if (this.hp < 1)
            {
                this.dead(null);
            }
        }

        public void SetMp(int mp)
        {
            //不做修改
            
        }

        public void attack(CallBack callBack)
        {
        }

        public void attacked(object attack, EventArgs args, CallBack callBack)
        {
        }

        public void dead(CallBack callBack)
        {
            this._isDead = true;
            if (callBack != null)
            {
                callBack(this,new EventArgs());
            }
        }

        public RoleType getRoleType()
        {
            return RoleType.NPC;
        }

        public void UseCard()
        {
        }

        public void UseSkill()
        {
            
        }

        public List<BaseSkill> GetBaseSkills()
        {
            return this.BaseSkills;
        }

        public List<BaseCard> GetBaseCards()
        {
            return this.BaseCards;
        }
    }
}