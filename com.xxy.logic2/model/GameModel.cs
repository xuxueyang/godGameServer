using System;
using System.Collections.Generic;
using System.Text;
using com.xxy.entity.Base.GameRole;

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
        }
    }
}
