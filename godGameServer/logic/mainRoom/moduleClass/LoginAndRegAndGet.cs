using com.xxy.NetFrame;
using godGameServer.dao.model;
using com.xxy.Protocol.DTO.MainRoomDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.xxy.Protocol;
using com.xxy.entity.model;

namespace godGameServer.logic.mainRoom.moduleClass
{
    public class LoginAndRegAndGet:AbsOneHandler
    {
        public void getRoleModel(UserToken token)
        {
            RoleModel model = this.roleBiz.GetModel(token);
            write(token, MainRoomProtocol.GET_ROLE_INFO_SRES, new ReturnDTO(RETURN_CODE.SUCCESS, model));
        }
        public void regRole(UserToken token,MainRoomLoginAndRegInfoDTO dto)
        {
            ReturnDTO result = this.roleBiz.Create(token, dto.name);
            write(token, MainRoomProtocol.REG_ROLE_SRES, result);
        }
        public void logRole(UserToken token,MainRoomLoginAndRegInfoDTO dto)
        {
            RoleModel model = this.roleBiz.Online(token);
            write(token, MainRoomProtocol.LOGIN_ROLE_SRES, new ReturnDTO(RETURN_CODE.SUCCESS,model));
        }

        public override byte GetGameType()
        {
            return TypeProtocol.TYPE_MAIN_ROOM;
        }


    }
}
