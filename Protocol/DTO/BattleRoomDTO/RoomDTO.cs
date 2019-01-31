using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Protocol.DTO.BattleRoomDTO
{
    public class RoomDTO
    {
        public string roomId;
        public string roomRoleId;

        public Dictionary<string,object> map = new Dictionary<string, object>();
        public List<RoomInfoDTO> roomInfoDTOs = new List<RoomInfoDTO>();

        /// <summary>
        /// 操作协议。不需要设置
        /// </summary>
        public int protocol;
    }
}
