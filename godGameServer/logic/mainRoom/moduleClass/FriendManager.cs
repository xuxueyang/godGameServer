using com.xxy.entity.model;
using com.xxy.NetFrame;
using com.xxy.Protocol;
using com.xxy.Protocol.DTO.MainRoomDTO;
using godGameServer.dao.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace godGameServer.logic.mainRoom.moduleClass
{
    class FriendManager:AbsOneHandler
    {
        public void addFriend(UserToken token, FriendManagerDTO dto)
        {
            //搜寻roleModel，添加到好友列表中
            RoleModel model = roleBiz.GetModel(token);
            //判断改用户是否存在
            if(roleBiz.isHasSameNameRole(dto.friendName,model.area))
            {
                model.friendSet.Add(dto.friendName);
                write(token, MainRoomProtocol.FRIEND_ADD_SRES, new ReturnDTO(RETURN_CODE.SUCCESS));
            }
            else
            {
                write(token, MainRoomProtocol.FRIEND_ADD_SRES, new ReturnDTO(RETURN_CODE.MAIN_ROOM_FRIEND_ADD_ROLE_NOT_EXIST));
            }
            
        }
        public void deleteFriend(UserToken token, FriendManagerDTO dto)
        {
            RoleModel model = roleBiz.GetModel(token);
            if(model.friendSet.Contains(dto.friendName))
            {
                model.friendSet.Remove(dto.friendName);
                write(token, MainRoomProtocol.FRIEND_DELETE_SRES, new ReturnDTO(RETURN_CODE.SUCCESS));
            }
            else
            {
                write(token, MainRoomProtocol.FRIEND_DELETE_SRES, new ReturnDTO(RETURN_CODE.MAIN_ROOM_DRIEND_DELETE_ROLE_NOT_EXIST));
            }

        }
        public override byte GetGameType()
        {
            return TypeProtocol.TYPE_MAIN_ROOM;
        }
    }
}
