using System.Collections.Generic;

namespace com.xxy.Protocol.DTO
{
    public enum MessageType {
        Error=-1,
        System=0,
        Player=1
    }
    public class ChatInfoDTO
    {
        public string message;
        public MessageType messageType;
        public List<string> broTargetName; 
        public ChatInfoDTO(MessageType messageType,string message)
        {
            this.message = message;
            this.messageType = messageType;
            broTargetName = new List<string>();
        }
    }
}
