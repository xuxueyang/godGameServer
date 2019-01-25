using com.xxy.NetFrame;
using com.xxy.Protocol;
using com.xxy.Protocol.DTO;
using godGameServer.dao.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace godGameServer.logic.mainRoom.moduleClass
{
    public class ChatManager:AbsMulitHandler
    {
        public void broAllPeople(UserToken token,ChatInfoDTO dto)
        {
            //处理，可能错误，所以会只返回token的erro的type的message
            //世界频道，发送消息
            ChatInfoDTO tmpDTO = new ChatInfoDTO(MessageType.Player, dto.message);
            brocast(MainRoomProtocol.CHAT_SRES, tmpDTO);
        }
        public void broSomePeople(UserToken token,ChatInfoDTO dto)
        {
            foreach(string str in dto.broTargetName)
            {
                RoleModel model = roleBiz.getModelByName(roleBiz.GetModel(token).area, str);
                ChatInfoDTO tmpDTO = new ChatInfoDTO(MessageType.Player, dto.message);
                write(roleBiz.getToken(model.id), MainRoomProtocol.CHAT_SRES, new com.xxy.entity.model.ReturnDTO(RETURN_CODE.SUCCESS,tmpDTO);
            }
        }
        public override byte GetGameType()
        {
            return TypeProtocol.TYPE_MAIN_ROOM;
        }
    }
}
