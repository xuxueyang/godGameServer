using com.xxy.logic.Base.Card;
using com.xxy.logic.Base.Skill;
using System;
using System.Collections.Generic;
using System.Text;

namespace com.xxy.entity.Base.GameRole
{
    public class BaseRoleData
    {
        //含有HPMP这些属性
        protected int maxhp = 100;
        protected int maxmp = 200;
        protected int hp = 100;
        protected int mp = 200;
        protected List<BaseCard> BaseCards = new List<BaseCard>();
        protected List<BaseSkill> BaseSkills = new List<BaseSkill>();
    }
}
