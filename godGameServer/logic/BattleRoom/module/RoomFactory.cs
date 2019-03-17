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
            List<RoomRole> roles = new List<RoomRole>();
            var role1 = RoomRoleFactory.Instance.getDemoSmallBoss();
            role1.camp = 1;
            roles.Add(role1);
            var role2 = RoomRoleFactory.Instance.getDemoSmallBoss();
            role2.camp = 2;
            roles.Add(role2);
//            room.roles = roles;
           
            Room room = new Room(RoomType.DEMO_NPC,new RoomMessageManaer(),roles);
            return room;
        }
        private Room createDemoOneNPCRoom()
        {
            //����һ�����䣬��Ҫ���е���Ա
            RoleModel roleModel = new RoleModel("�������",1,1);
            roleModel.gameModel.baseRole.SetHp(200);
            roleModel.gameModel.baseRole.SetMp(200);
            //TODO ���ü��ܺͿ���
            List<RoomRole> roles = new List<RoomRole>();
            roles.Add(RoomRoleFactory.Instance.getDemoSmallBoss());
            roles.Add(RoomRoleFactory.Instance.createPlayerRoleByModel(roleModel));
            
            Room room = new Room(RoomType.ONE_NPC_ROOM, new RoomMessageManaer(),roles);

//            room.roles = roles;
            return room;
        }
        public Room createOneRoom(RoleModel roleModel)
        {
            List<RoomRole> roles = new List<RoomRole>();
            var role1 = RoomRoleFactory.Instance.getDemoSmallBoss();
            role1.camp = 1;
            roles.Add(role1);
            var role2 = RoomRoleFactory.Instance.createPlayerRoleByModel(roleModel);
            role2.camp = 2;
            roles.Add(role2);
//            room.roles = roles;
            Room room = new Room(RoomType.ONE_NPC_ROOM, new RoomMessageManaer(),roles);

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