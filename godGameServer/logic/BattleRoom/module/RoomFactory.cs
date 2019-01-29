using System.Collections.Generic;
using com.xxy.entity.Base.GameRole;
using com.xxy.entity.Base.GameRole.NPCRole;
using com.xxy.entity.model.BattleRoom;
using com.xxy.logic.Base;
using godGameServer.dao.model;

namespace godGameServer.logic.BattleRoom.module
{
    public class RoomFactory
    {
        /// <summary>
        /// ����һ��NPC2��ս����demo����
        /// </summary>
        /// <returns></returns>
        public Room createDemoTwoNPCRoom()
        {
            //����һ�����䣬��Ҫ���е���Ա
            Room room = new Room(RoomType.DEMO_NPC);
            List<RoomRole> roles = new List<RoomRole>();
            roles.Add(RoomRoleFactory.Instance.getDemoSmallBoss());
            roles.Add(RoomRoleFactory.Instance.getDemoSmallBoss());
            room.roles = roles;
            return room;
        }
        public Room createDemoOneNPCRoom()
        {
            //����һ�����䣬��Ҫ���е���Ա
            RoleModel roleModel = new RoleModel("�������",1,1);
            roleModel.gameModel.baseRole.SetHp(200);
            roleModel.gameModel.baseRole.SetMp(200);
            //TODO ���ü��ܺͿ���
            Room room = new Room(RoomType.ONE_NPC_ROOM);
            List<RoomRole> roles = new List<RoomRole>();
            roles.Add(RoomRoleFactory.Instance.getDemoSmallBoss());
            roles.Add(RoomRoleFactory.Instance.createPlayerRoleByModel(roleModel));
            room.roles = roles;
            return room;
        }
        public Room createOneRoom(RoleModel roleModel)
        {
            Room room = new Room(RoomType.ONE_NPC_ROOM);
            List<RoomRole> roles = room.roles;
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