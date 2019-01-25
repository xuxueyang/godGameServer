using com.xxy.entity.model;
using com.xxy.NetFrame;
using com.xxy.NetFrame.auto;
using godGameServer.biz;
using godGameServer.dao.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace godGameServer.logic
{
   public class AbsOneHandler
    {
        public IRoleBiz roleBiz = BizFactory.roleBiz;

        private byte type;
        private int area;
        
        public  void SetArea(int area)
        {
            this.area = area;
        }
        public virtual int GetArea() { return this.area; }
        public void SetGameType(byte type) { this.type = type; }
        public virtual byte GetGameType() { return this.type; }

        /// <summary>
        /// 通过用户id获取数据模型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public RoleModel getUserModel(int id)
        {
            return roleBiz.GetModel(id);
        }

        public RoleModel getUserModel(UserToken token)
        {
            return roleBiz.GetModel(token);
        }
        public long getUserId(UserToken token)
        {
            return getUserModel(token).id;
        }
        public UserToken getToken(long id)
        {
            return roleBiz.getToken(id);
        }


        #region 通过连接对象发送
        public void write(UserToken token, int command)
        {
            write(token, command, null);
        }
        public void write(UserToken token, int command, ReturnDTO message)
        {
            write(token, GetArea(), command, message);
        }
        public void write(UserToken token, int area, int command, ReturnDTO message)
        {
            write(token, GetGameType(), area, command, message);
        }
        public void write(UserToken token, byte type, int area, int command, ReturnDTO message)
        {
            byte[] value = MessageEncoding.encode(createSocketModel(type, area, command, message));
            value = LengthEncoding.encode(value);
            token.write(value);
        }
        #endregion
        #region 通过id传送
        public void write(long id, int command)
        {
            write(id, command, null);
        }
        public void write(long id, int command, ReturnDTO message)
        {
            write(id, GetGameType(), command, message);
        }
        public void write(long id, byte type, int command, ReturnDTO message)
        {
            write(id, GetGameType(), area, command, message);
        }
        public void write(long id, byte type, int area, int command, ReturnDTO message)
        {
            UserToken token = getToken(id);
            if (token == null) return;
            write(token, type, area, command, message);
        }

        public void writeToUsers(long[] users, byte type, int area, int command, ReturnDTO message)
        {
            byte[] value = MessageEncoding.encode(createSocketModel(type, area, command, message));
            value = LengthEncoding.encode(value);
            foreach (var item in users)
            {
                UserToken token = getToken(item);
                if (token == null) continue;
                byte[] bs = new byte[value.Length];
                Array.Copy(value, bs, value.Length);
                token.write(bs);
            }
        }

        #endregion


        public SocketModel createSocketModel(byte type, int area, int command, ReturnDTO message)
        {
            return new SocketModel(type, area, command, message);
        }
    }
}
