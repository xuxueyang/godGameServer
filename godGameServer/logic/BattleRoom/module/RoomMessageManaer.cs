using com.xxy.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace godGameServer.logic.BattleRoom.module
{
    public class RoomMessageManaer:AbsOneHandler
    {
        public override byte GetGameType()
        {
            return TypeProtocol.TYPE_BATTLE_ROOM;
        }
    }
}
