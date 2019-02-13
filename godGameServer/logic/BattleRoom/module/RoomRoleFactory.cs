using com.xxy.entity.Base.GameRole;
using com.xxy.entity.Base.GameRole.NPCRole;
using com.xxy.entity.model.BattleRoom;
using com.xxy.entity.Util;
using com.xxy.logic.Base;
using com.xxy.logic.Base.Card.demo;
using com.xxy.logic.Base.Skill.demo;
using godGameServer.dao.model;

namespace godGameServer.logic.BattleRoom.module
{
    public class RoomRoleFactory
    {
        private RoomRoleFactory()
        {
            
        }

        private static RoomRoleFactory _Instance;
        public static RoomRoleFactory Instance
        {
            get
            {
                if(_Instance==null)
                    _Instance = new RoomRoleFactory();
                return _Instance;
            }
        }

        public RoomRole getDemoSmallBoss()
        {
            //创建boss，需要初始化他有的技能和卡牌，以及生命值
            DemoSmallBoss role =  new DemoSmallBoss();
            role.GetBaseCards().Add(DemoCardFactory.Instance.getDamangeCard(30));
            role.GetBaseCards().Add(DemoCardFactory.Instance.getRecoverCard(20));
            role.GetBaseSkills().Add(DemoSkillFactory.Instance.getDamangeSkill(30));
            role.GetBaseSkills().Add(DemoSkillFactory.Instance.getRecoverSkill(20));
            return  new RoomRole(CommonUtil.getUUID(), role, RoleType.NPC);
        }

        public RoomRole createPlayerRoleByModel(RoleModel roleModel)
        {
            PlayerBattleRole role = new PlayerBattleRole();
            role.SetHp(roleModel.gameModel.baseRole.GetHp());
            role.SetMp(roleModel.gameModel.baseRole.GetMp());
            role.GetBaseCards().Add(DemoCardFactory.Instance.getDamangeCard(30));
            role.GetBaseCards().Add(DemoCardFactory.Instance.getRecoverCard(20));
            role.GetBaseSkills().Add(DemoSkillFactory.Instance.getDamangeSkill(30));
            role.GetBaseSkills().Add(DemoSkillFactory.Instance.getRecoverSkill(20));
            return new RoomRole(CommonUtil.getUUID(), roleModel.accountId,role, RoleType.PLAYER);
        }
    }
}