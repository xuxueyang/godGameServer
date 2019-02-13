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
    class RoleCache : IRoleCache
    {
        private long index = 1;
        /// <summary>
        /// 角色id与模型
        /// </summary>
        Dictionary<long, RoleModel> idToRoleMap = new Dictionary<long, RoleModel>();
        /// <summary>

        /// <summary>
        /// 账号id与角色id
        /// </summary>
        Dictionary<long, long> accIdMap = new Dictionary<long, long>();
        ///缓存
        /// <summary>
        /// 连接token与账号id
        /// </summary>
        Dictionary<UserToken, long> tokenToIdMap = new Dictionary<UserToken, long>();
        /// <summary>
        /// 账号id与连接token
        /// </summary>
        Dictionary<long, UserToken> idToTokenMap = new Dictionary<long, UserToken>();

        public RoleCache()
        {
            createRole(1, "测试账号角色");
        }
        public void createRole(long accountId, string name)
        {
            RoleModel model = new RoleModel(name, index++, accountId);
            idToRoleMap.Add(model.id, model);
            accIdMap.Add(accountId, model.id);

        }

        public long getId(UserToken token)
        {
            if (!tokenToIdMap.ContainsKey(token)) { return -1; }
            return accIdMap[tokenToIdMap[token]];
        }

        public RoleModel getRoleModel(UserToken token)
        {
            if (!tokenToIdMap.ContainsKey(token))
                return null;
            return idToRoleMap[accIdMap[tokenToIdMap[token]]];
        }

        public UserToken getToken(long id)
        {
            if (!idToTokenMap.ContainsKey(id))
                return null;
            return idToTokenMap[id];
        }

        public bool isHasRole(UserToken token)
        {
            if (!tokenToIdMap.ContainsKey(token))
                return false;
            if (!accIdMap.ContainsKey(tokenToIdMap[token]))
                return false;
            return true;
        }

        public bool isHasRoleByAccountId(long id)
        {
            if (accIdMap.ContainsKey(id))
                return true;
            return false;
        }


        public bool isHasSameName(int area, string name)
        {

            foreach(RoleModel model in idToRoleMap.Values )
            {
                if(model.name.CompareTo(name)==0&&model.area==area)
                {
                    return true;
                }
            }
            return false;
        }

        public void offline(UserToken token)
        {
            if (tokenToIdMap.ContainsKey(token))
            {
                long accId = tokenToIdMap[token];
                tokenToIdMap.Remove(token);
                idToTokenMap.Remove(accId);
            }            
        }

        public void online(UserToken token,long accontId)
        {
            tokenToIdMap.Add(token, accontId);
            idToTokenMap.Add(accontId, token);
        }
        public RoleModel getModelByName(int area,string name)
        {
            foreach (long id  in idToTokenMap.Keys)
            {
                if(idToRoleMap[id].name.CompareTo(name)==0&&idToRoleMap[id].area==area)
                {
                    return idToRoleMap[id];
                }
            }
            return null;
        }
    }
}
