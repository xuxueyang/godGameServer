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

namespace godGameServer.logic.BattleRoom.module
{
    /// <summary>
    /// 房间管理
    /// </summary>
    public class RoomManager:AbsOneHandler
    {
        public void createRoom(List<UserToken> userTokens)
        {
            if (userTokens == null || userTokens.Count < 1)
            {
                return;
            }
            List<RoleModel> arrayList = new List< RoleModel>();
            bool createSuccess = true;
            foreach (var token in userTokens)
            {
                //根据连接，得到全部的用户信息,用战斗信息初始化战斗房间
                RoleModel roleModel =  getUserModel(token);
                if (roleModel == null)
                {
                    //说明有用户在这期间掉线了。重新连接
                    createSuccess = false;
                    continue;
                }
                arrayList.Add(roleModel);
            }
            if(createSuccess)
                // 告诉这些用户，创建房间成功
                createRoom(arrayList);
            else
            {
                // 告诉其他用户，有人掉线，创建失败！
                foreach (var roleModel in arrayList)
                {
                    write(roleModel.accountId, BattleRoomProtocol.CREATE_ONE_S, new ReturnDTO(RETURN_CODE.BAALE_ROOM_CAEATE_ERROR));
                }
            }
        }

        public void createRoom(List<RoleModel> roleModels)
        {
            // 初始化战斗回合
        }

        public override byte GetGameType()
        {
            return TypeProtocol.TYPE_BATTLE_ROOM;
        }
    }
}
