using com.xxy.entity.Base.Buff;
using com.xxy.logic.Base;
using com.xxy.logic.Base.Card;
using com.xxy.logic.Base.Skill;
using System.Collections.Generic;

namespace com.xxy.entity.model.BattleRoom
{
    public class MetaRoomRole
    {
        public bool isDead = false;
        public string id;
        public long accontId;
        //TODO 卡牌资源管理
        public RoleType roleType;
        public List<BaseBuff> buffs = new List<BaseBuff>();
        public List<BaseCard> cardList = new List<BaseCard>();
        //TODO 技能资源管理
        public List<BaseSkill> skillList = new List<BaseSkill>();
        public BattleTimeType battleTimeType = BattleTimeType.OVER;
        protected bool isMyTime = false;
        public virtual bool IsMyTime
        {
            get { return isMyTime; }
            set
            {
                isMyTime = value;
            }
        }
        //计时没有反应的时间
        protected int timesNoAction = 0;
        //默认没有超时
        protected int _timesNoActionRoom = -1;
    }
}