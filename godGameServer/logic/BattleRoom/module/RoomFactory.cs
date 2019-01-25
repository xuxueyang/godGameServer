using System.Collections.Generic;
using com.xxy.entity.Base.GameRole;
using com.xxy.entity.Base.GameRole.NPCRole;
using com.xxy.entity.model.BattleRoom;
using godGameServer.dao.model;

namespace godGameServer.logic.BattleRoom.module
{
    public class RoomFactory
    {
        public Room createOneRoom(RoleModel roleModel)
        {
            Room room = new Room();
            List<RoomRole> roles = new List<RoomRole>();
            roles.Add(RoomRoleFactory.Instance.getDemoSmallBoss());
            roles.Add(RoomRoleFactory.Instance.createPlayerRoleByModel(roleModel));
            room.roles = roles;
            return room;
        }
        
        private  static  RoomFactory _Instance;

        public static RoomFactory Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new RoomFactory();
                }
                return _Instance;
            }
        }
    }
}