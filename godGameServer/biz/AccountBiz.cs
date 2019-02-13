using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.xxy.NetFrame;
using godGameServer.cache;
using godGameServer.cache.impl;
using com.xxy.Protocol;
using com.xxy.entity.model;

namespace godGameServer.biz
{
    public class AccountBiz : IAccountBiz
    {
        private IAccountCache accountCache = CacheFactory.accountCache;
        private IRoleBiz roleBiz = BizFactory.roleBiz;
        public void close(UserToken token)
        {
            accountCache.offline(token);
            roleBiz.offline(token);
        }

        public ReturnDTO create(UserToken token, string account, string password)
        {
            //不同的错误码
            if (account == null || password == null)
                return new ReturnDTO(RETURN_CODE.INPUT_IS_ILLEGAL);

            if (accountCache.isHasAccount(account))
                return new ReturnDTO(RETURN_CODE.ACCOUNT_NAME_HAS_EXIST);
            
            accountCache.createAccount(account, password);
            return new ReturnDTO(RETURN_CODE.SUCCESS);
        }

        public long getId(UserToken token)
        {
            return accountCache.getId(token);
        }

        public ReturnDTO login(UserToken token, string account, string password)
        {
            if (account == null || password == null)
                return new ReturnDTO(RETURN_CODE.INPUT_IS_ILLEGAL);
            if (accountCache.isOnline(account))
                return new ReturnDTO(RETURN_CODE.ACCOUNT_HAS_ONLINE);
            if (!accountCache.isMatch(account, password))
                return new ReturnDTO(RETURN_CODE.ACCOUNT_IS_NOT_MATCH);
            accountCache.online(token, account);
            roleBiz.Online(token);
            return new ReturnDTO(RETURN_CODE.SUCCESS);
        }
    }
}
