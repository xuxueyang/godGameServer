namespace com.xxy.Protocol.DTO.BattleRoomDTO
{
    public enum UseType
    {
        Card,
        Skill
    }
    public class BattleRoomDTO
    {
        public string roomId;
        public string targetId;//对象
        public string sourceId; //使用者
        public string useId;//使用的ID
        public UseType useType;//使用的东西类型
//        public string 
    }
}