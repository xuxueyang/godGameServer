using System;
using System.Collections.Generic;
using System.Text;
using com.xxy.entity.Base.GameRole;
using com.xxy.logic.Base.Card;
using com.xxy.logic.Base.Skill;

namespace com.xxy.entity.model
{
    public class GameModel
    {
        public long roleId;
        public long accountId;
        public BaseRoleAction baseRole;
        public object BattleCardConfManager;//战斗卡牌配置——多个列表，有一个当前配置
        public object SkillConfManager;//技能配置——多个列表，有一个当前配置
        public object PackageManager;//背包管理//public List<object> PackageResourceList;//资源列表（拥有的物体)（背包）
        public object StoreManager;//仓库管理//public List<object> StoreResourceList; //仓库资源列表
        public object FeatManager;//成就列表
        public object CollectManager;//搜集的卡牌列表

        public GameModel(long accountId, long roleId)
        {
            this.roleId = roleId;
            this.accountId = accountId;
            //TODO 初始化一个游戏角色模型！
            this.baseRole = new PlayerBattleRole();
        }
        public List<BaseSkill> getBattleConfigSkills()
        {
            return null;//this.SkillConfManager.getBattleConfigSkills();//得到配置的技能列表，最多3个
        }
        public List<BaseCard> getBattleCardConfigList()
        {
            return null;//this.BattleCardConfManager.getBattleCardConfigList();//得到战斗配置的卡牌数目，最多10个，A级以上只能包含1张。每张卡牌不能2张以上。
            //校验，每张卡牌不能2张以上，如果不足10张，那么加入到消耗1岁造成一倍最大物理伤害或者魔法伤害的卡牌了。
        }
    }
}
