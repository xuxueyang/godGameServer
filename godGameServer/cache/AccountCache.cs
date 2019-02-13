using godGameServer.cache.impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.xxy.NetFrame;
using godGameServer.dao.model;

namespace godGameServer.cache
{
    class AccountCache : IAccountCache
    {
        private long index = 1;
        /// <summary>
        /// 存储注册信息,数据库绑定
        /// </summary>
        Dictionary<string, AccountModel> accMap = new Dictionary<string, AccountModel>();
        /// <summary>
        /// 存储id与用户名的映射
        /// </summary>
        Dictionary<long, string> idToAccMap = new Dictionary<long, string>();
        /// <summary>
        /// 缓存连接与用户名
        /// </summary>
        Dictionary<UserToken, string>  tokenMap= new Dictionary<UserToken, string>();
        public AccountCache()
        {
            createAccount("admin", "admin");
            
        }

        public void createAccount(string account, string password)
        {
            AccountModel model = new AccountModel(account, password,index++);
            accMap.Add(account, model);
            idToAccMap.Add(model.getId(), account);
        }

        public AccountModel getAccountModelById(long id)
        {
            if (!idToAccMap.ContainsKey(id))
                return null;
            return accMap[idToAccMap[id]];
        }

        public long getId(UserToken token)
        {
            if (tokenMap.ContainsKey(token))
                return accMap[tokenMap[token]].getId();
            else
                return -1;
                    
        }

        public bool isHasAccount(string account)
        {
            if (accMap.ContainsKey(account))
                return true;
            else
                return false;
        }

        public bool isMatch(string account, string password)
        {
            if (!accMap.ContainsKey(account)) { return false; }
            AccountModel model = accMap[account];
            if (model.getPassword().CompareTo(password) == 0)
                return true;
            return false;
        }

        public bool isOnline(string account)
        {
            if (tokenMap.ContainsValue(account))
                return true;
            return false;
        }

        public void offline(UserToken token)
        {
            if(tokenMap.ContainsKey(token))
                tokenMap.Remove(token);
        }

        public void online(UserToken token, string account)
        {
            tokenMap.Add(token, account);
        }
    }
}
