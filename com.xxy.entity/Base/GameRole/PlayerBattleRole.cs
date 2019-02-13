using com.xxy.logic.Base;
using com.xxy.logic.Base.Card;
using com.xxy.logic.Base.Skill;
using System;
using System.Collections.Generic;

namespace com.xxy.entity.Base.GameRole
{
    /// <summary>
    /// 战斗时的玩家角色
    /// </summary>
    public class PlayerBattleRole: BaseRoleData,BaseRoleAction
    {
        public bool _isDead = false;
        public bool isDead()
        {
            return this._isDead;
        }
        public int GetHp()
        {
            return this.hp;
        }

        public int GetMp()
        {
            return this.mp;
        }

        public void SetHp(int hp)
        {
            Console.WriteLine("change:生命值变化,从" + this.hp + " 变化到 " + (hp < this.maxhp ? hp : this.maxmp));
            this.hp = hp < this.maxhp ? hp : this.maxmp;
            

        }

        public void SetMp(int mp)
        {
            this.mp = mp<=this.maxmp?mp:this.maxmp;
        }

        public void attack(CallBack callBack)
        {
            throw new NotImplementedException();
        }

        public void attacked(object attack, EventArgs args, CallBack callBack)
        {
            throw new NotImplementedException();
        }

        public void dead(CallBack callBack)
        {
            //TODO委托与广播，在每次操作后，注册事件
            throw new NotImplementedException();
        }

        public RoleType getRoleType()
        {
            return RoleType.PLAYER;
        }


        public List<BaseSkill> GetBaseSkills()
        {
            return this.BaseSkills;
        }

        public List<BaseCard> GetBaseCards()
        {
            return this.BaseCards;
        }

        public int GetMaxHp()
        {
            return this.maxhp;
        }

        public int GetMaxMp()
        {
            return this.maxmp;
        }


    }
}