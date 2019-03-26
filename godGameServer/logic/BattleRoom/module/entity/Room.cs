using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using com.xxy.entity.Base.GameRole;
using com.xxy.entity.Util;
using com.xxy.logic.Base;
using com.xxy.logic.Base.Card;
using com.xxy.Protocol;
using com.xxy.Protocol.DTO.BattleRoomDTO;
using godGameServer.logic.BattleRoom.module;
using Protocol.CommandProtocol;
using Protocol.DTO.BattleRoomDTO;

namespace com.xxy.entity.model.BattleRoom
{
    /// <summary>
    /// 战斗房间的类
    /// </summary>
    public class Room: MetaRoom
    {
        public Room(RoomType roomType, RoomMessageManaer messageManaer,List<RoomRole> roles)
            : base(roomType, messageManaer,roles)
        {
            
        }


        /// <summary>
        /// 进行房间的战斗逻辑判断
        /// </summary>
        public override void _timerLogic()
        {
            //判断人员死亡问题：
            bool over = _solve_battle_game_over();
            if (over)
            {
                //TODO 结束游戏
                _game_over();
                return;
            }
            //TODO 在每阵中，判断消息队列。有的话，要做处理！
            //玩家没有回合结束的权利，只能发送自己想结束回合的请求，能不能结束等，一切，均为服务器决定
            //客户端一定是属于触发式的。
            //在客户端采用：发送请求（生成一个Key做唯一标识，但是相同的，比如回合结束时的key，却是和玩家id合并作为唯一)，等待一个请求回应,将回应的callback加入队列中，有回应就调用。
            //有成功与失败的回调（应该）
            //http socket应该有重发功能--不需要自己再实现吧

            //TODO 每一帧，仅处理一个请求(请求，影响状态，但是不改变逻辑）
            //处理看有没有特殊的逻辑
            //TODO 如果第一个逻辑不是自己需要的，那么移除。（一个消息仅仅仅仅可以呗消耗一次）
            //_checkMessageSpecialLogic();
            //如果是玩家使用什么技能这些，那就假如roomrole的处理队列中，然后委托他们自己处理！
            // 客户端传来什么key，返回也是这个key
            // kafka的源码看一下。应该就是这种生产者消费者的模型
            var result = new RoomDTO();
            var success = roomDTOs.TryDequeue(out result);
            if (success)
            {
                _solve_message_dto(result);
            }
            //需要执行所有角色的预处理状态(每帧，而不是每回合）
            Console.WriteLine("处理：" + this.id + " 的逻辑中...");
            foreach (var item in roles)
            {
                item._solve_each_logic();            
            }
            //校验是不是所有当前角色的属于回合over状态，是的话，进入下一个回合
            bool next = true;
            foreach (var item in this.__currentRoles)
            {
                item._solve_battle_logic(this.getNoCurrentTime());
                if (item.battleTimeType != BattleTimeType.OVER)
                {
                    next = false;
                }
            }

            if (next)
            {
                this.battleTimeType = BattleTimeType.OVER;
            }
            // 回合变化以及角色使用了技能，这些特殊点，需要发送网络请求！
            switch (this.battleTimeType)
            {
                case BattleTimeType.START:
                    break;
                case BattleTimeType.PRE:
                    break;
                case BattleTimeType.BATTLE:

                    break;
                case BattleTimeType.NEXT:
                    break;
                case BattleTimeType.OVER:
                    //TODO 将进入另一个回合
                    _nextTime();
                    break;
                default:
                    // 默认是全员等待OVER的状态
                    break;
            }
        }

  

  

        protected override void _game_over()
        {
            if (this.battleTimeType != BattleTimeType._GAME_OVER)
            {
                Console.WriteLine("房间:" + this.id + " 游戏结束");
                //TODO 
                //会在下一帧移除房间
                //结算，掉落、装备、经验等等
                this.battleTimeType = BattleTimeType._GAME_OVER;
            }
        }

        protected override bool _solve_battle_game_over()
        {
            switch (roomType)
            {
                case RoomType.DEMO_NPC:
                    //有一个怪物死了，那么游戏结束

                    return false;
                case RoomType.ONE_NPC_ROOM:
                    //玩家或者NPC死了，那么结束//均按照阵营
                    //还要判断玩家是不是不在线状态
                    return false;
                default:
                    return false;
            }
        }





        protected override void _nextTime()
        {
            //TODO 进入下一个回合，将排除当前的角色进入下一个阶段
            // 先将全员设置为不可用，从剩下的设置为回合开始
            var nextPlayers = getNoCurrentTime();
            foreach (var item in this.__currentRoles)
            {
                item.IsMyTime = false;
                Console.WriteLine(""+item.id+" 的回合结束啦");

            }
            Console.WriteLine("好啦。回合结束，让我们看看对面怎么操作的吧！~\n\n");
            int max = nextPlayers.Count-1;
            var ints = new List<int>();
            while (max >= 0)
            {
                ints.Add(max--);
            }
            __selectStartRole(nextPlayers, ints);

        }
     

        public override void _start_room()
        {
            switch (this.roomType)
            {
                case RoomType.DEMO_NPC:
                    // 定时器执行
                    pre_start_DEMO_NPC_ROOM();
                    break;
                case RoomType.ONE_NPC_ROOM:
                    pre_start_ONE_NPC_ROOM();
                    break;
            }
        }
         private void pre_start_ONE_NPC_ROOM()
        {
            //将玩家设置为回合开始，同时，通知一下room的用户们回合开始了！
            __brocast_role_skills_cards();
            var players = getAllPlayer();
            battleTimeType = BattleTimeType.START;
            __selectStartRole(players, new List<int>() { 0 });
            
//            idRoomMap.Add(room.id, room);
        }


        private void pre_start_DEMO_NPC_ROOM()
        {
            //先确定回合是谁(默认是玩家）,随机选一个NPC开始
            var npcs = getAllNPC();
            if (npcs.Count < 1)
                throw new Exception();
            //生成随机数
            Random rd = new Random();
            int i = rd.Next();
            int first = i % npcs.Count;
            // 因为预处理在这边解决了
            battleTimeType = BattleTimeType.START;
            __selectStartRole(npcs,new List<int>() { first });
            // 预处理结束，假如逻辑队列，定时器执行
            // 预处理结束，假如逻辑队列，定时器执行
//            idRoomMap.Add(room.id, room);
        }
    }
}
