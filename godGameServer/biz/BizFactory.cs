﻿using godGameServer.biz.impl;
using godGameServer.logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace godGameServer.biz
{
    class BizFactory
    {
        public readonly static IAccountBiz accountBiz;
        public readonly static IRoleBiz roleBiz;
        public readonly static IBattleRoomBiz battleRoomBiz;
        static BizFactory()
        {
            // 有顺序！
            roleBiz = new RoleBiz();
            accountBiz = new AccountBiz();
          
            battleRoomBiz = new BattleRoomBiz();
        }
    }
}
