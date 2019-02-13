using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Protocol.DTO.BattleRoomDTO
{
    [Serializable]
    public class BaseCardDTO
    {
        public int id;
        public string name;
        public string description;
    }
    [Serializable]
    public class BaseSkillDTO
    {
        public int id;
        public string name;
        public string description;
    }
    [Serializable]
    public class RoomInfoDTO
    {
        public string roomId;
        public string roomRoleId;
        public List<int> cardIds = new List<int>();
        public List<int> skillIds = new List<int>();
        public List<BaseCardDTO> cards = new List<BaseCardDTO>();
        public List<BaseSkillDTO> skills = new List<BaseSkillDTO>();
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(base.ToString());
            sb.AppendLine("房间ID：" + roomId);
            sb.AppendLine("角色ID：" + roomRoleId);
            foreach (var t in cards)
            {
                sb.AppendLine("卡牌ID：" + t.id+" 卡牌名称："+t.name+" 卡牌描述："+t.description);
            }
            foreach (var t in skills)
            {
                sb.AppendLine("技能ID：" + t.id + " 名称：" + t.name + " 描述：" + t.description);
            }
            return sb.ToString();
        }
    }
}
