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
                    room._timerLogic();
                }            
            }
        }
        public void start(MetaRoom room)
        {
//            switch (room.roomType)
//            {
//                case RoomType.DEMO_NPC:
//                    // 定时器执行
//                    pre_start_DEMO_NPC_ROOM(room);
//                    break;
//                case RoomType.ONE_NPC_ROOM:
//                    pre_start_ONE_NPC_ROOM(room);
//                    break;
//            }
            room._start_room();
            idRoomMap.Add(room.id,room);
        }

//        private void pre_start_ONE_NPC_ROOM(Room room)
//        {
//            //将玩家设置为回合开始，同时，通知一下room的用户们回合开始了！
//            _pre_one_start(room);
//            var players = room.getAllPlayer();
//            room.battleTimeType = BattleTimeType.START;
//            room.selectStartRole(players, new List<int>() { 0 });
//            
//            idRoomMap.Add(room.id, room);
//        }
//
//        private void _pre_one_start(Room room)
//        {
//            //通知所有的玩家，回合开始,以及那些人回合开始
//            RoomDTO dto = new RoomDTO();
//            List<string> ids = new List<string>();
//            //TODO 设置回合开始的人
//            dto.roomId = room.id;
//            dto.map.Add(CommonFieldProtocol.ids, ids);
//            //TODO 发送对应玩家的对应信息和自己的信息
//            //自己的：自己的ROOM_ID和ROOM_ROLE_ID、初始化的技能ID、卡牌ID、这些用于链接信息的
//            //别人的：ROOM_ID，角色模型ID，角色模型图片ID，没了。（不应该包含技能吧？）
//            List<RoomInfoDTO> list = new List<RoomInfoDTO>();
//            //boss采用固定编码，roomId都采用统一制作
//            foreach(var item in room.roles)
//            {
//                RoomInfoDTO infoDTO = new RoomInfoDTO();
//                infoDTO.roomId = room.id;
//                infoDTO.roomRoleId = item.id;
//                infoDTO.cardIds = new List<int>();
//                infoDTO.skillIds = new List<int>();
//                infoDTO.cards = new List<BaseCardDTO>();
//                infoDTO.skills = new List<BaseSkillDTO>();
//                foreach (var cardId in item.cardList)
//                {
//                    infoDTO.cardIds.Add(cardId.Id);
//                    BaseCardDTO baseCardDTO = new BaseCardDTO();
//                    baseCardDTO.id = cardId.Id;
//                    baseCardDTO.name = cardId.Name;
//                    baseCardDTO.description = cardId.Description;
//                    infoDTO.cards.Add(baseCardDTO);
//                }
//                foreach (var skillId in item.skillList)
//                {
//                    infoDTO.skillIds.Add(skillId.Id);
//                    BaseSkillDTO baseSkillDTO = new BaseSkillDTO();
//                    baseSkillDTO.id = skillId.Id;
//                    baseSkillDTO.name = skillId.Name;
//                    baseSkillDTO.description = skillId.Description;
//                    infoDTO.skills.Add(baseSkillDTO);
//                }
//                list.Add(infoDTO);
//            }
//            foreach (var item in room.getAllPlayer())
//            {
//                dto.roomRoleId = item.id;
//                List<RoomInfoDTO> selfInfoDTOs = new List<RoomInfoDTO>();
//                List<RoomInfoDTO> otherInfoDTOs = new List<RoomInfoDTO>();
//                foreach (var infoDTO in list)
//                {
//                    if(infoDTO.roomRoleId == item.id)
//                    {
//                        selfInfoDTOs.Add(infoDTO);
//                    }
//                    else
//                    {
//                        otherInfoDTOs.Add(infoDTO);
//                    }
//                }
//                dto.map.Add(CommonFieldProtocol.battleRoomBaseSelfInfo, selfInfoDTOs);
//                dto.map.Add(CommonFieldProtocol.battleRoomBaseOtherInfo, otherInfoDTOs);
//                room._write(item.accontId, BattleRoomProtocol.START_TIME_S, new ReturnDTO(RETURN_CODE.SUCCESS, dto));
//            }
//
//            //this.battleTimeType = BattleTimeType.START;
//            room.battleTimeType = BattleTimeType.START;
//        }
//
//        private void pre_start_DEMO_NPC_ROOM(Room room)
//        {
//            //先确定回合是谁(默认是玩家）,随机选一个NPC开始
//            var npcs = room.getAllNPC();
//            if (npcs.Count < 1)
//                throw new Exception();
//            //生成随机数
//            int i = rd.Next();
//            int first = i % npcs.Count;
//            // 因为预处理在这边解决了
//            room.battleTimeType = BattleTimeType.START;
//            room.selectStartRole(npcs,new List<int>() { first });
//            //TODO 预处理结束，假如逻辑队列，定时器执行
//            idRoomMap.Add(room.id, room);
//        }
    }
}
