

namespace com.xxy.Protocol.DTO.MainRoomDTO
{
    public class FriendManagerDTO
    {
        public long id;
        public string friendName;

        public FriendManagerDTO(long id,string friendName)
        {
            this.id = id;
            this.friendName = friendName;
        }
    }
}
