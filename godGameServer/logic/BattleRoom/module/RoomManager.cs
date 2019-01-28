using com.xxy.entity.model;
using com.xxy.NetFrame;
using com.xxy.Protocol;
using godGameServer.dao.model;
using Protocol.CommandProtocol;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.xxy.entity.model.BattleRoom;
using com.xxy.logic.Base;
using godGameServer.tool;
using System.Timers;

namespace godGameServer.logic.BattleRoom.module
{

    /// <summary>
    /// 房间管理
    /// </summary>
    public class RoomManager:AbsOneHandler
    {
        Timer timer;
        public RoomManager()
        {
            Room room = RoomFactory.Instance.createDemoOneNPCRoom();
            start(room);
            timer = new Timer(2000);
            timer.Elapsed += _timer;
            timer.Start();
//            ScheduleUtil.Instance().timeSchedule(new TimeEvent(_timer),2);
        }
        
        public Dictionary<string,Room> idRoomMap = new Dictionary<string, Room>();
        Random rd = new Random();
        public Room GetRoomById(string id)
        {
            if (idRoomMap.ContainsKey(id))
                return idRoomMap[id];
            return null;
        }
        //分配匹配，和创建战斗房间
        public Tuple<bool,List<RoleModel>> GetModelsByTokens(List<UserToken> userTokens)
        {
            if (userTokens == null || userTokens.Count < 1)
            {
                return null;
            }
            List<RoleModel> arrayList = new List< RoleModel>();
            bool success = true;
            foreach (var token in userTokens)
            {
                //根据连接，得到全部的用户信息,用战斗信息初始化战斗房间
                RoleModel roleModel =  getUserModel(token);
                if (roleModel == null)
                {
                    //说明有用户在这期间掉线了。重新连接
                    success = false;
                    continue;
                }
                arrayList.Add(roleModel);
            }

            return new Tuple<bool, List<RoleModel>>(success,arrayList);
        } 
        public void createOneRoom(List<UserToken> userTokens)
        {
            Tuple<bool,List<RoleModel>>  tuple = GetModelsByTokens(userTokens);
            if(tuple.Item1)
                // 告诉这些用户，创建房间成功
                createRoom(tuple.Item2,RoomType.ONE_NPC_ROOM);
            else
            {
                // 告诉其他用户，有人掉线，创建失败！
                foreach (var roleModel in tuple.Item2)
                {
                    write(roleModel.accountId, BattleRoomProtocol.CREATE_ONE_S, new ReturnDTO(RETURN_CODE.BAALE_ROOM_CAEATE_ERROR));
                }
            }
        }

        public void createRoom(List<RoleModel> roleModels,RoomType type)
        {
            // 初始化战斗回合
            switch (type)
            {
                case RoomType.ONE_NPC_ROOM:
                    // 判断是不是一个人
                    if (roleModels.Count == 1)
                    {
                        {
                            Room room = RoomFactory.Instance.createOneRoom(roleModels[0]);
                            start(room);
                        }
                    }
                    break;
                case RoomType.DEMO_NPC:
                    {
                        Room room = RoomFactory.Instance.createDemoOneNPCRoom();
                        start(room);
                    }
                    break;
            }
        }
        public override byte GetGameType()
        {
            return TypeProtocol.TYPE_BATTLE_ROOM;
        }
        //定时处理战斗逻辑
        private void _timer(object sender, ElapsedEventArgs e)
        {
            foreach (var room in this.idRoomMap.Values)
            {
                room._timerLogic();
            }
        }
        public void start(Room room)
        {
            switch (room.roomType)
            {
                case RoomType.DEMO_NPC:
                    // 定时器执行
                    pre_start_DEMO_NPC_ROOM(room);
                    break;
            }
        }
        private void pre_start_DEMO_NPC_ROOM(Room room)
        {
            //先确定回合是谁(默认是玩家）,随机选一个NPC开始
            var npcs = room.getAllNPC();
            if (npcs.Count < 1)
                throw new Exception();
            //生成随机数
            int i = rd.Next();
            int first = i % npcs.Count;
            room.selectStartRole(npcs,new List<int>() { first });
            //TODO 预处理结束，假如逻辑队列，定时器执行
            idRoomMap.Add(room.id, room);
        }
    }
}
