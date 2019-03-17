using com.xxy.entity.Base.GameRole;
using com.xxy.logic.Base;

namespace com.xxy.entity.model.BattleRoom
{
    public class TwoPlayerRoomRole:RoomRole
    {
        public TwoPlayerRoomRole(string roomId, BaseRoleAction role, RoleType roleType) : base(roomId, role, roleType)
        {
            
        }

        public TwoPlayerRoomRole(string roomId, long accontId, BaseRoleAction role, RoleType roleType) : base(roomId, accontId, role, roleType)
        {
            
        }
    }
}