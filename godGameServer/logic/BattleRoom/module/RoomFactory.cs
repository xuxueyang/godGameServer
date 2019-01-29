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
        /// 创建一个NPC2个战斗的demo房间
        /// </summary>
        /// <returns></returns>
        public Room createDemoTwoNPCRoom()
        {
            //创建一个房间，需要其中的人员
            Room room = new Room(RoomType.DEMO_NPC);
            List<RoomRole> roles = new List<RoomRole>();
            roles.Add(RoomRoleFactory.Instance.getDemoSmallBoss());
            roles.Add(RoomRoleFactory.Instance.getDemoSmallBoss());
            room.roles = roles;
            return room;
        }
        public Room createDemoOneNPCRoom()
        {
            //创建一个房间，需要其中的人员
            RoleModel roleModel = new RoleModel("测试玩家",1,1);
            roleModel.gameModel.baseRole.SetHp(200);
            roleModel.gameModel.baseRole.SetMp(200);
            //TODO 配置技能和卡牌
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