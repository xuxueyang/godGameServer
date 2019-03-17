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
using Protocol.DTO.BattleRoomDTO;
using com.xxy.logic.Base.Card;

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
//            Room room = RoomFactory.Instance.createDemoTwoNPCRoom();
//            start(room);
            timer = new Timer(2000);
            timer.Elapsed += _timer;
            timer.Start();
//            ScheduleUtil.Instance().timeSchedule(new TimeEvent(_timer),2);
        }
        
        public Dictionary<string,MetaRoom> idRoomMap = new Dictionary<string, MetaRoom>();
//        Random rd = new Random();
        public MetaRoom GetRoomById(string id)
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

        public void createTwoRoom(RoleModel playerOne, RoleModel playerTwo)
        {
            var twoPlayerRoom = RoomFactory.Instance.createTwoPlayerRoom(playerOne,playerTwo);
            // 消息通知创建成功
            write(playerOne.accountId,BattleRoomProtocol.CREATE_TWO_S);
            write(playerTwo.accountId,BattleRoomProtocol.CREATE_TWO_S);
            start(twoPlayerRoom);
        }
        public void createOneRoom(List<UserToken> userTokens)
        {
            Tuple<bool,List<RoleModel>>  tuple = GetModelsByTokens(userTokens);
            if (tuple.Item1)
            {
                // 告诉这些用户，创建房间成功
                createRoom(tuple.Item2, RoomType.ONE_NPC_ROOM);
                //TODO
                //write(tuple.Item2[0].accountId, BattleRoomProtocol.CREATE_ONE_S, new ReturnDTO(RETURN_CODE.SUCCESS));
            }

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
                        //创建成功，发送消息
                        Console.WriteLine(""+ roleModels[0].accountId +" 的玩家创建成功");
                        write(roleModels[0].accountId, BattleRoomProtocol.CREATE_ONE_S);
                    }
                    else
                    {
                        //错误创建失败，发送消息
                        Console.WriteLine("" + roleModels[0].accountId + " 的玩家创建失败，因为有多个");

                    }
                    break;
                //case RoomType.DEMO_NPC:
                //    {
                //        Room room = RoomFactory.Instance.createDemoOneNPCRoom();
                //        start(room);
                //    }
                //    break;
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
                if(room.battleTimeType == BattleTimeType._GAME_OVER)
                {
                    idRoomMap.Remove(room.id);
                }
                else
                {
                    try
                    {
                        room._timerLogic();
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine(exception);                 
                    }
                }            
            }
        }
        public void start(MetaRoom room)
        {
            room._start_room();
            idRoomMap.Add(room.id,room);
        }
    }
}
