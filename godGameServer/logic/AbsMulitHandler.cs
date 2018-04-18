using com.xxy.NetFrame;
using com.xxy.NetFrame.auto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace godGameServer.logic
{
    public class AbsMulitHandler:AbsOneHandler
    {
        public List<UserToken> list = new List<UserToken>();
        #region 消息群发API
        public void brocast(int command, object message, UserToken exToken = null)
        {
            this.brocast(GetArea(), command, message, exToken);
        }
        public void brocast(int area, int command, object message, UserToken exToken = null)
        {
            this.brocast(GetGameType(), area, command, message, exToken);
        }
        public void brocast(byte type, int area, int command, object message, UserToken exToken = null)
        {
            byte[] value = MessageEncoding.encode(createSocketModel(type, area, command, message));
            value = LengthEncoding.encode(value);
            foreach (var item in list)
            {
                if (exToken != item)
                {
                    byte[] bs = new byte[value.Length];
                    Array.Copy(value, bs, value.Length);
                    item.write(bs);
                }

            }
        }
        #endregion




        public bool enter(UserToken token)
        {
            if (list.Contains(token))
            {
                return false;
            }
            list.Add(token);
            return true;
        }
        public bool isEnterend(UserToken token)
        {
            return list.Contains(token);
        }
        public bool leave(UserToken token)
        {
            if (isEnterend(token))
            {
                list.Remove(token);
                return true;
            }
            return false;
        }
    }
}
