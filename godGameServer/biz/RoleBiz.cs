using com.xxy.NetFrame;
using godGameServer.cache;

using godGameServer.dao.model;
using godGameServer.cache.impl;
using com.xxy.Protocol;
using com.xxy.entity.model;

namespace godGameServer.biz
{
    public class RoleBiz : IRoleBiz
    {
        private IRoleCache roleCache = CacheFactory.roleCache;
        private IAccountCache accountCache = CacheFactory.accountCache;

        public ReturnDTO Create(UserToken token, string name)
        {
            //id分开算的
            long id = accountCache.getId(token);
            if (id < 0)
                return new ReturnDTO(RETURN_CODE.ACCOUNT_IS_NOT_EXIST);//账号不在线
            if (roleCache.isHasRoleByAccountId(id))
                return new ReturnDTO(RETURN_CODE.ACCOUNT_HAS_ROLE);//账号已有角色
            if (roleCache.isHasSameName(accountCache.getAccountModelById(id).area, name))
                return new ReturnDTO(RETURN_CODE.ROLE_NAME_HAS_EXIST);//角色名字已存在            
            roleCache.createRole(id, name);
            return new ReturnDTO(RETURN_CODE.SUCCESS);
        }

        public RoleModel GetModel(UserToken token)
        {
            return roleCache.getRoleModel(token);
        }

        public RoleModel GetModel(long id)
        {
            return roleCache.getRoleModel(roleCache.getToken(id));
        }


        public UserToken getToken(long id)
        {
            return roleCache.getToken(id);
        }

        public void offline(UserToken token)
        {
            roleCache.offline(token);
            
        }

        public RoleModel Online(UserToken token)
        {
            //上线，关联token和accountId,但是此时还没有role
            roleCache.online(token,accountCache.getId(token));
            //判断，如果没有角色，应该返回空，反之是角色信息。
            if (roleCache.isHasRole(token))
                return roleCache.getRoleModel(token);
            else
                return null;
        }
        public bool isHasSameNameRole(string name,int area)
        {
            return roleCache.isHasSameName( area,name);
        }
        public RoleModel getModelByName(int area,string name)
        {
            return roleCache.getModelByName(area, name);
        }
    }
}
