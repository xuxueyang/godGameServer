using godGameServer.cache.impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace godGameServer.cache
{
    public class CacheFactory
    {
        public readonly static IRoleCache roleCache;
        public readonly static IAccountCache accountCache;
        public readonly static IBattleRoomCache battleRoomCache;

        static CacheFactory()
        {
            roleCache = new RoleCache();
            accountCache = new AccountCache();
            battleRoomCache = new BattleRoomCache();
        }
    }
}
