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
        static BizFactory()
        {
            accountBiz = new AccountBiz();
            roleBiz = new RoleBiz();
        }
    }
}
