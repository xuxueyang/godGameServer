using com.xxy.entity.Base.GameRole;
using com.xxy.entity.Base.GameRole.NPCRole;
using com.xxy.entity.model.BattleRoom;
using com.xxy.entity.Util;
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
            return  new RoomRole(CommonUtil.getUUID(),new DemoSmallBoss());
        }

        public RoomRole createPlayerRoleByModel(RoleModel roleModel)
        {
            PlayerBattleRole role = new PlayerBattleRole();
            role.SetHp(roleModel.gameModel.baseRole.GetHp());
            role.SetMp(roleModel.gameModel.baseRole.GetMp());
            
            return new RoomRole(CommonUtil.getUUID(), role);
        }
    }
}