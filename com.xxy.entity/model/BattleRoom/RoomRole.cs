using System;
using System.Collections.Generic;
using System.Text;
using com.xxy.entity.Base.GameRole;

namespace com.xxy.entity.model.BattleRoom
{
    /// <summary>
    /// 房间里人员信息的类
    /// </summary>
    public class RoomRole
    {
        public BaseRole role;
        public string roomId;
        public RoomRole(string roomId,BaseRole role)
        {
            this.role = role;
            this.roomId = roomId;
            //初始化房间角色信息
            
        }

        /// <summary>
        /// 定时处理战斗逻辑
        /// </summary>
        void solveLogic()
        {
            //依次执行，回合操作，战斗结束，死亡判断
        }
    }
}
