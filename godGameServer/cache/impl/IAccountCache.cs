using com.xxy.NetFrame;
using godGameServer.dao.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace godGameServer.cache.impl
{
    public interface IAccountCache
    {
        void createAccount(string account, string password);    
        long getId(UserToken token);
        bool isOnline(string account);
        AccountModel getAccountModelById(long id);

        bool isMatch(string account, string password);
        bool isHasAccount(string account);
        void online(UserToken token, string account);
        void offline(UserToken token);
    }
}
