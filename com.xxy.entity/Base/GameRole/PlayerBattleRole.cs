using System;

namespace com.xxy.entity.Base.GameRole
{
    /// <summary>
    /// 战斗时的玩家角色
    /// </summary>
    public class PlayerBattleRole:BaseRole
    {
        public bool _isDead = false;
        public bool isDead()
        {
            return this._isDead;
        }
        public int GetHp()
        {
            throw new NotImplementedException();
        }

        public int GetMp()
        {
            throw new NotImplementedException();
        }

        public void SetHp(int hp)
        {
            throw new NotImplementedException();
        }

        public void SetMp(int mp)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public RoleType getRoleType()
        {
            throw new NotImplementedException();
        }
    }
}