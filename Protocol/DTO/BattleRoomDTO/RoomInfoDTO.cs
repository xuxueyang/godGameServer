using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Protocol.DTO.BattleRoomDTO
{
    public class BaseCardDTO
    {
        public int id;
        public string name;
        public string description;
    }
    public class BaseSkillDTO
    {
        public int id;
        public string name;
        public string description;
    }
    public class RoomInfoDTO
    {
        public string roomId;
        public string roomRoleId;
        public List<int> cardIds = new List<int>();
        public List<int> skillIds = new List<int>();
        public List<BaseCardDTO> cards = new List<BaseCardDTO>();
        public List<BaseSkillDTO> skills = new List<BaseSkillDTO>();
    }
}
