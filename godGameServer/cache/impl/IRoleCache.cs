using com.xxy.NetFrame;
using godGameServer.dao.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace godGameServer.cache.impl
{
    public interface IRoleCache
    {
        RoleModel getModelByName(int area, string name);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="name"></param>
        void createRole( long accountId,string name);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        RoleModel getRoleModel(UserToken token);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        long getId(UserToken token);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        bool isHasRole(UserToken token);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool isHasRoleByAccountId(long id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="area"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        bool isHasSameName( int area, string name);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        void offline(UserToken token);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        void online(UserToken token,long accountId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        UserToken getToken(long id);
    }
}
